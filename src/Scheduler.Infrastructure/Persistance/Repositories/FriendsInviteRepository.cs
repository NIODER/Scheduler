using Microsoft.EntityFrameworkCore;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.FriendsInviteAggregate;
using Scheduler.Domain.FriendsInviteAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Infrastructure.Persistance.Repositories;

public sealed class FriendsInviteRepository(SchedulerDbContext context) : IFriendsInviteRepository
{
    private readonly SchedulerDbContext _context = context;

    public void CreateFriendsInvite(FriendsInvite friendsInvite)
    {
        _context.FriendsInvites.Add(friendsInvite);
    }

    public void DeleteFriendsInviteById(FriendsInviteId friendsInviteId)
    {
        var friendsInvite = _context.FriendsInvites.SingleOrDefault(fi => fi.Id == friendsInviteId)
            ?? throw new NullReferenceException($"No friends invite with id {friendsInviteId.Value} found.");
        _context.FriendsInvites.Remove(friendsInvite);
    }

    public FriendsInvite? GetFriendsInviteById(FriendsInviteId friendsInviteId)
    {
        return _context.FriendsInvites.SingleOrDefault(fi => fi.Id == friendsInviteId)
    }

    public Task<FriendsInvite?> GetFriendsInviteByIdAsync(FriendsInviteId friendsInviteId)
    {
        var friendsInviteTask = _context.FriendsInvites.SingleOrDefaultAsync(fi => fi.Id == friendsInviteId);
        return friendsInviteTask;
    }

    public List<FriendsInvite> GetReceivedUserInvitesByUserId(UserId userId)
    {
        var receivedFriendInvites = _context.FriendsInvites.Where(fi => fi.AddressieId == userId)
            .ToList();
        return receivedFriendInvites;
    }

    public Task<List<FriendsInvite>> GetReceivedUserInvitesByUserIdAsync(UserId userId)
    {
        var receivedFriendInvitesTask = _context.FriendsInvites.Where(fi => fi.AddressieId == userId)
            .ToListAsync();
        return receivedFriendInvitesTask;
    }

    public List<FriendsInvite> GetRelatedUserInvitesByUserId(UserId userId)
    {
        var relatedFriendInvites = _context.FriendsInvites
            .Where(fi => fi.AddressieId == userId || fi.SenderId == userId)
            .ToList();
        return relatedFriendInvites;
    }

    public Task<List<FriendsInvite>> GetRelatedUserInvitesByUserIdAsync(UserId userId)
    {
        var relatedFriendInvitesTask = _context.FriendsInvites
            .Where(fi => fi.AddressieId == userId || fi.SenderId == userId)
            .ToListAsync();
        return relatedFriendInvitesTask;
    }

    public List<FriendsInvite> GetSendedUserInvitesByUserId(UserId userId)
    {
        var sendedFriendInvites = _context.FriendsInvites
            .Where(fi => fi.SenderId == userId)
            .ToList();
        return sendedFriendInvites;
    }

    public Task<List<FriendsInvite>> GetSendedUserInvitesByUserIdAsync(UserId userId)
    {
        var sendedFriendInvitesTask = _context.FriendsInvites
            .Where(fi => fi.SenderId == userId)
            .ToListAsync();
        return sendedFriendInvitesTask;
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
