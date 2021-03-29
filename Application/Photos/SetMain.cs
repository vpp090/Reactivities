using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.ExtensionMethods;
using Persistence;
using System.Linq;

namespace Application.Photos
{
    public class SetMain
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }

        public class Hanlder : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Hanlder(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;

            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.GetCurrentUser(_userAccessor);
            
                if(user == null) return null;

                var photo = user.Photos.FirstOrDefault(p => p.Id == request.Id);

                if(photo == null) return null;

                var currentMain = user.Photos.FirstOrDefault(p => p.IsMain);

                if(currentMain != null) currentMain.IsMain = false;

                photo.IsMain = true;

                var result = await _context.SaveChangesAsync() > 0;

                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem setting this photo as main");
            }
        }
    }
}