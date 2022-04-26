using UnityEngine;

namespace TinyDragon.Core
{
    public interface IMover
    {
        void Move(float inputX, float inputZ , Animator animator, Rigidbody rigidbody);

        void playerRotation(Vector3 velocity , Rigidbody rigidbody);

        bool Jump(Animator animator, Rigidbody rigidbody);
    }
}