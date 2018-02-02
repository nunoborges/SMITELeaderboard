using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using StravaLeaderboard.models;

namespace StravaLeaderboard.Pages
{
    public class LeaderboardModel : PageModel
    {
        //https://codingblast.com/asp-net-core-configuration-reloading-binding-injecting/
        public LeaderboardModel(IOptions<APITokens> apitokens)
        {
            _apitokens = apitokens.Value;
        }  
        
        public string Message { get; set; }
        public APITokens _apitokens { get; set; }

        public void OnGet()
        {
            Message = "Your Strava Access_Token = " +
                _apitokens.Access_Token;

        }
    }
}
