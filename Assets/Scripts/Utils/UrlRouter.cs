using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UrlRouter : MonoBehaviour
{
    [SerializeField] private string _url;
    
    private Button _button;

    private void Awake()
        => _button = GetComponent<Button>();

    private void OnEnable()
        => _button.onClick.AddListener(OnClick);

    private void OnDisable()
        => _button.onClick.AddListener(OnClick);

    private void OnClick()
    {
        if (string.IsNullOrEmpty(_url)) throw new Exception("---- UrlRouter OnClick: URL is empty");
        
        Application.OpenURL(_url);
    }
}
