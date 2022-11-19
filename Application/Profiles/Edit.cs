using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles;

public class Edit
{
    public class Command : IRequest<Result<Unit>>
    {
        public EditProfileDto ProfileDto { get; set; }
    }
    
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.ProfileDto.Username).NotEmpty();
            RuleFor(x => x.ProfileDto.DisplayName).NotEmpty();
        }
    }
    
    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.ProfileDto.Username, 
                cancellationToken: cancellationToken);
            if (user == null) return null;
           
            user.Bio = request.ProfileDto.Bio ?? user.Bio;
            user.DisplayName = request.ProfileDto.DisplayName ?? user.DisplayName;
            
            _context.Entry(user).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
            
            if (!result) return Result<Unit>.Failure("Failed to update profile");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}