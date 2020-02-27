namespace Kinopoisk.Pages
{
    public abstract class BasePage
    {
        protected ConciseApi ConciseApi { get; } 
        public BasePage(ConciseApi conciseApi)
        {
            ConciseApi = conciseApi;
        }

        public abstract void OpenPage();
    }
}
