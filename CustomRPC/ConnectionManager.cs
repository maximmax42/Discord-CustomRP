namespace CustomRPC
{
    /// <summary>
    /// Connection types for the app.
    /// </summary>
    public enum ConnectionType
    {
        None = -1,
        Disconnected,
        Connecting,
        Connected,
        Error,
    }

    /// <summary>
    /// A class that stores current and previous connection states.
    /// </summary>
    public static class ConnectionManager
    {
        static ConnectionType current = ConnectionType.Disconnected;
        static ConnectionType previous = ConnectionType.None;

        /// <summary>
        /// Current state of the connection.
        /// </summary>
        public static ConnectionType State
        {
            get
            {
                return current;
            }
            set
            {
                if (value == ConnectionType.None)
                    throw new System.ComponentModel.InvalidEnumArgumentException("Attempt to set State to ConnectionType.None.");

                if (current != ConnectionType.Connecting)
                    previous = current;
                current = value;
            }
        }

        /// <summary>
        /// Were the last two states different?
        /// </summary>
        /// <returns><see langword="True"/> if yes, <see langword="false"/> if no.</returns>
        public static bool HasChanged()
        {
            return current != previous;
        }
    }
}