using System;
using System.Collections.Generic;
using System.Linq;

namespace UmbracoTemplate.Web.Models
{
    public class Pager
    {
        public Pager(int itemsPerPage, int numberOfItems, int currentPage = 1)
        {
            var numberOfPages = numberOfItems % itemsPerPage == 0 ? Math.Ceiling((decimal)(numberOfItems / itemsPerPage)) : Math.Ceiling((decimal)(numberOfItems / itemsPerPage)) + 1;
            var pages = Enumerable.Range(1, (int)numberOfPages);

            NumberOfItems = numberOfItems;
            ItemsPerPage = itemsPerPage;
            CurrentPage = currentPage;
            Pages = pages;
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