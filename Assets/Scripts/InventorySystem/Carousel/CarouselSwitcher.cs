using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CarouselSwitcher : MonoBehaviour
{
    [SerializeField] private Direction _direction;

    private ItemCarouselController _controller;
    private Button _button;

    private void Awake()
    {
        _controller = GetComponentInParent<ItemCarouselController>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
        => _button.onClick.AddListener(OnClick);

    private void OnDisable()
        => _button.onClick.RemoveListener(OnClick);

    private void OnClick()
    {
        if (_direction == Direction.Next)
        {
            _controller.ShowNext();
            return;
        }
        
        _controller.ShowPrevious();
    }
    
    private enum Direction { Next, Back }
}