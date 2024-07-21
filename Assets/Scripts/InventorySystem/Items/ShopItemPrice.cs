using TMPro;
using UnityEngine;
using Zenject;

public class ShopItemPrice : MonoBehaviour
{
    [Inject] private IInventory _inventory;

    private Item _item;
    private CanvasGroup _canvasGroup;
    private TMP_Text _tmp;

    public void SetItem(Item item)
    {
        _item = item;
        _canvasGroup = GetComponent<CanvasGroup>();
        _tmp = GetComponentInChildren<TMP_Text>();
        UpdateView();
    }
    
    private void OnEnable()
        => _inventory.Updated += UpdateView;
    
    private void OnDisable()
        => _inventory.Updated -= UpdateView;
    
    private void UpdateView()
    {
        _canvasGroup.alpha = _item.Status == Status.Purchasable ? 1f : 0f;
        
        if (_item.Status != Status.Purchasable) return;

        _tmp.text = $"{_item.Price}";
    }
}
