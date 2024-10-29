using Dapper;
using Microsoft.IdentityModel.Tokens;
using QuestaAdminApi.DatabaseLayer;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QuestaAdminApi.ServiceLayer.Service
{
    public class ClsCrendential : ICrendential, IDisposable
    {
        ClsDbConnection Connectionmgr;


        private bool isDisposed = false;
        public ClsCrendential()
        {
            Connectionmgr = ClsDbConnection.Instance;
        }

        ~ClsCrendential()
        {
            Dispose(false);
        }

        public bool? ChkValidCrendential(string Email,string Encryptpassword)
        {
            try
            {
                using (IDbConnection cn = Connectionmgr.connection)
                {
                    return cn.ExecuteScalar<bool>("select * from [dbo].[mstUserLogin] " +
                        "where EmailId = @EmailId and PasswordHash = @PasswordHash", new
                        {
                            EmailId = Email,
                            PasswordHash = Encryptpassword,
                        });
                }

                return null;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public ClsUserLogin GetUserDataBaseOnEmail(string EmailId)
        {
            try
            {
                ClsUserLogin objLogin = new ClsUserLogin();

                using (IDbConnection cn = Connectionmgr.connection)
                {
                    objLogin = cn.Query<ClsUserLogin>("Select * from [dbo].[mstUserLogin] where EmailId = @EmailId", new
                    {
                        EmailId = EmailId
                    }).FirstOrDefault();
                }

                return objLogin;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void UpdateRefreshTokenBaseOnEmail(string EmailId,string RefreshToken,DateTime RefreshTokenExpireTime)
        {
            try
            {
                using(IDbConnection cn = Connectionmgr.connection)
                {
                    cn.Execute("Update [dbo].[mstUserLogin] set RefreshToken = @RefreshToken , RefreshTokenExpiryTime =@RefreshTokenExpiryTime" +
                        " where EmailId = @EmailId", new
                        {
                            EmailId = EmailId,
                            RefreshToken = RefreshToken,
                            RefreshTokenExpireTime = RefreshTokenExpireTime
                        });
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public JwtSecurityToken CreateToken(List<Claim> authClaims,string Secret , int tokenValidityInMinutes, string ValidIssuer, string ValidAudience)
        {
            try
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));

                var token = new JwtSecurityToken(
                    issuer: ValidIssuer,
                    audience: ValidAudience,
                    expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return token;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public string GenerateRefreshToken()
        {
            try
            {
                var randomNumber = new byte[64];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }


        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token, string Secret)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)),
                    ValidateLifetime = false
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;

            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

        #region Dispose


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {

                // Code to dispose the managed resources of the class
            }
            // Code to dispose the un-managed resources of the class
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
