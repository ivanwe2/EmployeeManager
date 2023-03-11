
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Services
{
    public class EntityService<T> : IEntityService<T> where T : class
    {
        protected readonly IRepository<T> _repository;

        public EntityService(IRepository<T> repository)
        {
            _repository= repository;
        }

        public void Delete(Guid Id)
        {
            try
            {
                _repository.Delete(Id);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message + "Entity service issue");
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message + "Entity service issue");
            }
        }

        public T GetById(Guid Id)
        {
            try
            {
                return _repository.GetById(Id);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message + "Entity service issue");
            }
        }

        public void Create(T obj)
        {
            try
            {
                 _repository.Insert(obj);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message + "Entity service issue");
            }
        }

        public void Update(T obj)
        {
            try
            {
                 _repository.Update(obj);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message + "Entity service issue");
            }
        }
    }
}
