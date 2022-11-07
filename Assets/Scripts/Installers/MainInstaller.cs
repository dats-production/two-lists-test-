using DataBases;
using Modules;
using Modules.Fabrics;
using UI.Models;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private PrefabsBase prefabBase;
        
        public override void InstallBindings()
        {
            BindModules();
            BindModels();
        }

        private void BindModules()
        {
            Container.BindInterfacesTo<PrefabsBase>().FromInstance(prefabBase).AsSingle();
            Container.BindInterfacesTo<InstantiateFabric>().AsSingle();
            Container.BindInterfacesTo<SpawnModule>().AsSingle();
            Container.BindInterfacesTo<ApplicationStartModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ListGenerator>().AsSingle();
            Container.BindInterfacesTo<ListChangerModule>().AsSingle();
        }

        private void BindModels()
        {
            Container.BindInterfacesAndSelfTo<FirstListModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<SecondListModel>().AsSingle();
            Container.Bind<ItemModel>().AsSingle();
        }
    }
}