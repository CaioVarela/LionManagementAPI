using ManageIt.Api.Services;
using ManageIt.Application.UseCases.Products.Delete;
using ManageIt.Application.UseCases.Products.Get.GetAllProducts;
using ManageIt.Application.UseCases.Products.Get.GetProductById;
using ManageIt.Application.UseCases.Products.Get.GetProductByName;
using ManageIt.Application.UseCases.Products.Register;
using ManageIt.Application.UseCases.Products.Update;
using ManageIt.Communication.ProductDTOs;
using ManageIt.Communication.Responses;
using ManageIt.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageIt.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ResponseGetAllProducts), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetAllProductsUseCase useCase,
            [FromServices] ICurrentUserService currentUserService)
        {
            var companyId = currentUserService.GetCurrentCompanyId();

            if (companyId == null)
            {
                return Unauthorized("Company ID not found.");
            }

            var response = await useCase.Execute(companyId.Value);

            if (response.Product.Count > 0)
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetById([FromServices] IGetProductByIdUseCase useCase, [FromRoute] Guid id)
        {
            var response = await useCase.Execute(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(List<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ProductDTO>), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SearchByName(
            [FromServices] IGetProductByNameUseCase useCase,
            [FromServices] ICurrentUserService currentUserService,
            string name)
        {
            var companyId = currentUserService.GetCurrentCompanyId();

            if (companyId == null)
            {
                return Unauthorized("Company ID not found.");
            }

            var response = await useCase.Execute(name, companyId.Value);

            if (response.Count > 0)
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(
            [FromBody] ProductDTO productDTO,
            [FromServices] IRegisterProductUseCase useCase,
            [FromServices] ICurrentUserService currentUserService)
        {
            var companyId = currentUserService.GetCurrentCompanyId();

            if (companyId == null)
            {
                return Unauthorized("Company ID not found.");
            }

            var response = await useCase.Execute(productDTO, companyId.Value);

            return Created(string.Empty, response);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateProductUseCase useCase,
            [FromServices] ICurrentUserService currentUserService,
            [FromRoute] Guid id,
            [FromBody] ProductDTO product)
        {
            var companyId = currentUserService.GetCurrentCompanyId();

            if (companyId == null)
            {
                return Unauthorized("Company ID not found.");
            }

            await useCase.Execute(id, product, companyId.Value);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            [FromServices] IDeleteProductUseCase useCase,
            [FromServices] ICurrentUserService currentUserService,
            [FromRoute] Guid id)
        {
            var companyId = currentUserService.GetCurrentCompanyId();

            if (companyId == null)
            {
                return Unauthorized("Company ID not found.");
            }

            await useCase.Execute(id, companyId.Value);

            return NoContent();
        }

        [HttpDelete("delete-all")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAll(
            [FromServices] IDeleteAllProductUseCase useCase,
            [FromServices] ICurrentUserService currentUserService)
        {
            var companyId = currentUserService.GetCurrentCompanyId();

            if (companyId == null)
            {
                return Unauthorized("Company ID not found.");
            }

            await useCase.Execute(companyId.Value);

            return NoContent();
        }
    }
}
