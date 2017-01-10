namespace EstiveAqui.Repository.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IBaseAsyncRepository<T> where T : ApiSerialize.BaseModel
    {
        Task<int> Remove(int id);
        Task<T> FindAsync(int id);
        Task<int> SaveAsync(T item);
        Task<IEnumerable<T>> FindAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
    }
}