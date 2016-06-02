﻿using System;
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
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }
    }
}