using System;
using UI.Views;
using UnityEngine;

namespace UI.Models
{
    public abstract class AbstractModel
    {
        public string Name { get; set; }
        public string PrefabName { get; set; }
        public ILinkable View { get; set; }
        public Transform ParentTransform { get; set; }
        public Action OnClearView { get; set; }
    }
}