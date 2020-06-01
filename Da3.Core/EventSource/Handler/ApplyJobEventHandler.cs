using System.Threading.Tasks;
using Da3.Core.EventSource.Event;
using JKang.EventBus;

namespace Da3.Core.EventSource.Handler
{
    public class ApplyJobEventHandler : IEventHandler<ApplyJobEvent>
    {
        public Task HandleEventAsync(ApplyJobEvent @event)
        {
            // send email to employer.
            return Task.FromResult(0);
        }
    }
}