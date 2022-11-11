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

        public override void Link(AbstractModel model)
        {
            base.Link(model);
            _listModel = Model as ListModel;
            listNameText.text = _listModel.Name;
            
            _listModel.OnUpdateListCount += SetCountText;
            _listModel.ItemContainer = itemContainer;
            _listModel.SpacingOffset = verticalLayoutGroup.spacing;
            _listModel.SortingPanelHeight = sortingPanel.gameObject.GetComponent<RectTransform>().rect.height;;
            
            _listModel.SetContainerTransform(itemContainer);
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
        }

        public void OnDrop(PointerEventData eventData)
        {
            _listModel.SetListToDrop();
            _listModel.SetDropData(eventData);
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