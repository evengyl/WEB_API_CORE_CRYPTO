using System.Collections.Generic;

namespace DAL.IRepositories
{
    public interface IRepository<TEntity>
    {
        List<TEntity> GetAll();

        TEntity Get(int id);

        TEntity GetBy(string row, string value);

        void Create(TEntity t);

        void Delete(int id);

        void Update(TEntity t);
    }
}
