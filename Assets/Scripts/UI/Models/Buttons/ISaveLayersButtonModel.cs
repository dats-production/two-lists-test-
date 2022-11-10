using System;
using System.Linq;
using Modules;
using Modules.SaveLoad;
using Modules.SaveLoad.Data;
using UniRx;
using UnityEngine;

namespace UI.Models.Buttons
{
    public interface ISaveLayersButtonModel
    {
        IObservable<IButtonModel> Button { get; }
    }

    public class SaveLayersButtonModel : ISaveLayersButtonModel
    {
        private readonly IFileBrowser fileBrowser;
        private readonly ISaveLoadModule _saveLoadModule;
        private FirstListModel _firstListModel;
        private SecondListModel _secondListModel;

        public SaveLayersButtonModel(IFileBrowser fileBrowser, ISaveLoadModule saveLoadModule,
            FirstListModel firstListModel, SecondListModel secondListModel)
        {
            _secondListModel = secondListModel;
            _firstListModel = firstListModel;
            this.fileBrowser = fileBrowser;
            this._saveLoadModule = saveLoadModule;
        }
        
        public IObservable<IButtonModel> Button =>
            Observable.Create<IButtonModel>(observer =>
            {
                var disposable = new CompositeDisposable();
                var model = new ButtonModel();
                model.AddTo(disposable);
                model.Click.Subscribe(_ =>
                {
                    Debug.Log("Save");
                    var firstListData = _firstListModel.ItemsList.Select(x => x.AsItemData()).ToList();
                    // var layers = dataStorage.Layers.Select(x => x.AsLayerData()).ToList();
                    // var path = fileBrowser.SaveFilePanel("Save file", null, null, null);
                    // var data = new LayersData(layers);
                    // saveLoadService.SaveLayers(data, path.Name);
                }).AddTo(disposable);
                observer.OnNext(model);
                return disposable;
            });
    }
}