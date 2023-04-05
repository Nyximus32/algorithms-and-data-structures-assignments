using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        SearchDeez<Anime> searchDeez = new SearchDeez<Anime>();
        SortDeez<Anime> sortDeez = new SortDeez<Anime>();
        Func<Anime, IComparable> sortByEpisodeCount = (anime) => anime.Episodes;
        Func<Anime, IComparable> sortByReleaseDate = (anime) => anime.AiredDate;
        Func<Anime, IComparable> sortByTitle = (anime) => anime.Title;

        private void Form1_Load(object sender, EventArgs e)
        {
            Anime testAnime = new Anime();
            testAnime.Title = "Mushishi";
            LoadJson("animeList.json");
            sortDeez.BubbleSort(animeStack, sortByEpisodeCount);
            //Anime animee = searchDeez.SearchFor<Anime>(animeArrayList, "Bleach", sortByTitle);
            //sortDeez.QuickSort(animeArrayList, 0, animeArrayList.Count() - 1, sortByEpisodeCount);
            //sortDeez.BubbleSort(animeArrayList, sortByTitle);
            string index = (searchDeez.binarySearch(animeArrayList, 0, animeArrayList.Count(), testAnime, sortByTitle)).ToString();
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
            string searchMethod = groupBox2.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            string searchfor = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            if (searchfor.Equals("Title"))
            {
                Anime animeToCheck = new Anime { Title = textBox1.Text };
                if (searchMethod.Equals("Linear search"))
                {
                    Anime foundAnime = searchDeez.SearchFor(animeArrayList, animeToCheck, sortByTitle);
                    if (foundAnime != null)
                    {
                        richTextBox1.Text = foundAnime.Title;
                    }
                }
                else if (searchMethod.Equals("Binary search"))
                {
                    animeArrayList = sortDeez.QuickSort(animeArrayList, 0, animeArrayList.Count() - 1, sortByTitle);
                    int index = searchDeez.binarySearch(animeArrayList, 0, animeArrayList.Count(), animeToCheck, sortByTitle);
                    if (index != -1)
                    {
                        richTextBox1.Text = animeArrayList[index].Title;
                    }
                }
                else
                {
                    MessageBox.Show("Select a searching algo");
                }
            }
            else if (searchfor.Equals("Episode count"))
            {
                Anime animeToCheck = new Anime { Episodes = int.Parse(textBox1.Text) };
                if (searchMethod.Equals("Linear search"))
                {
                    Anime foundAnime = searchDeez.SearchFor(animeArrayList, animeToCheck, sortByEpisodeCount);
                    if (foundAnime != null)
                    {
                        richTextBox1.Text = foundAnime.Title;
                    }
                }
                else if (searchMethod.Equals("Binary search"))
                {
                    animeArrayList = sortDeez.QuickSort(animeArrayList, 0, animeArrayList.Count() - 1, sortByEpisodeCount);
                    int index = searchDeez.binarySearch(animeArrayList, 0, animeArrayList.Count(), animeToCheck, sortByEpisodeCount);
                    if (index != -1)
                    {
                        richTextBox1.Text = animeArrayList[index].Title;
                    }
                }
                else
                {
                    MessageBox.Show("Select a searching algo");
                }
            }
            else if (searchfor.Equals("Release date"))
            {
                Anime animeToCheck = new Anime { AiredDate = Convert.ToDateTime(textBox1.Text) };
                if (searchMethod.Equals("Linear search"))
                {
                    Anime foundAnime = searchDeez.SearchFor(animeArrayList, animeToCheck, sortByReleaseDate);
                    if (foundAnime != null)
                    {
                        richTextBox1.Text = foundAnime.Title;
                    }
                }
                else if (searchMethod.Equals("Binary search"))
                {
                    int index = searchDeez.binarySearch(animeArrayList, 0, animeArrayList.Count(), animeToCheck, sortByReleaseDate);
                    if (index != -1)
                    {
                        richTextBox1.Text = animeArrayList[index].Title;
                        richTextBox1.Text = index.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Select a searching algo");
                }
            }
        }
    }
}
