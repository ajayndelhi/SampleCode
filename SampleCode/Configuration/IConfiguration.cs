using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Configuration
{
    /// <summary>
    /// Interface to get configuration values
    /// </summary>
    public interface IConfiguration
    {
        string GetAppSetting(string key);
    }
}
