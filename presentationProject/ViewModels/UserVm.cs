﻿namespace presentationProject.ViewModels
{
	public class UserVm
	{
        public string Id { get; set; }
		public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
