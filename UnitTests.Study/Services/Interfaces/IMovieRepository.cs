using UnitTests.Study.Entities;

namespace UnitTests.Study.Services.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie?> GetMovieByIdAsync(int id);
    }
}
