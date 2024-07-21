using UnityEngine;
using Zenject;

public class UserHelpDisplay : MonoBehaviour
{
    [Inject] private WindowsManager _windowsManager;
    
    private void Start()
        => CheckNewbie();
    
    private void CheckNewbie()
    {
        var isNewbie = PlayerPrefs.GetInt("IsNewbie", 0) == 0;
        
        if (!isNewbie) return;

        PlayerPrefs.SetInt("IsNewbie", 1);
        _windowsManager.ShowOver("popup.info");
    }
}
