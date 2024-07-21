using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private AudioPlayer _audioPlayer;
    
    public override void InstallBindings()
    {
        Container.Bind<IAudioPlayer>().FromInstance(_audioPlayer.GetComponent<IAudioPlayer>()).AsSingle();
        
        Container.BindInterfacesAndSelfTo<Wallet>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<Inventory>().AsSingle();
        
        Container.Bind<GameController>().AsSingle();
        
        Container.Bind<PauseSwitch>().AsSingle();
    }
}
