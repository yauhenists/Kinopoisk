using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kinopoisk.Pages.OtherSites;
using NUnit.Framework;

namespace Kinopoisk.Tests
{
    [TestFixture]
    public class OtherTests : BaseTest
    {
        [Test]
        public void DragAndDropTest()
        {
            DragAndDropSite page = new DragAndDropSite(ConciseApi);
            page.OpenPage();
            page.PerformDragAndDrop();

            Assert.That(page.IsDroppedResultDisplayed());
        }

        [Test]
        public void DragAndDropWitCoordinatesTest()
        {
            DragAndDropSite page = new DragAndDropSite(ConciseApi);
            page.OpenPage();
            page.PerformDragAndDrop(148, 33);

            Assert.That(page.IsDroppedResultDisplayed());
        }

        [Test]
        public void CheckDownload()
        {
            DownloadFilePage page = new DownloadFilePage(ConciseApi);
            page.OpenPage();
            page.Download();

            Task.Delay(5000);
            Thread.Sleep(3000);
            var file = Directory.GetFiles(DownloadPath).First();
            Console.WriteLine(file);
            Assert.AreEqual(DownloadPath + @"\myimage.jfif", file);
            Assert.IsTrue(File.Exists(DownloadPath + @"\myimage.jfif"));

            File.Delete(DownloadPath + @"\myimage.jfif");
        }

        [Test]
        public void CheckTable()
        {
            TablesPage page = new TablesPage(ConciseApi);
            page.OpenPage();

            page.GetRowOfTable(3,1);
        }
    }
}
