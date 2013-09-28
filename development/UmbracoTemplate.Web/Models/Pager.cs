﻿using System.Collections.Generic;

namespace UmbracoTemplate.Web.Models
{
    public class Pager
    {
        public Pager()
        {
        }

        public int NumberOfItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<int> Pages { get; set; }

        public bool IsFirstPage
        {
            get
            {
                return CurrentPage <= 1;
            }
        }

        public bool IsLastPage
        {
            get
            {
                return (CurrentPage * ItemsPerPage) >= NumberOfItems;
            }
        }
    }
}