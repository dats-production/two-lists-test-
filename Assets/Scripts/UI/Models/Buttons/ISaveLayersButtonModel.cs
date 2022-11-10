using System;
using Modules;
using Modules.SaveLoad;
using UniRx;

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
        
        public SaveLayersButtonModel(IFileBrowser fileBrowser,
            ISaveLoadModule saveLoadModule)
        {
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