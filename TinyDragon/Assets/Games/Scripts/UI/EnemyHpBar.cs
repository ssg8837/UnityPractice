using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyDragon.Core;


namespace TinyDragon.Core
{
    public class EnemyHpBar : MonoBehaviour
    {

        [SerializeField]
        private Transform targetTf;

        [SerializeField]
        private Vector3 offset;

        [SerializeField]
        private RectTransform rectCanvas;

        [SerializeField]
        private GameObject hpBarPrefab;

        [SerializeField]
        private Camera mainCamera;

        private GameObject hpBar;

        RectTransform rectHp;

        // Start is called before the first frame update
        void Start()
        {
            hpBar = Instantiate<GameObject>(hpBarPrefab, rectCanvas.transform); // 체력바 생성

            hpBar.GetComponent<HpBar>().Health = gameObject.GetComponent<Health>();

            rectHp = hpBar.GetComponent<RectTransform>();
        }

        private void LateUpdate()
        {
            var screenPos = Camera.main.WorldToScreenPoint(targetTf.position + offset); // 몬스터의 월드 3d좌표를 스크린좌표로 변환

            if (screenPos.z < 0.0f)
            {
                screenPos *= -1.0f;
            }

            var localPos = Vector2.zero;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectCanvas, screenPos, null, out localPos); // 스크린 좌표를 다시 체력바 UI 캔버스 좌표로 변환

            rectHp.localPosition = localPos; // 체력바 위치조정
        }
    }
}
