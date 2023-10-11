using UnitTests.Study.Entities;
using UnitTests.Study.Services.Interfaces;

namespace UnitTests.Study.Services
{
    public class MovieService
    {
        private readonly IRedisService _redisService;
        private readonly IMovieRepository _movieRepository;

        public MovieService(IRedisService redisService, IMovieRepository movieRepository)
        {
            _redisService = redisService;
            _movieRepository = movieRepository;
        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            var movieFromRedis = await _redisService.GetMovieByIdAsync(id);

            if (movieFromRedis is not null)
                return movieFromRedis;

            var movieFromRepo = await _movieRepository.GetMovieByIdAsync(id);

            if (movieFromRepo is not null)
            {
                await _redisService.AddMovieToCacheAsync(movieFromRepo);
                return movieFromRepo;
            }

            return null;
        }
    }
}
