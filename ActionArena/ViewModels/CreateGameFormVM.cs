using Microsoft.AspNetCore.Mvc.Rendering;

namespace ActionArena.ViewModels
{
    public class CreateGameFormVM
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        // validate extension and size
        public IFormFile Cover { get; set; } = default!;
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [Display(Name = "Supportes Devices")]
        public List<int> SelectedDevices { get; set; } = default!;
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();

    }
}
