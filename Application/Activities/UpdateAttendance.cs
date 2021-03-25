using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class UpdateAttendance
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext dataContext, IUserAccessor userAccessor)
            {
                _context = dataContext;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities
                    .Include(a => a.Attendees)
                    .ThenInclude(a => a.AppUser)
                    .SingleOrDefaultAsync(a => a.Id == request.Id);

                if(activity == null) return null;

                var user = await _context.Users
                            .FirstOrDefaultAsync(u => u.UserName == _userAccessor.GetUsername());
            
                if(user == null) return null;

                var hostUserName = activity.Attendees.FirstOrDefault(a => a.IsHost)?.AppUser?.UserName;

                var attendance = activity.Attendees.FirstOrDefault(a => a.AppUser.UserName == user.UserName);

                if(attendance != null && hostUserName == user.UserName)
                    activity.IsCancelled = !activity.IsCancelled;
            
                if(attendance != null && hostUserName != user.UserName)
                    activity.Attendees.Remove(attendance);

                if(attendance == null)
                {
                    attendance = new Domain.ActivityAttendee
                    {
                        AppUser = user,
                        Activity = activity,
                        IsHost = false
                    };

                    activity.Attendees.Add(attendance);
                }

                var result = await _context.SaveChangesAsync() > 0;

                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem updating attendance");
            }
        }
    }
}