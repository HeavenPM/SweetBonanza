using UnityEngine;
using Zenject;

public class StartWindowRouter : MonoBehaviour
{
    [SerializeField] private string _screenId = "screen.lobby";
    
    [Inject] private WindowsManager _windows;
    
    private void Start() 
        => _windows.ShowAlone(_screenId);
}