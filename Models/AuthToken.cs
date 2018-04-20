using System;
using System.Collections.Generic;
using System.Text;

namespace Manofthematch.Models
{
    public class AuthToken
    {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
    }
}
