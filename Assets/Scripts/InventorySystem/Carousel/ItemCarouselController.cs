using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemCarouselController : MonoBehaviour
{
    [Inject] private ResourcesLoader _resourcesLoader;
    [Inject] private JsonLoader _jsonLoader;

    private List<Item> _config;
    private Transform _container;
    private int _itemPointer;

    public void ShowNext()
    {
        _itemPointer++;
        if (_itemPointer > _config.Count - 1) _itemPointer = 0;
        
        ClearItem();
        ShowItem();
    }
    
    public void ShowPrevious()
    {
        _itemPointer--;
        if (_itemPointer < 0) _itemPointer = _config.Count - 1;
        
        ClearItem();
        ShowItem();
    }
    
    private void Awake()
    {
        _config = _jsonLoader.Get<List<Item>>("Configs/ItemsConfig");

        GetContainer();
        ShowItem();
    }

    private void ShowItem()
    {
        var itemId = _config[_itemPointer].Identifier;
        var item = _resourcesLoader.Get<ShopItem>($"prefab.shopItem.{itemId}");
        Instantiate(item, _container);
    }

    private void GetContainer()
    {
        foreach (Transform child in transform)
        {
            if (child.name != "Items") continue;
            _container = child;
            return;
        }
    }
    
    private void ClearItem()
        => Destroy(_container.GetChild(0).gameObject);
}
