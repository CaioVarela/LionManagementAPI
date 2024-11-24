using ManageIt.Communication.Responses;

namespace ManageIt.Application.UseCases.Excel.AddCollaboratorsBySheet
{
    public interface IAddCollaboratorsBySheetUseCase
    {
        public Task<ResponseImportedFromSheet> Execute(Stream excelStream);
    }
}
