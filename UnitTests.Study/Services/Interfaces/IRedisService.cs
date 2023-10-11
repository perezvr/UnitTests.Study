using UnitTests.Study.Entities;

namespace UnitTests.Study.Services.Interfaces
{
    public interface IRedisService
    {
        Task<Movie?> GetMovieByIdAsync(int id);
        Task AddMovieToCacheAsync(Movie movie);
    }
}
