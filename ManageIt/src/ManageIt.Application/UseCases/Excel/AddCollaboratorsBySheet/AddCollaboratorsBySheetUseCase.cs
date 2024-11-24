using ManageIt.Communication.Responses;
using ManageIt.Domain.Entities;
using ManageIt.Domain.Repositories.Collaborators;
using OfficeOpenXml;
using System.Globalization;

namespace ManageIt.Application.UseCases.Excel.AddCollaboratorsBySheet
{
    public class AddCollaboratorsBySheetUseCase : IAddCollaboratorsBySheetUseCase
    {
        private readonly ICollaboratorReadOnlyRepository _collaboratorReadOnlyRepository;
        private readonly ICollaboratorWriteOnlyRepository _collaboratorWriteOnlyRepository;
        private readonly ICollaboratorUpdateOnlyRepository _collaboratorUpdateOnlyRepository;

        public AddCollaboratorsBySheetUseCase(
            ICollaboratorReadOnlyRepository collaboratorReadOnlyRepository,
            ICollaboratorWriteOnlyRepository collaboratorWriteOnlyRepository,
            ICollaboratorUpdateOnlyRepository collaboratorUpdateOnlyRepository)
        {
            _collaboratorReadOnlyRepository = collaboratorReadOnlyRepository;
            _collaboratorWriteOnlyRepository = collaboratorWriteOnlyRepository;
            _collaboratorUpdateOnlyRepository = collaboratorUpdateOnlyRepository;
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

                    var startRow = 2;
                    var addedCollaboratorsCount = 0;
                    var editedCollaboratorsCount = 0;

                    for (int row = startRow; row <= worksheet.Dimension.End.Row; row++)
                    {
                        if (string.IsNullOrWhiteSpace(worksheet.Cells[row, 1]?.Text))
                        {
                            Console.WriteLine($"Linha {row} ignorada: Nome do colaborador está vazio.");
                            continue;
                        }

                        try
                        {
                            var name = worksheet.Cells[row, 1].Text;

                            var collaborator = await _collaboratorReadOnlyRepository.GetByName(name) ?? new Collaborator { Name = name };

                            AddOrUpdateExamsFromRow(collaborator, worksheet, row);

                            if (collaborator.Id == Guid.Empty)
                            {
                                await _collaboratorWriteOnlyRepository.Add(collaborator);
                                addedCollaboratorsCount++;
                            }
                            else
                            {
                                await _collaboratorUpdateOnlyRepository.Update(collaborator);
                                editedCollaboratorsCount++;
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine($"Erro ao processar a linha {row}: {ex.Message}");
                        }
                    }
                    return new ResponseImportedFromSheet
                    {
                        Message = $"{addedCollaboratorsCount} collaborators added, and {editedCollaboratorsCount} collaborators edited.",
                        ImportedElementsCount = addedCollaboratorsCount + editedCollaboratorsCount
                    };
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Erro ao importar o arquivo Excel: {ex.Message}");
                throw;
            }
        }

        private static void AddOrUpdateExamsFromRow(Collaborator collaborator, ExcelWorksheet worksheet, int row)
        {
            if (worksheet == null) throw new ArgumentNullException(nameof(worksheet));
            if (collaborator == null) throw new ArgumentNullException(nameof(collaborator));

            collaborator.Exams ??= new List<CollaboratorExam>();

            var exams = new List<(string ExamName, string ExamDateText)>
            {
                ("ASO", worksheet.Cells[row, 2].Text),
                ("Avaliacao Psicologica", worksheet.Cells[row, 3].Text),
                ("NR10", worksheet.Cells[row, 4].Text),
                ("NR35", worksheet.Cells[row, 5].Text),
                ("Direcao Defensiva", worksheet.Cells[row, 7].Text),
                ("HAR", worksheet.Cells[row, 8].Text)
            };

            foreach (var e in exams)
            {
                var examName = e.ExamName;
                var examDateText = e.ExamDateText;
                if (string.IsNullOrWhiteSpace(examDateText)) continue;

                if (DateTime.TryParseExact(examDateText, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime examDate))
                {
                    try
                    {
                        var exam = new CollaboratorExam
                        {
                            Collaborator = collaborator,
                            ExamName = examName,
                            ExamDate = examDate,
                            CollaboratorId = collaborator.Id,
                            Id = Guid.NewGuid(),
                        };

                        collaborator.Exams.Add(exam);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Erro ao calcular data de vencimento para o exame '{examName}': {ex.Message}");
                    }
                }
            }
        }
    }
}