using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
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

                askpService.Open();

                Console.WriteLine("SirenaGateService started:\n" + askpService.BaseAddresses.FirstOrDefault());
                Console.Read();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                Console.Read();
            }
        }
    }
}
