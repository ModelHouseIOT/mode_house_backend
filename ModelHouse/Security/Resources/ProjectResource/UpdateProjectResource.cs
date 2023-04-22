using System.ComponentModel.DataAnnotations;

namespace ModelHouse.Security.Resources.ProjectResource
{
    public class UpdateProjectResource
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }    }
}