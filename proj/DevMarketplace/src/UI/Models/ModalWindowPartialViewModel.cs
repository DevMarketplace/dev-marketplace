using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class ModalWindowPartialViewModel
    {
        public ModalWindowPartialViewModel()
        {
            this.Classes = new List<string>();
        }

        public string Id { get; set; }
        public string Content { get; set; }
        public string FooterContent { get; set; }
        public List<string> Classes { get; set; }
        public string Title { get; set; }
    }
}
