using UI.Models;
using UnityEngine;

namespace UI.Views
{
    public interface ILinkable
    {
        Transform Transform { get; }
        void Link(AbstractModel entity);
        void Destroy();
    }
    
    public abstract class LinkableView : MonoBehaviour, ILinkable
    {
        public AbstractModel Model;
        public Transform Transform => transform;

        public virtual void Link(AbstractModel model)
        {
            Model = model;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}