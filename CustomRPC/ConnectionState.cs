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
    public class ConnectionState
    {
        ConnectionType current;
        ConnectionType previous;

        public ConnectionType State
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

        public ConnectionState()
        {
            previous = ConnectionType.Unknown;
            current = ConnectionType.Disconnected;
        }

        public bool HasChanged()
        {
            return current != previous;
        }
    }
}