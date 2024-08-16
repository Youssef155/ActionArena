namespace ActionArena.Controllers
{
    public class GamesController : Controller
    {
        private readonly IDevicesServices _devicesServices;
        private readonly ICategoryServices _categoryServices;
        private readonly IGamesService _gamesService;

        public GamesController(ICategoryServices categoryServices, 
                IDevicesServices devicesServices, IGamesService gamesService)
        {
            _categoryServices = categoryServices;
            _devicesServices = devicesServices;
            _gamesService = gamesService;
        }

        public IActionResult Index()
        {
            var games = _gamesService.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = _gamesService.GetById(id);
            if(game is null)
            {
                return NotFound();
            }
            return View(game);
        }

        public IActionResult Update(int id)
        {
            var game = _gamesService.GetById(id);
            if (game is null)
            {
                return NotFound();
            }

            UpdateGameFormViewModel viewModel = new()
            {
                id = id,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoryServices.GetCategories(),
                Devices = _devicesServices.GetDevices(),
                CurrentCover = game.Cover,

            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {

            CreateGameFormVM vm = new()
            {
                Categories = _categoryServices.GetCategories(),

                Devices = _devicesServices.GetDevices()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormVM model)
        {
            if(!ModelState.IsValid)
            {
                model.Categories = _categoryServices.GetCategories();
                model.Devices = _devicesServices.GetDevices();

                return View(model);
            }

            await _gamesService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoryServices.GetCategories();
                model.Devices = _devicesServices.GetDevices();

                return View(model);
            }

            var game = await _gamesService.Update(model);
            if(game is null) 
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _gamesService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }

    }
}
