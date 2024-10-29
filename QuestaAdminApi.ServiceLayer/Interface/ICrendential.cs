

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QuestaAdminApi.ServiceLayer
{
    public interface ICrendential
    {
        bool? ChkValidCrendential(string Email, string Encryptpassword);
        JwtSecurityToken CreateToken(List<Claim> authClaims, string Secret, int tokenValidityInMinutes, string ValidIssuer, string ValidAudience);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token, string Secret);
        ClsUserLogin GetUserDataBaseOnEmail(string EmailId);
        void UpdateRefreshTokenBaseOnEmail(string EmailId, string RefreshToken, DateTime RefreshTokenExpireTime);
        void Dispose();
    }
}
