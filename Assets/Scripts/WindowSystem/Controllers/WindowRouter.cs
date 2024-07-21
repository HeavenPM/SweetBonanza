using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class WindowRouter : MonoBehaviour
{
    [SerializeField] private string _id;
    
    [Inject] private WindowsManager _windows;
    
    private Button _button;

    private void Awake()
        => _button = GetComponent<Button>();

    private void OnEnable() 
        => _button.onClick.AddListener(OnClick);

    private void OnDisable() 
        => _button.onClick.RemoveListener(OnClick);

    private void OnClick()
        => _windows.ShowAlone(_id);
    
}