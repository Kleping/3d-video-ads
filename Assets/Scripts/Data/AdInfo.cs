using Core;

namespace Data
{
    public class AdInfo
    {
        public readonly AdSource Source;
        public readonly string Id, URL;

        public AdInfo(string id, string url, AdSource source)
        {
            Id = id;
            URL = url;
            Source = source;
        }
    }
}