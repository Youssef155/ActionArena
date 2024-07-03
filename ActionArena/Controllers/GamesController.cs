namespace ActionArena.Controllers
{
    public class GamesController : Controller
    {
        private readonly IDevicesServices _devicesServices;
        private readonly ICategoryServices _categoryServices;
        private readonly IGameServices _gameServices;

        public GamesController(ICategoryServices categoryServices, 
                IDevicesServices devicesServices, IGameServices gameServices)
        {
            _categoryServices = categoryServices;
            _devicesServices = devicesServices;
            _gameServices = gameServices;
        }

        public IActionResult Index()
        {
            return View();
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

            await _gameServices.Create(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
