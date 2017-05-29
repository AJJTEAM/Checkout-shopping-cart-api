using CheckoutShopping.API.CustomMiddlewares;
using CheckoutShopping.Core.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace CheckoutShopping.API
{
        public partial class Startup
        {
            private void ConfigureAuth(IApplicationBuilder app)
            {
                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("TokenAuthentication:SecretKey").Value));

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                    ValidateAudience = true,
                    ValidAudience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            
                var tokenProviderOptions = new TokenProviderOptions
                    {
                        Path = Configuration.GetSection("TokenAuthentication:TokenPath").Value,
                        Audience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                        Issuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                        SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                    };
                app.UseMiddleware<TokenProviderMiddleware>(Options.Create(tokenProviderOptions));
                app.UseJwtBearerAuthentication(new JwtBearerOptions
                {
                    AutomaticAuthenticate = true,
                    AutomaticChallenge = true,
                    TokenValidationParameters = tokenValidationParameters
                });
            }
        }
}
