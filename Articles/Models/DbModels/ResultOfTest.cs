namespace Articles.Models.DbModels
{
    public class ResultOfTest
    {
        public int Id { get; set; }
        public int CountOfCorrectAnswers { get; set; }
        public DateTime DateTime { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
