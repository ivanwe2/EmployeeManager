using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{

    public class Repository<T> : IRepository<T> where T : class 
        //allows me to choose which class type later
    {
        /// <summary>
        /// implement only CRUD operations for db 
        /// </summary>
        protected EmployeeManagerAssingmentDbContext db;
        protected DbSet<T> dbSet;

        public Repository()
        {
            db = new EmployeeManagerAssingmentDbContext();
            dbSet = db.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(Guid Id)
        {
            if (dbSet.Find(Id) == null)
            {
                throw new ArgumentException("object to be found is null");
            }
            else
            {
                return dbSet.Find(Id);//to be fixed
            }
        }

        public void Insert(T obj)
        {
            dbSet.Add(obj);
            db.SaveChanges();
        }
        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(Guid Id)
        {
            T? getObjById = dbSet.Find(Id);
            if(getObjById==null)
            {
                throw new ArgumentException("object to be deleted is null");
            }
            dbSet.Remove(getObjById);
            db.SaveChanges();
        }
       


    }
}
