using System.Web.Mvc;
using Songs.DataLayer.Repositories;
using Songs.UI.Viewmodels;

namespace Songs.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly SongRepository _repo = new SongRepository();
        public ActionResult Index()
        {
            AllViewmodel model = new AllViewmodel
            {
                Songs = _repo.AllSongs,
                Albums = _repo.AllAlbums,
                Performers = _repo.AllPerformers,
                Genres = _repo.AllGenres
            };
            return View(model);
        }
    }
}