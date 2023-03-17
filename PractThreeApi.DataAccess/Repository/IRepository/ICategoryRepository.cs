using PractThreeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractThreeApi.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        void Update(Category obj);
    }
}
