﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BLL;
using BLL.Models;


namespace PL
{
    public partial class MainForm : Form
    {
        public FeedGroup _FeedGroup;

        public MainForm()
        {
            InitializeComponent();
            IntializeColumns();

            _FeedGroup = new FeedGroup();
            loadListWithFeeds();
        }

        private void loadListWithFeeds()
        {
            var listWithFeeds = FeedManager.deSerialize();
            _FeedGroup.Feeds.AddRange(listWithFeeds);

            foreach (Feed feed in _FeedGroup.Feeds)
            {
                ListViewItem item = new ListViewItem(new[] { feed.NumberOfEpisodes.ToString(), feed.Name });
                lvPodcasts.Items.Add(item);
            }
            

        }
        private void IntializeColumns()
        {
            lvPodcasts.Columns.Add("Antal");
            lvPodcasts.Columns.Add("Namn");
            lvPodcasts.Columns.Add("Frekvens");
            lvPodcasts.Columns.Add("Kategori");
            lvPodcasts.View = View.Details;
            lvPodcasts.FullRowSelect = true;

            lvEpisodes.Columns.Add("Avsnitt");
            lvEpisodes.Columns.Add("Namn");
            lvEpisodes.View = View.Details;
            lvEpisodes.FullRowSelect = true;
        }

        private void btnAddPodcast_Click(object sender, EventArgs e)
        {
            var feed = FeedManager.CreateFeed(txtURL.Text);
            _FeedGroup.Add(feed);

            ListViewItem item = new ListViewItem(new[] { feed.NumberOfEpisodes.ToString(), feed.Name });

            lvPodcasts.Items.Add(item);
        }

        private void lvPodcasts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lvEpisodes.Items.Clear();
            
            foreach(Feed feed in _FeedGroup.Feeds)
            {
                if(feed.Name.Equals(lvPodcasts.SelectedItems[0].SubItems[1].Text))
                {
                    foreach(Episode episode in feed.Episodes)
                    {
                        ListViewItem item = new ListViewItem(new[] { episode.EpisodeNumber.ToString(), episode.Title });
                        lvEpisodes.Items.Add(item);
                    }
                    break;
                }
            }
        }
        private void lvEpisodes_Click(object sender, EventArgs e)
        {
            foreach (Feed feed in _FeedGroup.Feeds)
            {
                if (feed.Name.Equals(lvPodcasts.SelectedItems[0].SubItems[1].Text))
                {
                    foreach (Episode episode in feed.Episodes)
                    {
                        if(episode.EpisodeNumber.ToString().Equals(lvEpisodes.SelectedItems[0].Text))
                        {
                            lblTitle.Text = episode.Title;
                            lblDesc.Text = episode.Description;
                        }
                    }
                    break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            foreach (var item in _FeedGroup.Feeds)
            {
            FeedManager.saveFeeds(item);
            }
            
        }
    }
}