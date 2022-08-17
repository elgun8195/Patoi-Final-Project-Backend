using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patoi_Final_Project_Backend.Models
{
    public class Bio
    {
        public int Id { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string YouTube { get; set; }
        public string LogoWhite { get; set; }
        public string LogoBlack { get; set; }

        [NotMapped]
        public IFormFile WhitePhoto { get; set; }
        public string Support { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }


        public string Hotline { get; set; }
        [NotMapped]
        public IFormFile BlackPhoto { get; set; }
    }
}
