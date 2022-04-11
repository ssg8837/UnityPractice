using UnityEngine;

namespace RPG.Combat
{
    ///<summary>
    /// 전투 처리 관련, 전투 대상 클래스
    ///</summary>
    public class CombatTarget : MonoBehaviour
    {
        public bool CanAttack()
        {
            Health targetHealth = gameObject.GetComponent<Health>();
            bool result = false;
            if(targetHealth != null)
            {
                result = !gameObject.GetComponent<Health>().IsDead();
            }
            return result;
        }

    }
}
