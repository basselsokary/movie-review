﻿namespace MovieReview.Models.Entities;

public class MovieActor
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public int ActorId { get; set; }
    public Actor Actor { get; set; }

    public string? CharacterName { get; set; }
}
