using System;
using System.Collections.Generic;
using System.Linq;
using Modules;
using Modules.DataStorage;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UI.Models
{
    public class ListModel: AbstractModel
    {
        private IClearModule<ItemModel> _clearModule;
        private ISpawnModule<AbstractModel> _spawnModule;
        private Transform _containerTransform;
        private IDataStorage _dataStorage;
        private IDragAndDropModule _dragAndDropModule;

        public bool IsSortingPanelActive { get; }
        public bool IsDropTo { get; }
        public List<ItemModel> Items { get; private set; } = new();
        public Action<int> OnUpdateListCount { get; set; }
        public Action OnListGenerated { get; set; }
        public Transform ItemContainer { get; set; }
        public float SpacingOffset { get; set; }
        public float SortingPanelHeight { get; set; }

        public ListModel(string name, List<ItemModel> items, bool isSortingPanelActive)
        {
            Name = name;
            Items = items;
            IsSortingPanelActive = isSortingPanelActive;
        }
        
        [Inject]
        public void Construct(IClearModule<ItemModel> clearModule,
            ISpawnModule<AbstractModel> spawnModule, IDataStorage dataStorage,
            IDragAndDropModule dragAndDropModule)
        {
            _dragAndDropModule = dragAndDropModule;
            _dataStorage = dataStorage;
            _spawnModule = spawnModule;
            _clearModule = clearModule;
            OnListGenerated += () => OnUpdateListCount?.Invoke(Items.Count);
        }

        public void SetContainerTransform(Transform containerTransform)
        {
            _containerTransform = containerTransform;
        }
        
        public void SortByInt(bool isAscendant)
        {
            if(Items == null || Items.Count == 0) return;

            IEnumerable<ItemModel> sortedList = isAscendant 
                ? Items.OrderBy(x => x?.IntValue).ToList() 
                : Items.OrderByDescending(x => x?.IntValue).ToList();
            
            Items = (List<ItemModel>)sortedList;
            UpdateList();
        }
        
        public void SortByString(bool isAscendant)
        {
            if(Items == null || Items.Count == 0) return;

            IEnumerable<ItemModel> sortedList = isAscendant 
                ? Items.OrderBy(x => x?.StringValue).ToList() 
                : Items.OrderByDescending(x => x?.StringValue).ToList();
            
            Items = (List<ItemModel>)sortedList;
            UpdateList();
        }
        
        public void InsertItem(int index, ItemModel itemModel)
        {
            RemoveFromList(itemModel);
            ClearItem(itemModel);
            
            if(index <= Items.Count)
                Items.Insert(index, itemModel);
            else
                Items.Add(itemModel);
            
            SpawnItem(itemModel);
            OnUpdateListCount?.Invoke(Items.Count);
        }

        public void SetListToDrop()
        {
            _dragAndDropModule.SetListToDrop(this);
        }

        public void SetDropData(PointerEventData eventData)
        {
            _dragAndDropModule.OnDrop(eventData);
        }

        private void RemoveFromList(ItemModel itemModel)
        {
            if (Items.Contains(itemModel))
            {
                Items.Remove(itemModel);
            }
            else
            {
                foreach (var listModel in _dataStorage.Lists)
                {
                    if (this == listModel) continue;
                    listModel.RemoveFromList(itemModel);
                    listModel.OnUpdateListCount?.Invoke(listModel.Items.Count);
                }
            }
        }

        private void UpdateList()
        {
            foreach (var itemModel in Items)
            {
                _clearModule.ClearView(itemModel);
                _spawnModule.Spawn(itemModel);
                OnUpdateListCount?.Invoke(Items.Count);
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