using System;

namespace Kinopoisk.Pages.OtherSites
{
    public sealed class TablesPage : BasePage
    {
        private const string Url = "https://en.wikipedia.org/wiki/Programming_languages_used_in_most_popular_websites";
        public TablesPage(ConciseApi conciseApi) : base(conciseApi)
        {
            OpenPage();
        }

        public override void OpenPage()
        {
            ConciseApi.OpenPage(Url);
        }

        public void GetRowOfTable(int row, int table) => Console.WriteLine(ConciseApi.GetFullRowOfTable(row, table));
    }
}
