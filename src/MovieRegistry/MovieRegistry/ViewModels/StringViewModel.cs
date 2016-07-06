using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.ViewModels
{
    public class StringViewModel : NotifyingViewModel
    {
        private string content;
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                if (value != null)
                {
                    content = value;
                    OnPropertyChanged("Content");
                }
            }
        }
    }
}
