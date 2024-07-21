using System;
using System.Threading.Tasks;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(WindowDirector))]
public class Window : MonoBehaviour
{
    public event Action BeforeShow, AfterShow;
    public event Action BeforeHide, AfterHide;
    public event Action Asleep, WokenUp;
    
    public bool isRevisitable { get; private set; }
    public WindowState state { get; private set; }

    private WindowDirector _director;

    private void Awake() 
        => _director = GetComponent<WindowDirector>();

    public async Task Show(bool isRevisitable = true, Action callback = null)
    {
        Log($"start SHOW [{name}]");
        
        MoveToFront();

        BeforeShow?.Invoke();
        await _director.Play();
        
        this.isRevisitable = isRevisitable;
        state = WindowState.Visible;
        
        Log($"end SHOW [{name}]");
            
        AfterShow?.Invoke();
        callback?.Invoke();
    }
    
    public async Task Hide(Action callback = null)
    {
        Log($"start HIDE [{name}]");
        
        BeforeHide?.Invoke();
        await _director.PlayReverse();
        
        state = WindowState.Invisible;
        
        Log($"end HIDE [{name}]");

        AfterHide?.Invoke();
        callback?.Invoke();

        MoveToBack();
    }

    public void Sleep()
    {
        Log($"{name} Sleep");
        
        state = WindowState.Dormant;
        Asleep?.Invoke();
    }

    public void Wake()
    {
        Log($"{name} Wake");
        
        state = WindowState.Visible;
        WokenUp?.Invoke();
    }
    
    private void MoveToFront()
    {
        transform.SetAsLastSibling();
        gameObject.SetActive(true);
    }
    
    private void MoveToBack()
    {
        transform.SetAsFirstSibling();
        gameObject.SetActive(false);
    }

    public void SetParent(Transform parent) 
        => transform.SetParent(parent);
    
    public bool isVisible
        => state == WindowState.Visible;
    
    public bool isDormant
        => state == WindowState.Dormant;
    
    public bool isInvisible
        => state == WindowState.Invisible;

    private void Log(string message)
        => Debug.Log($"---- Window {name} {message}");
}