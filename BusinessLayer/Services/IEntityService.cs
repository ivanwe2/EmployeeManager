using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BusinessLayer.Services
{
    public interface IEntityService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(Guid Id);
        void Create(T obj);
        void Update(T obj);
        void Delete(Guid Id);
     
    }
}
