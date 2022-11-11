using TMPro;
using UI.Models;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Views
{
    public class ListView : LinkableView, IDropHandler
    {
        [SerializeField] private Transform itemContainer;
        [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
        [SerializeField] private TMP_Text listNameText;
        [SerializeField] private TMP_Text itemsCountText;
        [SerializeField] private Toggle stringSortToggle;
        [SerializeField] private Toggle intSortToggle;
        [SerializeField] private Transform sortingPanel;
        [SerializeField] private Scrollbar scrollbar;

        private ListModel _listModel;
        private float _spacingOffset;
        private float _sortingPanelHeight;

        public Transform ItemContainer => itemContainer;

        public override void Link(AbstractModel model)
        {
            base.Link(model);
            _spacingOffset = verticalLayoutGroup.spacing;
            _listModel = Model as ListModel;
            listNameText.text = _listModel.Name;

            _listModel.OnUpdateListCount += SetCountText;

            _listModel.SetContainerTransform(ItemContainer);
            _listModel.OnListGenerated += AlignScrollView;
            _listModel.OnListGenerated += () =>
            {
                ToggleSortingPanel(_listModel.IsSortingPanelActive);
                stringSortToggle.OnValueChangedAsObservable()
                    .Subscribe(ToggleStingSorting)
                    .AddTo(this);

                intSortToggle.OnValueChangedAsObservable()
                    .Subscribe(ToggleIntSorting)
                    .AddTo(this);
            };

            _sortingPanelHeight = sortingPanel.gameObject.GetComponent<RectTransform>().rect.height;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if(!eventData.pointerDrag.TryGetComponent(out ItemView itemView)) return;

            var newDropSiblingIndex = GetNewDropSiblingIndex(eventData);
            var dragItemView = eventData.pointerDrag.GetComponent<ItemView>();
            var dragItemModel = dragItemView.Model as ItemModel;
            
            _listModel.InsertItem(newDropSiblingIndex, dragItemModel);
        }
        
        private int GetNewDropSiblingIndex(PointerEventData eventData)
        {
            var pointerDragRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
            var cellSize = pointerDragRectTransform.rect.height;
            var dropPosY = pointerDragRectTransform.localPosition.y;
            var dropLocalPosY = 0f;
            // if (!_listModel.IsSortingPanelActive)
            //     dropLocalPosY = dropPosY - ItemContainer.localPosition.y ;
            // else
                dropLocalPosY = dropPosY - ItemContainer.localPosition.y + _sortingPanelHeight;
            var offsetBetweenItems = cellSize + _spacingOffset;
            return (int)((cellSize / 2 - dropLocalPosY) / offsetBetweenItems);
        }

        private void SetCountText(int count) =>
            itemsCountText.text = count.ToString();

        private void AlignScrollView() =>
            scrollbar.value = 1;

        private void ToggleStingSorting(bool isOn) =>
            _listModel.SortByString(isOn);

        private void ToggleIntSorting(bool isOn) =>
            _listModel.SortByInt(isOn);

        private void ToggleSortingPanel(bool isOn) =>
            sortingPanel.gameObject.SetActive(isOn);
    }
}