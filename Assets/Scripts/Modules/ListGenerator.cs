using UI.Models;
using UI.Views;
using Zenject;
using Random = UnityEngine.Random;

namespace Modules
{
    public interface IListGenerator
    {
        void CreateList(AbstractListModel abstractListModel);
    }
    
    public class ListGenerator : IListGenerator
    {
        const string glyphs= "abcdefghijklmnopqrstuvwxyz";
        
        private readonly int _minItemIntValue = 0;
        private readonly int _maxItemIntValue = 100;
        private readonly int _minCharAmount = 1;
        private readonly int _maxCharAmount = 10;
        
        private ISpawnModule<AbstractModel> _spawnModule;

        [Inject]
        public void Construct(ISpawnModule<AbstractModel> spawnModule)
        {
            _spawnModule = spawnModule;
        }
        
        public void CreateList(AbstractListModel abstractListModel)
        {
            var listView = abstractListModel.View as ItemListView;
            for (var i = 0; i < abstractListModel.StartItemCount; i++)
            {
                var itemModel = new ItemModel()
                {
                    IntProperty = GetRandomInt(),
                    StringProperty = GetRandomString()
                };
                abstractListModel.ItemList.Add(itemModel);
                
                itemModel.PrefabName = "Item";
                itemModel.ParentTransform = listView.ItemContainer;
                _spawnModule.Spawn(itemModel);
            }
        }

        private int GetRandomInt()
        {
            return Random.Range(_minItemIntValue, _maxItemIntValue);
        }

        private string GetRandomString()
        {
            var charAmount = Random.Range(_minCharAmount, _maxCharAmount);
            var myString = "";
            for(int i=0; i<charAmount; i++)
            {
                myString += glyphs[Random.Range(0, glyphs.Length)];
            }
            return myString;
        }
    }
}