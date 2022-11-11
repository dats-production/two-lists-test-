using Modules;
using UnityEngine;
using Zenject;

namespace UI.Models
{
    public class ItemModel : AbstractModel
    {
        private IDragAndDropModule _dragAndDropModule;
        public string StringValue { get; }
        public int IntValue { get; }
        public float ItemHeight { get; set; }

        public ItemModel(string stringValue, int intValue)
        {
            StringValue = stringValue;
            IntValue = intValue;
        }

        [Inject]
        public void Construct(IDragAndDropModule dragAndDropModule)
        {
            _dragAndDropModule = dragAndDropModule;
        }

        public void SetDraggedItem(bool isDragged)
        {
            _dragAndDropModule.SetDraggedItem(isDragged ? this : null);
        }
    }
}