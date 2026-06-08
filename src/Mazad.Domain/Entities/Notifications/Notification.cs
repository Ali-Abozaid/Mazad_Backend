using Mazad.Domain.Enums;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Notifications;

/// <summary>
/// An in-app notification delivered to a user (and optionally pushed via SignalR).
/// </summary>
public class Notification : AggregateRoot<Guid>
{
    public Guid UserId { get; set; }

    public NotificationType Type { get; set; } = NotificationType.General;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }

    public NotificationReferenceType ReferenceType { get; set; } = NotificationReferenceType.None;
    public Guid? ReferenceId { get; set; }

    public void MarkAsRead()
    {
        if (IsRead) return;
        IsRead = true;
        ReadAt = DateTime.UtcNow;
    }
}
