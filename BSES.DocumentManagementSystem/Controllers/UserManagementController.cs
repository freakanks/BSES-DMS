using BSES.DocumentManagementSystem.Business.Contracts;
using BSES.DocumentManagementSystem.Common;
using BSES.DocumentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BSES.DocumentManagementSystem.Controllers
{
    [Route("api/UserManagement")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        /// <summary>
        /// Creates a new JWT token base on the configured symmetric keys.
        /// </summary>
        /// <returns></returns>
        private string GetNewToken(ReadOnlySpan<char> userName)
        {
            var issuer = _configuration.GetValue<string>(DMSConstants.JWT_ISSUER_CONFIG_KEY);
            var audience = _configuration.GetValue<string>(DMSConstants.JWT_AUDIENCE_CONFIG_KEY);
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>(DMSConstants.JWT_SECRET_KEY_CONFIG_KEY)!);

            var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

            var subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, $"{userName}")
            });
            var expires = DateTime.Now.AddHours(2);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = subject,
                SigningCredentials = signinCredentials,
                Issuer = issuer,
                Audience = audience,
                Expires = expires
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        /// <summary>
        /// Local instance for the Encryption/Decryption.
        /// </summary>
        private readonly IEncryptorDecryptorBA _encryptionService;
        /// <summary>
        /// Internal Logger for this class instance.
        /// </summary>
        private readonly ILogger<UserManagementController> _logger;

        /// <summary>
        /// Local Instance for user management service.
        /// </summary>
        private readonly IUserManagementBA _userManagementBA;

        /// <summary>
        /// Readonly instance of configuration.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userManagementBA"></param> 
        /// <param name="encryptionService"></param>
        /// <param name="configuration"></param>
        public UserManagementController(ILogger<UserManagementController> logger, IUserManagementBA userManagementBA, IEncryptorDecryptorBA encryptionService, IConfiguration configuration)
        {
            _logger = logger;
            _userManagementBA = userManagementBA;
            _encryptionService = encryptionService;
            _configuration = configuration;
        }

        /// <summary>
        /// API to get JWT token for the session.
        /// <para>Authenticates the user with the passed credentials and provide a session token with a validity of time.</para>
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("GetToken")]
        public async Task<IActionResult> GetToken([FromBody] UserModel userModel, CancellationToken cancellationToken)
        {
            try
            {
                string decryptedData = _encryptionService.Decrypt(userModel.Credentials, userModel.CompanyCode);
                string[] userData = decryptedData.Split(DMSConstants.DATA_DELIMITER);
                if (userData.Length < 3)
                    return new BadRequestObjectResult($"Invalid credentials passed for the system.");

                var userEntity = await _userManagementBA.AuthenticateDocumentUserAsync(userData[0], userData[1], cancellationToken);
                if (userEntity?.IsAuthenticated ?? true)
                    return new BadRequestObjectResult($"Invalid credentials passed for the system.");

                string token = GetNewToken(userData[0]);

                return new OkObjectResult(token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }
            return new BadRequestObjectResult("Something went wrong while getting the Token. Kindly get in touch with System Administrator.");
        }
    }
}
