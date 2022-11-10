using DataBases;
using Modules;
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
            Container.BindInterfacesTo<PrefabsBase>().FromInstance(prefabBase).AsSingle();
            Container.BindInterfacesTo<InstantiateFabric>().AsSingle();
            Container.BindInterfacesTo<SpawnModule>().AsSingle();
            Container.Bind<ApplicationStartModule>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesTo<ListGenerator>().AsSingle();
            Container.BindInterfacesTo<ClearModule<ItemModel>>().AsSingle();
            Container.BindInterfacesTo<SaveLoadModule>().AsSingle();
            Container.BindInterfacesTo<FileBrowser>().AsSingle();
        }

        private void BindModels()
        {
            Container.Bind<FirstListModel>().AsSingle();
            Container.Bind<SecondListModel>().AsSingle();
            Container.Bind<ItemModel>().AsSingle();
        }
    }
}