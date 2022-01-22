using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace GraphQlApi.Security
{
    public class AuthMutation : ObjectGraphType
    {
        public AuthMutation(IHttpContextAccessor contextAccessor)
        {
            FieldAsync<SessionType>(
                "sessions",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "password" }),
                resolve: async context =>
                {
                    string password = context.GetArgument<string>("password");

                    if (password != "123")
                    {
                        return new Session { IsAuthenticated = false };
                    }

                    var principal = new ClaimsPrincipal(new ClaimsIdentity("Cookie"));
                    await contextAccessor.HttpContext.SignInAsync(principal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMonths(6),
                        IsPersistent = true
                    });

                    return new Session { IsAuthenticated = true };
                });

        }
    }
}
