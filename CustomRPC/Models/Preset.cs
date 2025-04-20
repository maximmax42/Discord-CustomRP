using System;

namespace CustomRPC
{
    /// <summary>
    /// A struct for handling preset importing/exporting.
    /// </summary>
    [Serializable]
    public sealed class Preset
    {
        public string ID;
        public int Type;
        public string Details;
        public string State;
        public int PartySize;
        public int PartyMax;
        public int Timestamps;
        public DateTime CustomTimestamp;
        public string LargeKey;
        public string LargeText;
        public string SmallKey;
        public string SmallText;
        public string Button1Text;
        public string Button1URL;
        public string Button2Text;
        public string Button2URL;
        public string FriendlyName;

        public override string ToString()
        {
            return FriendlyName;
        }
    }
}
