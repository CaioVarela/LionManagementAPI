using ManageIt.Communication.Responses;

namespace ManageIt.Application.UseCases.Excel.AddProductFromSheet
{
    public interface IAddProductFromSheetUseCase
    {
        public Task<ResponseImportedFromSheet> Execute(Stream excelStream);
    }
}
