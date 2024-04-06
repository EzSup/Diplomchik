namespace Restaurant.Client.Contracts.Users
{
    public record RegisterUserResponse(
        bool Flag = false, 
        string Message = null!);
}
