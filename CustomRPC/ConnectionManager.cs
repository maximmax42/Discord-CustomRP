namespace CustomRPC
{
    /// <summary>
    /// Connection states for the app.
    /// </summary>
    public enum ConnectionState
    {
        None = -1,
        Disconnected,
        Connecting,
        UpdatingPresence,
        Connected,
        Error,
    }

    /// <summary>
    /// A class that stores current and previous connection states.
    /// </summary>
    public static class ConnectionManager
    {
        static ConnectionState current = ConnectionState.Disconnected;
        static ConnectionState previous = ConnectionState.None;

        /// <summary>
        /// Current state of the connection.
        /// </summary>
        public static ConnectionState State
        {
            get
            {
                return current;
            }
            set
            {
                if (value == ConnectionState.None)
                    throw new System.ComponentModel.InvalidEnumArgumentException("Attempt to set State to ConnectionState.None.");

                if (current != ConnectionState.Connecting)
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