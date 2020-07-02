namespace NetCheck.util
{
  class StringUtil
  {
    public static bool isEmpty(string str) 
    {
      if (str == null || str.Trim().Length == 0)
        return true;
      return false;
    }
  }
}
