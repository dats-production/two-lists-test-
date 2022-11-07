using Modules;
using TMPro;
using UI.Models;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Views
{
    public class ItemListView : LinkableView, IDropHandler
    {
        [SerializeField] private Transform itemContainer;
        [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
        [SerializeField] private TMP_Text listNameText;
        [SerializeField] private TMP_Text itemsCountText;
        [SerializeField] private Toggle stringSortToggle;
        [SerializeField] private Toggle intSortToggle;
        private Text testText;
        
        private IListGenerator _listGenerator;
        private AbstractListModel _listModel;

        private int _count;

        public Transform ItemContainer => itemContainer;
        private float _spacingOffset;

        public override void Link(AbstractModel model)
        {
            base.Link(model);
            
            _listModel = Model as AbstractListModel;
            
            _spacingOffset = verticalLayoutGroup.spacing;

            listNameText.text = _listModel.Name;
            itemsCountText.text = _listModel.StartItemCount.ToString();
            
            _listModel.ItemList
                .ObserveEveryValueChanged(x=> x.Count)
                .Subscribe(SetCountText)
                .AddTo(this);

            stringSortToggle.OnValueChangedAsObservable()
                .Subscribe(ToggleStingSorting)
                .AddTo(this);

            intSortToggle.OnValueChangedAsObservable()
                .Subscribe(ToggleIntSorting)
                .AddTo(this);
        }

        private void SetCountText(int count) =>
            itemsCountText.text = count.ToString();

        public void OnDrop(PointerEventData eventData)
        {
            if(!eventData.pointerDrag.TryGetComponent(out ItemView itemView)) return;

            var newDropSiblingIndex = GetNewDropSiblingIndex(eventData);
            
            // var dragItemTransform = eventData.pointerDrag.transform;
            // dragItemTransform.SetParent(ItemContainer);
            // dragItemTransform.SetSiblingIndex(newDropSiblingIndex);
            //Debug.Log(newDropSiblingIndex);

            var dragItemView = eventData.pointerDrag.GetComponent<ItemView>();
            var dragItemModel = dragItemView.Model as ItemModel;
            
            _listModel.InsertItem(newDropSiblingIndex, dragItemModel);
        }
        
        private int GetNewDropSiblingIndex(PointerEventData eventData)
        {
            var pointerDragRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
            var cellSize = pointerDragRectTransform.rect.height;
            var dropPosY = pointerDragRectTransform.localPosition.y;
            var dropLocalPosY = dropPosY - ItemContainer.localPosition.y;
            
            var offsetBetweenItems = cellSize + _spacingOffset;
            return (int)((cellSize / 2 - dropLocalPosY) / offsetBetweenItems);
        }

        private void ToggleStingSorting(bool isOn) 
        {
            _listModel.SortByString(isOn);
        }

        private void ToggleIntSorting(bool isOn) 
        {
            _listModel.SortByInt(isOn);
        }
    }
}