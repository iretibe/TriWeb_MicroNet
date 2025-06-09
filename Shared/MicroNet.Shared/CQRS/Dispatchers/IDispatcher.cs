using MicroNet.Shared.CQRS.Dispatchers.Commands;
using MicroNet.Shared.CQRS.Dispatchers.Queries;

namespace MicroNet.Shared.CQRS.Dispatchers
{
    public interface IDispatcher : ICommandDispatcher, IQueryDispatcher
    {
    }
}
