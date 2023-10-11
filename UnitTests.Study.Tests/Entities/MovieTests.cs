using UnitTests.Study.Entities;
using UnitTests.Study.Tests.Fixtures;
using static UnitTests.Study.Entities.Movie;

namespace UnitTests.Study.Tests.Entities
{
    public class MovieTests : IClassFixture<MovieFakerFixture>
    {
        private readonly Faker<Movie> _movieFaker;

        public MovieTests(MovieFakerFixture movieFakerFixture)
        {
            _movieFaker = movieFakerFixture.GetMovieFaker();
        }

        [Fact]
        public void Movies_should_be_Valid()
        {
            // Act
            var movies = _movieFaker.Generate(50);

            // Assert
            foreach (var movie in movies)
            {
                movie.Should().NotBeNull(); 
                movie.Id.Should().BeGreaterThan(0); 
                movie.Title.Should().NotBeNullOrEmpty(); 
                movie.Director.Should().NotBeNullOrEmpty();
                movie.Year.Should().BeInRange(1920, 2023); 
                movie.Genre.Should().BeDefined(); 
                movie.Genre.Should().NotBe(MovieGenre.Other);
                movie.Rating.Should().BeGreaterOrEqualTo(1).And.BeLessOrEqualTo(10);
            }
        }
    }
}
