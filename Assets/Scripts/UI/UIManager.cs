using UI.Views;

namespace UI
{
    public class UIManager
    {
        public void OpenScreen<TScreen>(TScreen screen) where TScreen : BaseScreen
        {
            screen.Show();
        }
        public void HideScreen<TScreen>(TScreen screen) where TScreen : BaseScreen
        {
            screen.Hide();
        }
    }
}