using System;
using UnityEngine;
using Zenject;

public class Wallet : IWallet, IInitializable
{
    private const string WalletKey = "WalletKey";

    public event Action BalanceChanged;
    public event Action WereNotEnoughFunds;

    public float Funds { get; private set; }

    public void Initialize()
        => Load();

    public void AddFunds(float quantity)
    {
        if (quantity >= 0)
        {
            Funds += quantity;
            Save();

            BalanceChanged?.Invoke();
        }
        else throw new UnityException("You cannot add a negative quantity");
    }

    public bool TrySpendFunds(float quantity)
    {
        if (quantity < 0) throw new UnityException("You cannot spend a negative quantity");

        var canSpend = Funds >= quantity;

        if (canSpend)
        {
            Funds -= quantity;
            Save();

            BalanceChanged?.Invoke();
            return true;
        }
        
        WereNotEnoughFunds?.Invoke();
        return false;
    }

    private void Save() => PlayerPrefs.SetFloat(WalletKey, Funds);
    
    private void Load() => Funds = PlayerPrefs.GetFloat(WalletKey, 0);
}