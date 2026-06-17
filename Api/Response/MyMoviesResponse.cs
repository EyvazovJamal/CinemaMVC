namespace Cinema.Api.Response;

public class MyMoviesResponse
{
    public string id { get; set; }
    public bool adult { get; set; }
    public string backdropPath { get; set; }
    public string title { get; set; }
    public string originalLanguage { get; set; }
    public string originalTitle { get; set; }
    public string overview { get; set; }
    public string posterPath { get; set; }
    public DateTime releaseDate { get; set; }
    public double voteAverage { get; set; }
    public int voteCount { get; set; }
    public int runtime { get; set; }
}