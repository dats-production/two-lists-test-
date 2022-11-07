using UI.Models;

namespace Modules
{
    public interface IClearModule<in TModel>  
        where TModel : AbstractModel
    {
        void ClearView(TModel model);
    }
    
    public class ClearModule<TModel> : IClearModule<TModel>
        where TModel : AbstractModel
    {
        public void ClearView(TModel model)
        {
            model.View.Destroy();
        }
    }
}