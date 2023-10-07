using KafkaProducer.Api.Requests;

namespace KafkaProducer.Api.Interfaces
{
    public interface IOrderService
    {
        Task<bool> SendOrderRequest(OrderRequest orderRequest);
    }
}
