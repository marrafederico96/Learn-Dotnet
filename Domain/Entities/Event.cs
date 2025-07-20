using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FriendStuff.Domain.Entities.Enums;

namespace FriendStuff.Domain.Entities;

public class Event
{
    [Key]
    public Guid EventId { get; init; }

    [Required]
    public Guid GroupId { get; init; }

    [Required]
    public Guid LocationId { get; init; }

    [Required]
    [MaxLength(100)]
    public required string EventName { get; init; }

    [Required]
    [MaxLength(100)]
    public required string NormalizeEventName { get; init; }

    [Required]
    [MaxLength(255)]
    public required string EventDescription { get; init; }


    [Required]
    public required EventCategory EventCategory { get; init; }

    [Required]
    public required DateOnly StartDate { get; init; }

    [Required]
    public required DateOnly EndDate { get; init; }

    [ForeignKey(name: "GroupId")]
    public UserGroup? Group { get; init; }

    [ForeignKey(name: "LocationId")]
    public Location? Location { get; init; }

}
