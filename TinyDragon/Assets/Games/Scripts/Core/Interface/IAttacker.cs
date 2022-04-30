using UnityEngine;

namespace TinyDragon.Core
{
    public interface IAttacker
    {
        bool Attack(bool mJumping, Animator animator);

        float getInterval();
    }
}