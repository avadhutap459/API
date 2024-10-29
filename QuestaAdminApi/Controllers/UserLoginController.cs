using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QuestaAdminApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private IAesOperation aesOperation { get; set; }
        private readonly IConfiguration _configuration;
        private ICrendential crendential { get; set; }
        public string Keyforsecurity { get; set; }
        public UserLoginController(IAesOperation _aesOperation, IConfiguration configuration, ICrendential _crendential)
        {
            aesOperation = _aesOperation;
            _configuration = configuration;
            crendential = _crendential;
            this.Keyforsecurity = _configuration.GetValue<string>("key");
        }

        [Route("UserLogin")]
        [HttpPost]
        public IActionResult Login(ClsUserLogin ObjLogin)
        {
            try
            {
                string _Encryptstr = aesOperation.EncryptString(ObjLogin.PasswordHash, this.Keyforsecurity);
                bool _Flgavailablerecords = crendential.ChkValidCrendential(ObjLogin.EmailId, _Encryptstr).HasValue ?
                    crendential.ChkValidCrendential(ObjLogin.EmailId, _Encryptstr).Value : false;


                if (_Flgavailablerecords)
                {
                    var authClaims = new List<Claim> { new Claim(ClaimTypes.Email, ObjLogin.EmailId) };
                    authClaims.Add(new Claim(ClaimTypes.Role, "Admin"));

                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                    string Secret = _configuration.GetValue<string>("JWT:Secret");
                    _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
                    string ValidAudience = _configuration.GetValue<string>("JWT:ValidAudience");
                    string ValidIssuer = _configuration.GetValue<string>("JWT:ValidIssuer");

                    var token = crendential.CreateToken(authClaims, Secret, tokenValidityInMinutes, ValidIssuer, ValidAudience);
                    var refreshToken = crendential.GenerateRefreshToken();

                    crendential.UpdateRefreshTokenBaseOnEmail(ObjLogin.EmailId, refreshToken, DateTime.Now.AddDays(refreshTokenValidityInDays));

                    return Ok(new
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        RefreshToken = refreshToken,
                        Expiration = token.ValidTo
                    });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(ClsTokenModel tokenModel)
        {
            string Secret = _configuration.GetValue<string>("JWT:Secret");
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            string ValidAudience = _configuration.GetValue<string>("JWT:ValidAudience");
            string ValidIssuer = _configuration.GetValue<string>("JWT:ValidIssuer");
            try
            {
                if (tokenModel is null)
                {
                    return BadRequest("Invalid client request");
                }

                string? accessToken = tokenModel.AccessToken;
                string? refreshToken = tokenModel.RefreshToken;

                var principal = crendential.GetPrincipalFromExpiredToken(accessToken, Secret);
                if (principal == null)
                {
                    return BadRequest("Invalid access token or refresh token");
                }
                string username = principal.Identity.Name;

                ClsUserLogin objLogin = crendential.GetUserDataBaseOnEmail(username);

                if (objLogin == null || objLogin.RefreshToken != refreshToken || objLogin.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    return BadRequest("Invalid access token or refresh token");
                }

                var newAccessToken = crendential.CreateToken(principal.Claims.ToList(), Secret, tokenValidityInMinutes, ValidIssuer, ValidAudience);
                var newRefreshToken = crendential.GenerateRefreshToken();

                crendential.UpdateRefreshTokenBaseOnEmail(objLogin.EmailId, refreshToken, DateTime.Now.AddDays(refreshTokenValidityInDays));

                return new ObjectResult(new
                {
                    accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    refreshToken = newRefreshToken
                });
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            try
            {
                ClsUserLogin objLogin = crendential.GetUserDataBaseOnEmail(username);
                if (objLogin == null) return BadRequest("Invalid user name");


                crendential.UpdateRefreshTokenBaseOnEmail(username, null, DateTime.Now.AddDays(refreshTokenValidityInDays));

                return NoContent();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
