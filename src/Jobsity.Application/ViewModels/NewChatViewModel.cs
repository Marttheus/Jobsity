using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Application.ViewModels
{
    public class NewChatViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
