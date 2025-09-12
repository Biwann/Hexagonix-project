using System.Collections.Generic;
using UnityEngine;

public class GameAudioController : MonoBehaviour
{
    [Header("SFX - Gameplay")]
    [SerializeField] private List<SoundData> _popHexagonSFX;
    [SerializeField] private SoundData _placeHexagonSFX;
    [SerializeField] private SoundData _cantPlaceHexagonSFX;
    [SerializeField] private SoundData _bombSFX;
    [SerializeField] private SoundData _coinSFX;

    [SerializeField] private SoundData _comboSFX;
    [SerializeField] private SoundData _newRecordSFX;
    [SerializeField] private SoundData _looseSFX;

    public static GameAudioController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayPopHexagon()
        => Play(_popHexagonSFX.SelectRandom());
    
    public void PlayPlaceHexagon()
        => Play(_placeHexagonSFX);
    
    public void PlayCantPlaceHexagon()
        => Play(_cantPlaceHexagonSFX);

    public void PlayBomb()
        => Play(_bombSFX);

    public void PlayCoin()
        => Play(_coinSFX);

    public void PlayCombo()
        => Play(_comboSFX);

    public void PlayNewRecord()
        => Play(_newRecordSFX);

    public void PlayLoose()
        => Play(_looseSFX);

    private void Play(SoundData sound)
    {
        AudioManager.Instance.PlaySFX(sound);
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        GameEvents.NoMovesLeft += PlayLoose;
    }

    private void Unsubscribe()
    {
        GameEvents.NoMovesLeft -= PlayLoose;
    }
}