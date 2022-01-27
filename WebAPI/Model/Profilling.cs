using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    [Table("TB_M_Profiling")]
    public class Profilling
    {
        [Key][Required]
        public string NIK { set; get; }
        [Required][JsonIgnore]
        public virtual Account Account { set; get; }
        [JsonIgnore]
        public virtual Education Education { set; get; }
        [ForeignKey("Education")] [JsonIgnore]
        public virtual int Education_id { set; get; }

    }
}
