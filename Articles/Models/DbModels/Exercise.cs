using System.ComponentModel.DataAnnotations;

namespace Articles.Models.DbModels
{
    public class Exercise
    {
        public int Id { get; set; }

        [Display(Name = "Question*")]
        [Required(ErrorMessage = "Enter the question")]
        public string Question { get; set; }

        public List<Answer>? Answers { get; set; }
        public int TestId { get; set; }
        public Test? Test { get; set; }
    }
}
