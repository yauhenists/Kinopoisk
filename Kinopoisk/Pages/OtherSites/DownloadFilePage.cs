using OpenQA.Selenium;

namespace Kinopoisk.Pages.OtherSites
{
    public sealed class DownloadFilePage : BasePage
    {
        private readonly By _downloadLink = By.LinkText("Download");
        public DownloadFilePage(ConciseApi conciseApi) : base(conciseApi)
        {
            OpenPage();
        }

        public override void OpenPage()
        {
            ConciseApi.OpenPage("https://www.softpost.org/selenium-test-page/");
        }

        public void Download()
        {
            ConciseApi.GetElement(_downloadLink).Click();
        }
    }
}
