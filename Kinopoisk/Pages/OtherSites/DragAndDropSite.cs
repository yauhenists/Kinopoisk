using OpenQA.Selenium;

namespace Kinopoisk.Pages.OtherSites
{
    public sealed class DragAndDropSite : BasePage
    {
        private const string Url = "https://demoqa.com/droppable/";
        private readonly By _dragElement = By.Id("draggable");
        private readonly By _dropElement = By.Id("droppable");
        private readonly By _droppedResult = By.XPath("//p[contains(text(),'Dropped!')]");
        public DragAndDropSite(ConciseApi conciseApi) : base(conciseApi)
        {
            OpenPage();
        }

        public override void OpenPage()
        {
            ConciseApi.OpenPage(Url);
        }

        public void PerformDragAndDrop()
        {
            ConciseApi.DragAndDrop(_dragElement, _dropElement);
        }

        public void PerformDragAndDrop(int x, int y)
        {
            ConciseApi.DragAndDrop(_dragElement, x, y);
        }

        public bool IsDroppedResultDisplayed() => ConciseApi.IsElementDisplayed(_droppedResult);
    }
}
