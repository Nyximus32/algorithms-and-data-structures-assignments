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
        public DateTime AiredDate;
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


        public void FixAiredDate(string aired)
        {
            string actualAired = aired;
            if(aired.Length > 15)
            {
                actualAired = aired.Split(new string[] { "to" }, StringSplitOptions.None)[0];
            }

            AiredDate = DateTime.Parse(actualAired);
        }
        //Compares the premiered anime with eachother returns whichever is the later date
        

        

        public int CompareTo(Anime other)
        {
            throw new NotImplementedException();
        }
    }

}
