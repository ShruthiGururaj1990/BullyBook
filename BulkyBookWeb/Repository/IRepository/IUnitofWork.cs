namespace BulkyBookWeb.Repository.IRepository
{
    public interface IUnitofWork
    {

        ICategoryRepository Category { get; }

        ICoverTypeRepository CoverType { get; }

        IProductRepository Product { get; }

        void Save();
    }
}
