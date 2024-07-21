using System;
using Zenject;

public class GameController
{
    [Inject] private WindowsManager _windowsManager;
    [Inject] private IAudioPlayer _audioPlayer;
    
    private PlatformMover _platformMover;
    private int _wave;

    public event Action Started;
    public event Action Cleared;

    public int GetWave() => _wave;
    
    public PlatformMover GetPlatform()
        => _platformMover;
    
    public void SetPlatform(PlatformMover platform) 
        => _platformMover = platform;

    public void Start()
    {
        _wave = 1;
        Started?.Invoke();
    }

    public void OnWaveCleared()
    {
        _wave++;
        Cleared?.Invoke();
    }

    public void OnFail()
    {
        _windowsManager.ShowOver("popup.fail");
        _audioPlayer.Play("audio.fail");
    }
}
