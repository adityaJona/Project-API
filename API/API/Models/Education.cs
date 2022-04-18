using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace API.Models
{
    [Table("EDUCATION")]
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Degree { get; set; }
        public string GPA { get; set; }
        [Required, ForeignKey("University")]
        public int University_Id { get; set; }

        [JsonIgnore]
        public virtual University University { get; set; }
        [JsonIgnore]
        public virtual ICollection<Profiling> Profiling { get; set; }
    }
}
