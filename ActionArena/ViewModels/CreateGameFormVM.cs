namespace ActionArena.ViewModels
{
    public class CreateGameFormVM : GameFormVM
    {
        

        [AllowedExtension(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;

    }
}
