using System;
using System.Collections.Generic;

namespace Modules.SaveLoad.Data
{
    [Serializable]
    public class ListsData
    {
        public List<ListData> Lists;
        public ListsData(List<ListData> lists)
        {
            Lists = lists;
        }
    }
}