using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Data.Repository.IRepository;
using TaskTracker.Model.View;

namespace TaskTracker.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AccountController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        ///<summary>
        ///User signup method
        ///</summary>
        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] SignUpViewModel signUpModel)
        {
            var result = unitOfWork.Account.SignUpAsync(signUpModel);

            if(result.Result.Succeeded)
            {
                return Ok();
            }
            return Unauthorized(result.Result.Errors.FirstOrDefault()?.Description);
        }

        ///<summary>
        ///User login method
        ///</summary>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] SignInViewModel signInViewModel)
        {
            var result = await unitOfWork.Account.LoginAsync(signInViewModel);
        
            if(string.IsNullOrEmpty(result))
                return Unauthorized();

            return Ok(result);
        }
    }
}
