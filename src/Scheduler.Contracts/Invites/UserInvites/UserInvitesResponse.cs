namespace Scheduler.Contracts.Invites.UserInvites;

public record UserInvitesResponse(
    Guid InviteId,
    Guid SenderId,
    Guid AddressieId,
    string Message
);