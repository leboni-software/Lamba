using Lamba.Identity.Application.Common.Constants;
using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Identity.Application.Infrastructure.Repositories.Writers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Application.Features.Commands.Users
{
    public class DeleteUserCommand : BaseAuthorizeRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserWriterRepository _userWriterRepository;
        private readonly IUserReaderRepository _userReaderRepository;

        public DeleteUserCommandHandler(IUserWriterRepository userWriterRepository, IUserReaderRepository userReaderRepository)
        {
            _userWriterRepository = userWriterRepository;
            _userReaderRepository = userReaderRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userReaderRepository.GetAsync(request.Id, cancellationToken);
            if (user is null) throw new Exception(UserMessages.UserNotFound);
            _userWriterRepository.Attach(user);
            _userWriterRepository.Delete(user);
            await _userWriterRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
