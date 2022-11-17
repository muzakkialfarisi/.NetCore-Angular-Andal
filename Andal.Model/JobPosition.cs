using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Andal.Models
{
    public class JobPosition
    {
        [Key]
        public int PositionId { get; set; }
        [Required(ErrorMessage = "Code Required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
        public int JobTitleId { get; set; }
        [ForeignKey("JobTitleId")]
        public JobTitle? JobTitle { get; set; }
    }
}
