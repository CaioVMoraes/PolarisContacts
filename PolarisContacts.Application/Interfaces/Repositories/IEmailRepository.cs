﻿using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IEmailRepository
    {
        Task<IEnumerable<Email>> GetEmailsByIdContatoAsync(int idContato);
        Task<Email> GetEmailByIdAsync(int id);
        Task AddEmailAsync(Email email);
        Task UpdateEmailAsync(Email email);
        Task DeleteEmailAsync(int id);
    }
}
