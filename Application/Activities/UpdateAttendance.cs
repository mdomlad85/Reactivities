using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities;

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

        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }
        
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities
                .Include(a => a.Attendees).ThenInclude(u => u.AppUser)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (activity == null) return Result<Unit>.Failure("Activity not found");
            
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername(), cancellationToken);
            
            if (user == null) return Result<Unit>.Failure("User not found");
            
            var hostUsername = activity.Attendees.FirstOrDefault(x => x.IsHost)?.AppUser?.UserName;

            var attendance = activity.Attendees.FirstOrDefault(x => x.AppUser.UserName == user.UserName);
            
            // Host can cancel attendance
            if (attendance != null && hostUsername == user.UserName)
                activity.IsCancelled = !activity.IsCancelled;
            
            // User can remove attendance
            if (attendance != null && hostUsername != user.UserName) 
                activity.Attendees.Remove(attendance);
            
            //User can add attendance
            if (attendance == null) 
                activity.Attendees.Add(new ActivityAttendee
                {
                    AppUser = user,
                    Activity = activity,
                    IsHost = false
                });
            
            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
            
            if (!result) return Result<Unit>.Failure("Failed to update attendance");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}