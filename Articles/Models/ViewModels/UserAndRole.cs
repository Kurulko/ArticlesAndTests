using Articles.Models.DbModels;

namespace Articles.Models.ViewModels
{
    public record UserAndRole(User User, string? Role);
}
