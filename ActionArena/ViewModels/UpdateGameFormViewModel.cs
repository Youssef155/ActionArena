namespace ActionArena.ViewModels
{
    public class UpdateGameFormViewModel : GameFormVM
    {
        public int id {  get; set; }
        public string? CurrentCover { get; set; }

        [AllowedExtension(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
