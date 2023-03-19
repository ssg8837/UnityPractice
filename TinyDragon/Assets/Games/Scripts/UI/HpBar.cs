using UnityEngine;
using UnityEngine.UI;


namespace TinyDragon.Core
{
	public class HpBar : MonoBehaviour
	{
		[SerializeField]
		private Health health;

		public Health Health
        {
			set { health = value; }
        }

		[SerializeField]
		private Image hpBar;

		[SerializeField]
		private Text hpText;

		private void Start()
		{
			if (health == null)
            {
				health = gameObject.GetComponentInParent<Health>();
            }
        }

        private void Update()

		{
			float hp = health.CurrentHealth;

			float maxHp = health.MaxHealth;

			hpBar.fillAmount = hp / maxHp;

			hpText.text = string.Format("HP {0}/{1}", hp, maxHp);
		}

	}
}
