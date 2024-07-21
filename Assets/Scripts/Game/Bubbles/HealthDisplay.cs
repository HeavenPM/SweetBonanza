using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    private Bubble _bubble;
    private TMP_Text _tmp;

    private void Awake()
    {
        _tmp = GetComponent<TMP_Text>();
        _bubble = GetComponentInParent<Bubble>();
    }

    private void OnEnable()
    {
        _tmp.text = $"{_bubble.Health}";
        _bubble.Bumped += BubbleOnBumped;
    }

    private void OnDisable()
    {
        _bubble.Bumped -= BubbleOnBumped;
    }

    private void BubbleOnBumped()
    {
        _tmp.text = $"{_bubble.Health}";
        _tmp.Enlarge();
    }
}