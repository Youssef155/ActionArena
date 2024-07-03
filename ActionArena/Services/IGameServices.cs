namespace ActionArena.Services
{
    public interface IGameServices
    {
        Task Create(CreateGameFormVM vm);
    }
}
