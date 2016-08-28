using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Sirena;
using SirenaTravelProxyGateWcf;

namespace SirenaServiceSelfhost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceHost askpService = new ServiceHost(typeof(SirenaGateService));
                bool loggerEnabled = false;

                askpService.Open();

                if (ConfigurationManager.AppSettings["LoggerEnabled"] != null)
                    loggerEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["LoggerEnabled"]);

                if(loggerEnabled)
                    Logger.Info("SirenaGateService started:\n" + askpService.BaseAddresses.FirstOrDefault());

                Console.WriteLine("SirenaGateService started:\n" + askpService.BaseAddresses.FirstOrDefault());
                Console.Read();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                Console.ReadLine();
            }
        }
    }
}
