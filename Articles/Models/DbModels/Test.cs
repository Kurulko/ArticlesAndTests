using System.ComponentModel.DataAnnotations;

namespace Articles.Models.DbModels
{
    public class Test
    {
        public int Id { get; set; }

        [Display(Name = "Name of the test*")]
        [Required(ErrorMessage = "Enter the name")]
        public string Name { get; set; }

        [Display(Name = "Description of the test*")]
        [Required(ErrorMessage = "Enter the description")]
        public string Description { get; set; }

        [Display(Name = "Image of the test*")]
        [Required(ErrorMessage = "Enter the url of the image")]
        [DataType(DataType.Url, ErrorMessage = "It isn't url!")]
        public string ImageUrl { get; set; }

        public List<Exercise>? Exercises { get; set; }
        public List<ResultOfTest>? Results { get; set; }
    }
}
