using System.ComponentModel.DataAnnotations;

namespace presentationProject.ViewModels
{
	public class LoginVm
	{
		[EmailAddress]
		[Required]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
