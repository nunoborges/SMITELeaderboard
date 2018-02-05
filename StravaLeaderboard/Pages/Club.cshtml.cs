using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StravaLeaderboard.Data;
using StravaLeaderboard.models;

namespace StravaLeaderboard.Pages
{
    public class ClubTableModel : PageModel
    {
        private readonly StravaLeaderboard.Data.DataContext _context;

        public ClubTableModel(StravaLeaderboard.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Club> Club { get;set; }

        public async Task OnGetAsync()
        {
            Club = await _context.Clubs.ToListAsync();
        }
    }
}
