namespace NetCheck.net
{
  public sealed class NetSingle
  {
    private static Logger logger = LogManager.GetCurrentClassLogger();
    private static volatile NetSingle instance;
    private static object syncRoot = new object();
    
    private BlockingCollection<NetCheckModel> queue;
    private CancellationTokenSource endToken;
    private NetProducer netProducer;
    private NetConsumer netConsumer;
    private Task[] tasksNetCheck;
    
    private NetSingle()
    {
      startNetTask();
    }
    
    private void startNetTask() 
    {
      queue = new BlockingCollection<NetCheckModel>();
      endToken = new CancellationTokenSource();
      
      netProducer = new NetProducer(queue, endToken);
      netConsumer = new NetConsumer(queue, endToken);
      
      tasksNetCheck = new[]
      {
        new Task(netConsumer.ThreadRun)
      };
      Array.ForEach(tasksNetCheck, t => t.Start());
      
      logger.Debug("MfpNetSingle.startMfpNetTask()");
    }
    
    public static MfpNetSingle Instance
    {
      get
      {
        if (instance == null)
        {
          lock (syncRoot) 
          {
            if (instance == null)
                instance = new NetSingle();
          }
        }
        return instance;
      }
    }
    
    public void stopNet() 
    {
      endToken.Cancel();
      Task.WaitAll(tasksNetCheck);
    }
    
    public void addNetProduce(NetCheckModel model) 
    {
      netProducer.add(model);
    }
  }
}
