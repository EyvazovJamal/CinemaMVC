using System.Text.Json.Serialization;

namespace Cinema.Api.Response;

public class MovieApiResponse
{
    public bool Adult { get; init; }

    [JsonPropertyName("backdrop_path")]
    public string? BackdropPath { get; init; }

    [JsonPropertyName("genre_ids")]
    public List<int> GenreIds { get; init; } = new();

    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("original_language")]
    public string OriginalLanguage { get; init; } = string.Empty;

    [JsonPropertyName("original_title")]
    public string OriginalTitle { get; init; } = string.Empty;

    public string? Overview { get; init; }

    public double Popularity { get; init; }

    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; init; }

    [JsonPropertyName("release_date")]
    public DateTime? ReleaseDate { get; init; }

    public bool Softcore { get; init; }

    public bool Video { get; init; }

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; init; }

    [JsonPropertyName("vote_count")]
    public int VoteCount { get; init; }
}