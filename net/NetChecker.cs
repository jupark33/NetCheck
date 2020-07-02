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
    
    public bool getCheckSocketResult(NetCheckModel model) 
    {
      bool result = false;
      
      var exceptions = new ConcurrentQueue<Exceptions>();
      try
      {
        for (int i = 0; i < model.retryCnt; i++)
        {
            Socket sock = null;
            try
            {
              logger.Debug("NetChecker.getCheckSocketResult, TRY COUNT (" + model.port + ") : " + (i + 1));
              sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
              var ep = new IPEndPoint(IPAddress.Parse(model.ip), model.port);
              sock.Connect(ep);
              
              result = sock.Connected;
              logger.Debug("NetChecker.getCheckSocketResult, getCheckSocket : " + result);
              if (result) break;
              
              sock.Shutdown(SocketShutdown.Both);
            }
            catch (Exception e) 
            {
              exceptions.Enqueue(e);
            }
            finally
            {
              if (sock != null)
              {
                sock.Close();
                logger.Debug("NetChecker.getCheckSocketResult, sock.Close() called ");                
              }
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
            logger.Debug("NetChecker.getCheckSocketResult, AggregateException : " + item.Message);
        }
        return = !existException;
      }
      
      return result;
    }
  }
}
