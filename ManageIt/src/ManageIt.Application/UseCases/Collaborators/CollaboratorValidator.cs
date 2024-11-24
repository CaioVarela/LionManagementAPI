using FluentValidation;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageIt.Application.UseCases.Collaborators
{
    public class CollaboratorValidator : AbstractValidator<CollaboratorDTO>
    {
        public CollaboratorValidator() 
        {
            RuleFor(collaborator => collaborator.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_Required);
            RuleFor(collaborator => collaborator.CPF).NotEmpty().WithMessage(ResourceErrorMessages.CPF_Required);
            RuleFor(collaborator => collaborator.Exams).NotEmpty().WithMessage(ResourceErrorMessages.EXAMS_Required);
            RuleFor(collaborator => collaborator.Position).NotEmpty().WithMessage(ResourceErrorMessages.POSITION_Required);
        }
    }
}
