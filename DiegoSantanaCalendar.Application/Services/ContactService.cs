using DiegoSantanaCalendar.Application.DTOs.Contact;
using DiegoSantanaCalendar.Application.Interfaces;
using DiegoSantanaCalendar.Application.Utils;
using DiegoSantanaCalendar.Domain.Entities;
using DiegoSantanaCalendar.Domain.Interfaces;

namespace DiegoSantanaCalendar.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly MapperDtoToEntities _mapper;
        private readonly IMessagePublisher _messagePublisher;

        public ContactService(
            IContactRepository contactRepository,
            MapperDtoToEntities mapper,
            IMessagePublisher messagePublisher)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
            _messagePublisher = messagePublisher;
        }

        public async Task Create(CreateContactDTO dto, Guid userId)
        {
            var objectSave = new Contact();
            await _mapper.MapToExistingAsync(dto, objectSave);
            objectSave.UserId = userId;
            await _contactRepository.AddAsync(objectSave);
        }

        public async Task Delete(Guid IdJobFunction)
        {
            var result = await _contactRepository.GetByIdAsync(IdJobFunction);
            if (result == null) throw new KeyNotFoundException("Contato não encontrada.");
            await _contactRepository.DeleteAsync(result);
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Contact>> GetAllByIdUser(Guid idUser)
        {
            return await _contactRepository.GetAllByIdUser(idUser);
        }


        public async Task<Contact> GetById(Guid id)
        {
            var result = await _contactRepository.GetByIdAsync(id);
            if (result == null) throw new KeyNotFoundException("Contato não encontrada.");
            return result;
        }

        public async Task Update(UpdateContactDTO dto)
        {
            var jobFunction = await _contactRepository.GetByIdAsync(dto.Id);

            if (jobFunction == null) throw new KeyNotFoundException($"Contato com ID {dto.Id} não encontrada.");
            await _mapper.MapToExistingAsync(dto, jobFunction);
            await _contactRepository.UpdateAsync(jobFunction);
        }

        public Task UpdateStatusAsync(UpdateContactStatusDto dto)
        {
            _messagePublisher.PublishUpdateContactStatus(dto);
            return Task.CompletedTask;
        }
    }
}