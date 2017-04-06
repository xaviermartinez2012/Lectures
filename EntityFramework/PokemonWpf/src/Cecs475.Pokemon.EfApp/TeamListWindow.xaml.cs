using System;
using System.Data.Entity;
using System.Windows;
using System.Windows.Input;
using Cecs475.Pokemon.Model;

namespace Cecs475.Pokemon.EfApp {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class TeamListWindow : Window {
		/// <summary>
		/// Retain a Context for the Pokemon database as a member of this window.
		/// </summary>
		private PokemonEntities mContext = new PokemonEntities();

		public TeamListWindow() {
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			// The Context constructor does NOT load data from the database; we have to do that once the window loads.
			mContext.PokemonTeams.Load();
			mContext.Pokemons.Load();
			mTeamList.ItemsSource = mContext.PokemonTeams.Local; 
			// .Local is a WPF requirement for data binding. WPF doesn't want to go to the database every time you rebind a control;
			// .Local is basically a cache of the database locally.
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			// Because we can't use a "using with resources" statement to auto-dispose our Context, we have to manually
			// take care of that when the window closes.
			mContext.Dispose();
		}

		private void mNewTeamNameBtn_Click(object sender, RoutedEventArgs e) {
			// Create a new PokemonTeam with the specified name.
			PokemonTeam t = new PokemonTeam() {
				Title = mNewTeamText.Text
			};
			// Add the team to the context.
			mContext.PokemonTeams.Add(t);
			// Save the staged changes.
			mContext.SaveChanges();
		}

		private void mTeamList_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			// Retrieve the selected item from the team list, then load an editor window.
			var item = mTeamList.SelectedItem as PokemonTeam;
			// We pass the team and the list of call Pokemon to the editor.
			var editor = new TeamEditor(item, mContext.Pokemons.Local);
			editor.ShowDialog();

			// After the editor closes, we save any changes to the context.
			mContext.SaveChanges();
		}
	}
}
