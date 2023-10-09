using FakeItEasy;
using UnitTests.Study.Entities;
using UnitTests.Study.Services;
using UnitTests.Study.Services.Interfaces;

namespace UnitTests.Study.Tests.Services
{
    public class MovieServiceTests
    {
        [Fact]
        public async Task GetMovieByIdAsync_Should_Return_Movie_From_Redis_If_Present()
        {
            // Arrange
            var redisService = A.Fake<IRedisService>();
            var movieRepository = A.Fake<IMovieRepository>();

            var movieInRedis = new Movie { Id = 1, Title = "Fake Movie" };
            A.CallTo(() => redisService.GetMovieByIdAsync(1)).Returns(movieInRedis);

            var movieService = new MovieService(redisService, movieRepository);

            // Act
            var result = await movieService.GetMovieByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Title.Should().Be("Fake Movie");
        }

        [Fact]
        public async Task GetMovieByIdAsync_Should_Return_Movie_From_Repository_If_Not_In_Redis()
        {
            // Arrange
            var redisService = A.Fake<IRedisService>();
            var movieRepository = A.Fake<IMovieRepository>();

            A.CallTo(() => redisService.GetMovieByIdAsync(1)).Returns<Movie>(null);

            var movieInRepository = new Movie { Id = 1, Title = "Real Movie" };
            A.CallTo(() => movieRepository.GetMovieByIdAsync(1)).Returns(movieInRepository);

            var movieService = new MovieService(redisService, movieRepository);

            // Act
            var result = await movieService.GetMovieByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Title.Should().Be("Real Movie");
        }

        [Fact]
        public async Task GetMovieByIdAsync_Should_Return_Null_If_Not_In_Redis_Nor_Repository()
        {
            // Arrange
            var redisService = A.Fake<IRedisService>();
            var movieRepository = A.Fake<IMovieRepository>();

            A.CallTo(() => redisService.GetMovieByIdAsync(1)).Returns<Movie>(null);
            A.CallTo(() => movieRepository.GetMovieByIdAsync(1)).Returns<Movie>(null);

            var movieService = new MovieService(redisService, movieRepository);

            // Act
            var result = await movieService.GetMovieByIdAsync(1);

            // Assert
            result.Should().BeNull();
        }
    }
}
