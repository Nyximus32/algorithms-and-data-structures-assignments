﻿using Newtonsoft.Json;
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
            var path = "animeList.json";
            List<Anime> animes = LoadJson(path);
            foreach (var anime in animes)
            {
                richTextBox1.Text += anime.Title + '\n';
            }
        }

        public static List<Anime> LoadJson(string path)
        {
            List<Anime> animes = new List<Anime>();
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
                animes.Add(anime);
            }
            return animes;
        }
        public class Anime
        {
            public string Title;
            public string Link;
            public double Score;
            public string Type;
            public int Episodes;
            public string Source;
            public string Premiered;
            public string AiredDate;
            public string Studios;
            public string Genres;
            public string Themes;
            public string Demographic;
            public string Duration;
            public string AgeRating;
            public double ReviewCount;
            public int Popularity;
            public string Members;
            public string Favorites;
            public string Adaptation;
            public string Sequel;
            public string Prequel;
            public string Characters;
        }
    }
}
