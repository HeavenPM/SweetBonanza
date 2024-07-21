using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class Restarter : MonoBehaviour
{
    [Inject] private WindowsManager _windowsManager;
    [Inject] private GameStarter _gameStarter;

    private Button _button;

    private void Awake()
        => _button = GetComponent<Button>();

    private void OnEnable()
        => _button.onClick.AddListener(OnClick);
    

    private void OnDisable()
        => _button.onClick.RemoveListener(OnClick);

    private void OnClick()
    {
        _windowsManager.ShowPrevious();
        _gameStarter.StartGame();
    }
}
