using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.ViewModel
{
    public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string UserAccount { get; set; }
        public string? UserName { get; set; }
        public string? UserFamily { get; set; }
        public string? Mobile { get; set; }
        public string Email { get; set; }

        public DateTime CreateAccount { get; set; }
    }
}
