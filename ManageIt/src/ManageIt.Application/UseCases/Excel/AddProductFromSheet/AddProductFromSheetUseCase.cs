using ManageIt.Communication.Responses;
using ManageIt.Domain.Entities;
using ManageIt.Domain.Repositories;
using ManageIt.Domain.Repositories.Collaborators;
using ManageIt.Domain.Repositories.Products;
using OfficeOpenXml;

namespace ManageIt.Application.UseCases.Excel.AddProductFromSheet
{
    public class AddProductFromSheetUseCase : IAddProductFromSheetUseCase
    {
        private readonly IProductReadOnlyRepository _productReadOnlyRepository;
        private readonly IProductWriteOnlyRepository _productWriteOnlyRepository;
        private readonly IProductUpdateOnlyRepository _productUpdateOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddProductFromSheetUseCase(IProductReadOnlyRepository productReadOnlyRepository, IProductWriteOnlyRepository productWriteOnlyRepository, IProductUpdateOnlyRepository productUpdateOnlyRepository, IUnitOfWork unitOfWork)
        {
            _productReadOnlyRepository = productReadOnlyRepository;
            _productUpdateOnlyRepository = productUpdateOnlyRepository;
            _productWriteOnlyRepository = productWriteOnlyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseImportedFromSheet> Execute(Stream excelStream)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            try
            {
                using (var package = new ExcelPackage(excelStream))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    if (worksheet == null)
                    {
                        throw new System.Exception("A planilha não foi encontrada.");
                    }

                    var startRow = 6;
                    var addedProductsCount = 0;

                    for (int row = startRow; row <= worksheet.Dimension.End.Row; row++)
                    {
                        if (string.IsNullOrWhiteSpace(worksheet.Cells[row, 2]?.Text))
                        {
                            Console.WriteLine($"Linha {row} ignorada: Nome do colaborador está vazio.");
                            continue;
                        }

                        try
                        {
                            var productName = worksheet.Cells[row, 2].Text;
                            var balance = worksheet.Cells[row, 5].Text;
                            var minimumStock = worksheet.Cells[row, 6].Text;

                            var product = new Product { ProductName = productName, Balance = int.Parse(balance), MinimumStock = int.Parse(minimumStock), Id = new Guid() };

                            var appprovalCertification = GetApprovalCertificationFromRow(worksheet.Cells[row, 3].Text, worksheet.Cells[row, 4].Text, product.Id);

                            if (appprovalCertification is not null)
                            {
                                product.ApprovalCertification = appprovalCertification;
                            }
                            await _productWriteOnlyRepository.Add(product);
                            await _unitOfWork.Commit();
                            addedProductsCount++;
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine($"Erro ao processar a linha {row}: {ex.Message}");
                        }
                    }
                    return new ResponseImportedFromSheet
                    {
                        Message = $"{addedProductsCount} collaborators added.",
                        ImportedElementsCount = addedProductsCount
                    };
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Erro ao importar o arquivo Excel: {ex.Message}");
                throw;
            }
        }

        private static ApprovalCertification? GetApprovalCertificationFromRow(string caCode, string manufacturer, Guid productId)
        {
            if (string.IsNullOrEmpty(caCode) || caCode is "NA")
                return null;

            ApprovalCertification approvalCertification = new ApprovalCertification
            {
                Manufacturer = manufacturer.Length > 0 ? manufacturer : "NA",
                CertificationNumber = int.Parse(caCode),
                ProductId = productId,
                Id = new Guid()
            };

            return approvalCertification;
        }
    }
}
