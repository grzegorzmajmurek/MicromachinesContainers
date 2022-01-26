using UserService.Dtos;

namespace UserService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewOrder(OrderPublishedDto orderPublishedDto);
    }
}
