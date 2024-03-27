using Microsoft.AspNetCore.Identity;

namespace MusicStarStore.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
