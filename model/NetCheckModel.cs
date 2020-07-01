namespace NetCheck.model
{
  public class NetCheckModel
  {
    public string ip { get; set; }
    public int port { get; set; }
    public int retry { get; set; }
    
    public NetCheckModel(string ip, int port, int retry) 
    {
      this.ip = ip;
      this.port = port;
      this.retry = retyr;
    }
    
    public override string ToString() 
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("ip : " + ip);
      sb.Append(", port : " + port);
      sb.Append(", retry : " + retry);
      
      return sb.ToString();
    }
  }
}
