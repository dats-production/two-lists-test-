using UI;
using UI.Views;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private MainScreen mainScreen;
        
        public override void InstallBindings()
        {
            Container.Bind<UIManager>().FromNew().AsSingle();
            Container.Bind<MainScreen>().FromInstance(mainScreen).AsSingle();
        }
    }
}