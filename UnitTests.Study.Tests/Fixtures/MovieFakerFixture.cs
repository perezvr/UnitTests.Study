using UnitTests.Study.Entities;
using static UnitTests.Study.Entities.Movie;

namespace UnitTests.Study.Tests.Fixtures
{
    public class MovieFakerFixture
    {
        public Faker<Movie> GetMovieFaker()
            => new Faker<Movie>()
                .RuleFor(x => x.Id, f => f.Random.Int(min: 1))
                .RuleFor(x => x.Title, f => f.Person.FullName)
                .RuleFor(x => x.Director, f => f.Person.FullName)
                .RuleFor(x => x.Year, f => f.Random.Int(1920, 2023))
                .RuleFor(x => x.Genre, f => f.Random.Enum(MovieGenre.Other))
                .RuleFor(x => x.Rating, f => f.Random.Double(1, 10));
    }
}
