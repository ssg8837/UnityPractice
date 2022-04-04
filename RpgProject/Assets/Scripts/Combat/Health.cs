using UnityEngine;

namespace RPG.Combat
{
    ///<summary>
    /// 체력관리 클래스
    ///</summary>
    public class Health : MonoBehaviour
    {
        ///<summary>
        ///체력
        ///</summary>
        [SerializeField] float currentHealth =100f;

        public void TakeDamage(float damage)
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0);

            Debug.Log(currentHealth);
        }
    }
}
