using BulkyBook.DataAccess;
using BulkyBook.Models;
using BulkyBookWeb.Repository.IRepository;

namespace BulkyBookWeb.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }       

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
