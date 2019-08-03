using codeBank.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace codeBank.Data
    {
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : bbBase
        {
        private readonly bbAppContext _db = new bbAppContext();

        public int Add(T obj)
            {
            _db.Set<T>().Add(obj);
            return _db.SaveChanges();
            }

        public int Count()
            {
            return _db.Set<T>().Count();
            }

        public int Delete(int id)
            {
            var obj = Get(id);
            _db.Set<T>().Remove(obj);
            return _db.SaveChanges();

            }

        public T Get(Expression<Func<T, bool>> expression)
            {
            return _db.Set<T>().FirstOrDefault(expression);
            }

        public T Get(int id)
            {
            return _db.Set<T>().FirstOrDefault(x=>x.Id == id);
            }

        public List<T> GetMany(Expression<Func<T, bool>> expression)
            {
            return _db.Set<T>().Where(expression).ToList();
            }

        public List<T> List()
            {
            return _db.Set<T>().ToList();
            }

        public int Update(T obj)
            {
            _db.Set<T>().AddOrUpdate(obj);
            return _db.SaveChanges();
            }
        }
    }
