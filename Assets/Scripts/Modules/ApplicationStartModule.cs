using UI.Models;
using UI.Views;
using Zenject;

namespace Modules
{
    public interface IApplicationStartModule
    {
        
    }
    
    public class ApplicationStartModule : IApplicationStartModule
    {
        private ISpawnModule<AbstractModel> _spawnModule;
        private FirstListModel _firstListModel;
        private SecondListModel _secondListModel;
        private MainScreen _mainScreen;
        private IListGenerator _listGenerator;


        [Inject]
        public void Construct(ISpawnModule<AbstractModel> spawnModule, FirstListModel firstListModel,
            SecondListModel secondListModel, MainScreen mainScreen, IListGenerator listGenerator)
        {
            _listGenerator = listGenerator;
            _mainScreen = mainScreen;
            _secondListModel = secondListModel;
            _firstListModel = firstListModel;
            _spawnModule = spawnModule;
            
            SetListModels();
        }

        private void SetListModels()
        {
            _firstListModel.Name = "ItemList";
            _firstListModel.StartItemCount = 5;
            _firstListModel.ParentTransform = _mainScreen.transform;
            _spawnModule.Spawn(_firstListModel);
            _listGenerator.CreateList(_firstListModel);
            
            _secondListModel.Name = "ItemList";
            _secondListModel.StartItemCount = 10;
            _secondListModel.ParentTransform = _mainScreen.transform;
            _spawnModule.Spawn(_secondListModel);
            _listGenerator.CreateList(_secondListModel);
        }
    }
}