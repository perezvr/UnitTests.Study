using UnitTests.Study.Entities;
using static UnitTests.Study.Entities.Movie;

namespace UnitTests.Study.Tests.Entities
{
    public class MovieTests
    {
        private readonly Faker<Movie> _movieFaker;

        public MovieTests()
        {
            _movieFaker = new Faker<Movie>()
                .RuleFor(x => x.Id, f => f.Random.Int(min: 1))
                .RuleFor(x => x.Title, f => f.Person.FullName)
                .RuleFor(x => x.Director, f => f.Person.FullName)
                .RuleFor(x => x.Year, f => f.Random.Int(1920, 2023))
                .RuleFor(x => x.Genre, f => f.Random.Enum(MovieGenre.Other))
                .RuleFor(x => x.Rating, f => f.Random.Double(1, 10));
        }

        [Fact]
        public void Movie_should_be_Valid()
        {
            // Act
            var movies = _movieFaker.Generate(50);

            // Assert
            foreach (var movie in movies)
            {
                movie.Should().NotBeNull(); // Verifica se o objeto movie não é nulo
                movie.Id.Should().BeGreaterThan(0); // Verifica se o Id é maior que zero
                movie.Title.Should().NotBeNullOrEmpty(); // Verifica se o título não é nulo ou vazio
                movie.Director.Should().NotBeNullOrEmpty(); // Verifica se o diretor não é nulo ou vazio
                movie.Year.Should().BeInRange(1920, 2023); // Verifica se o ano está no intervalo esperado
                movie.Genre.Should().BeDefined(); // Verifica se o gênero está definido como MovieGenre
                movie.Genre.Should().NotBe(MovieGenre.Other); // Verifica se o gênero não é "Other"
                movie.Rating.Should().BeGreaterOrEqualTo(1).And.BeLessOrEqualTo(10); // Verifica se a avaliação está dentro do intervalo esperado
            }
        }
    }
}
