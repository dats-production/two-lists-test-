using System.Collections.Generic;
using UI.Models;
using UI.Views;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Modules
{
    public interface IListChangerModule
    {
        void RemoveItemFromOtherList<TListModel>(in ItemModel itemModel, in TListModel list) where TListModel : AbstractListModel;
    }
    public class ListChangerModule : IListChangerModule
    {
        [Inject]
        public void Construct()
        {

        }
        public void RemoveItemFromOtherList<TListModel>(in ItemModel itemModel, in TListModel abstractListModel) where TListModel : AbstractListModel
        {

            
        }
    }
}