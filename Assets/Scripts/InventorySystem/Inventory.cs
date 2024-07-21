using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

public class Inventory : IInventory, IInitializable
{
    [Inject] private IWallet _walletController;
    [Inject] private JsonLoader _jsonLoader;
    
    private List<Item> _items;
    
    public event Action Updated;

    public void Initialize()
        => Load();
    
    public Item GetItemById(string identifier) => 
        _items.First(item => item.Identifier == identifier);
    
    public Item GetCurrentItem(string category) => 
        _items.First(item => item.Category == category && item.Status == Status.InUse);
    
    public void SetUsage(string identifier)
    {
        var newItem = _items.First(item => item.Identifier == identifier);
        var category = newItem.Category;
        var currentItem = _items.First(item => item.Category == category && item.Status == Status.InUse);
        
        currentItem.Status = Status.Received;
        newItem.Status = Status.InUse;
        
        Save();
    }

    public void SetReceived(string identifier)
    {
        var newItem = _items.First(item => item.Identifier == identifier);
        
        if (!_walletController.TrySpendFunds(newItem.Price)) return;
        
        newItem.Status = Status.Received;
        Save();
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey(nameof(_items)))
        {
            _items = _jsonLoader.Get<List<Item>>("Configs/ItemsConfig");
            Save();
            return;
        }
        
        var json = PlayerPrefs.GetString(nameof(_items));
        _items = JsonConvert.DeserializeObject<List<Item>>(json);
    }

    private void Save()
    {
        var json = JsonConvert.SerializeObject(_items);
        PlayerPrefs.SetString(nameof(_items), json);
        Updated?.Invoke();
    }
}