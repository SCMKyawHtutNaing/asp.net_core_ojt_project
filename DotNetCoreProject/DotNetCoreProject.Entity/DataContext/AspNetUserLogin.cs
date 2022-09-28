using System;
using System.Collections.Generic;

#nullable disable

namespace DotNetCoreProject.Entity.DataContext
{
    public partial class AspNetUserLogin
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public int UserId { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
