namespace ActionArena.Services
{
    public interface IGamesService
    {
        Task Create(CreateGameFormVM vm);
        IEnumerable<Game> GetAll();
    }
}
