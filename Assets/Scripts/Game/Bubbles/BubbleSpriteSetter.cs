using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BubbleSpriteSetter : MonoBehaviour
{
    [Inject] private ResourcesLoader _resourcesLoader;

    private Image _image;

    private void Awake()
        => _image = GetComponent<Image>();
    
    private void OnEnable()
    {
        var sprite = _resourcesLoader.Get<Sprite>($"sprite.bubble.{Random.Range(0, 5)}");
        _image.sprite = sprite;
    }
}
