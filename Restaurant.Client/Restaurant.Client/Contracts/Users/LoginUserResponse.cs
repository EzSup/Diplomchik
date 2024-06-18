namespace Restaurant.Client.Contracts.Users
{
    public record LoginUserResponse(bool Flag = false, string Message = null!, string JWTToken = null!);
}
