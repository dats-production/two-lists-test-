using Modules;
using UI.Models;
using UnityEngine;
using Zenject;

namespace UI.Views
{
    public class ItemListView : LinkableView
    {
        [SerializeField] private Transform itemContainer;
        private IListGenerator _listGenerator;

        public Transform ItemContainer => itemContainer;
    }
}