using SuperShop.Data.Ententies;
using SuperShop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public interface IOrderRepository : IGenericrepository<Order>
    {
        Task<IQueryable<Order>> GetOrderAsync(string userName);

        Task<IQueryable<OrderDetailTemp>> GetDetailsTempsAsync(string userName);

        Task AddItemToOrderAsync(AddItemViewModel model,string userName);

        Task ModifyOrderDetailTempQuantity(int id, double quantity);

        Task DeleteDetailtempAsync(int id);

        Task<bool> ConfirmOrderAsync(string username);


        Task DeliveryOrder(DeliveryViewModel model);

        Task<Order> GetOrderAsync(int id);  
    }
}
