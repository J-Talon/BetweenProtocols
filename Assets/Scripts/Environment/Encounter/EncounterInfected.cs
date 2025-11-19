using System.Collections.Concurrent;
using Entity;

namespace Environment.Encounter
{
    public class EncounterInfected
    {
        private ConcurrentDictionary< string, GameEntity> entities;

        public void clean()
        {
            foreach (GameEntity entity in entities.Values)
            {
                entity.die();
            }
            entities.Clear();
        }


        public void startEncounter()
        {
            
        }

        public void whileActive()
        {
            
            
        }


    }
}