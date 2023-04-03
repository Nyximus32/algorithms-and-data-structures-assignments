using Newtonsoft.Json;
using System;
using System.Diagnostics;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        SearchDeez searchDeez = new SearchDeez();
        SortDeez<Anime> sortDeez = new SortDeez<Anime>();
        Func<Anime, IComparable> sortByEpisodeCount = (anime) => anime.Episodes;
        Func<Anime, IComparable> sortByReleaseDate = (anime) => anime.AiredDate;
        Func<Anime, IComparable> sortByTitle = (anime) => anime.Title;

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadJson("animeList.json");
            sortDeez.BubbleSort(animeStack, sortByEpisodeCount);
            //Anime animee = searchDeez.SearchFor<Anime>(animeArrayList, "Bleach", sortByTitle);
            //sortDeez.QuickSort(animeArrayList, 0, animeArrayList.Count() - 1, sortByEpisodeCount);
            //sortDeez.BubbleSort(animeArrayList, sortByTitle);
            searchDeez.binarySearch(arrayList, 0, arrayList.Count(), )
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

        private void displayArrList_Click(object sender, EventArgs e)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            richTextBox1.Text = "";
            for (int i = 0; i < animeArrayList.Count(); i++)
            {
                richTextBox1.Text += animeArrayList[i].Title + "\n";
            }
            stopwatch.Stop();
            long elapsed_time = stopwatch.ElapsedMilliseconds;
            label2.Text = elapsed_time.ToString() + " miliseconds";
        }

        private void dispayStack_Click(object sender, EventArgs e)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            richTextBox1.Text = "";
            while (animeStack.Count > 0)
            {
                richTextBox1.Text += animeStack.Pop().Title + "\n";
            }
            stopwatch.Stop();
            long elapsed_time = stopwatch.ElapsedMilliseconds;
            label2.Text = elapsed_time.ToString() + " miliseconds";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchfor = groupBox1.Controls.OfType<System.Windows.Forms.RadioButton>().FirstOrDefault(r => r.Checked).Text;
            if (searchfor.Equals("Title"))
            {
                Anime animeToCheck = new Anime { Title = textBox1.Text };
                Anime foundAnime = searchDeez.SearchFor<Anime>(animeArrayList, animeToCheck, sortByTitle);
                if (foundAnime != null)
                {
                    richTextBox1.Text = foundAnime.Title;
                }
            }
            else if (searchfor.Equals("Episode count"))
            {
                Anime animeToCheck = new Anime { Episodes = int.Parse(textBox1.Text) };
                Anime foundAnime = searchDeez.SearchFor<Anime>(animeArrayList, animeToCheck, sortByEpisodeCount);
                if (foundAnime != null)
                {
                    richTextBox1.Text = foundAnime.Title;
                }
            }
            else if (searchfor.Equals("Release date"))
            {
                Anime animeToCheck = new Anime { AiredDate = Convert.ToDateTime(textBox1.Text) };
                Anime foundAnime = searchDeez.SearchFor<Anime>(animeArrayList, animeToCheck, sortByReleaseDate);
                if (foundAnime != null)
                {
                    richTextBox1.Text = foundAnime.Title;
                }
            }
        }
    }
}
