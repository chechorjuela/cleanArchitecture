using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistance;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand>
    {
        private readonly IStreamerRepository _stremarRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<UpdateStreamerCommand> _logger;

        public UpdateStreamerCommandHandler(IStreamerRepository stremarRepository, IMapper mapper, IEmailService emailService, ILogger<UpdateStreamerCommand> logger)
        {
            _stremarRepository = stremarRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToUpdate = await _stremarRepository.GetByIdAsync(request.Id);
            if (streamerToUpdate == null) {
                _logger.LogError($"No se encontro el streamer id {request.Id}");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }
            _mapper.Map(request, streamerToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer));
            await _stremarRepository.UpdateAsync(streamerToUpdate);
            _logger.LogInformation($"La operacion fue exitosa actualizado el stremaer {request.Id}");
            return Unit.Value;
        }
    }
}
