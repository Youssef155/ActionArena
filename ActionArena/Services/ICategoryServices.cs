namespace ActionArena.Services
{
    public interface ICategoryServices
    {
        IEnumerable<SelectListItem> GetCategories();
    }
}
