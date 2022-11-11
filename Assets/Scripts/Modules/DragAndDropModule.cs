using UI.Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules
{
    public interface IDragAndDropModule
    {
        void SetDraggedItem(ItemModel itemModel);
        void SetListToDrop(ListModel listModel);
        void OnDrop(PointerEventData eventData);
    }
    
    public class DragAndDropModule : IDragAndDropModule
    {
        private ItemModel _draggedItem;
        private ListModel _listToDrop;
        private PointerEventData _eventData;
        
        public void SetDraggedItem(ItemModel itemModel)
        {
            _draggedItem = itemModel;
        }

        public void SetListToDrop(ListModel listModel)
        {
            _listToDrop = listModel;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var newDropSiblingIndex = GetNewDropSiblingIndex(eventData);
            
            _listToDrop.InsertItem(newDropSiblingIndex, _draggedItem);
        }

        private int GetNewDropSiblingIndex(PointerEventData eventData)
        {
            var pointerDragRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
            var dropPosY = pointerDragRectTransform.localPosition.y;
            
            var cellSize = _draggedItem.ItemHeight;
            var dropLocalPosY = dropPosY - _listToDrop.ItemContainer.localPosition.y;
            var offsetBetweenItems = cellSize + _listToDrop.SpacingOffset;
            var index = 0;
            if (_listToDrop.Items.Contains(_draggedItem))
            {
                if(eventData.position.y > eventData.pressPosition.y)
                {
                    index = (int)((cellSize / 2 - dropLocalPosY) / (offsetBetweenItems));
                }
                else
                {
                    index = (int)((cellSize / 2 - dropLocalPosY) / (offsetBetweenItems)) - 1;
                }
            }
            else
            {
                if (_listToDrop.IsSortingPanelActive)
                {
                    dropLocalPosY += 35;
                }
                else
                {
                    dropLocalPosY -= 35;
                }
                index = (int)((cellSize / 2 - dropLocalPosY) / (offsetBetweenItems));
            }
            return index;
        }
    }
}