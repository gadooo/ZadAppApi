using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public int? PhoneNumber  { get; set; }
}
