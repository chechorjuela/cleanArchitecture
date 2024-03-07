using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistance;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
    {
        private readonly IStreamerRepository _stremarRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<DeleteStreamerCommandHandler> _logger;

        public DeleteStreamerCommandHandler(IStreamerRepository stremarRepository, IMapper mapper, IEmailService emailService, ILogger<DeleteStreamerCommandHandler> logger)
        {
            _stremarRepository = stremarRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerDelete = await _stremarRepository.GetByIdAsync(request.Id);
            if (streamerDelete == null) {
                _logger.LogError($"{request.Id} streamer no existe en el sistema");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }
            await _stremarRepository.DeleteAsync(streamerDelete);
            _logger.LogInformation($"Se ha eliminado correctamente el stremaer {request.Id}");
            return Unit.Value;
        }
    }
}
