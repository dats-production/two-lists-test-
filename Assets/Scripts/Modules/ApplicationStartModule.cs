using System.Collections.Generic;
using Modules.DataStorage;
using UI.Models;
using UnityEngine;
using Zenject;

namespace Modules
{
    public class ApplicationStartModule
    {
        private const string Glyphs = "abcdefghijklmnopqrstuvwxyz";
        
        private readonly int _minItemIntValue = 0;
        private readonly int _maxItemIntValue = 100;
        private readonly int _minCharAmount = 1;
        private readonly int _maxCharAmount = 10;
        
        private readonly int _firstListItemCount = 2;
        private readonly int _secondListItemCount = 4;
        private DiContainer _diContainer;
        private IDataStorage _dataStorage;

        [Inject]
        public void Construct(DiContainer diContainer, IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
            _diContainer = diContainer;
            
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
                var itemModel = _diContainer.Instantiate<ItemModel>(new object[]
                {
                    GetRandomString(),
                    GetRandomInt()
                });
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
                myString += Glyphs[Random.Range(0, Glyphs.Length)];
            }
            return myString;
        }
    }
}