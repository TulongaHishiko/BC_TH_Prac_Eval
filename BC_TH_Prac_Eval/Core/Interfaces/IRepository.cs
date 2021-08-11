using BC_TH_Prac_Eval.Domain.Abstract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC_TH_Prac_Eval.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetSingle(int Id);
        Task Add(T entity);
        Task Delete(int id);
    }
}
