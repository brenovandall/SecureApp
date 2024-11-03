namespace Movies.API.Models;

public class Movie
{
    public Movie(
        int id,
        string title,
        string genre,
        string rating,
        DateTime releaseDate,
        string imageUrl,
        string owner)
    {
        Id = id;
        Title = title;
        Genre = genre;
        Rating = rating;
        ReleaseDate = releaseDate;
        ImageUrl = imageUrl;
        Owner = owner;
    }

    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Genre { get; set; } = default!;
    public string Rating { get; set; } = default!;
    public DateTime ReleaseDate { get; set; }
    public string ImageUrl { get; set; } = default!;
    public string Owner { get; set; } = default!;
}
