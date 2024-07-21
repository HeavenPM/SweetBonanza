using UnityEngine;
using Zenject;

public class PlatformMover : MonoBehaviour
{
    [Inject] private GameController _gameController;
    
    private const float MinX = -355f;
    private const float MaxX = 355f;
    
    private RectTransform _platformRectTransform;
    
    public void SetPosition(Vector2 initialTouchPosition, Vector2 currentTouchPosition)
    {
        var difference = currentTouchPosition - initialTouchPosition;
        var newX = Mathf.Clamp(_platformRectTransform.anchoredPosition.x + difference.x, MinX, MaxX);
        _platformRectTransform.anchoredPosition = new Vector2(newX, _platformRectTransform.anchoredPosition.y);
    }
    
    private void Awake()
    {
        _gameController.SetPlatform(this);
        _platformRectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        GameControllerOnStarted();
        _gameController.Started += GameControllerOnStarted;
    }

    private void OnDisable()
    {
        _gameController.Started -= GameControllerOnStarted;
    }
    
    private void GameControllerOnStarted()
    {
        _platformRectTransform.anchoredPosition = new Vector2(0, _platformRectTransform.anchoredPosition.y);
    }
}