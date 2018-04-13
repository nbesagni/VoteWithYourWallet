using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VoteWithYourWallet.Models
{
    public class Signature
    {
        public int SignatureID { get; set; }
        [Display(Name = "User ID")]
        public string ApplicationUserID { get; set; }
        [Display(Name = "Signed on")]
        public DateTime DateTimeSigned { get; set; }
        public string Message { get; set; }

        public virtual Cause Causes { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}