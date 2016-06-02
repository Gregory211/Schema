using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using Sirena;

namespace SirenaTravelProxyGateWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISirenaGateService
    {
        [OperationContract]
        string SendStringRequest(string data, ConnectionMode connectionMode = ConnectionMode.Plain);

        [OperationContract]
        Task<string> StringRequestAsync(string data, ConnectionMode connectionMode = ConnectionMode.Plain);

        [OperationContract]
        Task<KeyInfoResponse> KeyInfoRequestAsync(KeyInfoRequest keyInfoRequest);

        [OperationContract]
        KeyInfoResponse SendKeyInfoRequest(KeyInfoRequest keyInfoRequest);

        [OperationContract]
        Task<ScheduleResponse> ScheduleRequestAsync(ScheduleRequest scheduleRequest);

        [OperationContract]
        ScheduleResponse SendScheduleRequest(ScheduleRequest scheduleRequest);

        [OperationContract]
        Task<AvailabilityResponse> AvailabilityRequestAsync(AvailabilityRequest availabilityRequest);

        [OperationContract]
        AvailabilityResponse SendAvailabilityRequest(AvailabilityRequest availabilityRequest);

        [OperationContract]
        Task<FaresResponse> FaresRequestAsync(FaresRequest faresRequest);

        [OperationContract]
        FaresResponse SendFaresRequest(FaresRequest faresRequest);

        [OperationContract]
        Task<FareremarkResponse> FareremarkRequestAsync(FareremarkRequest fareremarkRequest);

        [OperationContract]
        FareremarkResponse SendFareremarkRequest(FareremarkRequest fareremarkRequest);
    }
}
