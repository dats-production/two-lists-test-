using System.Collections.Generic;
using System.Linq;
using UI.Models;

namespace Modules.SaveLoad.Data
{
    public static class DataExtensions
    {
        public static ItemData AsItemData(this ItemModel listModel)
        {
            return new ItemData(listModel.StringValue, listModel.IntValue);
        }
    }
}