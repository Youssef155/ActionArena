namespace ActionArena.Services
{
    public class GameServices : IGameServices
    {
        private readonly ActionArenaDbContext _dbcontext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _coverPath;

        public GameServices(ActionArenaDbContext dbcontext, IWebHostEnvironment webHostEnvironment)
        {
            _dbcontext = dbcontext;
            _webHostEnvironment = webHostEnvironment;
            _coverPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public async Task Create(CreateGameFormVM model)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
            var path = Path.Combine(_coverPath, coverName);

            using var stream = File.Create(path);
            await model.Cover.CopyToAsync(stream);

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };

            _dbcontext.Add(game);
            _dbcontext.SaveChanges();
        }
    }
}
