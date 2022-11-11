using Modules.DataStorage;
using UI.Models;
using UI.Views;
using UniRx;
using UnityEngine;
using Zenject;

namespace Modules
{
    public interface IListGenerator
    {
        void GenerateList(ListModel listModel);
    }
    
    public class ListGenerator : IListGenerator
    {
        private ISpawnModule<AbstractModel> _spawnModule;
        private IDataStorage _dataStorage;
        private MainScreen _mainScreen;

        [Inject]
        public void Construct(ISpawnModule<AbstractModel> spawnModule, 
            IDataStorage dataStorage, MainScreen mainScreen)
        {
            _mainScreen = mainScreen;
            _dataStorage = dataStorage;
            _spawnModule = spawnModule;
            _dataStorage.Lists.ObserveAdd()
                .Select(x=>x.Value)
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