using System.ComponentModel.DataAnnotations;

namespace Ada.Kanban.Service.Models
{
    public class UpdateCardModel
    {
        [Required]
        public string? Titulo { get; set; }

        [Required]
        public string? Conteudo { get; set; }

        [Required]
        public string? Lista { get; set; }
    }
}
