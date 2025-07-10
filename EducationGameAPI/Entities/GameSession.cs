namespace EducationGameAPI.Entities
{
    public class GameSession
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // id của lượt chơi

        public Guid UserId { get; set; } // id của user
        public User User { get; set; }

        public string GameType { get; set; } // kiểu game, tên game

        public DateTime StartTime { get; set; } // thời gian bắt đầu chơi
        public DateTime EndTime { get; set; } // thời gian kết thúc

        public int MaxRounds { get; set; } // tổng số vòng của mỗi lượt chơi
        public int CorrectFirstTry { get; set; } // số lần chọn đúng ngay lần đầu
        public int CorrectSecondTry { get; set; } // số lần chọn đúng ở lần thứ hai (sau khi chọn sai lần đầu)
        public int TotalWrongAnswers { get; set; } // số lần chọn sai

        public double Accuracy { get; set; } // tỷ lệ chọn đúng
        public double Score { get; set; } // tổng điểm của lượt chơi
    }
}
