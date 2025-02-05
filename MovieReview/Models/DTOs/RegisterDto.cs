using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MovieReview.Models.DTOs;

public class RegisterDto
{
    [Required]
    [StringLength(maximumLength: 30, MinimumLength = 5)]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(maximumLength: 100)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
