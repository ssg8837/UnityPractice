using UnityEngine;

namespace RPG.Combat
{
    ///<summary>
    /// 전투 처리 관련, 전투 대상 클래스
    ///</summary>
    public class CombatTarget : MonoBehaviour
    {
        public bool isEnemy()
        {
            return true;
        }
    }
}
