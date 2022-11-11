using System.Collections.Generic;
using UI.Models;
using UniRx;

namespace Modules.DataStorage
{
    public interface IDataStorage
    {
        IReadOnlyReactiveCollection<ListModel> Lists { get; }
        void AddList(ListModel listModel);
        void FillLists(IEnumerable<ListModel> lists);
    }
    
    public class DataStorage : IDataStorage
    {
        public IReadOnlyReactiveCollection<ListModel> Lists => _lists;
        
        private readonly ReactiveCollection<ListModel> _lists = new();

        public void AddList(ListModel listModel)
        {
            _lists.Add(listModel);
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