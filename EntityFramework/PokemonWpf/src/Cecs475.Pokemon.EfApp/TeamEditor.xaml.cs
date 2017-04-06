using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cecs475.Pokemon.Model;

namespace Cecs475.Pokemon.EfApp {
	/// <summary>
	/// Interaction logic for TeamEditor.xaml
	/// </summary>
	public partial class TeamEditor : Window {
		public TeamEditor(PokemonTeam team, IEnumerable<Model.Pokemon> allPokemon) {
			InitializeComponent();
			this.Resources.Add("TheTeam", team);
			this.Resources.Add("AllPokemon", allPokemon);
		}

		private void mAddBtn_Click(object sender, RoutedEventArgs e) {
			var team = this.Resources["TheTeam"] as PokemonTeam;
			var pkmn = mAllPokemonList.SelectedItem as Model.Pokemon;
			team.Pokemons.Add(pkmn);
		}
	}
}
