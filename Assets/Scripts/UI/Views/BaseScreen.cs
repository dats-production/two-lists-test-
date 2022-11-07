namespace UI.Views
{
    public interface IBaseScreen
    {
        bool IsShown { get; }
        void Show();
        void Hide();
    }
    
    public abstract class BaseScreen: LinkableView, IBaseScreen
    {
        public bool IsShown { get; private set; }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            IsShown = true;
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            IsShown = false;
        }
    }
}