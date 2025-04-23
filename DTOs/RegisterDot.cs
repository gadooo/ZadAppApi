namespace ZadGroceryAppApi.DTOs
{
    public class RegisterDot
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "User" or "Admin"
    }
}
