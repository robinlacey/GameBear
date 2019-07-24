using System.Threading;
using System.Threading.Tasks;
using DealerBear.Adaptor.Interface;

namespace GameBearTests.Mocks
{
    public class PublishEndPointDummy : IPublishMessageAdaptor
    {
        public Task Publish<T>(T message, CancellationToken cancellationToken = new CancellationToken()) where T : class
        {
            return null;
        }
    }
}