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
        public List<ItemModel> ItemsList { get; private set; } = new();
        public int StartItemCount { get; set; }
        public Action<int> OnUpdateListCount { get; set; }
        public Action OnListGenerated { get; set; }

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
            if(ItemsList == null || ItemsList.Count == 0) return;

            IEnumerable<ItemModel> sortedList = isAscendant 
                ? ItemsList.OrderBy(x => x?.IntValue).ToList() 
                : ItemsList.OrderByDescending(x => x?.IntValue).ToList();
            
            ItemsList = (List<ItemModel>)sortedList;
            UpdateList();
        }
        
        public void SortByString(bool isAscendant)
        {
            if(ItemsList == null || ItemsList.Count == 0) return;

            IEnumerable<ItemModel> sortedList = isAscendant 
                ? ItemsList.OrderBy(x => x?.StringValue).ToList() 
                : ItemsList.OrderByDescending(x => x?.StringValue).ToList();
            
            ItemsList = (List<ItemModel>)sortedList;
            UpdateList();
        }
        
        public void InsertItem(int index, ItemModel itemModel)
        {
            if (ItemsList.Contains(itemModel))
            {
                ItemsList.Remove(itemModel);
            }
            else RemoveFromOtherList(itemModel);
            ClearItem(itemModel);
            
            if(index <= ItemsList.Count)
                ItemsList.Insert(index, itemModel);
            else
                ItemsList.Add(itemModel);
            
            SpawnItem(itemModel);
            OnUpdateListCount.Invoke(ItemsList.Count);
        }

        private void RemoveFromOtherList(ItemModel itemModel)
        {
            if (this is FirstListModel)
            {
                _secondListModel.ItemsList.Remove(itemModel);
                _secondListModel.OnUpdateListCount.Invoke(_secondListModel.ItemsList.Count);
            }
            else
            {
                _firstListModel.ItemsList.Remove(itemModel);
                _firstListModel.OnUpdateListCount.Invoke(_firstListModel.ItemsList.Count);
            }
        }

        private void UpdateList()
        {
            foreach (var itemModel in ItemsList)
            {
                _clearModule.ClearView(itemModel);
                _spawnModule.Spawn(itemModel);
                OnUpdateListCount.Invoke(ItemsList.Count);
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