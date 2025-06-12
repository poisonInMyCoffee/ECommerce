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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private ApplicationDbContext _db;

        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
      
        public new void Update(ShoppingCart obj) //Keep this update in mind
        {
            _db.ShoppingCarts.Update(obj);
        }
    }
}
