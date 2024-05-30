using AutoMapper;
using ClientSupplierApi.Enums;
using ClientSupplierApi.Models;
using ClientSupplierApi.Response;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ClientSupplierApi.Data
{
    public class Repository : IRepository
    {
        public DataContext context { get; }
        private readonly IMapper _mapper;

        public Repository(DataContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }

        //==========================Genericas======================
        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await context.SaveChangesAsync() > 0);
        }

        public async Task<bool> UserExistAsync(string email, string userName)
        {
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Email == email || a.UserName == userName);

            if (user == null)
                return false;

            return true;
        }

        public async Task<Users> GetEmailByName(string name)
        {
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.UserName == name);

            if (user == null)
            {
                Users userNull = new();
                return userNull;
            }

            return user;
        }

        public async Task<bool> LoginAsync(string name, string password)
        {
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.UserName == name);

            if (user == null)
                return false;

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int x = 0; x < computedHash.Length; x++)
            {
                if (computedHash[x] != user.PasswordHash[x])
                    return false;
            }

            return true;
        }

        public async Task<IEnumerable<UsersResponse>> GetAllUsuariosAsync()
        {
            var users = await context.Users
                .AsNoTracking()
                .ToListAsync();

            var objMapeado = _mapper
                .Map<IEnumerable<UsersResponse>>(users);

            return objMapeado;
        }

        public async Task<bool> CustomerSupplierExistAsync(string name, string cpfCnpj)
        {
            var customerSupplier = await context.Customer_Supplier
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Name == name || a.CpfCnpj == cpfCnpj);

            if (customerSupplier == null)
                return false;

            return true;
        }

        public async Task<Customer_supplier> GetCustomerSupplier(int id)
        {
            var customerSupplier = await context.Customer_Supplier
                .Include(a => a.Contact)
                .Include(a => a.Address)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (customerSupplier == null)
            {
                Customer_supplier cs = new(null, 0, null);
                return cs;
            }

            return customerSupplier;
        }

        public async Task<IEnumerable<CustomerSupplierResponse>> GetFiltersCustomerSupplier(
            string? cpfCnpj, int? type, string? name,
            string? country, string? city, string? email, int? id,
            int pageNumber = 1, int pageSize = 10)
        {
            var query = context.Customer_Supplier
                .AsNoTracking()
                .Include(a => a.Contact)
                .Include(a => a.Address)
                .AsQueryable();

            if (!string.IsNullOrEmpty(cpfCnpj))
                query = query.Where(c => c.CpfCnpj == cpfCnpj);
            if (type.HasValue)
            {
                var typeValue = (CustomerSupplierEnum)type.Value;
                query = query.Where(c => c.Type == typeValue);
            }
            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.Name.Contains(name));
            if (!string.IsNullOrEmpty(country))
                query = query.Where(c => c.Address.Country == country);
            if (!string.IsNullOrEmpty(city))
                query = query.Where(c => c.Address.City == city);
            if (!string.IsNullOrEmpty(email))
                query = query.Where(c => c.Contact.Email == email);
            if (id.HasValue)
                query = query.Where(c => c.Id == id);

            var result = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var objMapeado = _mapper.Map<IEnumerable<CustomerSupplierResponse>>(result);

            return objMapeado;

        }
    }
}
