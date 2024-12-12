using ManageIt.Application.UseCases.Excel.AddCollaboratorsBySheet;
using ManageIt.Application.UseCases.Excel.AddProductFromSheet;
using ManageIt.Application.UseCases.Login.DoLogin;
using ManageIt.Communication.Requests;
using ManageIt.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ManageIt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        [HttpPost]
        [Route("add-collaborators")]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddCollaboratorFromSheet([FromServices] IAddCollaboratorsBySheetUseCase useCase, IFormFile file)
        {
            using var stream = file.OpenReadStream();

            var response = await useCase.Execute(stream);

            return Ok(response);
        }

        [HttpPost]
        [Route("add-products")]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddProductsFromSheet([FromServices] IAddProductFromSheetUseCase useCase, IFormFile file)
        {
            using var stream = file.OpenReadStream();

            var response = await useCase.Execute(stream);

            return Ok(response);
        }
    }
}
