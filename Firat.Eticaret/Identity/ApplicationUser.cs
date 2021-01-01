using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Firat.Eticaret.Identity
{
    public class ApplicationUser:IdentityUser
    {
        [DisplayName("Ad")]
        public string Name { get; set; }
        [DisplayName("Soyad")]
        public string Surname { get; set; }  
    }
}