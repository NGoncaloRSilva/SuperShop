using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public interface IGenericrepository <T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(int id);

        //Gravar é mais complexo
    }
}
