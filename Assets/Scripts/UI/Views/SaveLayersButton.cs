using Modules;
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
        public void Construct(IFileBrowser fileBrowser, 
            ISaveLoadModule saveLoadModule)
        {
            var model = new SaveLayersButtonModel(fileBrowser, saveLoadModule);
            model.Button.Subscribe(button.Bind).AddTo(this);
        }
    }
}