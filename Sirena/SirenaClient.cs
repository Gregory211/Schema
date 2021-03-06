﻿using System;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Sirena.Cryptography;
using Sirena.Dto.Requests;
using Sirena.Dto.Responses;
using Sirena.Helpers;

namespace Sirena
{
    /// <summary>
    /// Provides low level techniques for working 
    /// with the Sirena tcp server.
    /// </summary>
    public sealed class SirenaClient
    {
        private DESCryptography desCryptography;

        private RSACryptography rsaCryptography;

        private SirenaClientSettings clientSettings;

        private TcpClient client;

        private bool LoggerEnabled;

        /// <summary>
        /// Gets the client unique id.
        /// </summary>
        public Guid Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the connected state.
        /// </summary>
        public Boolean IsConnected
        {
            get
            {
                return client.Connected;
            }
        }

        private SirenaClient() { }

        /// <summary>
        /// Initializes an instance of the SirenaClient class.
        /// </summary>
        /// <param name="clientSettings">The settings that will be applied during communication.</param>
        public SirenaClient(SirenaClientSettings clientSettings)
        {
            if (clientSettings == null)
            {
                throw new ArgumentNullException("clientSettings can not be null");
            }

            if (ConfigurationManager.AppSettings["LoggerEnabled"] != null)
                LoggerEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["LoggerEnabled"]);

            Id = Guid.NewGuid();

            desCryptography = DESCryptography.Instance;
            rsaCryptography = new RSACryptography();

            this.clientSettings = clientSettings;

            client = new TcpClient();
        }

        /// <summary>
        /// Sends a request to the Sirena server.
        /// </summary>
        /// <param name="data">A byte array containing the request.</param>
        /// <param name="connectionMode">The connection mode used for sending a request.</param>
        /// <returns>Returns a byte array contains the response.</returns>
        private Byte[] SendRequest(Byte[] data, ConnectionMode connectionMode = ConnectionMode.Plain)
        {
            var response = SendRequestAsync(data, connectionMode).Result;
            return response;
        }

        /// <summary>
        /// Asynchronously sends a request to the Sirena server.
        /// </summary>
        /// <param name="data">A byte array containing the request.</param>
        /// <param name="connectionMode">The connection mode used for sending a request.</param>
        /// <returns>Returns the task object representing the asynchronous operation.</returns>
        private async Task<Byte[]> SendRequestAsync(Byte[] data, ConnectionMode connectionMode = ConnectionMode.Plain)
        {
            data = EncryptData(data, connectionMode);
            var requestHeader = new QueryHeader(clientSettings.UserId, data.Length, connectionMode, desCryptography.Id);
            var rawRequest = new Byte[data.Length + QueryHeader.HeaderSize];
            Array.Copy(requestHeader.HeaderData, 0, rawRequest, 0, requestHeader.HeaderData.Length);
            Array.Copy(data, 0, rawRequest, QueryHeader.HeaderSize, data.Length);

            var stream = client.GetStream();
            await stream.WriteAsync(rawRequest, 0, rawRequest.Length);

            var rawResponseHeader = await stream.ReadBytesAsync(QueryHeader.HeaderSize);
            var header = new QueryHeader(rawResponseHeader.ToArray());
            var rawResponseBody = await stream.ReadBytesAsync(header.MessageLength);
            rawResponseBody = DecryptData(rawResponseBody, connectionMode);

            return rawResponseBody;
        }

        /// <summary>
        /// Sends a request to the Sirena server.
        /// </summary>
        /// <param name="data">A string containing the request.</param>
        /// <param name="connectionMode">The connection mode used for sending a request.</param>
        /// <returns>Returns a string contains the response.</returns>
        public string SendRequest(String data, ConnectionMode connectionMode = ConnectionMode.Plain)
        {
            var response = SendRequestAsync(data, connectionMode).Result;
            return response;
        }

        /// <summary>
        /// Asynchronously sends a request to the Sirena server.
        /// </summary>
        /// <param name="data">A string containing the request.</param>
        /// <param name="connectionMode">The connection mode used for sending a request.</param>
        /// <returns>Returns the task object representing the asynchronous operation.</returns>
        public async Task<String> SendRequestAsync(String data, ConnectionMode connectionMode = ConnectionMode.Plain)
        {
            var rawData = Encoding.UTF8.GetBytes(data);
            var rawResponse = await SendRequestAsync(rawData, connectionMode);
            var response = Encoding.UTF8.GetString(rawResponse);
            return response;
        }

        /// <summary>
        /// Sends a request to the Sirena server.
        /// </summary>
        /// <typeparam name="TDtoRequest">The dto request type.</typeparam>
        /// <typeparam name="TDtoResponse">The dto response type.</typeparam>
        /// <param name="dtoRequest">The dto request to send.</param>
        /// <param name="connectionMode">The connection mode used for sending a request.</param>
        /// <returns>Returns a dto response object.</returns>
        public TDtoResponse SendRequest<TDtoRequest, TDtoResponse>(TDtoRequest dtoRequest, ConnectionMode connectionMode = ConnectionMode.Plain)
            where TDtoRequest : DtoRequest
            where TDtoResponse : DtoResponse
        {
            var dtoResponse = SendRequestAsync<TDtoRequest, TDtoResponse>(dtoRequest, connectionMode).Result;
            return dtoResponse;
        }

        /// <summary>
        /// Asynchronously sends a request to the Sirena server.
        /// </summary>
        /// <typeparam name="TDtoRequest">The dto request type.</typeparam>
        /// <typeparam name="TDtoResponse">The dto response type.</typeparam>
        /// <param name="dtoRequest">The dto request to send.</param>
        /// <param name="connectionMode">The connection mode used for sending a request.</param>
        /// <returns>Returns the task object representing the asynchronous operation.</returns>
        public async Task<TDtoResponse> SendRequestAsync<TDtoRequest, TDtoResponse>(TDtoRequest dtoRequest, ConnectionMode connectionMode = ConnectionMode.Plain)
            where TDtoRequest : DtoRequest 
            where TDtoResponse : DtoResponse
        {
            var requestXml = SerializationHelper.Serialize(dtoRequest);
            var responseXml = await SendRequestAsync(requestXml, connectionMode);

            if (LoggerEnabled)
            {
                Logger.Info("Request:\n" + requestXml + "\nResponse:\n" + responseXml);
            }

            var dtoResponse = SerializationHelper.Deserialize<TDtoResponse>(responseXml);
            return dtoResponse;
        }

        /// <summary>
        /// Asynchronously sends a KeyInfoRequest object to the Sirena server.
        /// </summary>
        /// <param name="keyInfoRequest">The KeyInfoRequest request to send.</param>
        /// <returns>Returns the task object representing the asynchronous operation.</returns>
        public async Task<KeyInfoResponse> SendRequestAsync(KeyInfoRequest keyInfoRequest)
        {
            return await SendRequestAsync<KeyInfoRequest, KeyInfoResponse>(keyInfoRequest);
        }

        /// <summary>
        /// Sends a KeyInfoRequest object to the Sirena server.
        /// </summary>
        /// <param name="keyInfoRequest">The KeyInfoRequest object to send.</param>
        /// <returns>Returns a KeyInfoResponse object contains the response.</returns>
        public KeyInfoResponse SendRequest(KeyInfoRequest keyInfoRequest)
        {
            return SendRequest<KeyInfoRequest, KeyInfoResponse>(keyInfoRequest);
        }

        /// <summary>
        /// Asynchronously sends a ScheduleRequest object to the Sirena server.
        /// </summary>
        /// <param name="scheduleRequest">The ScheduleRequest request to send.</param>
        /// <returns>Returns the task object representing the asynchronous operation.</returns>
        public async Task<ScheduleResponse> SendRequestAsync(ScheduleRequest scheduleRequest)
        {
            return await SendRequestAsync<ScheduleRequest, ScheduleResponse>(scheduleRequest);
        }

        /// <summary>
        /// Sends a ScheduleRequest object to the Sirena server.
        /// </summary>
        /// <param name="scheduleRequest">The ScheduleRequest object to send.</param>
        /// <returns>Returns a ScheduleResponse object contains the response.</returns>
        public ScheduleResponse SendRequest(ScheduleRequest scheduleRequest)
        {
            return SendRequest<ScheduleRequest, ScheduleResponse>(scheduleRequest);
        }

        /// <summary>
        /// Asynchronously sends an AvailabilityRequest object to the Sirena server.
        /// </summary>
        /// <param name="availabilityRequest">The AvailabilityRequest object to send.</param>
        /// <returns>Returns the task object representing the asynchronous operation.</returns>
        public async Task<AvailabilityResponse> SendRequestAsync(AvailabilityRequest availabilityRequest)
        {
            return await SendRequestAsync<AvailabilityRequest, AvailabilityResponse>(availabilityRequest);
        }

        /// <summary>
        /// Sends an AvailabilityRequest object to the Sirena server.
        /// </summary>
        /// <param name="availabilityRequest">The AvailabilityRequest object to send.</param>
        /// <returns>Returns a AvailabilityResponse object contains the response.</returns>
        public AvailabilityResponse SendRequest(AvailabilityRequest availabilityRequest)
        {
            return SendRequest<AvailabilityRequest, AvailabilityResponse>(availabilityRequest);
        }

        /// <summary>
        /// Asynchronously sends a FaresRequest object to the Sirena server.
        /// </summary>
        /// <param name="faresRequest">The FaresRequest object to send.</param>
        /// <returns>Returns the task object representing the asynchronous operation.</returns>
        public async Task<FaresResponse> SendRequestAsync(FaresRequest faresRequest)
        {
            return await SendRequestAsync<FaresRequest, FaresResponse>(faresRequest);
        }

        /// <summary>
        /// Sends a FaresRequest object to the Sirena server.
        /// </summary>
        /// <param name="faresRequest">The FaresRequest object to send.</param>
        /// <returns>Returns a FaresResponse object contains the response.</returns>
        public FaresResponse SendRequest(FaresRequest faresRequest)
        {
            return SendRequest<FaresRequest, FaresResponse>(faresRequest);
        }

        /// <summary>
        /// Asynchronously sends a FareremarkRequest object to the Sirena server.
        /// </summary>
        /// <param name="fareremarkRequest">The FareremarkRequest object to send.</param>
        /// <returns>Returns the task object representing the asynchronous operation.</returns>
        public async Task<FareremarkResponse> SendRequestAsync(FareremarkRequest fareremarkRequest)
        {
            return await SendRequestAsync<FareremarkRequest, FareremarkResponse>(fareremarkRequest);
        }

        /// <summary>
        /// Sends a FareremarkRequest object to the Sirena server.
        /// </summary>
        /// <param name="fareremarkRequest">The FareremarkRequest object to send.</param>
        /// <returns>Returns a FareremarkResponse object contains the response.</returns>
        public FareremarkResponse SendRequest(FareremarkRequest fareremarkRequest)
        {
            return SendRequest<FareremarkRequest, FareremarkResponse>(fareremarkRequest);
        }

        /// <summary>
        /// Asynchronously sends a DescribeRequest object to the Sirena server.
        /// </summary>
        /// <param name="describeRequest"></param>
        /// <returns>Returns the task object representing the asynchronous operation.</returns>
        public async Task<DescribeResponse> SendRequestAsync(DescribeRequest describeRequest)
        {
            return await SendRequestAsync<DescribeRequest, DescribeResponse>(describeRequest);
        }

        /// <summary>
        /// Sends a DescribeRequest object to the Sirena server.
        /// </summary>
        /// <returns>Returns a DescribeResponse object contains the response.</returns>
        public DescribeResponse SendRequest(DescribeRequest describeRequest)
        {
            return SendRequest<DescribeRequest, DescribeResponse>(describeRequest);
        }


        /// <summary>
        /// Asynchronously sends a PricingRequest object to the Sirena server.
        /// </summary>
        /// <param name="pricingRequest"></param>
        /// <returns>Returns the task object representing the asynchronous operation.</returns>
        public async Task<PricingResponse> SendRequestAsync(PricingRequest pricingRequest)
        {
            return await SendRequestAsync<PricingRequest, PricingResponse>(pricingRequest);
        }

        /// <summary>
        /// Sends a PricingRequest object to the Sirena server.
        /// </summary>
        /// <returns>Returns a PricingResponse object contains the response.</returns>
        public PricingResponse SendRequest(PricingRequest pricingRequest)
        {
            return SendRequest<PricingRequest, PricingResponse>(pricingRequest);
        }

        /// <summary>
        /// Asynchronously sends a bookingRequest object to the Sirena server.
        /// </summary>
        /// <param name="bookingRequest"></param>
        /// <returns>Returns the task object representing the asynchronous operation.</returns>
        public async Task<BookingResponse> SendRequestAsync(BookingRequest bookingRequest)
        {
            return await SendRequestAsync<BookingRequest, BookingResponse>(bookingRequest);
        }

        /// <summary>
        /// Sends a bookingRequest object to the Sirena server.
        /// </summary>
        /// <returns>Returns a BookingResponse object contains the response.</returns>
        public BookingResponse SendRequest(BookingRequest bookingRequest)
        {
            return SendRequest<BookingRequest, BookingResponse>(bookingRequest);
        }

        public async Task<BookingCancelResponse> SendRequestAsync(BookingCancelRequest bookingRequest)
        {
            return await SendRequestAsync<BookingCancelRequest, BookingCancelResponse>(bookingRequest);
        }

        public BookingCancelResponse SendRequest(BookingCancelRequest bookingRequest)
        {
            return SendRequest<BookingCancelRequest, BookingCancelResponse>(bookingRequest);
        }

        public async Task<SellingQueryResponse> SendRequestAsync(SellingQueryRequest sellingQueryRequest)
        {
            return await SendRequestAsync<SellingQueryRequest, SellingQueryResponse>(sellingQueryRequest);
        }

        public SellingQueryResponse SendRequest(SellingQueryRequest bookingRequest)
        {
            return SendRequest<SellingQueryRequest, SellingQueryResponse>(bookingRequest);
        }

        public async Task<PaymentExtAuthConfirmResponse> SendRequestAsync(PaymentExtAuthConfirmRequest paymentExtAuthConfirmRequest)
        {
            return await SendRequestAsync<PaymentExtAuthConfirmRequest, PaymentExtAuthConfirmResponse>(paymentExtAuthConfirmRequest);
        }

        public PaymentExtAuthConfirmResponse SendRequest(PaymentExtAuthConfirmRequest paymentExtAuthConfirmRequest)
        {
            return SendRequest<PaymentExtAuthConfirmRequest, PaymentExtAuthConfirmResponse>(paymentExtAuthConfirmRequest);
        }

        public async Task<PaymentExtAuthQueryResponse> SendRequestAsync(PaymentExtAuthQueryRequest paymentExtAuthQueryRequest)
        {
            return await SendRequestAsync<PaymentExtAuthQueryRequest, PaymentExtAuthQueryResponse>(paymentExtAuthQueryRequest);
        }

        public PaymentExtAuthQueryResponse SendRequest(PaymentExtAuthQueryRequest paymentExtAuthQueryRequest)
        {
            return SendRequest<PaymentExtAuthQueryRequest, PaymentExtAuthQueryResponse>(paymentExtAuthQueryRequest);
        }

        public async Task<GetItinReceiptsResponse> SendRequestAsync(GetInitReceiptsRequest getInitReceiptsRequest)
        {
            return await SendRequestAsync<GetInitReceiptsRequest, GetItinReceiptsResponse>(getInitReceiptsRequest);
        }

        public GetItinReceiptsResponse SendRequest(GetInitReceiptsRequest getInitReceiptsRequest)
        {
            return SendRequest<GetInitReceiptsRequest, GetItinReceiptsResponse>(getInitReceiptsRequest);
        }


        /// <summary>
        /// Connects to the Sirena server.
        /// </summary>
        public void Connect()
        {
            ConnectAsync().Wait();
        }

        /// <summary>
        /// Asynchronously connects to the Sirena server.
        /// </summary>
        /// <returns>Returns the task object representing the asynchronous operation.</returns>
        public async Task ConnectAsync()
        {
            client = new TcpClient();
            await AcquireConnectionAsync();
          //  await RequestPublicKeyAsync();
          //  await HandshakeAsync();
        }

        private async Task AcquireConnectionAsync()
        {
            await client.ConnectAsync(clientSettings.Host, clientSettings.Port);
        }

        private async Task RequestPublicKeyAsync()
        {
            var keyInfoRequest = new KeyInfoRequest();
            var keyInfoResponse = await SendRequestAsync<KeyInfoRequest, KeyInfoResponse>(keyInfoRequest, ConnectionMode.Plain);

            if (keyInfoResponse.Answer.Body.Error != null)
            {
                var error = keyInfoResponse.Answer.Body.Error;
                throw new SirenaException(error.ToString());
            }

            var publicKey = keyInfoResponse.Answer.Body.KeyManager.ServerPublicKey;

            clientSettings.PublicRsaKey = CryptographyHelper.PKCS1PublicKeyToRSAParameters(publicKey);
        }

        private async Task HandshakeAsync()
        {
            var encryptedServerKey = rsaCryptography.Encrypt(clientSettings.PublicRsaKey, desCryptography.Key);
            var signature = rsaCryptography.Sign(clientSettings.PrivateRsaKey, encryptedServerKey);
            var rawEncryptedServerKeyLength = BitConverter.GetBytes(encryptedServerKey.Length);
            Array.Reverse(rawEncryptedServerKeyLength);

            var requestDataLength = encryptedServerKey.Length + signature.Length + rawEncryptedServerKeyLength.Length;
            var requestData = new Byte[requestDataLength];

            Array.Copy(rawEncryptedServerKeyLength, 0, requestData, 0, 4);
            Array.Copy(encryptedServerKey, 0, requestData, 4, encryptedServerKey.Length);
            Array.Copy(signature, 0, requestData, 4 + encryptedServerKey.Length, signature.Length);

            var rawResponse = await SendRequestAsync(requestData, ConnectionMode.Assymmetric);
            var response = Convert.ToBase64String(rawResponse);
        }

        private Byte[] EncryptData(Byte[] data, ConnectionMode connectionMode)
        {
            switch (connectionMode)
            {
                case ConnectionMode.Symmetric:
                    return EncryptDataWithDES(data);
                case ConnectionMode.Plain:
                case ConnectionMode.Assymmetric:
                default:
                    return data;
                case ConnectionMode.Zipped:
                    throw new NotImplementedException();
            }
        }

        private Byte[] DecryptData(Byte[] data, ConnectionMode connectionMode)
        {
            switch (connectionMode)
            {
                case ConnectionMode.Symmetric:
                    return DecryptDataWithDES(data);
                case ConnectionMode.Plain:
                case ConnectionMode.Assymmetric:
                default:
                    return data;
                case ConnectionMode.Zipped:
                    throw new NotImplementedException();
            }
        }

        private Byte[] EncryptDataWithDES(Byte[] data)
        {
            return desCryptography.Encrypt(data);
        }

        private Byte[] DecryptDataWithDES(Byte[] data)
        {
            return desCryptography.Decrypt(data);
        }

        /// <summary>
        /// Closes the client.
        /// </summary>
        public void Close()
        {
            client.GetStream().Close();
            client.Close();
        }
    }
}
