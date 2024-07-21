using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Bubble : MonoBehaviour
{
    [Inject] private GameController _gameController;
    [Inject] private IAudioPlayer _audioPlayer;
    [Inject] private IWallet _wallet;

    private Wave _wave;
    private int _health;

    public event Action Bumped;

    public int Health => _health;

    private void Awake()
    {
        _wave = GetComponentInParent<Wave>();
        Init();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.TryGetComponent(out Ball _)) return;
        
        _audioPlayer.Play("audio.bubble");
        
        if (_health == 0) return;

        _health--;
        _wallet.AddFunds(1f);
        Bumped?.Invoke();
        
        if (_health > 0) return;
        
        _wave.RemoveBubble(this);
        Destroy(gameObject, 0.6f);
    }

    private void Init()
    {
        var waveNumber = _gameController.GetWave();
        _health = Random.Range(1, 4) * waveNumber;
        _wave.AddBubble(this);
    }
}
