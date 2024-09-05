using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsProject.DTOs
{
    public class FlashCardDTO
    {
        public int CardId { get; set; }
        public string? Front { get; set; }
        public string? Back { get; set; }
    }
}
