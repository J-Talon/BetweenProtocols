using Entity;

namespace Item
{
    public abstract class GameItemDynamic: GameItemBase
    {
        public abstract void use(EntityLiving entity);

        public abstract bool canUse();
    }
}