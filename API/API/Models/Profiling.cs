using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace API.Models
{
    [Table("PROFILING")]
    public class Profiling
    {
        [Key, ForeignKey("Account")]
        public string NIK { get; set; }
        [Required, ForeignKey("Education")]
        public int Education_Id { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual Education Education { get; set; }
    }
}
