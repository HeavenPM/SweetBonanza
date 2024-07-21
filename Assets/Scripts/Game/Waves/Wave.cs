using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class Wave : MonoBehaviour
{
    [Inject] private GameController _gameController;
    
    private readonly List<Bubble> _bubbles = new();

    public void AddBubble(Bubble newBubble)
        => _bubbles.Add(newBubble);

    public void RemoveBubble(Bubble bubble)
    {
        _bubbles.Remove(bubble);
        
        if (_bubbles.Count > 0) return;

        DOVirtual.DelayedCall(0.7f, () =>
        {
            _gameController.OnWaveCleared();
        });
    }
}
