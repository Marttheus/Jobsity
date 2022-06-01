using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Application.ViewModels
{
    public class MessageViewModel
    {
        public string Text { get; set; }
        public string Sender { get; set; }
        public string Group { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
