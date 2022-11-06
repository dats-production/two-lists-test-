using Modules.Fabrics;
using UI.Models;
using UI.Views;
using Zenject;
namespace Modules
{
    public interface ISpawnModule<in TModel>
    {
        void Spawn(TModel model);
    }
    
    public class SpawnModule: ISpawnModule<AbstractModel>
    {
        private IInstantiateFabric<AbstractModel, ILinkable> _instantiateFabric;

        [Inject]
        public void Construct(IInstantiateFabric<AbstractModel, ILinkable> instantiateFabric)
        {
            _instantiateFabric = instantiateFabric;
        }
        
        public void Spawn(AbstractModel model)
        {
            var linkable = _instantiateFabric.Instantiate(model);
            linkable?.Link(model);
            model.View = linkable;
        }
    }
}