using System.Drawing;

namespace ArtsyApp.Models
{
    public class ArtWorksModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public Image? Image { get; set; }
        public string? Description { get; set; }
        public string? Artist { get; set; }
        public string? Category { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
