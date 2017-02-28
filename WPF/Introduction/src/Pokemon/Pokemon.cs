using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Model {
	public class Pokemon : INotifyPropertyChanged {
		private int mHP, mLevel, mAttack, mDefense, mPower;
		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string name) {
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}
		static Random mRandom = new Random();

		public void AttackTarget(Pokemon other, int attackPower) {
			double modifier = mRandom.NextDouble() * 0.15 + 0.85;
			double damage = ((2.0 * Level + 10) / 250 * (Attack / other.Defense) * attackPower + 2) * modifier;
			other.HP = (int)Math.Max(other.HP - damage, 0);
		}
		
		public int HP {
			get { return mHP; }
			set {
				mHP = value;
				OnPropertyChanged("HP");
			}
		}

		public int Level {
			get { return mLevel; }
			set {
				mLevel = value;
				OnPropertyChanged("Level");
			}
		}

		public int Attack {
			get { return mAttack; }
			set {
				mAttack = value;
				OnPropertyChanged("Attack");
			}
		}

		public int Defense {
			get { return mDefense; }
			set {
				mDefense = value;
				OnPropertyChanged("Defense");
			}
		}

		public int Power {
			get { return mPower; }
			set {
				mPower = value;
				OnPropertyChanged("Power");
			}
		}
	}
}
