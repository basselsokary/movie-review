using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models.Entities;

public class Genres
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}

    //Action = 1,
    //Comedy,
    //Crime,
    //Drama,
    //Documentary,
    //Cartoon,
    //Horror