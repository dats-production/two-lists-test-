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
            Container.BindInterfacesTo<ClearModule<ItemModel>>().AsSingle();
            Container.BindInterfacesTo<DataStorage>().AsSingle();
            Container.BindInterfacesTo<PrefabsBase>().FromInstance(prefabBase).AsSingle();
            Container.BindInterfacesTo<InstantiateFabric>().AsSingle();
            Container.BindInterfacesTo<SpawnModule>().AsSingle();
            Container.Bind<ListGenerator>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesTo<SaveLoadModule>().AsSingle();
            Container.BindInterfacesTo<DragAndDropModule>().AsSingle();
            Container.Bind<ApplicationStartModule>().FromNew().AsSingle().NonLazy();
        }
    }
}