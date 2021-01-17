using System.Threading.Tasks;

namespace AppBuscaCep.Core.Command
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
