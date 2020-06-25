using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SrtTranslator.DeepL
{
    public class ApiHttpRequestSender : IHttpRequestSender
    {
        private readonly HttpClient client;

        public ApiHttpRequestSender(HttpClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("User-Agent", "SrtSubtitle");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            this.client = client;
        }

        public HttpResponseMessage SendRequest(HttpRequestMessage request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return client.SendAsync(request).Result;
        }
    }
}
