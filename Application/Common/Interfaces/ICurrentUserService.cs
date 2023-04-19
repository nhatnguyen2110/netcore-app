﻿namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? Email { get; }
        string? ExpireInMinutes { get; }
    }
}
