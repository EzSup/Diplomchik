using Restaurant.Client.Services;

namespace Restaurant.Client.Extensions
{
    public class JwtTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpcontextAccessor;

        public JwtTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpcontextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await base.SendAsync(request, cancellationToken);
        }


    }
}
