using Newtonsoft.Json;
using System;
using System.Timers;
using System.Collections;
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
        private static System.Timers.Timer aTimer;
        public Form1()
        {
            InitializeComponent();
        }
        StackBetter<Anime> animeStack = new StackBetter<Anime>();
        CoolerArrayList<Anime> animeArrayList = new CoolerArrayList<Anime>();

        private void Form1_Load(object sender, EventArgs e)
        {
            SortDeez<Anime> sortDeez = new SortDeez<Anime>();
            Func<Anime, IComparable> sortByEpisodeCount = (anime) => anime.Episodes;
            Func<Anime, IComparable> sortByReleaseDate = (anime) => anime.AiredDate;
            Func<Anime, IComparable> sortByTitle = (anime) => anime.Title;
            LoadJson("animeList.json");
            sortDeez.BubbleSort(animeStack, sortByReleaseDate);
            for (int i = 0; i < animeStack.Count; i++)
            {
<<<<<<< HEAD
                richTextBox1.Text += animeStack[i].Title + "\n";
            };
        }

        public static CoolerArrayList<Anime> ArrayListLoadJson(string path)
        {
            CoolerArrayList<Anime> animes = new CoolerArrayList<Anime>();
=======
                richTextBox1.Text += animeStack.Pop().Title + Environment.NewLine;
            }
        }

        public void LoadJson(string path)
        {
>>>>>>> 724e506412fef716600601e37240cb06843a875a
            dynamic items;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<dynamic>(json);
            }
            foreach (dynamic item in items)
            {
<<<<<<< HEAD
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
                animes.Add(anime);
=======
                Anime anime = new Anime
                {
                    Title = item.Title,
                    Link = item.Link,
                    Score = item.Score,
                    Type = item.Type,
                    Episodes = item.Episodes,
                    Source = item.Source,
                    Premiered = item.Premiered,
                    Studios = item.Studios,
                    Genres = item.Genres,
                    Themes = item.Themes,
                    Demographic = item.Demographic,
                    Duration = item.Duration,
                    AgeRating = item.AgeRating,
                    ReviewCount = item.ReviewCount,
                    Popularity = item.Popularity,
                    Members = item.Members,
                    Favorites = item.Favorites,
                    Adaptation = item.Adaptation,
                    Sequel = item.Sequel,
                    Prequel = item.Prequel,
                    Characters = item.Characters
                };
                anime.FixAiredDate(item.AiredDate.ToString());
                animeStack.Push(anime);
                animeArrayList.Add(anime);
>>>>>>> 724e506412fef716600601e37240cb06843a875a
            }
        }

        public static StackBetter<Anime> StackLoadJson(string path)
        public void LoadJson(string path)
        {
            dynamic items;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<dynamic>(json);
            }
            foreach (dynamic item in items)
            {
                Anime anime = new Anime
                {
                    Title = item.Title,
                    Link = item.Link,
                    Score = item.Score,
                    Type = item.Type,
                    Episodes = item.Episodes,
                    Source = item.Source,
                    Premiered = item.Premiered,
                    Studios = item.Studios,
                    Genres = item.Genres,
                    Themes = item.Themes,
                    Demographic = item.Demographic,
                    Duration = item.Duration,
                    AgeRating = item.AgeRating,
                    ReviewCount = item.ReviewCount,
                    Popularity = item.Popularity,
                    Members = item.Members,
                    Favorites = item.Favorites,
                    Adaptation = item.Adaptation,
                    Sequel = item.Sequel,
                    Prequel = item.Prequel,
                    Characters = item.Characters
                };
                anime.FixAiredDate(item.AiredDate.ToString());
                animeStack.Push(anime);
                animeArrayList.Add(anime);
            }
        }

        private static void SetTimer()
   {
        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(2000);
        // Hook up the Elapsed event for the timer. 
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }

    }
}
