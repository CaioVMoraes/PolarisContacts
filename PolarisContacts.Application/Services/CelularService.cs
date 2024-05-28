using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Application.Services
{
    public class CelularService(ICelularRepository celularRepository, IContatoService contatoService, IRegiaoService regiaoService) : ICelularService
    {
        private readonly ICelularRepository _celularRepository = celularRepository;
        private readonly IContatoService _contatoService = contatoService;
        private readonly IRegiaoService _regiaoService = regiaoService;

        public async Task<IEnumerable<Celular>> GetCelularesByIdContato(int idContato)
        {
            if (idContato <= 0)
            {
                throw new InvalidIdException();
            }

            return await _celularRepository.GetCelularesByIdContato(idContato);
        }

        public async Task<Celular> GetCelularById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var celular = await _celularRepository.GetCelularById(id);

            if (celular == null)
            {
                throw new CelularNotFoundException();
            }

            return celular;
        }

        //public async Task AddCelular(Celular celular)
        //{
        //    if (celular == null)
        //    {
        //        throw new ArgumentNullException(nameof(celular));
        //    }


        //    await _celularRepository.AddCelular(celular);
        //}

        public async Task UpdateCelular(Celular celular)
        {
            if (celular == null || celular.Id <= 0)
            {
                throw new ArgumentNullException(nameof(celular));
            }

            await _celularRepository.UpdateCelular(celular);
        }

        public async Task DeleteCelular(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var existingCelular = await _celularRepository.GetCelularById(id);

            if (existingCelular == null)
            {
                throw new CelularNotFoundException();
            }

            await _celularRepository.DeleteCelular(id);
        }
    }
}