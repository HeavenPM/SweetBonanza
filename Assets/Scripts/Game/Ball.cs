using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [Inject] private GameController _gameController;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _initialPosition;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _initialPosition = new Vector2(transform.position.x, transform.position.y);
    }
    
    private void OnEnable()
    {
        GameControllerOnStarted();
        _gameController.Started += GameControllerOnStarted;
    }

    private void OnDisable()
         => _gameController.Started -= GameControllerOnStarted;

    private void GameControllerOnStarted()
    {
        transform.position = new Vector2(_initialPosition.x, _initialPosition.y);
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0f;
    }
}
