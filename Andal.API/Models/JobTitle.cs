using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Andal.Models
{
    public class JobTitle
    {
        [Key]
        public int TitleId { get; set; }
        [Required(ErrorMessage = "Code Required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
    }
}
