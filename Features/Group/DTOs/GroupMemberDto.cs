using System;
using System.ComponentModel.DataAnnotations;
using FriendStuff.Domain.Entities.Enums;

namespace FriendStuff.Features.Group.DTOs;

public record class GroupMemberDto
{
    [Required]
    public required string GroupName { get; init; }

    [Required]
    public required string Username { get; init; }

    [Required]
    public required string AdminUSername { get; init; }

}
