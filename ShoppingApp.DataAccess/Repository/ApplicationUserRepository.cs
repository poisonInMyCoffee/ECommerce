using Microsoft.EntityFrameworkCore;
using ShoppingApp.DataAccess.Data;
using ShoppingApp.DataAccess.Repository.IRepository;
using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
