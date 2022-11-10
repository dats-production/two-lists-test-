// using System;
// using System.Linq;
// using Constructor.DataStorage;
// using Services;
// using Services.SaveLoad;
// using Services.SaveLoad.Data;
using UniRx;

namespace UI.Models
{
    public interface ILoadLayersButtonModel
    {
        //IObservable<IButtonModel> Button { get; }
    }

    public class LoadLayersButtonModel : ILoadLayersButtonModel
    {
        // private readonly IDataStorage dataStorage;
        // private readonly IFileBrowser fileBrowser;
        // private readonly ISaveLoadService saveLoadService;
        //
        // public LoadLayersButtonModel(IDataStorage dataStorage, IFileBrowser fileBrowser,
        //     ISaveLoadService saveLoadService)
        // {
        //     this.dataStorage = dataStorage;
        //     this.fileBrowser = fileBrowser;
        //     this.saveLoadService = saveLoadService;
        // }
        //
        // public IObservable<IButtonModel> Button =>
        //     Observable.Create<IButtonModel>(observer =>
        //     {
        //         var disposable = new CompositeDisposable();
        //         var model = new ButtonModel();
        //         model.AddTo(disposable);
        //         model.Click.Subscribe(_ =>
        //         {
        //             var path = fileBrowser.OpenFilePanel("Open file", null, null, false);
        //             var data = saveLoadService.LoadLayers(path.First().Name);
        //             var layers = data.Layers.Select(x => x.ToLayer()).ToList();
        //             dataStorage.FillLayers(layers);
        //         }).AddTo(disposable);
        //         observer.OnNext(model);
        //         return disposable;
        //     });
    }
}