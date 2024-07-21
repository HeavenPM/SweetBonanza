using UnityEngine;
using Zenject;

public class GameStarter : MonoBehaviour
{
    [Inject] private DiContainer _container;
    [Inject] private GameController _gameController;

    public void StartGame()
        => _gameController.Start();
    
    private void Awake()
        => _container.Rebind<GameStarter>().FromInstance(this).AsSingle();

    private void OnEnable()
        => StartGame();
}
