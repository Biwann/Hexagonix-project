using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using Zenject;

public sealed class UpgradeView : MonoBehaviour
{
    [SerializeField] TMP_Text _currentLevel;
    [SerializeField] TMP_Text _upgradeCost;
    [SerializeField] GameObject _upgradeMaxOut;
    [SerializeField] GameObject _upgradeLocked;
    [SerializeField] GameObject _upgradeNoMoney;
    [SerializeField] TMP_Text _needLevelPlayer;

    public void Inject(IUpgradeInformation upgradeInformation, CoinsLocal coins)
    {
        _upgradeInformation = upgradeInformation;
        _coins = coins;

        UpdateState();
        _upgradeInformation.OnInformationChanged += UpdateState;
        _coins.CoinsChanged += OnCoinsUpdate;
        GlobalProgramEvents.LevelChanging += Unsubscribe;
        Debug.Log("Injected");
    }

    public void TryBuyUpgrade()
    {
        Debug.Log("try buy upgrade");
        if (_state != UpgradeState.Ready)
        {
            return;
        }

        var isSuccess = _coins.TrySpendCoins(_upgradeInformation.UpgradeCost);

        if (isSuccess)
        {
            Debug.Log("do upgrade");
            _upgradeInformation.DoUpgrade();
        }
    }

    private void OnCoinsUpdate(int coins)
    {
        UpdateState();
    }

    private void UpdateState()
    {
        Debug.Log("Updating upgrade state");
        var currentLevel = _upgradeInformation.UpgradeLevel;
        _currentLevel.text = currentLevel.ToString();
        _upgradeCost.text = _upgradeInformation.UpgradeCost.ToString();

        if (!_upgradeInformation.IsAvailable)
        {
            _needLevelPlayer.text = _upgradeInformation.MinPlayerLevelToUse.ToString();
            SetState(UpgradeState.NeedReachLevel);
        }
        else if (currentLevel == _upgradeInformation.MaxUpgradeLevel)
        {
            SetState(UpgradeState.MaxedOut);
        }
        else if (_coins.Coins < _upgradeInformation.UpgradeCost)
        {
            SetState(UpgradeState.NotEnoughMoney);
        }
        else
        {
            SetState(UpgradeState.Ready);
        }

        Debug.Log(_state);
    }

    private void SetState(UpgradeState state)
    {
        var maxOut = false;
        var locked = false;
        var noMoney = false;

        switch (state)
        {
            case UpgradeState.NotInitialized:
            case UpgradeState.Ready:
                break;
            case UpgradeState.NotEnoughMoney:
                noMoney = true;
                break;
            case UpgradeState.NeedReachLevel:
                locked = true;
                break;
            case UpgradeState.MaxedOut:
                maxOut = true;
                break;
            default: throw new ArgumentOutOfRangeException();
        }

        _upgradeMaxOut.SetActive(maxOut);
        _upgradeLocked.SetActive(locked);
        _upgradeNoMoney.SetActive(noMoney);

        _state = state;
    }

    private void Unsubscribe()
    {
        _upgradeInformation.OnInformationChanged -= UpdateState;
        _coins.CoinsChanged -= OnCoinsUpdate;
        GlobalProgramEvents.LevelChanging -= Unsubscribe;
    }

    private IUpgradeInformation _upgradeInformation;
    private CoinsLocal _coins;
    private UpgradeState _state = UpgradeState.NotInitialized;

    private enum UpgradeState
    {
        NotInitialized = 0,
        Ready,
        NotEnoughMoney,
        NeedReachLevel,
        MaxedOut,
    }
}