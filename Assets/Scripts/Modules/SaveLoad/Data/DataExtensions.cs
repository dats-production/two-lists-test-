using System.Linq;
using UI.Models;
using Zenject;

namespace Modules.SaveLoad.Data
{
    public static class DataExtensions
    {
        public static ListData AsListData(this ListModel listModel) 
        {
            return new ListData(
                listModel.Name,
                listModel.Items.Select(x => x.AsItemData()).ToList(), 
                listModel.IsSortingPanelActive);
        }

        public static ListModel ToListModel(this ListData data, DiContainer diContainer)
        {
            return diContainer.Instantiate<ListModel>(new object[]
            {
                data.Name,
                data.Items.Select(x => x.ToItemModel(diContainer)).ToList(),
                data.IsSortingPanelActive
            });
        }

        private static ItemData AsItemData(this ItemModel itemModel)
        {
            return new ItemData(itemModel.StringValue, itemModel.IntValue);
        }
        
        private static ItemModel ToItemModel(this ItemData data, DiContainer diContainer)
        {
            return diContainer.Instantiate<ItemModel>(new object[]
            {
                data.StringValue,
                data.IntValue
            });
        }
    }
}