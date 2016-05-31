using System;

namespace SirenaTravelProxyGateWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SirenaGateService" in both code and config file together.
    public class SirenaGateService : ISirenaGateService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
