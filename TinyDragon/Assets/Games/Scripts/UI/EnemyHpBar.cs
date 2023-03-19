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
            hpBar = Instantiate<GameObject>(hpBarPrefab, rectCanvas.transform); // ü�¹� ����

            hpBar.GetComponent<HpBar>().Health = gameObject.GetComponent<Health>();

            rectHp = hpBar.GetComponent<RectTransform>();
        }

        private void LateUpdate()
        {
            var screenPos = Camera.main.WorldToScreenPoint(targetTf.position + offset); // ������ ���� 3d��ǥ�� ��ũ����ǥ�� ��ȯ

            if (screenPos.z < 0.0f)
            {
                screenPos *= -1.0f;
            }

            var localPos = Vector2.zero;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectCanvas, screenPos, null, out localPos); // ��ũ�� ��ǥ�� �ٽ� ü�¹� UI ĵ���� ��ǥ�� ��ȯ

            rectHp.localPosition = localPos; // ü�¹� ��ġ����
        }
    }
}
