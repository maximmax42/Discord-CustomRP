namespace CustomRPC
{
    public class AppCenterSecret
    {
        public static string Value = System.Environment.GetEnvironmentVariable("APPCENTER_SECRET") ?? "{app secret}";
    }
}
