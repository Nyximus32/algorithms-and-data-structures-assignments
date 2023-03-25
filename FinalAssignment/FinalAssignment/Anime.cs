using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment
{
    public class Anime : IComparable<Anime>
    {
        public string Title; //
        public string Link;
        public double Score; //
        public string Type;
        public int Episodes;
        public string Source;
        public string Premiered; //might need a struct //
        public string AiredDate;
        public string Studios;
        public string Genres;
        public string Themes;
        public string Demographic;
        public string Duration;
        public string AgeRating; //doable with finesse
        public double ReviewCount;
        public int Popularity;
        public string Members; //parse to int
        public string Favorites; //parse to int
        public string Adaptation;
        public string Sequel;
        public string Prequel;
        public string Characters;

        //Compares the premiered anime with eachother returns whichever is the later date
        private class AnimePremieredComparer : IComparer<Anime>
        {
            public int Compare(Anime x, Anime y)
            {
                string[] xPremiered = x.Premiered.Split(' ');
                string[] yPremiered = y.Premiered.Split(' ');

                //seasons
                int xSeason = 0;
                int ySeason = 0;

                //years
                int xYear = int.Parse(xPremiered[1]);
                int yYear = int.Parse(yPremiered[1]);

                //gives a number for each season for x and y
                switch (xPremiered[0])
                {
                    case "Winter":
                        xSeason = 1;
                        break;
                    case "Spring":
                        xSeason = 2;
                        break;
                    case "Summer":
                        xSeason = 3;
                        break;
                    case "Fall":
                        xSeason = 4;
                        break;
                    default:
                        xSeason = 0;
                        break;
                }

                switch (yPremiered[0])
                {
                    case "Winter":
                        ySeason = 1;
                        break;
                    case "Spring":
                        ySeason = 2;
                        break;
                    case "Summer":
                        ySeason = 3;
                        break;
                    case "Fall":
                        ySeason = 4;
                        break;
                    default:
                        ySeason = 0;
                        break;
                }

                //if years are equal
                if (xYear == yYear)
                {
                    //check to see which season comes first
                    if(xSeason > ySeason)
                    {
                        return 1;
                    } else if (xSeason < ySeason)
                    {
                        return -1;
                    } else
                    {
                        return 0;
                    }
                } else if (xYear > yYear)
                {
                    return 1;
                } else
                {
                    return -1;
                }
            }
        }

        public int CompareTo(Anime other)
        {
            throw new NotImplementedException();
        }
    }

}
