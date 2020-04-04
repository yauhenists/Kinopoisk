using Kinopoisk.Pages.Kinopoisk;
using TechTalk.SpecFlow;

namespace SpecflowKinopoisk.Steps
{
    [Binding]
    public sealed class AdvancedSearchStep
    {
        private readonly ScenarioContext _scenarioContext;
        private AdvancedSearchPage _advancedSearchPage;
        private PageWithResults _pageWithResults;
        public AdvancedSearchStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _advancedSearchPage = _scenarioContext.Get<AdvancedSearchPage>(typeof(AdvancedSearchPage).ToString());
        }

        [When(@"I enter ""(.*)"" and search")]
        [Scope(Scenario = "Advanced Search")]
        public void WhenIEnterAndSearch(string film)
        {
            _pageWithResults =_advancedSearchPage.SearchByNameAndCountry(film);
            _scenarioContext.Add(typeof(PageWithResults).ToString(), _pageWithResults);
        }
    }
}
