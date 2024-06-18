namespace Restaurant.API.Contracts.Users
{
    public record LoginUserResponse(bool Flag = false, string Message = null!, string JWTToken = null!);
}
