using System;
using System.Collections.Generic;
using System.Linq;
using Modules;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Models
{
    public abstract class AbstractListModel: AbstractModel
    {
        private FirstListModel _firstListModel;
        private SecondListModel _secondListModel;
        private IClearModule<ItemModel> _clearModule;
        private ISpawnModule<AbstractModel> _spawnModule;
        private Transform _containerTransform;

        public ReactiveProperty<bool> IsSortingPanelActive { get; set; } = new();
        public List<ItemModel> ItemList { get; private set; } = new();
        public int StartItemCount { get; set; }
        public Action<int> OnUpdateListCount { get; set; }

        [Inject]
        public void Construct(FirstListModel firstListModel, SecondListModel secondListModel,
            IClearModule<ItemModel> clearModule, ISpawnModule<AbstractModel> spawnModule)
        {
            _spawnModule = spawnModule;
            _clearModule = clearModule;
            _secondListModel = secondListModel;
            _firstListModel = firstListModel;
        }

        public void SetContainerTransform(Transform containerTransform)
        {
            _containerTransform = containerTransform;
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
            }
            else RemoveFromOtherList(itemModel);
            ClearItem(itemModel);
            
            if(index <= ItemList.Count)
                ItemList.Insert(index, itemModel);
            else
                ItemList.Add(itemModel);
            
            SpawnItem(itemModel);
            OnUpdateListCount.Invoke(ItemList.Count);
        }
        
        //public void Ali

        private void RemoveFromOtherList(ItemModel itemModel)
        {
            if (this is FirstListModel)
            {
                _secondListModel.ItemList.Remove(itemModel);
                _secondListModel.OnUpdateListCount.Invoke(_secondListModel.ItemList.Count);
            }
            else
            {
                _firstListModel.ItemList.Remove(itemModel);
                _firstListModel.OnUpdateListCount.Invoke(_firstListModel.ItemList.Count);
            }
        }

        private void UpdateList()
        {
            foreach (var itemModel in ItemList)
            {
                _clearModule.ClearView(itemModel);
                _spawnModule.Spawn(itemModel);
                OnUpdateListCount.Invoke(ItemList.Count);
            }
        }

        private void SpawnItem(ItemModel itemModel)
        {
            itemModel.ParentTransform = _containerTransform;
            UpdateList();
        }
        
        private void ClearItem(ItemModel itemModel)
        {
            _clearModule.ClearView(itemModel);
        }
    }
}