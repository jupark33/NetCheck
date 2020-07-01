namespace NetCheck
{
  public partial class MainForm : Form
  {
    private void actionConnectTh() 
    {
      string ip = "localhost";
      int port = "80";
      int retry = 3;
      
      NetSingle ns = NetSingle.Instance;
      ns.addNetProduce(new NetCheckModel(ip, port, retryCnt));
    }
    
    private void btnConn_Click(object sender, EventArgs e) 
    {
      actionConnectTh();
    }
  }
}
