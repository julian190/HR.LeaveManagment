using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.Contracts.Presistance
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Exists(int Id);
        Task <T> Add(T entity);
        Task Update(T entity);
        Task Delete (T entity);
    }
}
