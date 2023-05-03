using System.ComponentModel.DataAnnotations;

namespace ModelHouse.Security.Resources.ProjectResource
{
    public class CreateProjectResource
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
        [Required]
        public long BusinessProfileId { get; set; }
    }
}