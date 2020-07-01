namespace NetCheck.util
{
  class UriUtil
  {
    public static UriBuilder getMfpUriBuilder(string ip, int port)
    {
      UriBuilder builder = new UriBuilder("http", ip);
      builder.Port = port;
      return builder;
    }
  }
}
