using EducationGameAPI.Data;
using EducationGameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationGameAPI.Services
{
    public class GameSessionService(AppDbContext context, IConfiguration configuration) : IGameSessionService
    {
        public async Task<Guid> CreateGameSessionAsync(GameSessionCreateDto gameSessionCreateDto, Guid userId)
        {
            var user = await context.Users.FindAsync(userId);
            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            // Tổng số câu có đáp án đúng (lần 1 + lần 2)
            int totalCorrect = gameSessionCreateDto.CorrectFirstTry + gameSessionCreateDto.CorrectSecondTry;

            // Tổng số lựa chọn của người dùng
            int totalAttempts = totalCorrect + gameSessionCreateDto.TotalWrongAnswers;

            double totalAccuracy = 0;
            if (totalAttempts > 0)
            {
                totalAccuracy = (double)totalCorrect / totalAttempts;
            }

            double totalScore = (gameSessionCreateDto.CorrectFirstTry * 2) + (gameSessionCreateDto.CorrectSecondTry * 1);

            var gameSession = new Entities.GameSession
            {
                UserId = userId,
                GameType = gameSessionCreateDto.GameType,
                StartTime = gameSessionCreateDto.StartTime,
                EndTime = gameSessionCreateDto.EndTime,
                MaxRounds = gameSessionCreateDto.MaxRounds,
                CorrectFirstTry = gameSessionCreateDto.CorrectFirstTry,
                CorrectSecondTry = gameSessionCreateDto.CorrectSecondTry,
                TotalWrongAnswers = gameSessionCreateDto.TotalWrongAnswers,
                Accuracy = totalAccuracy,
                Score = totalScore
            };

            context.GameSessions.Add(gameSession);
            await context.SaveChangesAsync();

            return gameSession.Id;
        }

        public async Task<GameSummaryDto?> GetGameSummaryAsync(Guid userId)
        {
            var gameSessions = await context.GameSessions
                .Where(gs => gs.UserId == userId)
                .ToListAsync();

            if (gameSessions.Count == 0) return null;

            var totalSeconds = gameSessions.Sum(gs => (int)(gs.EndTime - gs.StartTime).TotalSeconds);
            var totalScore = gameSessions.Sum(gs => gs.Score);
            var averageAccuracy = gameSessions.Average(gs => gs.Accuracy);

            return new GameSummaryDto
            {
                TotalSeconds = totalSeconds,
                TotalScore = totalScore,
                Accuracy = averageAccuracy
            };
        }

        public async Task<GameSessionDto?> GetLatestSessionAsync(Guid userId, string gameType)
        {
            var latestSession = await context.GameSessions
                .Where(gs => gs.UserId == userId && gs.GameType == gameType)
                .OrderByDescending(gs => gs.EndTime)
                .FirstOrDefaultAsync();

            if (latestSession == null) return null;

            var seconds = (int)(latestSession.EndTime - latestSession.StartTime).TotalSeconds;

            return new GameSessionDto
            {
                Seconds = seconds,
                MaxRounds = latestSession.MaxRounds,
                CorrectFirstTry = latestSession.CorrectFirstTry,
                CorrectSecondTry = latestSession.CorrectSecondTry
            };
        }
    }
}
