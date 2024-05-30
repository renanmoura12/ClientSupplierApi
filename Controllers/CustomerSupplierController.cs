using AutoMapper;
using ClientSupplierApi.Data;
using ClientSupplierApi.Dtos;
using ClientSupplierApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ClientSupplierApi.Controllers
{
    [ApiController]
    [Authorize]
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
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> PostCustomerSupplier(CustomerSupplierDto customerSupplier)
        {
            var boolExist = await _repository.CustomerSupplierExistAsync(customerSupplier.Name, customerSupplier.CpfCnpj);

            if (boolExist)
                return BadRequest("Cliente ou fornecedor já cadastrado.");

            var mapCustomerSupllier = _mapper.Map<Customer_supplier>(customerSupplier);

            _repository.Add(mapCustomerSupllier);

            if (await _repository.SaveChangesAsync())
                return Ok("Novo registro adicionado");

            return BadRequest("Algo ocorreu, verifique");
        }

        [HttpDelete("deleteCostumerSupplier")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> DeleteCustomerSupplier(int id)
        {
            var item = await _repository.GetCustomerSupplier(id);

            if (item == null)
            {
                return NotFound("nenhum registro com esse ID");
            }

            _repository.Delete(item);

            if (await _repository.SaveChangesAsync())
            {
                return Ok("Registro excluido");
            }

            return BadRequest("Algo deu errado, verifique");
        }

        ///<summary>
        /// Método Patch Json para realizar alterações parciais em um objeto cliente/fornecedor.
        ///</summary>
        ///<remarks>
        /// <para>
        /// Exemplo para edição:
        /// </para>
        /// <code>
        /// [
        ///   {
        ///     "path": "Nome do campo/Nome do campo",
        ///     "op": "replace",
        ///     "value": "novo valor"
        ///   }
        /// ]
        /// </code>
        /// <para>
        /// Objeto para consulta:
        /// <code>
        /// {
        ///   "id": 14,
        ///   "name": "string",
        ///   "cpfCnpj": "string",
        ///   "type": 0,
        ///   "contact": {
        ///     "id": 12,
        ///     "customerSupplierId": 14,
        ///     "phone": "999999999",
        ///     "email": "email@email.com"
        ///   },
        ///   "address": {
        ///     "id": 5,
        ///     "customerSupplierId": 14,
        ///     "country": "string",
        ///     "address": "string",
        ///     "complement": "string",
        ///     "city": "string",
        ///     "state": "string",
        ///     "postalCode": "string"
        ///   }
        /// }
        /// </code>
        /// </para>
        ///</remarks>
        [HttpPatch("updateCustomerSupplier")]
        [ProducesResponseType(typeof(Customer_supplier), 200)]
        public async Task<IActionResult> PatchCustomerSupplier(int id, [FromBody] JsonPatchDocument<Customer_supplier> patchDoc)
        {
            var result = await _repository.GetCustomerSupplier(id);

            if (result.Name == null)
            {
                return NotFound("Nada foi encontrado!");
            }

            patchDoc.ApplyTo(result, ModelState);

            Console.WriteLine(ModelState.FirstOrDefault());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerSupplierToPatch = _mapper.Map<CustomerSupplierDto>(result);

            if (await _repository.SaveChangesAsync())
            {
                return Ok(result);
            }

            return BadRequest("Algo ocorreu, verifique!");
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerSupllier(string? cpfCnpj, int? type, string? name, string? country, string? city, string? email, int? id)
        {
            var result = await _repository.GetFiltersCustomerSupplier(cpfCnpj, type, name, country, city, email, id);

            if (!result.Any())
                return NotFound("Nada encontrado");

            return Ok(result);
        }
    }
}
