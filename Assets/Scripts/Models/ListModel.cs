using System.Collections.Generic;

namespace Models
{
    public interface IListModel
    {
        public List<ItemModel> ModelsList { get; set; }
    }
    
    public class ListModel
    {
        public List<ItemModel> ModelsList { get; set; }
    }
}