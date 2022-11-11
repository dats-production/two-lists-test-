using DataBases;
using Modules;
using Modules.DataStorage;
using Modules.Fabrics;
using Modules.SaveLoad;
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
            Container.BindInterfacesTo<ClearModule<ItemModel>>().AsSingle();
            Container.BindInterfacesTo<DataStorage>().AsSingle();
            Container.BindInterfacesTo<PrefabsBase>().FromInstance(prefabBase).AsSingle();
            Container.BindInterfacesTo<InstantiateFabric>().AsSingle();
            Container.BindInterfacesTo<SpawnModule>().AsSingle();
            Container.Bind<ApplicationStartModule>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesTo<ListGenerator>().AsSingle();
            Container.BindInterfacesTo<SaveLoadModule>().AsSingle();
        }

        private void BindModels()
        {
            //Container.Bind<ListModel>().AsSingle();
            Container.Bind<ItemModel>().AsSingle();
        }
    }
}