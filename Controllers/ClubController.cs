using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Helpers;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        public readonly ApplicationDbContext _context;

        public ClubController(IClubRepository clubRepository, ApplicationDbContext context)
        {
            _clubRepository = clubRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int category = - 1, int page = 1, int pageSize = 6)
        {
            if (page < 1 || pageSize < 1)
            {
                return NotFound();
            }

            // if category is -1 ( All ) dont filter else filter by selected category
            var clubs = category switch
            {
                -1 => await _clubRepository.GetSliceAsync((page - 1) * pageSize, pageSize),
                _ => await _clubRepository.GetClubsByCategoryAndSliceAsync((ClubCategory)category, (page - 1) * pageSize, pageSize)

            };

            var count = category switch
            {
                -1 => await _clubRepository.GetCountAsync(),
                _ => await _clubRepository.GetCountByCategoryAsync((ClubCategory)category),
            };

            var clubViewModel = new IndexClubViewModel
            {
                Clubs = clubs,
                Page = page,
                PageSize = pageSize,
                TotalClubs = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Category = category
            };
            return View(clubViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DetailClub(int id, string runningClub)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            return club == null ? NotFound() : View(club);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Club club)
        { 
            if (!ModelState.IsValid)
            {
                return View(club);
            }
            _clubRepository.Add(club);

            return RedirectToAction("Index");
        }


        //[HttpPost]
        //public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // TODO implement photo service
        //        var club = new Club
        //        {
        //            Title = clubVM.Title,
        //            Description = clubVM.Description,
        //            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
        //            ClubCategory = clubVM.ClubCategory,
        //            AppUserId = clubVM.AppUserId,
        //            Address = new Address
        //            {
        //                Street = clubVM.Address.Street,
        //                City = clubVM.Address.City,
        //                State = clubVM.Address.State
        //            }
        //        };
        //        _clubRepository.Add(club);
        //        return RedirectToAction("Index");

        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Photo upload failed");
        //    }

        //    return View(clubVM);
        //}
    }
}
