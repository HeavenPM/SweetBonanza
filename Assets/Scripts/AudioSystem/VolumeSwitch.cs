using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class VolumeSwitch : MonoBehaviour
{
    [SerializeField] private AudioType _audioType;
    [SerializeField] private Transform _visualTransform;
    
    [Inject] private ResourcesLoader _resources;
    [Inject] private IAudioPlayer _audioPlayer;
    
    private Image _iconImage;
    private Button _button;
    private Sprite _enableSprite;
    private Sprite _disableSprite;

    private void Awake()
    {
        _iconImage = _visualTransform.GetComponent<Image>();
        _button = GetComponent<Button>();
        
        _enableSprite = _resources.Get<Sprite>($"sprite.soundOn");
        _disableSprite = _resources.Get<Sprite>($"sprite.soundOff");
    }

    private void OnEnable()
    {
        SetVisual();
        _button.onClick.AddListener(SwitchAudio);
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(SwitchAudio);
    }
    
    private void SwitchAudio()
    {
        _audioPlayer.Toggle(_audioType == AudioType.Music
            ? AudioType.Music.ToString()
            : AudioType.Sound.ToString());

        SetVisual();
    }

    private void SetVisual()
    {
        bool isEnable;
        if (_audioType == AudioType.Music) isEnable = _audioPlayer.MusicVolume > 0;
        else isEnable = _audioPlayer.SoundVolume > 0;
        
        _iconImage.sprite = isEnable ? 
            _enableSprite : _disableSprite;
    }
}
