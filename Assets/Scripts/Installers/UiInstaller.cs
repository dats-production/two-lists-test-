using UI;
using UI.Views;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private MainScreen mainScreen;
        
        [FormerlySerializedAs("Canvas"), SerializeField]
        private Canvas canvas;
        
        public override void InstallBindings()
        {
            var canvasObj = Instantiate(canvas);
            var canvasTransform = canvasObj.transform;
            
            Container.BindInterfacesAndSelfTo<MainScreen>()
                .FromComponentInNewPrefab(mainScreen)
                .UnderTransform(canvasTransform).AsSingle();
            Container.Bind<UIManager>().FromNew().AsSingle();
        }
    }
}