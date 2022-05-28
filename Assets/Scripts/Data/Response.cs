using UnityEngine.Networking;

namespace Data
{
    public class Response
    {
        public readonly string Error, Text;
        public readonly byte[] Data;

        public Response(string error, DownloadHandler handler)
        {
            Error = error;
            if (handler == null) return;
            Data = handler.data;
            Text = handler.text;
        }
    }
}