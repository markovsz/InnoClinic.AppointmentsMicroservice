using Application.Abstractions;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using InnoClinic.SharedModels.DTOs.Appointments.Incoming;
using InnoClinic.SharedModels.DTOs.Appointments.Outgoing;

namespace Application.Services;

public class ResultsService : IResultsService
{
	private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly IPdfGenerationService _pdfGenerationService;

	public ResultsService(IRepositoryManager repositoryManager, IMapper mapper, IPdfGenerationService pdfGenerationService)
	{
		_repositoryManager = repositoryManager;
        _mapper = mapper;
        _pdfGenerationService = pdfGenerationService;
	}

	public async Task<Guid> CreateAsync(ResultIncomingDto incomingDto)
    {
        var hasAnotherResult = await _repositoryManager.Appointments.HasAnotherResult(incomingDto.AppointmentId);
        if (hasAnotherResult)
            throw new OperationNotAllowedException("you cant add another result to this appointment");
        var entity = _mapper.Map<Result>(incomingDto);
        entity.Id = Guid.NewGuid();
        await _repositoryManager.Results.CreateAsync(entity);
        await _repositoryManager.SaveChangesAsync();
        return entity.Id;
    }

    public async Task ApproveAsync(Guid id)
    {
        var entity = await _repositoryManager.Appointments.GetByIdAsync(id);
        if (entity is null)
            throw new EntityNotFoundException();
        entity.IsApproved = true;
        await _repositoryManager.SaveChangesAsync();
    }


    public async Task UpdateAsync(Guid id, UpdateResultIncomingDto incomingDto)
    {
        var entity = await _repositoryManager.Results.GetByIdAsync(id);
        if (entity is null)
            throw new EntityNotFoundException();
        entity.Complaints = incomingDto.Complaints;
        entity.Conclusion = incomingDto.Conclusion;
        entity.Recomendations = incomingDto.Recomendations;
        _repositoryManager.Results.Update(entity);
        await _repositoryManager.SaveChangesAsync();
    }

    public async Task<ResultOutgoingDto> GetByIdAsync(Guid id)
    {
        var entity = await _repositoryManager.Results.GetByIdAsync(id);
        if (entity is null)
            throw new EntityNotFoundException();
        var mappedEntity = _mapper.Map<ResultOutgoingDto>(entity);
        return mappedEntity;
    }

    public async Task<byte[]> GetAsPdfAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null)
            throw new EntityNotFoundException();
        var bytes = _pdfGenerationService.Generate(entity);
        return bytes;
    }
}
