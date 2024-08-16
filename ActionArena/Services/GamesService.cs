
namespace ActionArena.Services
{
    public class GamesService : IGamesService
    {
        private readonly ActionArenaDbContext _dbcontext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _coverPath;

        public GamesService(ActionArenaDbContext dbcontext, IWebHostEnvironment webHostEnvironment)
        {
            _dbcontext = dbcontext;
            _webHostEnvironment = webHostEnvironment;
            _coverPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public async Task Create(CreateGameFormVM model)
        {
            var coverName = await SaveCover(model.Cover);

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

        public bool Delete(int id)
        {
            var isDeleted = false;

            var game = _dbcontext.Games.Find(id);
            if (game is null) 
            {
                return isDeleted;
            }

            _dbcontext.Remove(game);

            var effectedRows = _dbcontext.SaveChanges();

            if(effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_coverPath, game.Cover);
                File.Delete(cover);
            }

            return isDeleted;
        }

        public IEnumerable<Game> GetAll()
        {
            return _dbcontext.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();
        }

        public Game? GetById(int id)
        {
            return _dbcontext.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }

        public async Task<Game?> Update(UpdateGameFormViewModel vm)
        {
            var game = _dbcontext.Games
                .Include(g=>g.Devices)
                .SingleOrDefault(g => g.Id == vm.id);
            if (game == null)
                return null;

            var hasNewCover = vm.Cover is not null;
            var oldCover = game.Cover;

            game.Name = vm.Name;
            game.Description = vm.Description;
            game.CategoryId = vm.CategoryId;
            game.Devices = vm.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if (hasNewCover)
            {
                game.Cover = await SaveCover(vm.Cover!);
            }

            var effectedRows = _dbcontext.SaveChanges();

            if (effectedRows > 0)
            {
                if(hasNewCover)
                {
                    var cover = Path.Combine(_coverPath, oldCover);
                    File.Delete(cover);
                }

                return game;
            }
            else
            {
                var cover = Path.Combine(_coverPath, game.Cover);
                File.Delete(cover);

                return null;
            }

           
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_coverPath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }
    }
}
