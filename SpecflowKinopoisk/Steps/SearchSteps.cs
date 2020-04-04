using Kinopoisk.Pages.Kinopoisk;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecflowKinopoisk.Steps
{
    [Binding]
    public sealed class SearchSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly KinopoiskHomePage _homePage;
        private PageWithResults _pageWithResults;
        private AdvancedSearchPage _advancedSearchPage;

        public SearchSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _homePage = _scenarioContext.Get<KinopoiskHomePage>(typeof(KinopoiskHomePage).ToString());
        }

        [When(@"I enter ""(.*)"" and search")]
        [Scope(Scenario = "Simple Search")]
        public void WhenIEnterAndSearch(string film)
        {
            _pageWithResults = _homePage.Search(film);
            _scenarioContext.Add(typeof(PageWithResults).ToString(), _pageWithResults);
        }

        [Given(@"I go to advanced search page")]
        public void GivenIGoToAdvancedSearchPage()
        {
            _advancedSearchPage = _homePage.GoToAdvancedSearchPage();
            _scenarioContext.Add(typeof(AdvancedSearchPage).ToString(), _advancedSearchPage);
        }

        [When(@"I enter ""(.*)"" and choose country ""(.*)"" and search")]
        public void WhenIEnterAndChooseCountryAndSearch(string film, string country)
        {
            _pageWithResults = _advancedSearchPage.SearchByNameAndCountry(film, country);
            _scenarioContext.Add(typeof(PageWithResults).ToString(), _pageWithResults);
        }


        [Then(@"I get ""(.*)"" as the first result on the page with results")]
        public void ThenIGetAsTheFirstResultOnThePageWithResults(string expectedFilm)
        {
            _pageWithResults = _scenarioContext.Get<PageWithResults>(typeof(PageWithResults).ToString());
            Assert.AreEqual(expectedFilm, _pageWithResults.GetFirstResultTitle());
        }

    }
}
