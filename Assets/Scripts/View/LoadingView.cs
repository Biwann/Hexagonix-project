using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class LoadingView : MonoBehaviour
{
    [SerializeField] private Image _loadingView;

    private void Awake()
    {
        _loadingView.fillAmount = 0;
    }

    private void Start()
    {
        _mainMenuLoad = GlobalProgramEvents.NavigateOnMainMenuAsync();

    }

    private void Update()
    {
        if (_mainMenuLoad == null)
        {
            return;
        }

        _loadingView.fillAmount = _mainMenuLoad.progress;
    }

    private AsyncOperation _mainMenuLoad;
}