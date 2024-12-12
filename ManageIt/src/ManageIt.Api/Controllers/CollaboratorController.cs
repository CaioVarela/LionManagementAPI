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
        public async Task<IActionResult> GetAll([FromServices] IGetAllCollaboratorsUseCase useCase)
        {
            var response = await useCase.Execute();

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
        public async Task<IActionResult> GetExpired([FromServices] IGetExpiredCollaboratorExamUseCase useCase)
        {
            var response = await useCase.Execute();
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("expiring-soon")]
        [ProducesResponseType(typeof(ResponseGetAllCollaboratorsWithExpiringSoonExams), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExpiringSoon([FromServices] IGetExpiringSoonCollaboratorExamUseCase useCase)
        {
            var response = await useCase.Execute();
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(List<CollaboratorDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<CollaboratorDTO>), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SearchByName([FromServices] IGetCollaboratorByNameUseCase useCase, string name)
        {
            var response = await useCase.Execute(name);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("upcoming-exams")]
        [ProducesResponseType(typeof(ExpiringExamDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetUpcomingExams([FromServices] IGetUpcomingExpiringExamsUseCase useCase)
        {
            var response = await useCase.Execute();
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CollaboratorDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Add([FromBody] CollaboratorDTO collaboratorDTO, [FromServices] IRegisterCollaboratorUseCase useCase)
        {
            var response = await useCase.Execute(collaboratorDTO);

            return Created(string.Empty, response);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromServices] IUpdateCollaboratorUseCase useCase, [FromRoute] Guid id, [FromBody] CollaboratorDTO collaborator)
        {
            await useCase.Execute(id, collaborator);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromServices] IDeleteCollaboratorUseCase useCase, [FromRoute] Guid id)
        {
            await useCase.Execute(id);

            return NoContent();
        }
    }
}
