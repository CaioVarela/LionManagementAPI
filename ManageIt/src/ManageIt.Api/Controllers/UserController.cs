using ManageIt.Application.UseCases.Users.Register;
using ManageIt.Communication.Requests;
using ManageIt.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ManageIt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase, [FromBody] RequestRegisterUserJson user)
        {
            var response = await useCase.Execute(user);

            return Created(string.Empty, response);
        }
    }
}
