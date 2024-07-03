
namespace ActionArena.Services
{
    public class DevicesServices : IDevicesServices
    {
        private readonly ActionArenaDbContext _dbContext;

        public DevicesServices(ActionArenaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<SelectListItem> GetDevices()
        {
            return _dbContext.Devices
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .OrderBy(d => d.Text)
                    .AsNoTracking()
                    .ToList();
        }
    }
}
