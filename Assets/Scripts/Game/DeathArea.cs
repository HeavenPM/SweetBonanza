using UnityEngine;
using Zenject;

public class DeathArea : MonoBehaviour
{
    [Inject] private GameController _gameController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent(out Ball _)) return;
        
        _gameController.OnFail();
    }
}
