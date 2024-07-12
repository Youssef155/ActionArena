namespace ActionArena.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ActionArenaDbContext _db;

        public CategoryServices(ActionArenaDbContext db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetCategories()
        {
            return _db.Categories
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .OrderBy(c => c.Text)
                    .AsNoTracking()
                    .ToList();
        }
    }
}
