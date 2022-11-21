
namespace Store_Web_API.Repositories.Interfaces
{
    public interface IRepository
    {
        T Add<T>(T item) where T:class;
    }
}
