using System.ComponentModel.DataAnnotations;

namespace Articles.Models.DbModels
{
    public class Answer
    {
        public int Id { get; set; }

        [Display(Name = "Value*")]
        [Required(ErrorMessage = "Enter the value")]
        public string Value { get; set; }

        [Display(Name = "Is correct?")]
        public bool IsCorrect { get; set; }

        public int ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }
    }
}
