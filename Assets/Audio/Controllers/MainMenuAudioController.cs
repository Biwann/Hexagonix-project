using UnityEngine;

public class MainMenuAudioController : MonoBehaviour
{
    [SerializeField] private SoundData _upgradeSFX;
    [SerializeField] private SoundData _cantUpgradeSFX;

    public void PlayUpgradeSound()
    {
        AudioManager.Instance.PlaySFX(_upgradeSFX);
    }

    public void PlayCantUpgradeSound()
    {
        AudioManager.Instance.PlaySFX(_cantUpgradeSFX);
    }
}