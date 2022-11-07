namespace STPP_Project.Auth.Model
{
    public class ProjectRoles
    {
        public const string Admin = nameof(Admin);
        public const string RegisteredUser = nameof(RegisteredUser);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, RegisteredUser };
    }
}
