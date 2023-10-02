using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AutoPAS.Models
{
    public class userpolicylist
    {
        [Key]
        public int id { get; set; }

        public int userid { get; set; }

        public int policynumber { get; set; }

      
        [ForeignKey("userid")]
        [JsonIgnore]
        public portaluser portaluser { get; set; }
    }
}
