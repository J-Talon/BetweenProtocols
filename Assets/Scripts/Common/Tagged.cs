namespace Common
{
    public interface Tagged
    {
        public string createId()
        {
            return System.Guid.NewGuid().ToString();
        }

        
        
    }
}