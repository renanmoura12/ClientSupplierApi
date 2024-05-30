using ClientSupplierApi.Models;
using ClientSupplierApi.Response;

namespace ClientSupplierApi.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        Task<bool> UserExistAsync(string email, string userName);
        Task<bool> CustomerSupplierExistAsync(string name, string cpfCnpj);
        Task<Users> GetEmailByName(string name);
        Task<bool> LoginAsync(string name, string password);
        Task<Customer_supplier> GetCustomerSupplier(int id);
        Task<IEnumerable<UsersResponse>> GetAllUsuariosAsync();
        Task<IEnumerable<CustomerSupplierResponse>> GetFiltersCustomerSupplier(string? cpfCnpj, int? type, string? name, string? country, string? city, string? email, int? id, int pageNumber = 1, int pageSize = 10);


    }
}
