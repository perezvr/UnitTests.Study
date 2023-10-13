using UnitTests.Study.Entities;
using UnitTests.Study.Services;
using UnitTests.Study.Services.Interfaces;
using UnitTests.Study.Tests.Fixtures;

namespace UnitTests.Study.Tests.Services
{
    public class MovieServiceTests : IClassFixture<MovieFakerFixture>
    {
        private readonly IRedisService _redisServiceFake;
        private readonly IMovieRepository _movieRepositoryFake;
        private readonly MovieFakerFixture _movieFakerFixture;

        public MovieServiceTests(MovieFakerFixture movieFakerFixture)
        {
            _redisServiceFake = A.Fake<IRedisService>();
            _movieRepositoryFake = A.Fake<IMovieRepository>();

            _movieFakerFixture = movieFakerFixture;
        }

        [Fact]
        public async Task GetMovieByIdAsync_Should_Return_Movie_From_Redis_If_Present()
        {
            // Arrange
            var expected = _movieFakerFixture.GetMovieFaker().Generate();

            A.CallTo(() => _redisServiceFake.GetMovieByIdAsync(A<int>.Ignored))
                .Returns(expected);

            var movieService = new MovieService(_redisServiceFake, _movieRepositoryFake);

            // Act
            var actual = await movieService.GetMovieByIdAsync(1);

            // Assert
            actual.Should().BeEquivalentTo(expected);
            A.CallTo(() => _redisServiceFake.GetMovieByIdAsync(A<int>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _movieRepositoryFake.GetMovieByIdAsync(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task GetMovieByIdAsync_Should_Return_Movie_From_Repository_If_Not_In_Redis()
        {
            // Arrange
            var expected = _movieFakerFixture.GetMovieFaker().Generate();

            A.CallTo(() => _redisServiceFake.GetMovieByIdAsync(A<int>.Ignored))
                .Returns<Movie?>(null);
            A.CallTo(() => _movieRepositoryFake.GetMovieByIdAsync(A<int>.Ignored))
                .Returns(expected);

            var movieService = new MovieService(_redisServiceFake, _movieRepositoryFake);

            // Act
            var actual = await movieService.GetMovieByIdAsync(1);

            // Assert
            actual.Should().BeEquivalentTo(expected);
            A.CallTo(() => _redisServiceFake.GetMovieByIdAsync(A<int>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _movieRepositoryFake.GetMovieByIdAsync(A<int>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetMovieByIdAsync_Should_Return_Null_If_Not_In_Redis_Nor_Repository()
        {
            // Arrange
            A.CallTo(() => _redisServiceFake.GetMovieByIdAsync(A<int>.Ignored)).Returns<Movie?>(null);
            A.CallTo(() => _movieRepositoryFake.GetMovieByIdAsync(A<int>.Ignored)).Returns<Movie?>(null);

            var movieService = new MovieService(_redisServiceFake, _movieRepositoryFake);

            // Act
            var result = await movieService.GetMovieByIdAsync(1);

            // Assert
            result.Should().BeNull();
            A.CallTo(() => _redisServiceFake.GetMovieByIdAsync(A<int>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _movieRepositoryFake.GetMovieByIdAsync(A<int>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}
