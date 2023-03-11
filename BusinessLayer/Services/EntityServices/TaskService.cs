using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.EntityServices
{
    public class TaskService : EntityService<TaskWork>
    {
        public TaskService(IRepository<TaskWork> repository) : base(repository)
        {
        }
    }
    
}
