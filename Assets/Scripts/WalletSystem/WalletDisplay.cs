using TMPro;
using UnityEngine;
using Zenject;

public class WalletDisplay : MonoBehaviour
{
    [Inject] private IWallet _wallet;
    
    private TMP_Text _balanceText;

    private void Awake() =>
        _balanceText = GetComponent<TMP_Text>();
    
    private void OnEnable()
    {
        _balanceText.text = _wallet.Funds.FormatWithSpaces();

        _wallet.BalanceChanged += OnBalanceChanged;
        _wallet.WereNotEnoughFunds += ShowNotEnoughFunds;
    }

    private void OnDisable()
    {
        _wallet.BalanceChanged -= OnBalanceChanged;
        _wallet.WereNotEnoughFunds -= ShowNotEnoughFunds;
    }
    
    private void OnBalanceChanged()
    {
        _balanceText.text = _wallet.Funds.FormatWithSpaces();

        _balanceText.Enlarge(scaleMultiplier: 1.2f);
    }

    private void ShowNotEnoughFunds()
    {
        _balanceText.WarningEnlarge(scaleMultiplier: 1.2f);
    }
}