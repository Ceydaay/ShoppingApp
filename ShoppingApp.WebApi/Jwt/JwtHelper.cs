using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoppingApp.WebApi.Jwt
{
    public static class JwtHelper
    {
        // JWT token'ı oluşturmak için kullanılan metod
        public static string GenerateJwtToken(JwtDto jwtInfo)
        {
            // Secret key (gizli anahtar) oluşturuluyor. JWT'nin güvenliği için gereklidir.
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo.SecretKey));

            // Signing credentials: gizli anahtar ve HMACSHA256 algoritması kullanılarak imzalama işlemi yapılır.
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // Claim (id, ad, soyad, e-posta vb.) bilgileri ekleniyor. Bu bilgiler, token'a dahil edilen kimlik bilgilerini temsil eder.
            var claims = new[]
            {
                new Claim(JwtClaimNames.Id, jwtInfo.Id.ToString()),
                new Claim(JwtClaimNames.FirstName, jwtInfo.FirstName),
                new Claim(JwtClaimNames.LastName, jwtInfo.LastName),

                new Claim(JwtClaimNames.Email, jwtInfo.Email),

               new Claim(JwtClaimNames.UserType, jwtInfo.UserType.ToString()),


                new Claim(ClaimTypes.Role, jwtInfo.UserType.ToString())




            };

            var expireTime = DateTime.Now.AddMinutes(jwtInfo.ExpireMinutes);

            var tokenDescriptor = new JwtSecurityToken(jwtInfo.Issuer, jwtInfo.Audience, claims, null, expireTime, credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return token;

          
        }

    }
}
