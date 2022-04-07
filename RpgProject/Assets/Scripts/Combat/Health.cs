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

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            if(!isDead)
            {
                currentHealth = Mathf.Max(currentHealth - damage, 0);
                if(currentHealth == 0)
                {
                    GetComponent<Animator>().SetTrigger("triggerDie");
                    isDead = true;
                }
            }
            Debug.Log(currentHealth);
        }
    }
}
