using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Ententies;
using SuperShop.Helpers;
using SuperShop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public class OrderRepository : Genericrepository<Order>, IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public OrderRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task<IQueryable<Order>> GetOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserbyEmailAsync(userName);
            if(user == null)
            {
                return null;
            }

            if(await _userHelper.IsUserInRoleAsync(user,"Admin"))
            {
                return _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Product)
                    .OrderByDescending(o => o.OrderDate);
            }

            return _context.Orders
                .Include(o => o.Items)
                .ThenInclude(p => p.Product)
                .Where(o => o.User == user)
                .OrderByDescending(o => o.OrderDate);
        }

        
        public async Task<IQueryable<OrderDetailTemp>> GetDetailsTempsAsync(string userName)
        {
            var user = await _userHelper.GetUserbyEmailAsync(userName);
            if (user == null)
            {
                return null;
            }


            return _context.OrderDetailsTemp
                .Include(p => p.Product)
                .Where(o => o.User == user)
                .OrderByDescending(o => o.Product.Name);
        }

        public async Task AddItemToOrderAsync(AddItemViewModel model, string userName)
        {
            var user = await _userHelper.GetUserbyEmailAsync(userName);

            if(user == null)
            { return; }

            var product = await _context.Products.FindAsync(model.ProductId);

            if(product == null)
            {
                return;
            }

            var orderDetailTemp = await _context.OrderDetailsTemp
                .Where(odt => odt.User == user && odt.Product == product)
                .FirstOrDefaultAsync();

            if(orderDetailTemp == null)
            {
                orderDetailTemp = new OrderDetailTemp
                {
                    Price = product.Price,
                    Product = product,
                    Quantity = model.Quantity,
                    User = user
                };

                _context.OrderDetailsTemp.Add(orderDetailTemp);
            }
            else
            {
                orderDetailTemp.Quantity += model.Quantity;
                _context.OrderDetailsTemp.Update(orderDetailTemp);
            }

            await _context.SaveChangesAsync();
        }


        public async Task ModifyOrderDetailTempQuantity(int id, double quantity)
        {
            var orderDetailTemp = await _context.OrderDetailsTemp.FindAsync(id);
            if(orderDetailTemp == null)
            {
                return;
            }

            orderDetailTemp.Quantity += quantity;
            if(orderDetailTemp.Quantity > 0)
            {
                _context.OrderDetailsTemp.Update(orderDetailTemp);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDetailtempAsync(int id)
        {
            var orderDetailTemp = await _context.OrderDetailsTemp.FindAsync(id);

            if (orderDetailTemp == null)
            {
                return;
            }

            _context.OrderDetailsTemp.Remove(orderDetailTemp);
            await _context.SaveChangesAsync();

        }




        public Task<IQueryable<Order>> GetOrderByUserNameAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task<IQueryable<OrderDetailTemp>> GetDetailsTempsByUserNameAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        
    }
}
