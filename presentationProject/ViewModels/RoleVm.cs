namespace presentationProject.ViewModels
{
    public class RoleVm
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public RoleVm()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
