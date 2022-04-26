using UnityEngine;

namespace TinyDragon.Core
{
    public interface IAttacker
    {
        bool Attack(bool mJumping, Animator animator, Rigidbody rigidbody);

        bool PlayerMeleeAttack(int aComboAttack, Animator animator);

        float getInterval();
    }
}