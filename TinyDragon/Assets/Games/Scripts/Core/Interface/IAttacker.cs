using UnityEngine;

namespace TinyDragon.Core
{
    public interface IAttacker
    {
        bool Attack(bool mJumping, Animator animator);

        bool PlayerMeleeAttack(int aComboAttack, Animator animator);

        float getInterval();
    }
}