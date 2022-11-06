using TMPro;
using UI.Models;
using UnityEngine;

namespace UI.Views
{
    public class ItemView : LinkableView
    {
        [SerializeField] private TMP_Text stringText;
        [SerializeField] private TMP_Text intText;
        
        public override void Link(AbstractModel model)
        {
            base.Link(model);

            var itemModel = model as ItemModel;
            stringText.text = itemModel.StringProperty;
            intText.text = itemModel.IntProperty.ToString();
        }
    }
}