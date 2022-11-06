using UI.Views;
using UnityEngine;

namespace UI.Models
{
    public abstract class AbstractModel
    {
        public string Name { get; set; }
        public Transform ParentTransform { get; set; }
        public ILinkable View { get; set; }
    }
}