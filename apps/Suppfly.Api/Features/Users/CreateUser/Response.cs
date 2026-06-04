using Suppfly.Api.Domain.Enums;

namespace Suppfly.Api.Features.Users.CreateUser;

public record Response(
    Guid Id,
    string FullName,
    string Email,
    UserRole Role,
    UserStatus Status,
    DateTime CreatedAt
    );
