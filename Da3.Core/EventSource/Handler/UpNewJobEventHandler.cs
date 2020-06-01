using System.Threading.Tasks;
using Da3.Core.EventSource.Event;
using JKang.EventBus;

namespace Da3.Core.EventSource.Handler
{
    public class UpNewJobEventHandler : IEventHandler<UpNewJobEvent>
    {
        public Task HandleEventAsync(UpNewJobEvent @event)
        {
            // send email to employee that follow that employee
            return Task.CompletedTask;
        }
    }
}