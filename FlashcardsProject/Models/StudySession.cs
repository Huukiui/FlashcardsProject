using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsProject.Models
{
    public class StudySession
    {
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }
        public int StackId { get; set; }
    }
}
