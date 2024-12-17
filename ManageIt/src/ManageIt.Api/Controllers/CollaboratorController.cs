using ManageIt.Api.Services;
using ManageIt.Application.UseCases.Collaborators.Delete;
using ManageIt.Application.UseCases.Collaborators.Get.GetAllCollaborators;
using ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByExpiredExams;
using ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByExpiringSoon;
using ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorById;
using ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByName;
using ManageIt.Application.UseCases.Collaborators.Get.GetUpcomingExpiringExams;
using ManageIt.Application.UseCases.Collaborators.Register;
using ManageIt.Application.UseCases.Collaborators.Update;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageIt.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<CollaboratorDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllCollaboratorsUseCase useCase, [FromServices] ICurrentUserService currentUserService)
        {
            var companyId = currentUserService.GetCurrentCompanyId();

            if (companyId == null)
            {
                return Unauthorized("Company ID not found.");
            }

            var response = await useCase.Execute((Guid)companyId!);

            if (response.Count > 0)
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CollaboratorDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetById([FromServices] IGetCollaboratorByIdUseCase useCase, [FromRoute] Guid id)
        {
            var response = await useCase.Execute(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("expired")]
        [ProducesResponseType(typeof(ResponseGetAllCollaboratorsWithExpiringSoonExams), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExpired([FromServices] IGetExpiredCollaboratorExamUseCase useCase, [FromServices] ICurrentUserService currentUserService)
        {
            var companyId = currentUserService.GetCurrentCompanyId();

            if (companyId == null)
            {
                return Unauthorized("Company ID not found.");
            }

            var response = await useCase.Execute((Guid)companyId);

            if (response.Collaborator.Any())
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpGet("expiring-soon")]
        [ProducesResponseType(typeof(ResponseGetAllCollaboratorsWithExpiringSoonExams), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExpiringSoon([FromServices] IGetExpiringSoonCollaboratorExamUseCase useCase, [FromServices] ICurrentUserService currentUserService)
        {
            var companyId = currentUserService.GetCurrentCompanyId();

            if (companyId == null)
            {
                return Unauthorized("Company ID not found.");
            }

            var response = await useCase.Execute((Guid)companyId);

            if (response.Collaborator.Any())
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(List<CollaboratorDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SearchByName([FromServices] IGetCollaboratorByNameUseCase useCase, [FromServices] ICurrentUserService currentUserService, string name)
        {
            var companyId = currentUserService.GetCurrentCompanyId();

            if (companyId == null)
            {
                return Unauthorized("Company ID not found.");
            }

            var response = await useCase.Execute(name, (Guid)companyId);

            if (response.Count > 0)
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpGet("upcoming-exams")]
        [ProducesResponseType(typeof(ExpiringExamDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetUpcomingExams([FromServices] IGetUpcomingExpiringExamsUseCase useCase, [FromServices] ICurrentUserService currentUserService)
        {
            var companyId = currentUserService.GetCurrentCompanyId();

            if (companyId == null)
            {
                return Unauthorized("Company ID not found.");
            }

            var response = await useCase.Execute((Guid)companyId);

            if (response != null && response.Any())
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(CollaboratorDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(
            [FromBody] CollaboratorDTO collaboratorDTO,
            [FromServices] IRegisterCollaboratorUseCase useCase,
            [FromServices] ICurrentUserService currentUserService)
        {
            var companyId = currentUserService.GetCurrentCompanyId();

            if (companyId == null)
            {
                return Unauthorized("Company ID not found.");
            }

            var response = await useCase.Execute(collaboratorDTO, companyId.Value);

            return Created(string.Empty, response);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateCollaboratorUseCase useCase,
            [FromServices] ICurrentUserService currentUserService,
            [FromRoute] Guid id,
            [FromBody] CollaboratorDTO collaborator)
        {
            var companyId = currentUserService.GetCurrentCompanyId();

            if (companyId == null)
            {
                return Unauthorized("Company ID not found.");
            }

            await useCase.Execute(id, collaborator, companyId.Value);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            [FromServices] IDeleteCollaboratorUseCase useCase,
            [FromServices] ICurrentUserService currentUserService,
            [FromRoute] Guid id)
        {
            var companyId = currentUserService.GetCurrentCompanyId();

            if (companyId == null)
            {
                return Unauthorized("Company ID not found.");
            }

            await useCase.Execute(id);

            return NoContent();
        }
    }
}
