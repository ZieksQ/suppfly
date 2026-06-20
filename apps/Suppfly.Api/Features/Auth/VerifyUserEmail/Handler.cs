using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Auth.VerifyUserEmail;

public class Handler : IRequestHandler<Command, Result<Response>>
{
  private readonly AppDbContext _context;

  public Handler(AppDbContext context)
  {
    _context = context;
  }

  // TODO: Add SMTP Verification Email use link to verify email
  // Option 1: email link automatic verification, automatically verifies email once a emailed link is opened.
  // Option 2: email verification code, generate verification code to verify email. (recommended for MVP)
  public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
  {
    var user = await _context.Users
      .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

    if (user is null)
      return Result<Response>.Fail("User verification does not exits.");

    if (user.IsEmailVerified)
      return Result<Response>.Fail("User is already verified.");

    user.VerifyEmail();

    await _context.SaveChangesAsync(cancellationToken);

    var result = new Response(
        Id: user.Id,
        Email: user.Email,
        IsEmailVerified: user.IsEmailVerified,
        IsActive: user.IsActive,
        UpdatedAt: user.UpdatedAt
    );

    return Result<Response>.Ok(result);
  }
}
