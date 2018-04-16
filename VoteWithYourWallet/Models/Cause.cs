using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VoteWithYourWallet.Models
{
    public class Cause
    {
        public int CauseID { get; set; }
        [Display(Name = "User ID")]
        public string ApplicationUserID { get; set; }
        public string Title { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }
        public string Target { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Signature> Signatures { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}