using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Sirena;
using Sirena.Dto.Requests;
using Sirena.Dto.Responses;

namespace SirenaTravelProxyGateWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SirenaGateService" in both code and config file together.
    public class SirenaGateService : ISirenaGateService
    {
        private readonly SirenaClient _client;

        public SirenaGateService()
        {

            var settings = new SirenaClientSettings()
            {
                UserId = 922,
                Host = ConfigurationManager.AppSettings["SirenaGateIp"] ?? "193.104.87.251",
                Port = 34323,
                PublicRsaKey = new RSAParameters()
            };

            if (ConfigurationManager.AppSettings["SirenaGatePort"] != null)
                settings.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SirenaGatePort"]);

            _client = new SirenaClient(settings);
        }
        public string SendStringRequest(string data, ConnectionMode connectionMode = ConnectionMode.Plain)
        {
            try
            {
                _client.Connect();
                var result = _client.SendRequest(data);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }

        }
        public async Task<string> StringRequestAsync(string data, ConnectionMode connectionMode = ConnectionMode.Plain)
        {
            try
            {
                _client.Connect();
                var result = await _client.SendRequestAsync(data);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }

        public async Task<KeyInfoResponse> KeyInfoRequestAsync(KeyInfoRequest keyInfoRequest)
        {
            try
            {
                _client.Connect();
                var result = await _client.SendRequestAsync(keyInfoRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }
        public KeyInfoResponse SendKeyInfoRequest(KeyInfoRequest keyInfoRequest)
        {
            try
            {
                _client.Connect();
                var result = _client.SendRequest(keyInfoRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }

        public async Task<ScheduleResponse> ScheduleRequestAsync(ScheduleRequest scheduleRequest)
        {
            try
            {
                _client.Connect();
                var result = await _client.SendRequestAsync(scheduleRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }
        public ScheduleResponse SendScheduleRequest(ScheduleRequest scheduleRequest)
        {
            try
            {
                _client.Connect();
                var result = _client.SendRequest(scheduleRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }

        public async Task<AvailabilityResponse> AvailabilityRequestAsync(AvailabilityRequest availabilityRequest)
        {
            try
            {
                _client.Connect();
                var result = await _client.SendRequestAsync(availabilityRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }
        public AvailabilityResponse SendAvailabilityRequest(AvailabilityRequest availabilityRequest)
        {
            try
            {
                _client.Connect();
                var result = _client.SendRequest(availabilityRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }

        public async Task<FaresResponse> FaresRequestAsync(FaresRequest faresRequest)
        {
            try
            {
                _client.Connect();
                var result = await _client.SendRequestAsync(faresRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }
        public FaresResponse SendFaresRequest(FaresRequest faresRequest)
        {
            try
            {
                _client.Connect();
                var result = _client.SendRequest(faresRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }

        public async Task<FareremarkResponse> FareremarkRequestAsync(FareremarkRequest fareremarkRequest)
        {
            try
            {
                _client.Connect();
                var result = await _client.SendRequestAsync(fareremarkRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }
        public FareremarkResponse SendFareremarkRequest(FareremarkRequest fareremarkRequest)
        {
            try
            {
                _client.Connect();
                var result = _client.SendRequest(fareremarkRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }

        public async Task<PricingResponse> PricingRequestAsync(PricingRequest pricingRequest)
        {
            try
            {
                _client.Connect();
                var result = await _client.SendRequestAsync(pricingRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }
        public PricingResponse SendPricingRequest(PricingRequest pricingRequest)
        {
            try
            {
                _client.Connect();
                var result = _client.SendRequest(pricingRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }

        public async Task<BookingResponse> BookingRequestAsync(BookingRequest bookingRequest)
        {
            try
            {
                _client.Connect();
                var result = await _client.SendRequestAsync(bookingRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }
        public BookingResponse SendBookingRequest(BookingRequest bookingRequest)
        {
            try
            {
                _client.Connect();
                var result = _client.SendRequest(bookingRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }

        public async Task<BookingCancelResponse> BookingCancelRequestAsync(BookingCancelRequest bookingCancelRequest)
        {
            try
            {
                _client.Connect();
                var result = await _client.SendRequestAsync(bookingCancelRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }
        public BookingCancelResponse SendBookingCancelRequest(BookingCancelRequest bookingCancelRequest)
        {
            try
            {
                _client.Connect();
                var result = _client.SendRequest(bookingCancelRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }

        public async Task<SellingQueryResponse> SellingQueryRequestAsync(SellingQueryRequest sellingQueryRequest)
        {
            try
            {
                _client.Connect();
                var result = await _client.SendRequestAsync(sellingQueryRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }
        public SellingQueryResponse SendSellingQueryRequest(SellingQueryRequest sellingQueryRequest)
        {
            try
            {
                _client.Connect();
                var result = _client.SendRequest(sellingQueryRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }

        public async Task<PaymentExtAuthConfirmResponse> PaymentExtAuthConfirmRequestAsync(PaymentExtAuthConfirmRequest paymentExtAuthConfirmRequest)
        {
            try
            {
                _client.Connect();
                var result = await _client.SendRequestAsync(paymentExtAuthConfirmRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }
        public PaymentExtAuthConfirmResponse SendPaymentExtAuthConfirmRequest(PaymentExtAuthConfirmRequest paymentExtAuthConfirmRequest)
        {
            try
            {
                _client.Connect();
                var result = _client.SendRequest(paymentExtAuthConfirmRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }

        public async Task<PaymentExtAuthQueryResponse> PaymentExtAuthQueryRequestAsync(PaymentExtAuthQueryRequest paymentExtAuthQueryRequest)
        {
            try
            {
                _client.Connect();
                var result = await _client.SendRequestAsync(paymentExtAuthQueryRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }
        public PaymentExtAuthQueryResponse SendPaymentExtAuthQueryRequest(PaymentExtAuthQueryRequest paymentExtAuthQueryRequest)
        {
            try
            {
                _client.Connect();
                var result = _client.SendRequest(paymentExtAuthQueryRequest);
                _client.Close();
                return result;
            }
            catch (Exception)
            {
                _client.Close();
                return null;
            }
        }
    }
}
