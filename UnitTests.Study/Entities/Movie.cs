using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Study.Entities
{
    public class Movie
    {
        public int Id { get; set; }            
        public string Title { get; set; }      
        public string Director { get; set; }   
        public int Year { get; set; }          
        public MovieGenre Genre { get; set; }      
        public double Rating { get; set; }

        public enum MovieGenre
        {
            Action,
            Adventure,
            Animation,
            Comedy,
            Crime,
            Drama,
            Fantasy,
            Horror,
            Mystery,
            Romance,
            SciFi,
            Thriller,
            Western,
            Other
        }
    }
}
