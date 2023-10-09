using UnitTests.Study.Entities;

namespace UnitTests.Study.Services.Interfaces
{
    public interface IMovieService
    {
        Task<Movie> GetMovieByIdAsync(int id);
    }
}
