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
                richTextBox1.Text += animeStack.Pop().Title + Environment.NewLine;
            }
        }

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
    }
}
