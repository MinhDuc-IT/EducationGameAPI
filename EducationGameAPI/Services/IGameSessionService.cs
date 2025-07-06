using EducationGameAPI.Models;

namespace EducationGameAPI.Services
{
    public interface IGameSessionService
    {
        Task<Guid> CreateGameSessionAsync(GameSessionCreateDto gameSessionCreateDto, Guid userId);
        Task<GameSessionDto?> GetLatestSessionAsync(Guid userId);
        Task<GameSummaryDto> GetGameSummaryAsync(Guid userId);
    }
}
