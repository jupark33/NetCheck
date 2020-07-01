namespace NetCheck.net 
{
  class NetProducer
  {
    private static Logger logger = LogManager.GetCurrentClassLogger();
    private BlockingCollection<NetCheckModel> queue;
    private CancellationTokenSource endTokenSource;
    
    public NetProducer(BlockingCollection<NetCheckModel> queue, CancellationTokenSource endTokenSource) 
    {
      this.queue = queue;
      this.endTokenSource = endTokenSoure;
    }
    
    public void add(NetCheckModel model) 
    {
      queue.Add(model);
      logger.Debug("NetProducer.add 생산 : " + model);
    }
  }
  
  class NetConsumer 
  {
    private static Logger logger = LogManager.GetCurrentClassLogger();
    private BlockingCollection<NetCheckModel> queue;
    private CancellationTokenSource endTokenSource;
    private NetChecker netChecker;
    
    public NetConsumer(BlockingCollection<NetCheckModel> queue, CancellationTokenSource endTokenSource)
    {
      this.queue = queue;
      this.endTokenSource = endTokenSource;
      netChecker = new NetChecker();
    }
    
    public void ThreadRun() 
    {
      while (!endTokenSource.IsCancellationRequested)
      {
        if (queue.Count > 0)
        {
          // 소비
          logger.Debug("NetConsumer.ThreadRun(), 소비 : " + model);
          execute(model);
        }
        Thread.Sleep(1);
      }
      logger.Debug("NetConsumer.ThreadRun(), 소비자 중지");
    }
    
    private void execute(NetCheckModel model)
    {
      bool result = false;
      result = netChecker.getCheckResult(model);
      logger.Debug("NetConsumer.execute(), result : " + result);
    }
  }
}
