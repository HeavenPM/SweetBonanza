using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private string _identifier;

    [Inject] private IInventory _inventory;
    [Inject] private ResourcesLoader _resourcesLoader;
    
    private Item _item;
    private Button _button;

    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
        _item = _inventory.GetItemById(_identifier);
    }

    private void OnEnable()
    {
        Init();
        _button.onClick.AddListener(OnClick);
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        if (_item.Status == Status.Received)
            _inventory.SetUsage(_identifier);
        
        if (_item.Status == Status.Purchasable) 
            _inventory.SetReceived(_identifier);
    }

    private void Init()
    {
        var shopButton = GetComponentInChildren<ShopItemButton>();
        shopButton.SetItem(_item);
        
        var price = GetComponentInChildren<ShopItemPrice>();
        price.SetItem(_item);
    }
}