using System;
using Modules;
using Modules.SaveLoad;
using UniRx;

namespace UI.Models.Buttons
{
    public interface ILoadLayersButtonModel
    {
        IObservable<IButtonModel> Button { get; }
    }

    public class LoadLayersButtonModel : ILoadLayersButtonModel
    {
        private readonly IFileBrowser fileBrowser;
        private readonly ISaveLoadModule _saveLoadModule;
        
        public LoadLayersButtonModel(IFileBrowser fileBrowser,
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
                    // var path = fileBrowser.OpenFilePanel("Open file", null, null, false);
                    // var data = saveLoadService.LoadLayers(path.First().Name);
                    // var layers = data.Layers.Select(x => x.ToLayer()).ToList();
                    // dataStorage.FillLayers(layers);
                }).AddTo(disposable);
                observer.OnNext(model);
                return disposable;
            });
    }
}