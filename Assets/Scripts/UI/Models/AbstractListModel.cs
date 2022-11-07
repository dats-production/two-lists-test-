using System;
using System.Collections.Generic;
using Modules;
using UI.Views;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UI.Models
{
    public abstract class AbstractListModel: AbstractModel
    {
        public ReactiveCollection<ItemModel> ItemList { get; set; } = new();
        public int StartItemCount { get; set; }

        public Action<ItemModel> OnInsert;
        private IListChangerModule _listChangerModule;
        private FirstListModel _firstListModel;
        private SecondListModel _secondListModel;

        [Inject]
        public void Construct(IListChangerModule listChangerModule, FirstListModel firstListModel,
            SecondListModel secondListModel)
        {
            _secondListModel = secondListModel;
            _firstListModel = firstListModel;
            _listChangerModule = listChangerModule;
        }
        public void InsertNewItem(int index, ItemModel itemModel)
        {
            DecideToRemoveFrom(itemModel);
            if(index <= ItemList.Count)
                ItemList.Insert(index, itemModel);
            else
                ItemList.Add(itemModel);
        }

        private void DecideToRemoveFrom(ItemModel itemModel)
        {
            if (ItemList.Contains(itemModel)) ItemList.Remove(itemModel);
            else RemoveFromOtherList(itemModel);
        }

        private void RemoveFromOtherList(ItemModel itemModel)
        {
            if (this is FirstListModel)
                _secondListModel.ItemList.Remove(itemModel);
            else
                _firstListModel.ItemList.Remove(itemModel);
        }
    }
}