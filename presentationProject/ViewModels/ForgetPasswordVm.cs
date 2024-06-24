using System.ComponentModel.DataAnnotations;

namespace presentationProject.ViewModels
{
	public class ForgetPasswordVm
	{
        [EmailAddress]
        public string Email { get; set; }
    }
}
