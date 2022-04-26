using UnityEngine;

namespace TinyDragon.Core
{
    public interface IDodger
    {
        bool Dodge(float inputX, float inputZ, Animator animator , Rigidbody rigidbody);

        float getInterval();
    }
}