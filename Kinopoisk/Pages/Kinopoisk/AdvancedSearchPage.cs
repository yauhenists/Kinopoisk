using OpenQA.Selenium;

namespace Kinopoisk.Pages.Kinopoisk
{
    public class AdvancedSearchPage : BasePage
    {
        private By SearchField { get; } = By.Id("find_film");
        private By SearchButton{ get; } = By.XPath("//form[@name='film_search']/input[@value='поиск']");
        private By CountryList{ get; } = By.Id("country");
        public AdvancedSearchPage(ConciseApi conciseApi) : base(conciseApi)
        {
        }
        public override void OpenPage()
        {
            throw new System.NotImplementedException();
        }

        public PageWithResults SearchByNameAndCountry(string name, string country)
        {
            ConciseApi.InputText(name, SearchField);
            ConciseApi.SelectElementFromList(CountryList, country);
            ConciseApi.ClickOnElement(SearchButton);

            return new PageWithResults(ConciseApi);
        }       
    }
}
