using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class ExpirienceBarView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _currentLevelText;
    [SerializeField] private TMP_Text _currentLevelTextBehind;
    [SerializeField] private TMP_Text _nextLevelText;
    [SerializeField] private TMP_Text _nextLevelTextBehind;

    [Inject]
    public void Inject(
        ExpirienceLevelProvider levelProvider)
    {
        _levelProvider = levelProvider;

        _currentLevel = _levelProvider.CurrentLevelInformation.Level;
        UpdateSlider(_levelProvider.CurrentLevelInformation);
        UpdateLevelText(_currentLevel);

        _levelProvider.LevelInformationChanged += OnLevelInformationChanged;
        _levelProvider.LevelUp += OnLevelUp;
    }

    private void OnLevelUp()
    {
        Debug.Log($"On Level up {_currentLevel}");
        UpdateLevelText(_currentLevel);
    }

    private void OnLevelInformationChanged(LevelInformation newLevelInformation)
    {
        Debug.Log($"level information changed {newLevelInformation}");
        _currentLevel = newLevelInformation.Level;

        UpdateSlider(newLevelInformation);
    }

    private void UpdateLevelText(int currentLevel)
    {
        _currentLevelText.text = currentLevel.ToString();
        _currentLevelTextBehind.text = currentLevel.ToString();
        _nextLevelText.text = (currentLevel + 1).ToString();
        _nextLevelTextBehind.text = (currentLevel + 1).ToString();
    }

    private void UpdateSlider(LevelInformation newLevelInformation)
    {
        Debug.Log($"Update slider {newLevelInformation.LevelPoints}/{newLevelInformation.MaxLevelPoints}");
        _slider.maxValue = newLevelInformation.MaxLevelPoints;
        _slider.value = newLevelInformation.LevelPoints;
    }

    private ExpirienceLevelProvider _levelProvider;
    private int _currentLevel;
}