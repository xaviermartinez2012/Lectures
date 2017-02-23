using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_Basics {
	public partial class Form1 : Form {
		public Form1() {
			// Never remove this method call.
			InitializeComponent();
		}

		private void mFightBtn_Click(object sender, EventArgs e) {
			Pokemon togekiss = new Pokemon() {
				HP = Convert.ToInt32(mHPText1.Text),
				Attack = Convert.ToInt32(mAttackText1.Text),
				Defense = Convert.ToInt32(mDefenseText1.Text),
				Level = Convert.ToInt32(mLevelText1.Text)
			};

			Pokemon charmander = new Pokemon() {
				HP = Convert.ToInt32(mHPText2.Text),
				Attack = Convert.ToInt32(mAttackText2.Text),
				Defense = Convert.ToInt32(mDefenseText2.Text),
				Level = Convert.ToInt32(mLevelText2.Text)
			};

			togekiss.AttackTarget(charmander, Convert.ToInt32(mPowerText1.Text));
			charmander.AttackTarget(togekiss, Convert.ToInt32(mPowerText2.Text));

			mHPText1.Text = togekiss.HP.ToString();
			mHPText2.Text = charmander.HP.ToString();
		}

		private void mPicture2_MouseDoubleClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				System.IO.Stream file = System.Reflection.Assembly.GetExecutingAssembly()
					.GetManifestResourceStream("Basics.Resources.charizard.png");
				mPicture2.Image = Image.FromStream(file);
				mHPText2.Text = "100";
				mAttackText2.Text += "0";
				mDefenseText2.Text += "0";
			}
		}
	}
}
