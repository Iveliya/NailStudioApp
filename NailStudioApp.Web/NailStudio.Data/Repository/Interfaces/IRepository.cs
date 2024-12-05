using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudio.Data.Repository.Interfaces
{
    public interface IRepository<TType,TId>
    {
        IEnumerable<TType> GetAll();
        Task<IEnumerable<TType>> GetAllAsync();

        TType GetById(TId id);
        Task<TType> GetByIdAsync(TId id);

        IQueryable<TType> GetAllAttached();

        void Add(TType item);   
        Task AddAsync(TType item);

        bool Update(TType item);    
        Task<bool> UpdateAsync(TType item);

        bool Delete(TId id);
        Task<bool> DeleteAsync(TId id);
    }
}
