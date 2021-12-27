namespace CustomRPC
{
    public enum ConnectionType
    {
        Disconnected,
        Connected,
        Error,
        Unknown = 10
    }

    // A class that stores current and previous connection states
    public static class ConnectionManager
    {
        static ConnectionType current = ConnectionType.Disconnected;
        static ConnectionType previous = ConnectionType.Unknown;

        public static ConnectionType State
        {
            get
            {
                return current;
            }
            set
            {
                previous = current;
                current = value;
            }
        }

        public static bool HasChanged()
        {
            return current != previous;
        }
    }
}