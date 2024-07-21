using UnityEngine;
using Zenject;

public class WavesLoader : MonoBehaviour
{
    [Inject] private GameController _gameController;
    [Inject] private ResourcesLoader _resourcesLoader;

    private void OnEnable()
    {
        Create();
        _gameController.Cleared += Create;
        _gameController.Started += Create;
    }

    private void OnDisable()
    {
        _gameController.Cleared -= Create;
        _gameController.Started -= Create;
    }

    private void Create()
    {
        Clear();

        var waveId = Random.Range(0, 5);
        var waveTemplate = _resourcesLoader.Get<Wave>($"wave.{waveId}");
        Instantiate(waveTemplate, transform);
    }

    private void Clear()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}
