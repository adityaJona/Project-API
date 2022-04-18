using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace API.Models
{
    public class AccountRole
    {
        [Key, ForeignKey("Account")]
        public string NIK { get; set; }
        [Required, ForeignKey("Role")]
        public int Role_Id { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; }
    }
}
