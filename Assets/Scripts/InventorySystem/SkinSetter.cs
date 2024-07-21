using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SkinSetter : MonoBehaviour
{
    [SerializeField] private string _category;
    
    [Inject] private IInventory _inventory;
    [Inject] private ResourcesLoader _resourcesLoader;
    
    private void OnEnable()
    {
        OnUpdated();
        _inventory.Updated += OnUpdated;
    }

    private void OnDisable()
        => _inventory.Updated -= OnUpdated;
    
    private void OnUpdated()
    {
        var identifier = _inventory.GetCurrentItem(_category).Identifier;
        var sprite = _resourcesLoader.Get<Sprite>($"sprite.{identifier}");
            
        if (TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            spriteRenderer.sprite = sprite;
            return;
        }

        TryGetComponent(out Image image);
        image.sprite = sprite;
    }
}