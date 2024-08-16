namespace ActionArena.Services
{
    public interface IGamesService
    {
        Task Create(CreateGameFormVM vm);
        Task<Game?> Update(UpdateGameFormViewModel vm);
        IEnumerable<Game> GetAll();
        Game? GetById(int id);
        bool Delete(int id);
    }
}
