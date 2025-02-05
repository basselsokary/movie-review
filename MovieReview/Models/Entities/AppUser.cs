using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models.Entities;

public class AppUser : IdentityUser
{
    public string? FirstName { set; get; }

    public string? LastName { set; get; }

    public string? Country { get; set; }

    public string? ProfilePictureUrl { get; set; }
    
    public DateTime? DateOfBirth { get; set; }
    
    public List<Review> Reviews { get; set; }

    public List<Watchlist> Watchlists { get; set; }
}