using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using Sirena;
using Sirena.Dto.Requests;
using Sirena.Dto.Responses;

namespace SirenaTravelProxyGateWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISirenaGateService
    {
        [OperationContract]
        string SendStringRequest(string data);

        [OperationContract]
        Task<string> StringRequestAsync(string data);


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

        [OperationContract]
        Task<PricingResponse> PricingRequestAsync(PricingRequest pricingRequest);

        [OperationContract]
        PricingResponse SendPricingRequest(PricingRequest pricingRequest);

        [OperationContract]
        Task<BookingResponse> BookingRequestAsync(BookingRequest bookingRequest);

        [OperationContract]
        BookingResponse SendBookingRequest(BookingRequest bookingRequest);

        [OperationContract]
        Task<BookingCancelResponse> BookingCancelRequestAsync(BookingCancelRequest bookingCancelRequest);

        [OperationContract]
        BookingCancelResponse SendBookingCancelRequest(BookingCancelRequest bookingCancelRequest);

        [OperationContract]
        Task<SellingQueryResponse> SellingQueryRequestAsync(SellingQueryRequest sellingQueryRequest);

        [OperationContract]
        SellingQueryResponse SendSellingQueryRequest(SellingQueryRequest bookingCancelRequest);

        [OperationContract]
        Task<PaymentExtAuthConfirmResponse> PaymentExtAuthConfirmRequestAsync(PaymentExtAuthConfirmRequest paymentExtAuthConfirmRequest);

        [OperationContract]
        PaymentExtAuthConfirmResponse SendPaymentExtAuthConfirmRequest(PaymentExtAuthConfirmRequest paymentExtAuthConfirmRequest);

        [OperationContract]
        Task<PaymentExtAuthQueryResponse> PaymentExtAuthQueryRequestAsync(PaymentExtAuthQueryRequest paymentExtAuthQueryRequest);

        [OperationContract]
        PaymentExtAuthQueryResponse SendPaymentExtAuthQueryRequest(PaymentExtAuthQueryRequest paymentExtAuthQueryRequest);

        [OperationContract]
        Task<GetItinReceiptsResponse> GetInitReceiptsRequestAsync(GetInitReceiptsRequest getInitReceiptsRequest);

        [OperationContract]
        GetItinReceiptsResponse SendGetInitReceiptsRequest(GetInitReceiptsRequest getInitReceiptsRequest);

    }
}
