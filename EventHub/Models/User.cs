using Microsoft.AspNetCore.Identity;
using System.Runtime.Intrinsics.X86;

namespace EventHub.Models
{
    public class User : IdentityUser
    {
        // From IdentityUser:
        // string Id (GUID as string)
        // string UserName
        // string Email
        // string PasswordHash

        public ICollection<Booking> Bookings { get; set; } = []; //ordinary user
        public ICollection<Event> OrganizedEvents { get; set; } = []; //organizer
        public ICollection<Venue> CreatedVenues { get; set; } = []; //admin
    }
}
