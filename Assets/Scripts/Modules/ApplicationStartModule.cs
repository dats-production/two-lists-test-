using UI.Models;
using UI.Views;
using Zenject;

namespace Modules
{
    public class ApplicationStartModule
    {
        private ISpawnModule<AbstractModel> _spawnModule;
        private FirstListModel _firstListModel;
        private SecondListModel _secondListModel;
        private MainScreen _mainScreen;
        private IListGenerator _listGenerator;
        private int _firstListItemCount = 15;
        private int _secondListItemCount = 12;

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
            _firstListModel.Name = "First List";
            _firstListModel.PrefabName = "ItemList";
            _firstListModel.StartItemCount = _firstListItemCount;
            _firstListModel.ParentTransform = _mainScreen.transform;
            _spawnModule.Spawn(_firstListModel);
            _listGenerator.CreateList(_firstListModel);
            _firstListModel.IsSortingPanelActive.Value = true;
            
            _secondListModel.Name = "Second List";
            _secondListModel.PrefabName = "ItemList";
            _secondListModel.StartItemCount = _secondListItemCount;
            _secondListModel.ParentTransform = _mainScreen.transform;
            _spawnModule.Spawn(_secondListModel);
            _listGenerator.CreateList(_secondListModel);
            _secondListModel.IsSortingPanelActive.Value = false;
        }
    }
}