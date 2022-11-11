using System;
using System.Linq;
using Modules;
using Modules.DataStorage;
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
        private readonly ISaveLoadModule _saveLoadModule;
        private readonly IDataStorage _dataStorage;

        public SaveLayersButtonModel(ISaveLoadModule saveLoadModule,
            IDataStorage dataStorage)
        {
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
                    var lists = _dataStorage.Lists.Select(x => x.AsListData()).ToList();
                    var data = new ListsData(lists);
                    _saveLoadModule.Save(data);
                }).AddTo(disposable);
                observer.OnNext(model);
                return disposable;
            });
    }
}