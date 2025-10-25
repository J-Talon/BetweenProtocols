namespace Story
{
    public interface StoryProgressor
    {

        public abstract bool isSpent();

        public abstract void onTrigger();
        
        public abstract void onEnd();
        
    }
}