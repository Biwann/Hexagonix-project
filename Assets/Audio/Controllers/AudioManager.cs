using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class AudioManager : MonoBehaviour
{
    [Header("Mixer Groups")]
    [SerializeField] private AudioMixerGroup _musicMixerGroup;
    [SerializeField] private AudioMixerGroup _sfxMixerGroup;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private int _sfxPoolSize = 8;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float MasterVolume = 1f;
    [Range(0f, 1f)] public float MusicVolume = 1f;
    [Range(0f, 1f)] public float SFXVolume = 1f;


    private List<AudioSource> _sfxPool = new List<AudioSource>();
    private Coroutine _musicFadeCoroutine;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        for (int i = 0; i < _sfxPoolSize; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.spatialBlend = 0f;
            _sfxPool.Add(source);
        }
    }

    [Inject]
    public void Inject(IDataSaver dataSaver)
    {
        _dataSaver = dataSaver;

        LoadVolumeSettings();
    }

    #region Music Methods

    public void PlayMusic(MusicData musicData, bool forceRestart = false)
    {
        if (musicData == null || _musicSource.clip == musicData.Clip && _musicSource.isPlaying && !forceRestart)
            return;
        Debug.Log("Start music");
        if (_musicFadeCoroutine != null)
            StopCoroutine(_musicFadeCoroutine);

        _musicFadeCoroutine = StartCoroutine(FadeMusicCoroutine(musicData));
        _currentMusicData = musicData;
    }

    public void StopMusic()
    {
        if (_musicFadeCoroutine != null)
            StopCoroutine(_musicFadeCoroutine);

        _musicFadeCoroutine = StartCoroutine(FadeOutMusicCoroutine());
    }

    public void PauseMusic()
    {
        _musicSource.Pause();
    }

    public void ResumeMusic()
    {
        _musicSource.UnPause();
    }

    private IEnumerator FadeMusicCoroutine(MusicData musicData)
    {
        if (_musicSource.isPlaying)
        {
            yield return StartCoroutine(FadeOutMusicCoroutine());
        }

        _musicSource.clip = musicData.Clip;
        _musicSource.outputAudioMixerGroup = _musicMixerGroup;
        _musicSource.volume = 0f;
        _musicSource.loop = musicData.ShouldLoop;
        _musicSource.Play();

        float elapsed = 0f;
        while (elapsed < musicData.FadeInDuration)
        {
            elapsed += Time.deltaTime;
            _musicSource.volume = Mathf.Lerp(0f, musicData.Volume * MusicVolume * MasterVolume,
                elapsed / musicData.FadeInDuration);
            yield return null;
        }

        _musicSource.volume = musicData.Volume * MusicVolume * MasterVolume;
    }

    private IEnumerator FadeOutMusicCoroutine()
    {
        float startVolume = _musicSource.volume;
        float elapsed = 0f;

        while (elapsed < 2f)
        {
            elapsed += Time.deltaTime;
            _musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / 2f);
            yield return null;
        }

        _musicSource.Stop();
        _musicSource.clip = null;
    }

    #endregion

    #region SFX Methods

    public void PlaySFX(SoundData soundData)
    {
        if (soundData == null) return;

        AudioSource source = GetAvailableSFXSource();
        if (source == null) return;

        SetupSFXSource(source, soundData);
        source.Play();
    }

    public void PlaySFXDelayed(SoundData soundData, float delay)
    {
        StartCoroutine(PlayDelayedCoroutine(soundData, delay));
    }

    private IEnumerator PlayDelayedCoroutine(SoundData soundData, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySFX(soundData);
    }

    public void StopAllSFX()
    {
        foreach (var source in _sfxPool)
        {
            source.Stop();
        }
    }

    public void StopSFX(SoundData soundData)
    {
        foreach (var source in _sfxPool)
        {
            if (source.isPlaying && source.clip == soundData.Clip)
            {
                source.Stop();
            }
        }
    }

    private AudioSource GetAvailableSFXSource()
    {
        foreach (var source in _sfxPool)
        {
            if (!source.isPlaying)
                return source;
        }

        return _sfxPool[0];
    }

    private void SetupSFXSource(AudioSource source, SoundData soundData)
    {
        source.clip = soundData.Clip;
        source.outputAudioMixerGroup = _sfxMixerGroup;
        source.volume = soundData.Volume * SFXVolume * MasterVolume;
        source.pitch = soundData.IsRandomPitch 
            ? soundData.Pitch + Random.Range(-soundData.RandomPitchRange, soundData.RandomPitchRange) 
            : soundData.Pitch;
        source.loop = soundData.ShoudLoop;
        source.spatialBlend = 0f;
    }

    #endregion

    #region Volume Control

    public void SetMasterVolume(float volume)
    {
        MasterVolume = Mathf.Clamp01(volume);
        UpdateAllVolumes();
        SaveVolumeSettings();
    }

    public void SetMusicVolume(float volume)
    {
        MusicVolume = Mathf.Clamp01(volume);
        UpdateMusicVolume();
        SaveVolumeSettings();
    }

    public void SetSFXVolume(float volume)
    {
        SFXVolume = Mathf.Clamp01(volume);
        UpdateSFXVolume();
        SaveVolumeSettings();
    }

    private void UpdateAllVolumes()
    {
        UpdateMusicVolume();
        UpdateSFXVolume();
    }

    private void UpdateMusicVolume()
    {
        if (_musicSource != null && _musicSource.clip != null)
        {
            float baseVolume = _currentMusicData != null ? _currentMusicData.Volume : 1f;
            _musicSource.volume = baseVolume * MusicVolume * MasterVolume;
        }
    }

    private void UpdateSFXVolume()
    {
        foreach (var source in _sfxPool)
        {
            if (source.isPlaying)
            {
                // Для SFX обновляем громкость напрямую
                source.volume = SFXVolume * MasterVolume;
            }
        }
    }

    #endregion

    #region Save/Load Settings

    private void SaveVolumeSettings()
    {
        _dataSaver.SetFloat("MasterVolume", MasterVolume);
        _dataSaver.SetFloat("MusicVolume", MusicVolume);
        _dataSaver.SetFloat("SFXVolume", SFXVolume);
    }

    private void LoadVolumeSettings()
    {
        MasterVolume = _dataSaver.GetFloat("MasterVolume", 1f);
        MusicVolume = _dataSaver.GetFloat("MusicVolume", 1f);
        SFXVolume = _dataSaver.GetFloat("SFXVolume", 1f);
        UpdateAllVolumes();
    }

    #endregion

    #region Utility Methods

    public bool IsMusicPlaying => _musicSource.isPlaying;
    public bool IsAnySFXPlaying => GetActiveSFXCount() > 0;

    public int GetActiveSFXCount()
    {
        int count = 0;
        foreach (var source in _sfxPool)
        {
            if (source.isPlaying) count++;
        }
        return count;
    }

    #endregion

    private IDataSaver _dataSaver;
    private MusicData _currentMusicData;
}