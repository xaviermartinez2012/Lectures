using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms_Basics {
	public class Pokemon {
		public int Level { get; set; }
		public int Attack { get; set; }
		public int Defense { get; set; }
		public int HP { get; set; }

		static Random mRandom = new Random();

		public void AttackTarget(Pokemon other, int attackPower) {
			double modifier = mRandom.NextDouble() * 0.15 + 0.85;
			double damage = ((2.0 * Level + 10) / 250 * (Attack / other.Defense) * attackPower + 2) * modifier;
			other.HP = (int)Math.Max(other.HP - damage, 0);

		}
	}
}
