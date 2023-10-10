using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AutoPAS.Models
{
    public class portaluser
    {
        public portaluser()
        {
            this.userpolicylist = new HashSet<userpolicylist>();
        }
        [Key]
        public int userid { get; set; }

        public string? username { get; set; }

        public string? password { get; set; }

        [JsonIgnore]
        public ICollection<userpolicylist> userpolicylist { get; set; }
    }
}
