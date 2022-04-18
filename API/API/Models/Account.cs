using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace API.Models
{
    [Table("ACCOUNT")]
    public class Account
    {

        [Key, ForeignKey("Employee")]
        public string NIK { get; set; }
        [Required]
        public string Password { get; set; }

        public int OTP { get; set; }
        public DateTime EkspiredToken { get; set; }
        public bool isUsed { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        public virtual Profiling Profiling { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRole { get; set; }
    }
}
