using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Modules;
using UI.Views;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace UI.Models
{
    public abstract class AbstractListModel: AbstractModel
    {

        private FirstListModel _firstListModel;
        private SecondListModel _secondListModel;
        private IClearModule<ItemModel> _clearModule;
        private ISpawnModule<AbstractModel> _spawnModule;
        private Action<int> OnReplaceInsideList;
        public List<ItemModel> ItemList { get; private set; } = new();
        public Transform ItemContainer { get; set; }
        public int StartItemCount { get; set; }

        [Inject]
        public void Construct(FirstListModel firstListModel, SecondListModel secondListModel,
            IClearModule<ItemModel> clearModule, ISpawnModule<AbstractModel> spawnModule)
        {
            _spawnModule = spawnModule;
            _clearModule = clearModule;
            _secondListModel = secondListModel;
            _firstListModel = firstListModel;
            // ItemList
            //     .ObserveEveryValueChanged(x=> x.Count)
            //     .Subscribe(OnValueChanged);
        }
        
        public void SortByInt(bool isAscendant)
        {
            if(ItemList == null || ItemList.Count == 0) return;

            IEnumerable<ItemModel> sortedList = isAscendant 
                ? ItemList.OrderBy(x => x?.IntProperty).ToList() 
                : ItemList.OrderByDescending(x => x?.IntProperty).ToList();
            
            ItemList = (List<ItemModel>)sortedList;
            UpdateList();
        }
        
        public void SortByString(bool isAscendant)
        {
            if(ItemList == null || ItemList.Count == 0) return;

            IEnumerable<ItemModel> sortedList = isAscendant 
                ? ItemList.OrderBy(x => x?.StringProperty).ToList() 
                : ItemList.OrderByDescending(x => x?.StringProperty).ToList();
            
            ItemList = (List<ItemModel>)sortedList;
            UpdateList();
        }
        
        public void InsertItem(int index, ItemModel itemModel)
        {
            
            if (ItemList.Contains(itemModel))
            {
                ItemList.Remove(itemModel);
                OnReplaceInsideList?.Invoke(index);
            }
            else RemoveFromOtherList(itemModel);
            
            if(index <= ItemList.Count)
                ItemList.Insert(index, itemModel);
            else
                ItemList.Add(itemModel);

            var listView = View as ItemListView;
            SpawnItem(itemModel, listView.ItemContainer);
            Debug.Log(_firstListModel.ItemList.Count());
            Debug.Log(_secondListModel.ItemList.Count());
            //SpawnItem(itemModel, );
            //_firstListModel.UpdateList();
            //_secondListModel.UpdateList();
        }

        private void RemoveFromOtherList(ItemModel itemModel)
        {
            if (this is FirstListModel)
                _secondListModel.ItemList.Remove(itemModel);
            else
                _firstListModel.ItemList.Remove(itemModel);
            ClearItem(itemModel);
        }

        private void UpdateList()
        {
            foreach (var itemModel in ItemList)
            {
                _clearModule.ClearView(itemModel);
                _spawnModule.Spawn(itemModel);
            }
        }

        private void SpawnItem(ItemModel itemModel, Transform parent)
        {
            itemModel.ParentTransform = parent;
            UpdateList();
        }
        
        private void ClearItem(ItemModel itemModel)
        {
            _clearModule.ClearView(itemModel);
        }
    }
}