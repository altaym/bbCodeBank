using codeBank.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace codeBank.Data
    {
    public interface IGenericRepository<T> where T : bbBase
        {
        List<T> List();
        List<T> GetMany(Expression<Func<T, bool>> expression);
        T Get(int id);
        T Get(Expression<Func<T, bool>> expression);
        int Add(T obj);
        int Update(T obj);
        int Delete(int id);
        int Count();
        }
    }
