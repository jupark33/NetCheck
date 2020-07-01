namespace NetCheck.net
{
  public class NetChecker
  {
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    public NetChecker() 
    {
    }
    
    public bool getCheckResult(NetCheckModel model)
    {
      bool result = false;
      var exceptions = new ConcurrentQueue<Exception>();
      try 
      {
        for (int i = 0; i < model.retryCnt; i++)
        {
          try 
          {
            logger.Debug("NetChecker.getCheckResult, TRY COUNT : " + (i + 1));
            var client = new HttpClient();
            var response = client.GetAsync(UriUtil.getMfpUriBuilder(model.ip, model.port).Uri);
            logger.Debug("NetChecker.getCheckResult, Result : " + response.Result);
            break;
          }
          catch (Exception e)
          {
            exceptions.Enqueue(e);
          }
        }
        throw new AggregateException(exceptions);
      }
      catch (AggregateException e) 
      {
        bool existException = false;
        foreach (var item in e.InnerExceptions)
        {
          existException = true;
          logger.Debug("NetChecker.getCheckResult, AggregateException : " + item.Message);
        }
        
        result = !existException;
      }
      
      return result;
    }
  }
}
