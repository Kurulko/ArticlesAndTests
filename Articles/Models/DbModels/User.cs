using Microsoft.AspNetCore.Identity;

namespace Articles.Models.DbModels
{
    public class User : IdentityUser
    {
        public DateTime DateTime { get; set; }
        public List<ResultOfTest>? ResultsOfTests { get; set; }
    }
}
