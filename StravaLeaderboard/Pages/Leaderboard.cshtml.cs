using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StravaLeaderboard.models;

namespace StravaLeaderboard.Pages
{
    public class LeaderboardModel : PageModel
    {

        public APITokens _apitokens { get; set; }
        public LeaderboardModel(IOptions<APITokens> apitokens)
        {
            _apitokens = apitokens.Value;
        }


        public void OnGet()
        {

            
        }       
    }
}
