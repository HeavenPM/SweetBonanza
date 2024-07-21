using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class WindowDirector : MonoBehaviour
{
    private PlayableDirector _director;
    private bool _isCoroutineRunning;

    private void Awake() 
        => _director = GetComponent<PlayableDirector>();

    public async Task Play()
    {
        _director.time = 0;
        _director.Play();
            
        while (_director.state == PlayState.Playing) await Task.Yield();
    }

    public async Task PlayReverse()
    {
        StartCoroutine(WaitCoroutine());
            
        while (_isCoroutineRunning) await Task.Yield();
    }
    
    private IEnumerator WaitCoroutine()
    {
        _isCoroutineRunning = true;
        
        yield return StartCoroutine(_director.Reverse());
        
        _isCoroutineRunning = false;
    }
}