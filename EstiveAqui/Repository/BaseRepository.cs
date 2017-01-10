namespace EstiveAqui.Repository
{
    using EstiveAqui.Repository.Abstract;
    using SQLite.Net;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Xamarin.Forms;

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly SQLiteConnection _repository;

        public BaseRepository()
        {
            var config = DependencyService.Get<ISQLite>();
            _repository = new SQLiteConnection(config.Platform, System.IO.Path.Combine(config.Path, "estiveaqui.db3"));
            _repository.CreateTable<T>();
        }

        public IEnumerable<T> Find()
        {
            return _repository.Table<T>();
        }

        public T Find(int id)
        {
            return _repository.Find<T>(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _repository.Table<T>().Where(expression);
        }

        public int Remove(int id)
        {
            return _repository.Delete<T>(id);
        }

        public int DeleteAll()
        {
            return _repository.DeleteAll<T>();
        }

        public void Save(T item)
        {
            _repository.Insert(item);
        }

        public void Update(T item)
        {
            _repository.Update(item);
        }
    }
}