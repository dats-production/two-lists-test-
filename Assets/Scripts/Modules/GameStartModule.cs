using Zenject;

namespace Modules
{
    public interface IGameStartModule
    {
        
    }
    public class GameStartModule : IGameStartModule
    {
        

        [Inject]
        public void Construct()
        {
        }

    }
}