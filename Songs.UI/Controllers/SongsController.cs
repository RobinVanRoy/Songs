using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Songs.Classes;
using Songs.DataLayer.Repositories;
using Songs.UI.Viewmodels;

namespace Songs.UI.Controllers
{
    public class SongsController : Controller
    {
        private readonly SongRepository _repoSong = new SongRepository();

        // GET: Songs
        public ActionResult Index() => View(_repoSong.AllSongs.ToList());

        // GET: Songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Song song = _repoSong.Find((int)id);
            if (song == null) return HttpNotFound();
            return View(song);
        }

        // GET: Songs/Create
        public ActionResult Create()
        {
            EditCreateSong ecs = new EditCreateSong
            {
                EditSong = new Song(),
                SelectedPerformers = new List<int>(),
                SelectedAlbums = new List<int>(),
                Performers = new MultiSelectList(_repoSong.AllPerformers, "PerformerId", "FullName"),
                Albums = new MultiSelectList(_repoSong.AllAlbums, "AlbumId", "Title")
            };
            ViewBag.GenreId = new SelectList(_repoSong.AllGenres, "GenreId", "Name");
            return View(ecs);
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EditSong, SelectedPerformers, SelectedAlbums")] EditCreateSong ecs)
        {
            if (ModelState.IsValid)
            {
                Song song = new Song
                {
                    Title = ecs.EditSong.Title,
                    Duration = ecs.EditSong.Duration,
                    ReleaseDate = ecs.EditSong.ReleaseDate
                };
                if (ecs.SelectedPerformers != null)
                {
                    foreach (var performer in _repoSong.AllPerformers)
                    {
                        if (ecs.SelectedPerformers.Contains(performer.PerformerId)) song.Performers.Add(performer);
                    }
                }

                if (ecs.SelectedAlbums != null)
                {
                    foreach (var album in _repoSong.AllAlbums)
                    {
                        if (ecs.SelectedAlbums.Contains(album.AlbumId)) song.Albums.Add(album);
                    }
                }
                
                if (TryUpdateModel(song, "", new[] { "GenreId" }))
                {
                    _repoSong.InsertOrUpdate(song);
                    _repoSong.Save();
                    return RedirectToAction("Index");
                }
                
            }
            ViewBag.GenreId = new SelectList(_repoSong.AllGenres, "GenreId", "Name", ecs.EditSong.GenreId);
            return View();
        }

        // GET: Songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            EditCreateSong ecs = new EditCreateSong
            {
                EditSong = _repoSong.FindWithGenreAlbumPerformer((int) id)
            };
            if (ecs.EditSong == null) return HttpNotFound();
            ecs.SelectedPerformers = new List<int>(ecs.EditSong.Performers.Select(p => p.PerformerId));
            ecs.SelectedAlbums = new List<int>(ecs.EditSong.Albums.Select(a=>a.AlbumId));
            ecs.Performers = new MultiSelectList(_repoSong.AllPerformers, "PerformerId", "FullName", ecs.SelectedPerformers);
            ecs.Albums = new MultiSelectList(_repoSong.AllAlbums, "AlbumId", "Title", ecs.SelectedAlbums);
            ViewBag.GenreId = new SelectList(_repoSong.AllGenres, "GenreId", "Name", ecs.EditSong.GenreId);
            return View(ecs);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "SongId,Title,Duration,ReleaseDate,GenreId")] Song song)
        public ActionResult Edit([Bind(Include = "EditSong, SelectedPerformers, SelectedAlbums")] EditCreateSong ecs)
        {
            if (ModelState.IsValid)
            {
                Song song = _repoSong.FindWithGenreAlbumPerformer(ecs.EditSong.SongId);
                song.Title = ecs.EditSong.Title;
                song.Duration = ecs.EditSong.Duration;
                song.ReleaseDate = ecs.EditSong.ReleaseDate;
                if (ecs.SelectedPerformers != null)
                {
                    foreach (var performer in _repoSong.AllPerformers)
                    {
                        if (ecs.SelectedPerformers.Contains(performer.PerformerId))
                        {
                            if (!song.Performers.Contains(performer)) song.Performers.Add(performer);
                        }
                        else
                        {
                            if (song.Performers.Contains(performer)) song.Performers.Remove(performer);
                        }
                    }
                }

                if (ecs.SelectedAlbums != null)
                {
                    foreach (var album in _repoSong.AllAlbums)
                    {
                        if (ecs.SelectedAlbums.Contains(album.AlbumId))
                        {
                            if(!song.Albums.Contains(album)) song.Albums.Add(album);
                        }
                        else
                        {
                            if (song.Albums.Contains(album)) song.Albums.Remove(album);
                        }
                    }
                }
                if (TryUpdateModel(song, "", new[] {"GenreId"}))
                {
                    _repoSong.InsertOrUpdate(song);
                    _repoSong.Save();
                    return RedirectToAction("Index");
                }
                
            }
            ViewBag.GenreId = new SelectList(_repoSong.AllGenres, "GenreId", "Name", ecs.EditSong.GenreId);
            return View();
        }

        // GET: Songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Song song = _repoSong.Find((int) id);
            if (song == null) return HttpNotFound();
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repoSong.Delete(id);
            _repoSong.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repoSong.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
