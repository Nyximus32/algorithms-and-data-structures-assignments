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

        //variables
        Boolean isSorted = false;
        StackBetter<Anime> animeStack = new StackBetter<Anime>();
        CoolerArrayList<Anime> animeArrayList = new CoolerArrayList<Anime>();
        LinkedListBeyond<Anime> animeLinkedList = new LinkedListBeyond<Anime>();
        SearchDeez<Anime> searchDeez = new SearchDeez<Anime>();
        SortDeez<Anime> sortDeez = new SortDeez<Anime>();
        Func<Anime, IComparable> sortByEpisodeCount = (anime) => anime.Episodes;
        Func<Anime, IComparable> sortByReleaseDate = (anime) => anime.AiredDate;
        Func<Anime, IComparable> sortByTitle = (anime) => anime.Title;

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadJson("animeList.json");
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
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            richTextBox1.Text = "";
            for (int i = 0; i < animeArrayList.Count(); i++)
            {
                richTextBox1.Text += animeArrayList[i].Title + "\n";
            }
            stopwatch.Stop();
            long elapsed_time = stopwatch.ElapsedMilliseconds;
            label1.Text = elapsed_time.ToString() + " miliseconds";
        }

        //really weird and finnicky, but its to make sure we keep the original working and just run through copies
        private void displayStack_Click(object sender, EventArgs e)
        {
            //Create a copy of animeStack that we can iterate over without modifying the original stack
            StackBetter<Anime> copyStack = (StackBetter<Anime>)animeStack.Clone();

            //Iterate over the elements of the copyStack and add deep copies of each element to stackBuffer

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            richTextBox1.Text = "";
            while (copyStack.Count > 0)
            {
                richTextBox1.Text += copyStack.Pop().Title + "\n";
            }
            stopwatch.Stop();
            long elapsed_time = stopwatch.ElapsedMilliseconds;
            label1.Text = elapsed_time.ToString() + " milliseconds";
        }

        //searches through the structure for a specific thing
        private void searchButton(object sender, EventArgs e)
        {
            //the method that will be used to search, and the thing we are looking for
            string searchMethod = groupBox2.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            string searchfor = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            string searchStructure = groupBox5.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;

            //if the list isnt sorted don't bother because it wont work
            if (!isSorted)
            {
                MessageBox.Show("Sort the data structure first");
                return;
            }
            if (searchStructure.Equals("ArrayList"))
            {
                //TITLE
                if (searchfor.Equals("Title"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { Title = textBox1.Text };
                    if (searchMethod.Equals("Linear search"))
                    {
                        Anime foundAnime = searchDeez.LinearSearch(animeArrayList, animeToCheck, sortByTitle);
                        if (foundAnime != null)
                        {
                            richTextBox1.Text = foundAnime.Title;
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        //animeArrayList = sortDeez.QuickSort(animeArrayList, 0, animeArrayList.Count() - 1, sortByTitle);
                        int index = searchDeez.BinarySearch(animeArrayList, 0, animeArrayList.Count(), animeToCheck, sortByTitle);
                        if (index != -1)
                        {
                            richTextBox1.Text = animeArrayList[index].Title + " was found at index: " + index.ToString();
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a searching algo");
                    }
                }
                //EPISODE COUNT
                else if (searchfor.Equals("Episode count"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { Episodes = int.Parse(textBox1.Text) };
                    if (searchMethod.Equals("Linear search"))
                    {
                        Anime foundAnime = searchDeez.LinearSearch(animeArrayList, animeToCheck, sortByEpisodeCount);
                        if (foundAnime != null)
                        {
                            richTextBox1.Text = foundAnime.Title;
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        //animeArrayList = sortDeez.QuickSort(animeArrayList, 0, animeArrayList.Count() - 1, sortByEpisodeCount);
                        int index = searchDeez.BinarySearch(animeArrayList, 0, animeArrayList.Count(), animeToCheck, sortByEpisodeCount);
                        if (index != -1)
                        {
                            richTextBox1.Text = animeArrayList[index].Title + " was found at index: " + index.ToString();
                        }
                    }
                    //NO SEARCH METHOD SELECTED
                    else
                    {
                        MessageBox.Show("Select a searching algo");
                    }
                }
                //RELEASE DATE
                else if (searchfor.Equals("Release date"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { AiredDate = DateTime.Parse(textBox1.Text) };
                    if (searchMethod.Equals("Linear search"))
                    {
                        Anime foundAnime = searchDeez.LinearSearch(animeArrayList, animeToCheck, sortByReleaseDate);
                        if (foundAnime != null)
                        {
                            richTextBox1.Text = foundAnime.Title;
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        int index = searchDeez.BinarySearch(animeArrayList, 0, animeArrayList.Count(), animeToCheck, sortByReleaseDate);
                        if (index != -1)
                        {
                            richTextBox1.Text = animeArrayList[index].Title + " was found at index: " + index.ToString();
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
                if (searchfor.Equals("Title"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { Title = textBox1.Text };
                    if (searchMethod.Equals("Linear search"))
                    {
                        Anime foundAnime = searchDeez.LinearSearch(animeLinkedList, animeToCheck, sortByTitle);
                        if (foundAnime != null)
                        {
                            richTextBox1.Text = foundAnime.Title;
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        int index = searchDeez.BinarySearch(animeLinkedList, 0, animeLinkedList.Count, animeToCheck, sortByTitle);
                        if (index != -1)
                        {
                            richTextBox1.Text = "Anime found at index:" + index.ToString();
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
                else if (searchfor.Equals("Episode count"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { Episodes = int.Parse(textBox1.Text) };
                    if (searchMethod.Equals("Linear search"))
                    {
                        Anime foundAnime = searchDeez.LinearSearch(animeLinkedList, animeToCheck, sortByEpisodeCount);
                        if (foundAnime != null)
                        {
                            richTextBox1.Text = foundAnime.Title;
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        int index = searchDeez.BinarySearch(animeLinkedList, 0, animeLinkedList.Count, animeToCheck, sortByEpisodeCount);
                        if (index != -1)
                        {
                            richTextBox1.Text = "Anime found at index:" + index.ToString();
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
                else if (searchfor.Equals("Release date"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { AiredDate = DateTime.Parse(textBox1.Text) };
                    if (searchMethod.Equals("Linear search"))
                    {
                        Anime foundAnime = searchDeez.LinearSearch(animeLinkedList, animeToCheck, sortByReleaseDate);
                        if (foundAnime != null)
                        {
                            richTextBox1.Text = foundAnime.Title;
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        int index = searchDeez.BinarySearch(animeLinkedList, 0, animeLinkedList.Count, animeToCheck, sortByReleaseDate);
                        if (index != -1)
                        {
                            richTextBox1.Text = "Anime found at index:" + index.ToString();
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
                if (searchfor.Equals("Title"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { Title = textBox1.Text };
                    if (searchMethod.Equals("Linear search"))
                    {
                        Anime foundAnime = searchDeez.LinearSearch(animeLinkedList, animeToCheck, sortByTitle);
                        if (foundAnime != null)
                        {
                            richTextBox1.Text = foundAnime.Title;
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        int index = searchDeez.BinarySearch(animeLinkedList, 0, animeLinkedList.Count, animeToCheck, sortByTitle);
                        if (index != -1)
                        {
                            richTextBox1.Text = "Anime found at index:" + index.ToString();
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
                else if (searchfor.Equals("Episode count"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { Episodes = int.Parse(textBox1.Text) };
                    if (searchMethod.Equals("Linear search"))
                    {
                        Anime foundAnime = searchDeez.LinearSearch(animeLinkedList, animeToCheck, sortByEpisodeCount);
                        if (foundAnime != null)
                        {
                            richTextBox1.Text = foundAnime.Title;
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        int index = searchDeez.BinarySearch(animeLinkedList, 0, animeLinkedList.Count, animeToCheck, sortByEpisodeCount);
                        if (index != -1)
                        {
                            richTextBox1.Text = "Anime found at index:" + index.ToString();
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
                else if (searchfor.Equals("Release date"))
                {
                    //LINEAR SEARCH
                    Anime animeToCheck = new Anime { AiredDate = DateTime.Parse(textBox1.Text) };
                    if (searchMethod.Equals("Linear search"))
                    {
                        Anime foundAnime = searchDeez.LinearSearch(animeLinkedList, animeToCheck, sortByReleaseDate);
                        if (foundAnime != null)
                        {
                            richTextBox1.Text = foundAnime.Title;
                        }
                    }
                    //BINARY SEARCH
                    else if (searchMethod.Equals("Binary search"))
                    {
                        int index = searchDeez.BinarySearch(animeLinkedList, 0, animeStack.Count, animeToCheck, sortByReleaseDate);
                        if (index != -1)
                        {
                            richTextBox1.Text = "Anime found at index:" + index.ToString();
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
            //the method that will be used to search, and the thing we are looking for
            string sortMethod = groupBox3.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            string sortfor = groupBox4.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            string searchStructure = groupBox5.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;

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
                            sortDeez.BubbleSort(animeArrayList, sortByTitle);
                            isSorted = true;
                            displayArrList_Click(sender, e);
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
                            animeArrayList = sortDeez.QuickSort(animeArrayList, 0, animeArrayList.Count() - 1, sortByTitle);
                            isSorted = true;
                            displayArrList_Click(sender, e);
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
                            sortDeez.BubbleSort(animeArrayList, sortByEpisodeCount);
                            isSorted = true;
                            displayArrList_Click(sender, e);
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
                            animeArrayList = sortDeez.QuickSort(animeArrayList, 0, animeArrayList.Count() - 1, sortByEpisodeCount);
                            isSorted = true;
                            displayArrList_Click(sender, e);
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
                            sortDeez.BubbleSort(animeArrayList, sortByReleaseDate);
                            isSorted = true;
                            displayArrList_Click(sender, e);
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
                            animeArrayList = sortDeez.QuickSort(animeArrayList, 0, animeArrayList.Count() - 1, sortByReleaseDate);
                            isSorted = true;
                            displayArrList_Click(sender, e);
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
            } else if (searchStructure.Equals("Stack"))
            {
                //TITLE
                if (sortfor.Equals("Title"))
                {
                    //BUBBLE SORT
                    if (sortMethod.Equals("Bubble sort"))
                    {
                        if (animeStack != null)
                        {
                            sortDeez.BubbleSort(animeStack, sortByTitle);
                            isSorted = true;
                            displayStack_Click(sender, e);
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
                            animeStack = sortDeez.QuickSort(animeStack, sortByTitle);
                            isSorted = true;
                            displayStack_Click(sender, e);
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
                            sortDeez.BubbleSort(animeStack, sortByEpisodeCount);
                            isSorted = true;
                            displayStack_Click(sender, e);
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
                            animeStack = sortDeez.QuickSort(animeStack, sortByEpisodeCount);
                            isSorted = true;
                            displayStack_Click(sender, e);
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
                            sortDeez.QuickSort(animeStack, sortByReleaseDate);
                            isSorted = true;
                            displayStack_Click(sender, e);
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
                            animeStack = sortDeez.QuickSort(animeStack, sortByReleaseDate);
                            isSorted = true;
                            displayStack_Click(sender, e);
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
                            sortDeez.BubbleSort(animeLinkedList, sortByTitle);
                            isSorted = true;
                            displayLinkList_Click(sender, e);
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
                            animeLinkedList = sortDeez.QuickSort(animeLinkedList, sortByTitle);
                            isSorted = true;
                            displayLinkList_Click(sender, e);
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
                            sortDeez.BubbleSort(animeLinkedList, sortByEpisodeCount);
                            isSorted = true;
                            displayLinkList_Click(sender, e);
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
                            animeLinkedList = sortDeez.QuickSort(animeLinkedList, sortByEpisodeCount);
                            isSorted = true;
                            displayLinkList_Click(sender, e);
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
                            sortDeez.BubbleSort(animeLinkedList, sortByReleaseDate);
                            isSorted = true;
                            displayLinkList_Click(sender, e);
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
                            animeLinkedList = sortDeez.QuickSort(animeLinkedList, sortByReleaseDate);
                            isSorted = true;
                            displayLinkList_Click(sender, e);
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

        private void displayLinkList_Click(object sender, EventArgs e)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            richTextBox1.Text = "";
            LinkedListBeyond<Anime>.Node currentNode = animeLinkedList.GetHead();
            while (currentNode != null)
            {
                richTextBox1.Text += currentNode.Value.Title.ToString() + Environment.NewLine;
                currentNode = currentNode.Next;
            }
            stopwatch.Stop();
            long elapsed_time = stopwatch.ElapsedMilliseconds;
            label1.Text = elapsed_time.ToString() + " miliseconds";
        }
    }
}
