using AutoMapper;
using ClientSupplierApi.Data;
using ClientSupplierApi.Dtos;
using ClientSupplierApi.Models;
using ClientSupplierApi.Response;
using ClientSupplierApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ClientSupplierApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration=configuration;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        public async Task<IActionResult> AddNewUser([FromBody]UserDto user) 
        {
            try
            {
                var userExist = await _repository.UserExistAsync(user.Email, user.UserName);

                if (userExist)
                    return BadRequest("Usuario existente, verifque!");


                var objMapeado = _mapper.Map<Users>(user);
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                byte[] passwordSalt = hmac.Key;

                objMapeado.AlterarSenha(passwordHash, passwordSalt);

                _repository.Add(objMapeado);

                if (await _repository.SaveChangesAsync())
                {
                    var token = TokenService.GenerateToken(_configuration, user.UserName, user.Email);

                    TokenResponse userToken = new()
                    {
                        Token = token,
                        UserName = user.UserName,
                    };

                    return Ok(userToken);
                };

                return BadRequest("Algo deu errado, verifique!");
            }
            catch (Exception) 
            {
                return BadRequest("Algo deu errado, verifique!");

            }
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        public async Task<ActionResult<TokenResponse>> Login(LoginDto login)
        {
            var userExist = await _repository.GetEmailByName(login.UserName);

            if(userExist.UserName == null)
            {
                return NotFound("Nenhum registro localizado, registre para fazer o login");
            }

            var result = await _repository.LoginAsync(login.UserName, login.Password);

            if (!result)
            {
                return Unauthorized("Usuário ou senha inválidos");
            }

            var token = TokenService.GenerateToken(_configuration, userExist.UserName, userExist.Email);

            return new TokenResponse()
            {
                UserName = userExist.UserName,
                Token = token
            };
        }

        [HttpGet("allUsers")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<UsersResponse>), 200)]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _repository.GetAllUsuariosAsync();

            if (!users.Any())
                return NotFound("Nenhum cadastro");

            return Ok(users);
        }

    }
}
