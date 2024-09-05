using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsProject.Models
{
    public class Flashcard
    {
        public int CardId { get; set; }
        public string? Front { get; set; }
        public string? Back { get; set; }

        public int StackId { get; set; }
    }
}
