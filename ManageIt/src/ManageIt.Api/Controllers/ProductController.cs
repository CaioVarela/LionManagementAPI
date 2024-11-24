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
        [ProducesResponseType(typeof(List<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllProductsUseCase useCase)
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
        public async Task<IActionResult> SearchByName([FromServices] IGetProductByNameUseCase useCase, string name)
        {
            var response = await useCase.Execute(name);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status201Created)]
        //[ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Add([FromBody] ProductDTO productDTO, [FromServices] IRegisterProductUseCase useCase)
        {
            var response = await useCase.Execute(productDTO);

            return Created(string.Empty, response);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromServices] IUpdateProductUseCase useCase, [FromRoute] Guid id, [FromBody] ProductDTO product)
        {
            await useCase.Execute(id, product);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromServices] IDeleteProductUseCase useCase, [FromRoute] Guid id)
        {
            await useCase.Execute(id);

            return NoContent();
        }
    }
}
