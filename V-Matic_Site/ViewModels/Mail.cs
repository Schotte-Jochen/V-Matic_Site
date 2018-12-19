using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vmatic_Site.ViewModels
{
    public class Mail
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Sender { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}
