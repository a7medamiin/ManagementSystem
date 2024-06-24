using System.ComponentModel.DataAnnotations;

namespace presentationProject.ViewModels
{
	public class ResetPasswordVm
	{
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare("Password")]
		public string ConfirmPassowrd { get; set; }
	}
}
