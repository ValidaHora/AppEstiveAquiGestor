namespace EstiveAqui.Repository.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IBaseRepository<T> where T : class
    {
        T Find(int id);
        int DeleteAll();
        void Save(T item);
        int Remove(int id);
        void Update(T item);
        IEnumerable<T> Find();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    }
}