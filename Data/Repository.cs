using AutoMapper;
using ClientSupplierApi.Models;
using ClientSupplierApi.Response;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ClientSupplierApi.Data
{
    public class Repository : IRepository
    {
        public DataContext _context { get; }
        private readonly IMapper _mapper;

        public Repository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //==========================Genericas======================
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<bool> UserExistAsync(string email, string userName)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Email == email || a.UserName == userName);

            if (user == null)
                return false;

            return true;
        }

        public async Task<Users> GetEmailByName(string name)
        {
            var user = await _context.Users
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
            var user = await _context.Users
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
            var users = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            var objMapeado = _mapper
                .Map<IEnumerable<UsersResponse>>(users);

            return objMapeado;
        }

        public async Task<bool> CustomerSupplierExistAsync(string name, string cpfCnpj)
        {
            var customerSupplier = await _context.Customer_Supplier
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Name == name || a.CpfCnpj == cpfCnpj);

            if (customerSupplier == null)
                return false;

            return true;
        }

        public async Task<Customer_supplier> GetCustomerSupplier(int id)
        {
            var customerSupplier = await _context.Customer_Supplier
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (customerSupplier == null)
            {
                Customer_supplier cs = new(null, 0, null);
                return cs;
            }

            return customerSupplier;
        }
    }
}
