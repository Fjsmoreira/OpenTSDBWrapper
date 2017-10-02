using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OpenTSDBApiWrapper
{
    public class GZipHttpMessageHandler : HttpClientHandler
    {
        public GZipHttpMessageHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var newContent = CompressContent(request.Content);
            newContent.Headers.ContentEncoding.Add("gzip");

            foreach (var header in request.Content.Headers)
            {
                newContent.Headers.Add(header.Key, header.Value);
            }

            return await base.SendAsync(request, cancellationToken);
        }

        private HttpContent CompressContent(HttpContent content)
        {
            var ms = new MemoryStream();

            using (var gzip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                content.CopyToAsync(gzip);
            }

            ms.Seek(0, SeekOrigin.Begin);

            return new StreamContent(ms);
        }
    }
}