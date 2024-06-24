using System.ComponentModel.DataAnnotations;

namespace presentationProject.ViewModels
{
	public class RegisterVm
	{
        public string FName { get; set; }
		public string LName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassowrd { get; set; }
        public bool Agree { get; set; }
    }
}
