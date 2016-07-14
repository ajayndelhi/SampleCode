using BusinessLogic.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    /// <summary>
    /// This class is used to store/provide dependencies
    /// </summary>
    public static class Dependency
    {
        public static IConfiguration BLConfig { get; set; }
    }
}
