using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{

    [Table("TB_M_Role")]
    public class Role
    {
        [Key]
        [Required]
        public int Id { set; get; }

        public string Name { set; get; }

        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRole { set; get; }
    }
}
