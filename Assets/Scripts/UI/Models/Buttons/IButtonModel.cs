using System;
using UniRx;

namespace UI.Models.Buttons
{
    public interface IButtonModel
    {
        IReactiveCommand<Unit> Click { get; }
    }

    public class ButtonModel : IButtonModel, IDisposable
    {
        public IReactiveCommand<Unit> Click => _click;

        private readonly ReactiveCommand _click = new();

        public void Dispose()
        {
            _click?.Dispose();
        }
    }
}