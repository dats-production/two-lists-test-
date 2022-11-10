using UnityEngine;

namespace UI.Views
{
    public class MainScreen : BaseScreen
    {
        [SerializeField] private Transform listsContainer;

        public Transform ListsContainer => listsContainer;
    }
}