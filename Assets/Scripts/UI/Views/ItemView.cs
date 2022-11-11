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
        private ItemModel _itemModel;
        private RectTransform _rectTransform;

        public override void Link(AbstractModel model)
        {
            base.Link(model);

            _itemModel = model as ItemModel;
            stringText.text = _itemModel.StringValue;
            intText.text = _itemModel.IntValue.ToString();
            _rectTransform = GetComponent<RectTransform>();
            _itemModel.ItemHeight = _rectTransform.rect.height;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _beginDragPosition = rectTransform.anchoredPosition;
            
            _canvasComponent.overrideSorting = true;
            _canvasComponent.sortingOrder = 999;
            _canvasGroup.blocksRaycasts = false;
            _itemModel.SetDraggedItem(true);
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
            _itemModel.SetDraggedItem(false);
        }
    }
}