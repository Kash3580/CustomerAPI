using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerAPI.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object Id);
        T GetByEmail(object email);
        T Insert(T obj);
        void Delete(object Id);
        T Update(T obj);
        void Save();
    }
}