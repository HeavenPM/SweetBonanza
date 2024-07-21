using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class SwipeInputHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [Inject] private GameController _gameController;

    private PlatformMover _platformMover;
    private Vector2 _initialTouchPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        SetPlatform();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _platformMover.GetComponent<RectTransform>().parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out _initialTouchPosition
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _platformMover.GetComponent<RectTransform>().parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out var currentTouchPosition
        );

        _platformMover.SetPosition(_initialTouchPosition, currentTouchPosition);
        _initialTouchPosition = currentTouchPosition;
    }

    private void SetPlatform()
    {
        if (_platformMover != null) return;
        _platformMover = _gameController.GetPlatform();
    }
}