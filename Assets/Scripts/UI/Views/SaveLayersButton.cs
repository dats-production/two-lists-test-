using Modules.DataStorage;
using Modules.SaveLoad;
using UI.Models.Buttons;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Views
{
    public class SaveLayersButton : MonoBehaviour
    {
        [SerializeField] private ButtonView button;

        [Inject]
        public void Construct(ISaveLoadModule saveLoadModule, IDataStorage dataStorage)
        {
            var model = new SaveLayersButtonModel(saveLoadModule, dataStorage);
            model.Button.Subscribe(button.Bind).AddTo(this);
        }
    }
}