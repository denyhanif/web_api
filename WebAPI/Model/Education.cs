using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    [Table("TB_M_Education")]
    public class Education
    {
        [Key][Required]
        public int Id { set; get; }
        [Required]
        public string Degree { set; get; }
        [Required]
        public string GPA { set; get; }
        [ForeignKey("University")]
        [JsonIgnore]
        public int University_id { set; get; }
        [JsonIgnore]
        public virtual University University { set; get; }
        [JsonIgnore]
        public virtual ICollection<Profilling> Profillings { get; set; }
    }
}
