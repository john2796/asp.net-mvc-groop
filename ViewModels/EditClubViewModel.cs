using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.ViewModels
{
    public class EditClubViewModel
    {
        public int Id { get; set; }
        public string Titl { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public Address? AddressId { get; set; }
        public ClubCategory Address { get; set; }
    }
}
