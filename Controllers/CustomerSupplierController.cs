using AutoMapper;
using ClientSupplierApi.Data;
using ClientSupplierApi.Dtos;
using ClientSupplierApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientSupplierApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerSupplierController : Controller
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CustomerSupplierController(IRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("addCustomerSupplier")]
        public async Task<IActionResult> PostCustomerSupplier(CustomerSupplierDto customerSupplier)
        {
            var boolExist = await _repository.CustomerSupplierExistAsync(customerSupplier.Name, customerSupplier.CpfCnpj);

            if (boolExist)
                return BadRequest("Cliente ou fornecedor já cadastrado.");

            var mapCustomerSupllier = _mapper.Map<Customer_supplier>(customerSupplier);

            _repository.Add(mapCustomerSupllier);

            if(await _repository.SaveChangesAsync())
            {
                var mapCustomerSupplierContact = _mapper.Map<Customer_supplier_contact>(customerSupplier);

                Customer_supplier_contact customerSupplierContact = new(mapCustomerSupllier.Id, mapCustomerSupplierContact.Phone, mapCustomerSupplierContact.Email);

                _repository.Add(customerSupplierContact);

                if (await _repository.SaveChangesAsync())
                {
                    var mapCustomerSupplierAddress = _mapper.Map<Customer_supplier_address>(customerSupplier);

                    Customer_supplier_address customerSupplierAddress = new(mapCustomerSupllier.Id, mapCustomerSupplierAddress.Country, mapCustomerSupplierAddress.Address, mapCustomerSupplierAddress.Complement, mapCustomerSupplierAddress.City, mapCustomerSupplierAddress.State, mapCustomerSupplierAddress.PostalCode);
                    
                    _repository.Add(customerSupplierAddress);

                    await _repository.SaveChangesAsync();

                    return Ok("Novo registro adicionado");
                }
            }

            return BadRequest("Algo ocorreu, verifique");
        }

        [HttpDelete("deleteCostumerSupplier")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _repository.GetCustomerSupplier(id);

            if (item == null)
            {
                return NotFound("nenhum registro com esse ID");
            }

            _repository.Delete(item);

            if(await _repository.SaveChangesAsync())
            {
                return Ok("Registro excluido");
            }

            return BadRequest("Algo deu errado, verifique");
        }
    }
}
