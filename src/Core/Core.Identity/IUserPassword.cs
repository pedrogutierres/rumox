namespace Core.Identity
{
    public interface IUserPassword
    {
        byte[] Salt { get; }
        byte[] Hash { get; }
    }
}
