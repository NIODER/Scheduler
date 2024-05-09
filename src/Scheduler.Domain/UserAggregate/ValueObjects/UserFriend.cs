namespace Scheduler.Domain.UserAggregate.ValueObjects;

public class UserFriend
{
    public UserId UserId1 { get; private set; }
    public UserId UserId2 { get; private set; }

    public virtual User User1 { get; private set; } = default!;
    public virtual User User2 { get; private set; } = default!;

    private UserFriend()
    {
        UserId1 = default!;
        UserId2 = default!;
    }

    public UserFriend(UserId userId1, UserId userId2)
    {
        UserId1 = userId1;
        UserId2 = userId2;
    }

    public UserId GetFriendId(UserId userId) => (userId == UserId1, userId == UserId2) switch
    {
        (_, true) => UserId1,
        (true, _) => UserId2,
        _ => throw new ArgumentException($"User id wasn't equal to {nameof(UserId1)} nor {nameof(UserId2)}.", nameof(userId))
    };
}
