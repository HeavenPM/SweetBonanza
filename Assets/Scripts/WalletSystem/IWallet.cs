using System;

public interface IWallet
{
    event Action BalanceChanged;
    
    event Action WereNotEnoughFunds;
    
    float Funds { get; }

    void AddFunds(float quantity);

    bool TrySpendFunds(float quantity);
}