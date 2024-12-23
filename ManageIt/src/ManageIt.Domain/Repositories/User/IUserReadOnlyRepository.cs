﻿using System.Threading.Tasks;

namespace ManageIt.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> ExistActiveUserWithEmail(string email);
        Task<Entities.User?> GetUserByEmail(string email);
        Task<Entities.User?> GetQualityManager();
        Task<Entities.User?> GetById(Guid id);
    }
}
