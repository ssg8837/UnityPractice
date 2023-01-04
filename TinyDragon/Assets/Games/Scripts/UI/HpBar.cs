using UnityEngine;
using UnityEngine.UI;


namespace TinyDragon.Core
{
	public class HpBar : MonoBehaviour
	{
		[SerializeField]
		private Health health;

		[SerializeField]
		private Image hpBar;

		[SerializeField]
		private Text hpText;

        private void Update()

		{
			PlayerHPbar();
		}

		public void PlayerHPbar()

		{

			float hp = health.CurrentHealth;

			float maxHp = health.MaxHealth;

			hpBar.fillAmount = hp / maxHp;

			hpText.text = string.Format("HP {0}/{1}", hp, maxHp);

		}
	}
}
