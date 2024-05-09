namespace Scheduler.Contracts.Users.UserInvites;

public record UserInvitesResponse(
    Guid InviteId,
    Guid SenderId,
    Guid AddressieId,
    string Message
);