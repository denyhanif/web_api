using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    [Table("TB_M_Account")]
    public class Account
    {
        [Key]
        [Required]
        public string NIK {set;get;}
        public string Password { set; get; }
        [JsonIgnore]
        public virtual Employee Employee { set; get; }
        [JsonIgnore]
        public virtual Profilling Profilling { set; get; }

        public int OTP { set; get; }

        public DateTime ExpiredToken { set; get; }

        public bool isUsed { set; get; }


        public virtual ICollection<AccountRole> AccountRole { set; get; }
       // public virtual ICollection<AccountRole> AccountRole { get; set; }
    }
}
