using UnityEngine;

public class MainAudioController : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private MusicData _mainMusic;

    [Header("UI")]
    [SerializeField] private SoundData _buttonClickSFX;
    [SerializeField] private SoundData _menuOpenSFX;
    [SerializeField] private SoundData _menuCloseSFX;

    private void Start()
    {
        if (!AudioManager.Instance.IsMusicPlaying)
            AudioManager.Instance.PlayMusic(_mainMusic);
    }

    public void PlayButtonClickSound()
    {
        AudioManager.Instance.PlaySFX(_buttonClickSFX);
    }

    public void PlayMenuOpenSound(float delay = 0f)
    {
        AudioManager.Instance.PlaySFXDelayed(_menuOpenSFX, delay);
    }

    public void PlayMenuCloseSound(float delay = 0f)
    {
        AudioManager.Instance.PlaySFXDelayed(_menuCloseSFX, delay);
    }
}