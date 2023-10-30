using BSES.DocumentManagementSystem.Business.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BSES.DocumentManagementSystem.Controllers
{
    [Route("api/UserManagement")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        /// <summary>
        /// Internal Logger for this class instance.
        /// </summary>
        private readonly ILogger<UserManagementController> _logger;

        /// <summary>
        /// Local Instance for user management service.
        /// </summary>
        private readonly IUserManagementBA _userManagementBA;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userManagementBA"></param>
        public UserManagementController(ILogger<UserManagementController> logger, IUserManagementBA userManagementBA)
        {
            _logger = logger;
            _userManagementBA = userManagementBA;
        }

    }
}
