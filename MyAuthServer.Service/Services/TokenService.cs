

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyAuthServer.Core.Models;
using MyAuthSrever.Core.Configuration;
using MyAuthSrever.Core.Dtos;
using MyAuthSrever.Core.Services;
using SharedLibrary.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MyAuthServer.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOptions _tokenOption;

        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOptions> opt)
        {
            _userManager = userManager;
            _tokenOption = opt.Value;
        }
        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];

            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }
        private IEnumerable<Claim> GetClaims(UserApp userApp, List<string> audiences)
        {
            var userList = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,userApp.Id),
            new Claim(JwtRegisteredClaimNames.Email,userApp.Email!),
            new Claim(ClaimTypes.Name,userApp.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
           };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return userList;
        }

        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();

            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            new Claim(JwtRegisteredClaimNames.Sub, client.Id);
            return claims;
        }



        public TokenDto CreateToken(UserApp userApp)
        {
            var accessTokenExp = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExp = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);

            var seruceKey = SignInService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);
            SigningCredentials signingCredentials =
                new SigningCredentials(seruceKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExp,
                 notBefore: DateTime.Now,
                 claims: GetClaims(userApp, _tokenOption.Audience),
                 signingCredentials: signingCredentials
                );
            var handler = new JwtSecurityTokenHandler();
            var token =handler.WriteToken(jwtSecurityToken);

             var tokenDto=new TokenDto
             {
                  AccessToken = token,
                  RefreshToken=CreateRefreshToken(),
                  AccesTokenExpiration=accessTokenExp,
                  RefreshTokenExpiration=refreshTokenExp,
             };
            return tokenDto;
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            var accessTokenExp = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);

            var seruceKey = SignInService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);
            SigningCredentials signingCredentials =
                new SigningCredentials(seruceKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExp,
                 notBefore: DateTime.Now,
                 claims:GetClaimsByClient(client),
                 signingCredentials: signingCredentials
                );
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            var tokenDto = new ClientTokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExp
            };
            return tokenDto;
        }
    }
}
