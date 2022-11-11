using System.Collections.Generic;
using Modules.DataStorage;
using UI.Models;
using UI.Views;
using UnityEngine;
using Zenject;

namespace Modules
{
    public class ApplicationStartModule
    {
        const string glyphs= "abcdefghijklmnopqrstuvwxyz";
        
        private readonly int _minItemIntValue = 0;
        private readonly int _maxItemIntValue = 100;
        private readonly int _minCharAmount = 1;
        private readonly int _maxCharAmount = 10;
        
        private ISpawnModule<AbstractModel> _spawnModule;
        private MainScreen _mainScreen;
        private IListGenerator _listGenerator;
        private int _firstListItemCount = 2;
        private int _secondListItemCount = 4;
        private DiContainer _diContainer;
        private IDataStorage _dataStorage;

        [Inject]
        public void Construct(ISpawnModule<AbstractModel> spawnModule, DiContainer diContainer,
            MainScreen mainScreen, IListGenerator listGenerator, IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
            _diContainer = diContainer;
            _listGenerator = listGenerator;
            _mainScreen = mainScreen;
            _spawnModule = spawnModule;
            
            CreateListModels();
        }

        private void CreateListModels()
        {
            var firstListModel = _diContainer.Instantiate<ListModel>(new object[]
            {
                "First List",
                CreateItemModels(_firstListItemCount),
                true
            });
            _dataStorage.AddList(firstListModel);

            var secondListModel = _diContainer.Instantiate<ListModel>(new object[]
            {
                "Second List",
                CreateItemModels(_secondListItemCount),
                false
            });
            _dataStorage.AddList(secondListModel);
        }

        private List<ItemModel> CreateItemModels(int count)
        {
            var itemModelsList = new List<ItemModel>();
            for (var i = 0; i < count; i++)
            {
                var itemModel = new ItemModel(GetRandomString(), GetRandomInt());
                itemModelsList.Add(itemModel);
            }
            return itemModelsList;
        }
                
        private int GetRandomInt()
        {
            return Random.Range(_minItemIntValue, _maxItemIntValue);
        }

        private string GetRandomString()
        {
            var charAmount = Random.Range(_minCharAmount, _maxCharAmount);
            var myString = "";
            for(var i = 0; i < charAmount; i++)
            {
                myString += glyphs[Random.Range(0, glyphs.Length)];
            }
            return myString;
        }
    }
}