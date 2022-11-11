using System;
using System.Collections.Generic;

namespace Modules.SaveLoad.Data
{
    [Serializable]
    public class ListData
    {
        public string Name;
        public List<ItemData> Items;
        public bool IsSortingPanelActive;
        public ListData(string name, List<ItemData> items, bool isSortingPanelActive)
        {
            Name = name;
            Items = items;
            IsSortingPanelActive = isSortingPanelActive;
        }
    }
}