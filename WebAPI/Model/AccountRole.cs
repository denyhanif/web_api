using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class AccountRole
    {
        public int Id { set; get; }

        [JsonIgnore]
        public virtual Account Account { set; get; }
        

        [ForeignKey("Account")]
        public string Account_id { set; get; }

        [JsonIgnore]
        public virtual Role Role { set; get; }

        [ForeignKey("Role")]
        public int Role_id { set; get; }
    }
}


