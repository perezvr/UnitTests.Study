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

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            // Tente buscar o filme no Redis pelo ID
            var movieFromRedis = await _redisService.GetMovieByIdAsync(id);

            if (movieFromRedis != null)
            {
                return movieFromRedis;
            }
            else
            {
                // Se não encontrou no Redis, procure na MovieRepository
                var movieFromRepo = await _movieRepository.GetMovieByIdAsync(id);
                if (movieFromRepo != null)
                {
                    // Adicione o filme encontrado ao Redis para cache
                    await _redisService.AddMovieToCacheAsync(movieFromRepo);
                    return movieFromRepo;
                }
                else
                {
                    // O filme não foi encontrado em nenhum lugar
                    return null;
                }
            }
        }
    }
}
