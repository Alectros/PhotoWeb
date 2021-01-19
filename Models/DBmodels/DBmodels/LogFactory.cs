using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Config;

[assembly: log4net.Config.XmlConfigurator(Watch =true)]
namespace PhotoWEB.Models.DBmodels.DBmodels
{
    public class LogFactory
    {
        private ILog log; 
        public LogFactory()
        {
            XmlConfigurator.Configure(System.IO.File.OpenRead("log4net.config.xml"));
            log = LogManager.GetLogger("Logger");
        }

        public ILog GetLogger()
        {
            return log;
        }
    }
}
