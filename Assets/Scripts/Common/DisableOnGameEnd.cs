using UnityEngine;
using Zenject;

public sealed class DisableOnGameEnd : MonoBehaviour
{
    [Inject]
    public void Inject()
    {
        GameEvents.GameEnded += OnGameEnded;
    }

    private void OnGameEnded()
    {
        GameEvents.GameEnded -= OnGameEnded;
        gameObject.SetActive(false);
    }
}