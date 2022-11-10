using System;
using System.Collections.Generic;

namespace Modules.SaveLoad.Data
{
    [Serializable]
    public class ListsData
    {
        public List<ItemData> Lists;
        public ListsData(List<ItemData> lists)
        {
            Lists = lists;
        }
    }
}