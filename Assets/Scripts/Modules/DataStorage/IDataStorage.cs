using System;
using System.Collections.Generic;
using UI.Models;
using UniRx;
using Zenject;

namespace Modules.DataStorage
{
    public interface IDataStorage
    {
        IReadOnlyReactiveCollection<ListModel> Lists { get; }
        IObservable<ListModel> OnListChanged { get; }
        void AddList(ListModel listModel);
        void FillLists(IEnumerable<ListModel> lists);
    }
    
    public class DataStorage : IDataStorage
    {
        public IReadOnlyReactiveCollection<ListModel> Lists => _lists;
        public IObservable<ListModel> OnListChanged => onListChanged;
        
        private ReactiveCollection<ListModel> _lists = new();
        private ReactiveCommand<ListModel> onListChanged = new();
        private IClearModule<ItemModel> _clearModule;

        public void AddList(ListModel listModel)
        {
            _lists.Add(listModel);
        }

        [Inject]
        public void Construct(IClearModule<ItemModel> clearModule)
        {
            _clearModule = clearModule;
        }

        public void FillLists(IEnumerable<ListModel> lists)
        {
            ClearLists();
            foreach (var list in lists) _lists.Add(list);
        }

        private void ClearLists()
        {
            foreach (var list in _lists) list.View.Destroy();
            _lists.Clear();
        }
    }
}