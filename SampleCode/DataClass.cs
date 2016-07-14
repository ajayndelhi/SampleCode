using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    /// <summary>
    /// Data class for List View
    /// </summary>
    public class DataClass
    {
        /// <summary>
        /// Item Serial Number
        /// </summary>
        public int SerialNumber { get; set; }

        /// <summary>
        /// Text to be displayed on screen
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// Text containing link to more details
        /// </summary>
        public string LinkText { get; set; }
    }
}
