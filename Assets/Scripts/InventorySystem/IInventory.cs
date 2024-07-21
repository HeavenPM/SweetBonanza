using System;

public interface IInventory
{
    event Action Updated;
        
    void Initialize();

    Item GetItemById(string identifier);
        
    Item GetCurrentItem(string category);
        
    void SetUsage(string identifier);

    void SetReceived(string identifier);
}