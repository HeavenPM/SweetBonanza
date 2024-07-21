using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopItemButton : MonoBehaviour
{
    [Inject] private IInventory _inventory;
    [Inject] private ResourcesLoader _resourcesLoader;

    private Item _item;
    private Image _image;

    public void SetItem(Item item)
    {
        _item = item;
        _image = GetComponent<Image>();
        UpdateView();
    }

    private void OnEnable()
        => _inventory.Updated += UpdateView;
    
    private void OnDisable()
        => _inventory.Updated -= UpdateView;
    
    private void UpdateView()
    {
        var sprite = _resourcesLoader.Get<Sprite>($"sprite.button.{_item.Status}");
        _image.sprite = sprite;
    }
}
