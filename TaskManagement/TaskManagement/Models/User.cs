namespace TaskManagementAPI.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;

        private User() { } // For EF Core

        public static User Create(string username, string email, string passwordHash)
        {
            return new User
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHash
            };
        }
    }
}