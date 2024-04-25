using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ArtsyApp.Models
{
    public class ArtPost
    {
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Artist { get; set; }

        [Required]
        [DisplayName("Photo URL")]
        public string? PhotoUrl { get; set; }

        [Required]
        public string? Description { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime PostDate { get; set; }
    }
}
