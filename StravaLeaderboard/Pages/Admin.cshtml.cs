using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StravaLeaderboard.Data;
using StravaLeaderboard.models;

namespace StravaLeaderboard.Pages
{
    public class ClubModel : PageModel
    {
        
        private readonly DataContext _db;

        public ClubModel(DataContext db)
        {
            _db = db;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public Club club { get; set; }

        public void OnGet()
        {
            Message = "Your contact page.";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Clubs.Add(club);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
