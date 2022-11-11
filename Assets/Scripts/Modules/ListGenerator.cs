using Modules.DataStorage;
using UI.Models;
using UI.Views;
using UniRx;
using UnityEngine;
using Zenject;

namespace Modules
{
    public class ListGenerator
    {
        private ISpawnModule<AbstractModel> _spawnModule;
        private MainScreen _mainScreen;

        [Inject]
        public void Construct(ISpawnModule<AbstractModel> spawnModule, 
            IDataStorage dataStorage, MainScreen mainScreen)
        {
            _mainScreen = mainScreen;
            _spawnModule = spawnModule;
            dataStorage.Lists.ObserveAdd()
                .Select(x => x.Value)
                .Subscribe(GenerateList);
        }
        
        public void GenerateList(ListModel listModel)
        {
            listModel.PrefabName = "ItemList";
            listModel.ParentTransform = _mainScreen.ListsContainer;
            _spawnModule.Spawn(listModel);
            
            var listView = listModel.View as ListView;
            foreach (var itemModel in listModel.Items)
                GenerateItem(itemModel, listView.ItemContainer);
            listModel.OnListGenerated?.Invoke();
        }

        private void GenerateItem(ItemModel itemModel,Transform parent)
        {
            itemModel.PrefabName = "Item";
            itemModel.ParentTransform = parent;
            _spawnModule.Spawn(itemModel);
        }
    }
}