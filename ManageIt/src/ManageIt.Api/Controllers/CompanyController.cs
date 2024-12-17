using ManageIt.Application.UseCases.Companies.Get.GetAllCollaboratorsUseCase;
using ManageIt.Application.UseCases.Companies.Register;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Communication.CompanyDTOs;
using ManageIt.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ManageIt.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CompanyDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Add([FromBody] CompanyDTO companyDTO, [FromServices] IRegisterCompanyUseCase useCase)
        {
            var response = await useCase.Execute(companyDTO);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CompanyDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllCompaniesUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Count > 0)
            {
                return Ok(response);
            }

            return NoContent();
        }
    }
}
