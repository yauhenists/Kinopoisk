using OpenQA.Selenium;

namespace Kinopoisk.Pages.Kinopoisk
{
    public class PageWithResults : BasePage
    {
        private readonly By _firstResult = By.XPath("//div[@class='search_results'][1]//p[@class='name']/a");
        public PageWithResults(ConciseApi conciseApi) : base(conciseApi)
        {
        }

        public override void OpenPage()
        {
            throw new System.NotImplementedException();
        }

        public ParticularFilmPage FollowFirstResult()
        {
            ConciseApi.ClickOnElement(_firstResult);

            return new ParticularFilmPage(ConciseApi);
        }

        public string GetFirstResultTitle() => ConciseApi.GetTextOfElement(_firstResult);
    }
}
