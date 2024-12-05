using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Domain.Repositories.Collaborators;

namespace ManageIt.Application.UseCases.Collaborators.Get.GetUpcomingExpiringExams
{
    public class GetUpcomingExpiringExamsUseCase : IGetUpcomingExpiringExamsUseCase
    {
        private readonly ICollaboratorReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetUpcomingExpiringExamsUseCase(ICollaboratorReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ExpiringExamDTO>> Execute()
        {
            var collaborators = await _repository.GetAll();
            var collaboratorsDTO = _mapper.Map<List<CollaboratorDTO>>(collaborators);

            var today = DateTime.Today;
            var sixMonthsLater = today.AddMonths(6);

            var expiringExams = collaboratorsDTO
                .SelectMany(c => c.Exams)
                .Where(e => e.ExpiryDate >= today && e.ExpiryDate < sixMonthsLater && e.IsExpiringSoon)
                .GroupBy(e => new { Year = e.ExpiryDate.Value.Year, Month = e.ExpiryDate.Value.Month })
                .Select(g => new ExpiringExamDTO
                {
                    Date = $"{g.Key.Month:D2}/{g.Key.Year}",
                    ExpiringCount = g.Count()
                })
                .OrderBy(e => e.Date)
                .ToList();

            var allMonths = Enumerable.Range(0, 6)
                .Select(i => today.AddMonths(i))
                .Select(d => new ExpiringExamDTO
                {
                    Date = $"{d.Month:D2}/{d.Year}",
                    ExpiringCount = 0
                });

            var result = allMonths.GroupJoin(
                expiringExams,
                a => a.Date,
                e => e.Date,
                (a, e) => new ExpiringExamDTO
                {
                    Date = a.Date,
                    ExpiringCount = e.FirstOrDefault()?.ExpiringCount ?? 0
                })
                .OrderBy(e => DateTime.ParseExact(e.Date, "MM/yyyy", null))
                .ToList();

            return result;
        }
    }
}