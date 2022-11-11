using System;
using System.Linq;
using Modules;
using Modules.DataStorage;
using Modules.SaveLoad;
using Modules.SaveLoad.Data;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Models.Buttons
{
    public interface ILoadLayersButtonModel
    {
        IObservable<IButtonModel> Button { get; }
    }

    public class LoadLayersButtonModel : ILoadLayersButtonModel
    {
        private readonly ISaveLoadModule _saveLoadModule;
        private IDataStorage _dataStorage;
        private DiContainer _diContainer;

        public LoadLayersButtonModel(ISaveLoadModule saveLoadModule,
            IDataStorage dataStorage, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _dataStorage = dataStorage;
            _saveLoadModule = saveLoadModule;
        }
        
        public IObservable<IButtonModel> Button =>
            Observable.Create<IButtonModel>(observer =>
            {
                var disposable = new CompositeDisposable();
                var model = new ButtonModel();
                model.AddTo(disposable);
                model.Click.Subscribe(_ =>
                {
                    var data = _saveLoadModule.Load();
                    var layers = data.Lists.Select(x => x.ToListModel(_diContainer)).ToList();
                    _dataStorage.FillLists(layers);
                }).AddTo(disposable);
                observer.OnNext(model);
                return disposable;
            });
    }
}