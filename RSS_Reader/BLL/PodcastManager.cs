﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using System.Xml;

namespace BLL
{
    public static class PodcastManager
    {

        public static List<SyndicationItem> getList(string url)
        {
            XmlReader xr = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(xr);
            xr.Close();
            return feed.Items.ToList();
        }
    }

}
