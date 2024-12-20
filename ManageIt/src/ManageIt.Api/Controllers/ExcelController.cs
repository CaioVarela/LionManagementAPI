using ManageIt.Api.Services;
using ManageIt.Application.UseCases.Excel.AddCollaboratorsBySheet;
using ManageIt.Application.UseCases.Excel.AddProductFromSheet;
using ManageIt.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageIt.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ExcelController : ControllerBase
    {
        [HttpPost]
        [Route("add-collaborators")]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddCollaboratorFromSheet([FromServices] IAddCollaboratorsBySheetUseCase useCase, IFormFile file, [FromServices] ICurrentUserService currentUserService)
        {
            using var stream = file.OpenReadStream();
            var companyId = currentUserService.GetCurrentCompanyId();

            var response = await useCase.Execute(stream, companyId!.Value);

            return Ok(response);
        }

        [HttpPost]
        [Route("add-products")]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddProductsFromSheet([FromServices] IAddProductFromSheetUseCase useCase, [FromServices] ICurrentUserService currentUserService, IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var companyId = currentUserService.GetCurrentCompanyId();

            var response = await useCase.Execute(stream, companyId!.Value);

            return Ok(response);
        }
    }
}
