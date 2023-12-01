using System.ComponentModel.DataAnnotations;

namespace BSES.DocumentManagementSystem.Models
{
    public class UserModel
    {
        public string CompanyCode { get; set; }
        public string Credentials { get; set; }
    }
    public class CreateUserModel
    {
        [Required(AllowEmptyStrings = false)]
        public string CompanyCode { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
