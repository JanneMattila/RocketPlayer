namespace Rocket.Player
{
    public static class LoggingEvents
    {
        public const int Connecting = 5000;
        public const int Connected = 5001;
        public const int WaitingMetadata = 5002;
        public const int WaitingMetadataCancelled = 5003;

        public const int ConstantsReceived = 5010;
        public const int ShotReceived = 5011;
        public const int PlayerReceived = 5012;
        public const int PlayerMetadataReceived = 5013;
    }
}
