using Microsoft.AspNetCore.Mvc;
using MusicApp.Models;
using MusicApp.Data; // DbContext'in do�ru import edilmesi gerekir
using System.Linq;

namespace MusicApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MusicAppDbContext _context;  // DbContext'i buraya ekliyoruz

        public HomeController(ILogger<HomeController> logger, MusicAppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // En �ok dinlenen �ark�lar� al�yoruz (Plays'e g�re s�ralama)
            var mostPlayedSongs = _context.Songs
                                          .OrderByDescending(s => s.Plays)  // Plays say�s�na g�re azalan s�ralama
                                          .Take(5)  // �lk 5 �ark�y� al�yoruz
                                          .ToList();

            // View'a g�nderilecek model
            return View(mostPlayedSongs);  // `mostPlayedSongs` ile view'� d�nd�r�yoruz
        }
    }
}
