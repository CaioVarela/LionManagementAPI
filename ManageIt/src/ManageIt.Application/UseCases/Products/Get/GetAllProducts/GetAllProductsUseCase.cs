using AutoMapper;
using ManageIt.Communication.ProductDTOs;
using ManageIt.Communication.Responses;
using ManageIt.Domain.Repositories.Products;

namespace ManageIt.Application.UseCases.Products.Get.GetAllProducts
{
    public class GetAllProductsUseCase : IGetAllProductsUseCase
    {
        private readonly IProductReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllProductsUseCase(IProductReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseGetAllProducts> Execute(Guid companyId)
        {
            var getAllresult = await _repository.GetAll();
            var result = getAllresult.Where(p => p.CompanyId == companyId);

            var productsDTO = _mapper.Map<List<ProductDTO>>(result);

            var okProductsInStock = result.Count(p => p.Status == "Estoque Adequado");
            var okEpiProductsInStock = result.Count(p => p.Status == "Estoque Adequado" && p.IsEPI == true);

            var okStockPercentage = ((double)okProductsInStock / result.Count()) * 100;

            var okApprovalCertificationDate = result.Where(p => p.IsEPI).Count(p => p.ApprovalCertification!.IsCertificationExpired);

            var epiQuantity = result.Count(p => p.IsEPI == true);

            var okEpiPercentage = ((double)okEpiProductsInStock / epiQuantity) * 100;

            var notOkApprovalCertificationDatePercentage = ((double)okApprovalCertificationDate / epiQuantity) * 100;

            var response = new ResponseGetAllProducts()
            {
                Product = productsDTO,
                StockOkPercentage = okStockPercentage,
                EPICount = epiQuantity,
                EPIOkPercentage = okEpiPercentage,
                ApprovalCertificationExpiryDateNotOkCount = okApprovalCertificationDate,
                ApprovalCertificationExpiryDateNotOkPercentage = notOkApprovalCertificationDatePercentage
            };

            return response;
        }
    }
}
