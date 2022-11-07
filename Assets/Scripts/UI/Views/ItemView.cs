using TMPro;
using UI.Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Views
{
    public class ItemView : LinkableView, IBeginDragHandler,
        IDragHandler,IEndDragHandler
    {
        [SerializeField] private TMP_Text stringText;
        [SerializeField] private TMP_Text intText;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Canvas _canvasComponent;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private Vector3 _beginDragPosition;

        public override void Link(AbstractModel model)
        {
            base.Link(model);

            var itemModel = model as ItemModel;
            stringText.text = itemModel.StringProperty;
            intText.text = itemModel.IntProperty.ToString();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _beginDragPosition = rectTransform.anchoredPosition;
            
            _canvasComponent.overrideSorting = true;
            _canvasComponent.sortingOrder = 999;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = _beginDragPosition;
            _canvasComponent.sortingOrder = 1;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}