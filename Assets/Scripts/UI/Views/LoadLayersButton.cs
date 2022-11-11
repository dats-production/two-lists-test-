using Modules.DataStorage;
using Modules.SaveLoad;
using UI.Models.Buttons;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Views
{
    public class LoadLayersButton : MonoBehaviour
    {
        [SerializeField] private ButtonView button;

        [Inject]
        public void Construct(ISaveLoadModule saveLoadModule, IDataStorage dataStorage,
            DiContainer diContainer)
        {
            var model = new LoadLayersButtonModel(saveLoadModule, dataStorage, diContainer);
            model.Button.Subscribe(button.Bind).AddTo(this);
        }
    }
}