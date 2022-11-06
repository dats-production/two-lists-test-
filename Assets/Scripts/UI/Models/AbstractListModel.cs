using System.Collections.Generic;

namespace UI.Models
{
    public abstract class AbstractListModel: AbstractModel
    {
        public List<ItemModel> ItemList { get; set; }
        public int ItemCount { get; set; }
    }
}