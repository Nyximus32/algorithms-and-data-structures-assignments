using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace FinalAssignment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //variables
        Stopwatch stopwatch = new Stopwatch();
        StackBetter<Anime> animeStack = new StackBetter<Anime>();
        CoolerArrayList<Anime> animeArrayList = new CoolerArrayList<Anime>();
        LinkedListBeyond<Anime> animeLinkedList = new LinkedListBeyond<Anime>();
        SearchDeez<Anime> searchDeez = new SearchDeez<Anime>();
        SortDeez<Anime> sortDeez = new SortDeez<Anime>();
        Func<Anime, IComparable> CompareEpisodeCount = (anime) => anime.Episodes;
        Func<Anime, IComparable> CompareReleaseDate = (anime) => anime.AiredDate;
        Func<Anime, IComparable> CompareTitle = (anime) => anime.Title;

        private void Form1_Load(object sender, EventArgs e)
        {


            //animeLinkedList = sortDeez.QuickSort(animeLinkedList, sortByTitle);
            //sortDeez.BubbleSort(animeStack, sortByEpisodeCount);
            //Anime animee = searchDeez.SearchFor<Anime>(animeArrayList, "Bleach", sortByTitle);
            //sortDeez.QuickSort(animeArrayList, 0, animeArrayList.Count() - 1, sortByEpisodeCount);
            //sortDeez.BubbleSort(animeArrayList, sortByTitle);
        }
        public void LoadJson(string path)
        {
            dynamic items;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<dynamic>(json);
            }
            //int counter = 0;
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
                animeLinkedList.Add(anime);
            }
        }

        private void displayArrList_Click(object sender, EventArgs e)
        {
            stopwatch.Restart();
            DisplayArrayList();
            stopwatch.Stop();
            GetElapsedTime();
        }

        public void DisplayArrayList()
        {
            richTextBox1.Text = "";
            for (int i = 0; i < animeArrayList.Count(); i++)
            {
                richTextBox1.Text += animeArrayList[i].Title + "\n";
            }
        }

        private void displayStack_Click(object sender, EventArgs e)
        {
            stopwatch.Restart();
            DisplayStack();
            stopwatch.Stop();
            GetElapsedTime();
        }

        private void DisplayStack()
        {
            StackBetter<Anime> copyStack = (StackBetter<Anime>)animeStack.Clone();
            richTextBox1.Text = "";
            while (copyStack.Count > 0)
            {
                richTextBox1.Text += copyStack.Pop().Title + "\n";
            }
        }

        private void displayLinkList_Click(object sender, EventArgs e)
        {
            stopwatch.Restart();
            DisplayLinkedList();
            stopwatch.Stop();
            GetElapsedTime();
        }

        private void DisplayLinkedList()
        {
            richTextBox1.Text = "";
            LinkedListBeyond<Anime>.Node currentNode = animeLinkedList.GetHead();
            while (currentNode != null)
            {
                richTextBox1.Text += currentNode.Value.Title.ToString() + Environment.NewLine;
                currentNode = currentNode.Next;
            }
        }

        private void GetElapsedTime()
        {
            double elapsed_time = stopwatch.ElapsedMilliseconds;
            elapsed_time = elapsed_time / 1000;
            label1.Text = "Elapsed time: " + elapsed_time.ToString("F1") + " seconds";
        }

        //searches through the structure for a specific thing
        private void searchButton(object sender, EventArgs e)
        {
            string searchMethod;
            string searchFor;
            string searchStructure;
            try
            { 
                //the method that will be used to search, and the thing we are looking for
                searchMethod = groupBox2.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
                searchFor = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
                searchStructure = groupBox5.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            } catch
            {
                MessageBox.Show("Please select all the options");
                return;
            }

            if (searchStructure.Equals("ArrayList"))
            {
                //TITLE
                if (searchFor.Equals("Title"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { Title = textBox1.Text };
                    sortDeez.BubbleSort(animeArrayList, CompareTitle);
                    if (searchMethod.Equals("Linear search"))
                    {
                        stopwatch.Restart();
                        int foundAnimeIndex = searchDeez.LinearSearch(animeArrayList, animeToCheck, CompareTitle);
                        stopwatch.Stop();
                        if (foundAnimeIndex != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + foundAnimeIndex.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        stopwatch.Restart();
                        int index = searchDeez.BinarySearch(animeArrayList, 0, animeArrayList.Count(), animeToCheck, CompareTitle);
                        stopwatch.Stop();
                        if (index != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + index.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a searching algo");
                    }
                }
                //EPISODE COUNT
                else if (searchFor.Equals("Episode count"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { Episodes = int.Parse(textBox1.Text) };
                    sortDeez.BubbleSort(animeArrayList, CompareEpisodeCount);
                    if (searchMethod.Equals("Linear search"))
                    {
                        stopwatch.Restart();
                        int foundAnimeIndex = searchDeez.LinearSearch(animeArrayList, animeToCheck, CompareEpisodeCount);
                        stopwatch.Stop();
                        if (foundAnimeIndex != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + foundAnimeIndex.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        stopwatch.Restart();
                        int foundAnimeIndex = searchDeez.BinarySearch(animeArrayList, 0, animeArrayList.Count(), animeToCheck, CompareEpisodeCount);
                        stopwatch.Stop();
                        if (foundAnimeIndex != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + foundAnimeIndex.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a searching algo");
                    }
                }
                //RELEASE DATE
                else if (searchFor.Equals("Release date"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { AiredDate = DateTime.Parse(textBox1.Text) };
                    sortDeez.BubbleSort(animeArrayList, CompareReleaseDate);
                    if (searchMethod.Equals("Linear search"))
                    {
                        stopwatch.Restart();
                        int foundAnimeIndex = searchDeez.LinearSearch(animeArrayList, animeToCheck, CompareReleaseDate);
                        stopwatch.Stop();
                        if (foundAnimeIndex != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + foundAnimeIndex.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        stopwatch.Restart();
                        int index = searchDeez.BinarySearch(animeArrayList, 0, animeArrayList.Count(), animeToCheck, CompareReleaseDate);
                        stopwatch.Stop();
                        if (index != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + index.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a searching algo");
                    }
                }
            }
            else if (searchStructure.Equals("LinkedList"))
            {
                if (searchFor.Equals("Title"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { Title = textBox1.Text };
                    sortDeez.BubbleSort(animeLinkedList, CompareTitle);
                    if (searchMethod.Equals("Linear search"))
                    {
                        stopwatch.Restart();
                        int foundAnimeIndex = searchDeez.LinearSearch(animeLinkedList, animeToCheck, CompareTitle);
                        stopwatch.Stop();
                        if (foundAnimeIndex != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + foundAnimeIndex.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        stopwatch.Restart();
                        int index = searchDeez.BinarySearch(animeLinkedList, 0, animeLinkedList.Count, animeToCheck, CompareTitle);
                        stopwatch.Stop();
                        if (index != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + index.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a searching algo");
                    }
                }
                //EPISODE COUNT
                else if (searchFor.Equals("Episode count"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { Episodes = int.Parse(textBox1.Text) };
                    sortDeez.BubbleSort(animeLinkedList, CompareEpisodeCount);
                    if (searchMethod.Equals("Linear search"))
                    {
                        stopwatch.Restart();
                        int foundAnimeIndex = searchDeez.LinearSearch(animeLinkedList, animeToCheck, CompareEpisodeCount);
                        stopwatch.Stop();
                        if (foundAnimeIndex != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + foundAnimeIndex.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        stopwatch.Restart();
                        int index = searchDeez.BinarySearch(animeLinkedList, 0, animeLinkedList.Count, animeToCheck, CompareEpisodeCount);
                        stopwatch.Stop();
                        if (index != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + index.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a searching algo");
                    }
                }
                //RELEASE DATE
                else if (searchFor.Equals("Release date"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { AiredDate = DateTime.Parse(textBox1.Text) };
                    sortDeez.BubbleSort(animeLinkedList, CompareReleaseDate);
                    if (searchMethod.Equals("Linear search"))
                    {
                        stopwatch.Restart();
                        int foundAnimeIndex = searchDeez.LinearSearch(animeLinkedList, animeToCheck, CompareReleaseDate);
                        stopwatch.Stop();
                        if (foundAnimeIndex != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + foundAnimeIndex.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        stopwatch.Restart();
                        int index = searchDeez.BinarySearch(animeLinkedList, 0, animeLinkedList.Count, animeToCheck, CompareReleaseDate);
                        stopwatch.Stop();
                        if (index != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + index.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a searching algo");
                    }
                }
            }
            else if (searchStructure.Equals("Stack"))
            {
                StackBetter<Anime> copyStack = (StackBetter<Anime>)animeStack.Clone();
                //TITLE
                if (searchFor.Equals("Title"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { Title = textBox1.Text };
                    sortDeez.BubbleSort(copyStack, CompareTitle);
                    if (searchMethod.Equals("Linear search"))
                    {
                        stopwatch.Restart();
                        int foundAnimeIndex = searchDeez.LinearSearch(copyStack, animeToCheck, CompareTitle);
                        stopwatch.Stop();
                        if (foundAnimeIndex != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + foundAnimeIndex.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        stopwatch.Restart();
                        int index = searchDeez.BinarySearch(copyStack, 0, copyStack.Count, animeToCheck, CompareTitle);
                        stopwatch.Stop();
                        if (index != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + index.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a searching algo");
                    }
                }
                //EPISODE COUNT
                else if (searchFor.Equals("Episode count"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { Episodes = int.Parse(textBox1.Text) };
                    sortDeez.BubbleSort(copyStack, CompareEpisodeCount);
                    if (searchMethod.Equals("Linear search"))
                    {
                        stopwatch.Restart();
                        int foundAnimeIndex = searchDeez.LinearSearch(copyStack, animeToCheck, CompareEpisodeCount);
                        stopwatch.Stop();
                        if (foundAnimeIndex != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + foundAnimeIndex.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        stopwatch.Restart();
                        int index = searchDeez.BinarySearch(copyStack, 0, copyStack.Count, animeToCheck, CompareEpisodeCount);
                        stopwatch.Stop();
                        if (index != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + index.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a searching algo");
                    }
                }
                //RELEASE DATE
                else if (searchFor.Equals("Release date"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { AiredDate = DateTime.Parse(textBox1.Text) };
                    sortDeez.BubbleSort(copyStack, CompareReleaseDate);
                    if (searchMethod.Equals("Linear search"))
                    {
                        stopwatch.Restart();
                        int foundAnimeIndex = searchDeez.LinearSearch(copyStack, animeToCheck, CompareReleaseDate);
                        stopwatch.Stop();
                        if (foundAnimeIndex != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + foundAnimeIndex.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        stopwatch.Restart();
                        int index = searchDeez.BinarySearch(copyStack, 0, animeStack.Count, animeToCheck, CompareReleaseDate);
                        stopwatch.Stop();
                        if (index != -1)
                        {
                            richTextBox1.Text = "Anime was found at index: " + index.ToString();
                            GetElapsedTime();
                        }
                        else
                        {
                            richTextBox1.Text = "Anime doesn't exist";
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a searching algo");
                    }
                }
            }
            else if (searchStructure.Equals("LinkedList"))
            {

            }

        }

        private void SortButton(object sender, EventArgs e)
        {
            string sortMethod;
            string sortfor;
            string searchStructure;

            //checks if they are all filled in else stop the method
            try
            {
                //the method that will be used to search, and the thing we are looking for
                sortMethod = groupBox3.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
                sortfor = groupBox4.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
                searchStructure = groupBox5.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            } catch
            {
                MessageBox.Show("Select one of each option");
                return;
            }

            if (searchStructure.Equals("ArrayList"))
            {
                //TITLE
                if (sortfor.Equals("Title"))
                {
                    //BUBBLE SORT
                    if (sortMethod.Equals("Bubble sort"))
                    {
                        if (animeArrayList != null)
                        {
                            stopwatch.Restart();
                            sortDeez.BubbleSort(animeArrayList, CompareTitle);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayArrayList();
                        }
                        else
                        {
                            MessageBox.Show("Arraylist is null");
                        }
                    }
                    //QUICK SORT
                    else if (sortMethod.Equals("Quick sort"))
                    {
                        if (animeArrayList != null)
                        {
                            stopwatch.Restart();
                            animeArrayList = sortDeez.QuickSort(animeArrayList, 0, animeArrayList.Count() - 1, CompareTitle);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayArrayList();
                        }
                        else
                        {
                            MessageBox.Show("Arraylist is null");
                        }
                    }
                    //NO SORT METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a sorting algo");
                    }
                }
                //EPISODE COUNT
                else if (sortfor.Equals("Episode count"))
                {
                    //BUBBLE SORT
                    //Anime animeToCheck = new Anime { Episodes = int.Parse(textBox1.Text) };
                    if (sortMethod.Equals("Bubble sort"))
                    {
                        if (animeArrayList != null)
                        {
                            stopwatch.Restart();
                            sortDeez.BubbleSort(animeArrayList, CompareEpisodeCount);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayArrayList();
                        }
                        else
                        {
                            MessageBox.Show("Arraylist is null");
                        }
                    }
                    //QUICK SORT
                    else if (sortMethod.Equals("Quick sort"))
                    {
                        if (animeArrayList != null)
                        {
                            stopwatch.Restart();
                            animeArrayList = sortDeez.QuickSort(animeArrayList, 0, animeArrayList.Count() - 1, CompareEpisodeCount);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayArrayList();
                        }
                        else
                        {
                            MessageBox.Show("Arraylist is null");
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a sorting algo");
                    }
                }
                //RELEASE DATE
                else if (sortfor.Equals("Release date"))
                {
                    //BUBBLE SORT
                    //Anime animeToCheck = new Anime { AiredDate = Convert.ToDateTime(textBox1.Text) };
                    if (sortMethod.Equals("Bubble sort"))
                    {
                        if (animeArrayList != null)
                        {
                            stopwatch.Restart();
                            sortDeez.BubbleSort(animeArrayList, CompareReleaseDate);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayArrayList();
                        }
                        else
                        {
                            MessageBox.Show("Arraylist is null");
                        }
                    }
                    //QUICK SORT
                    else if (sortMethod.Equals("Quick sort"))
                    {
                        if (animeArrayList != null)
                        {
                            stopwatch.Restart();
                            animeArrayList = sortDeez.QuickSort(animeArrayList, 0, animeArrayList.Count() - 1, CompareReleaseDate);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayArrayList();
                        }
                        else
                        {
                            MessageBox.Show("Arraylist is null");
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a sorting algo");
                    }
                }
            }
            else if (searchStructure.Equals("Stack"))
            {
                //TITLE
                if (sortfor.Equals("Title"))
                {
                    //BUBBLE SORT
                    if (sortMethod.Equals("Bubble sort"))
                    {
                        if (animeStack != null)
                        {
                            stopwatch.Restart();
                            sortDeez.BubbleSort(animeStack, CompareTitle);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayStack();
                        }
                        else
                        {
                            MessageBox.Show("Stack is null");
                        }
                    }
                    //QUICK SORT
                    else if (sortMethod.Equals("Quick sort"))
                    {
                        if (animeStack != null)
                        {
                            stopwatch.Restart();
                            animeStack = sortDeez.QuickSort(animeStack, CompareTitle);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayStack();
                        }
                        else
                        {
                            MessageBox.Show("Stack is null");
                        }
                    }
                    //NO SORT METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a sorting algo");
                    }
                }
                //EPISODE COUNT
                else if (sortfor.Equals("Episode count"))
                {
                    //BUBBLE SORT
                    if (sortMethod.Equals("Bubble sort"))
                    {
                        if (animeStack != null)
                        {
                            stopwatch.Restart();
                            sortDeez.BubbleSort(animeStack, CompareEpisodeCount);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayStack();
                        }
                        else
                        {
                            MessageBox.Show("Stack is null");
                        }
                    }
                    //QUICK SORT
                    else if (sortMethod.Equals("Quick sort"))
                    {
                        if (animeStack != null)
                        {
                            stopwatch.Restart();
                            animeStack = sortDeez.QuickSort(animeStack, CompareEpisodeCount);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayStack();
                        }
                        else
                        {
                            MessageBox.Show("Stack is null");
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a sorting algo");
                    }
                }
                //RELEASE DATE
                else if (sortfor.Equals("Release date"))
                {
                    //BUBBLE SORT
                    //Anime animeToCheck = new Anime { AiredDate = Convert.ToDateTime(textBox1.Text) };
                    if (sortMethod.Equals("Bubble sort"))
                    {
                        if (animeStack != null)
                        {
                            stopwatch.Restart();
                            sortDeez.BubbleSort(animeStack, CompareReleaseDate);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayStack();
                        }
                        else
                        {
                            MessageBox.Show("Stack is null");
                        }
                    }
                    //QUICK SORT
                    else if (sortMethod.Equals("Quick sort"))
                    {
                        if (animeStack != null)
                        {
                            stopwatch.Restart();
                            animeStack = sortDeez.QuickSort(animeStack, CompareReleaseDate);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayStack();
                        }
                        else
                        {
                            MessageBox.Show("Arraylist is null");
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a sorting algo");
                    }
                }
            }
            else if (searchStructure.Equals("LinkedList"))
            {
                //TITLE
                if (sortfor.Equals("Title"))
                {
                    //BUBBLE SORT
                    if (sortMethod.Equals("Bubble sort"))
                    {
                        if (sortDeez != null)
                        {
                            stopwatch.Restart();
                            sortDeez.BubbleSort(animeLinkedList, CompareTitle);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayLinkedList();
                        }
                        else
                        {
                            MessageBox.Show("LinkedList is null");
                        }
                    }
                    //QUICK SORT
                    else if (sortMethod.Equals("Quick sort"))
                    {
                        if (sortDeez != null)
                        {
                            stopwatch.Restart();
                            animeLinkedList = sortDeez.QuickSort(animeLinkedList, CompareTitle);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayLinkedList();
                        }
                        else
                        {
                            MessageBox.Show("LinkedList is null");
                        }
                    }
                    //NO SORT METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a sorting algo");
                    }
                }
                //EPISODE COUNT
                else if (sortfor.Equals("Episode count"))
                {
                    //BUBBLE SORT
                    //Anime animeToCheck = new Anime { Episodes = int.Parse(textBox1.Text) };
                    if (sortMethod.Equals("Bubble sort"))
                    {
                        if (sortDeez != null)
                        {
                            stopwatch.Restart();
                            sortDeez.BubbleSort(animeLinkedList, CompareEpisodeCount);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayLinkedList();
                        }
                        else
                        {
                            MessageBox.Show("LinkedList is null");
                        }
                    }
                    //QUICK SORT
                    else if (sortMethod.Equals("Quick sort"))
                    {
                        if (sortDeez != null)
                        {
                            stopwatch.Restart();
                            animeLinkedList = sortDeez.QuickSort(animeLinkedList, CompareEpisodeCount);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayLinkedList();
                        }
                        else
                        {
                            MessageBox.Show("LinkedList is null");
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a sorting algo");
                    }
                }
                //RELEASE DATE
                else if (sortfor.Equals("Release date"))
                {
                    //BUBBLE SORT
                    //Anime animeToCheck = new Anime { AiredDate = Convert.ToDateTime(textBox1.Text) };
                    if (sortMethod.Equals("Bubble sort"))
                    {
                        if (sortDeez != null)
                        {
                            stopwatch.Restart();
                            sortDeez.BubbleSort(animeLinkedList, CompareReleaseDate);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayLinkedList();
                        }
                        else
                        {
                            MessageBox.Show("LinkedList is null");
                        }
                    }
                    //QUICK SORT
                    else if (sortMethod.Equals("Quick sort"))
                    {
                        if (sortDeez != null)
                        {
                            stopwatch.Restart();
                            animeLinkedList = sortDeez.QuickSort(animeLinkedList, CompareReleaseDate);
                            stopwatch.Stop();
                            GetElapsedTime();
                            DisplayLinkedList();
                        }
                        else
                        {
                            MessageBox.Show("LinkedList is null");
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a sorting algo");
                    }
                }
            }
        }

        private void AddFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadJson(openFileDialog1.FileName);
                MessageBox.Show("File Upoaded");
            }
        }
    }
}
