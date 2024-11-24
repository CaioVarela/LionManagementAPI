using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageIt.Application.UseCases.Email
{
    public interface ISendEmailUseCase
    {
        Task Execute(string to, string subject, string body);
    }
}
