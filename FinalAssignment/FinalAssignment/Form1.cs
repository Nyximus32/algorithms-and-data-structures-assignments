using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalAssignment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SortDeez<Anime> sortDeez = new SortDeez<Anime>();
            Func<Anime, IComparable> sortByName = (anime) => anime.Episodes;
            var path = "animeList.json";
            StackBetter<Anime> animeStack = LoadJson(path);
            sortDeez.BubbleSort(animeStack, sortByName);
            for (int i = 0; i < animeStack.Count; i++)
            {
                Console.WriteLine(animeStack.Pop().Title);
            }
            /*StackBetter<string> animes1 = new StackBetter<string>();
            animes1.Push("asdas");
            animes1.Push("wasda");
            animes1.Push("sdasa");
            animes1.Push("asmas");
            animes1.Pop();*/
            //animes1.BubbleSort(sortByName);

        }

        public static StackBetter<Anime> LoadJson(string path)
        {
            StackBetter<Anime> animes = new StackBetter<Anime>();
            dynamic items;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<dynamic>(json);
            }
            foreach (dynamic item in items)
            {
                Anime anime = new Anime();
                anime.Title = item.Title;
                anime.Link = item.Link;
                anime.Score = item.Score;
                anime.Type = item.Type;
                anime.Episodes = item.Episodes;
                anime.Source = item.Source;
                anime.Premiered = item.Premiered;
                anime.AiredDate = item.AiredDate;
                anime.Studios = item.Studios;
                anime.Genres = item.Genres;
                anime.Themes = item.Themes;
                anime.Demographic = item.Demographic;
                anime.Duration = item.Duration;
                anime.AgeRating = item.AgeRating;
                anime.ReviewCount = item.ReviewCount;
                anime.Popularity = item.Popularity;
                anime.Members = item.Members;
                anime.Favorites = item.Favorites;
                anime.Adaptation = item.Adaptation;
                anime.Sequel = item.Sequel;
                anime.Prequel = item.Prequel;
                anime.Characters = item.Characters;
                animes.Push(anime);
            }
            return animes;
        }
    }
}
