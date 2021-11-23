using System;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OneLeft.UI.Game.MainUI.Top_Players
{


	public partial class Player_Renderer : Panel
	{
		public Client Client { get; set; }
		private Panel MainPanel;
		private Panel NamePanel;
		public Image Avatar;
		public Label NameLabel;
		public Label CardsLabel;

		public Player_Renderer( Client cl )
		{
			Client = cl;

			MainPanel = Add.Panel( $"plr plr-{cl.Name}" );
			Avatar = MainPanel.Add.Image( $"avatarbig:{cl.PlayerId }", "plr_avatar" );
			NamePanel = MainPanel.Add.Panel( "NameConatiner" );
				NameLabel = NamePanel.Add.Label( $"{cl.Name}", "plr_name" );
				NamePanel.Add.Panel( "outline-top " );
				NamePanel.Add.Panel( "outline-bottom" );
				CardsLabel = NamePanel.Add.Label( "LOL", "plr_name" );
		}

		public override void Tick()
		{
			//CardsLabel.Text = Client.GetValue<int>( "plr.totalCards", Client.GetValue<int>( "plr.totalCards" , 0 ) ).ToString();
		}
	}
	public partial class InGamePlayers : Panel
	{
		private Panel W_MainPanel;

		Panel PlayersContainers;
		Label PlayersText { get; set; }
		//Label ReadyCount { get; set; }

		public InGamePlayers()
		{
			StyleSheet.Load( "/UI/Content/CurrentleyPlaying.scss" );
			//SetTemplate( "/UI/MainMenu/ReadyPlayers.html" );

			PlayersContainers = Add.Panel( "Player_container_style" );
		}
		// Copied code
		// Credit: Layla / matt :3
		public override void Tick()
		{
				base.Tick();
			//	// Remove any invalid clients (disconnected)
			foreach ( var playerPanel in PlayersContainers.Children.OfType<Player_Renderer>() )
			{
				if ( playerPanel.Client.IsValid() )
					continue;

				playerPanel.Delete();
			}

			//Add any new clients that aren't already in the list
			foreach ( var client in Client.All )
			{
				if ( PlayersContainers.Children.OfType<Player_Renderer>().Any( panel => panel.Client == client ) )
					continue;

				var panel = new Player_Renderer( client );
				panel.Parent = PlayersContainers;
			}
		}
	}


	// BROKEN



	//public partial class T_Player_Renderer : Panel
	//{
	//	public Client Client { get; set; }
	//	public Panel Plr;
	//	public Image Plr_Aavatar;
	//	public Label Plr_Name;
	//
	//	public T_Player_Renderer( Client cl)
	//	{
	//		//Plr = Add.Panel( "plr" );
	//		//Plr_Aavatar = Plr.Add.Image( $"avatarbig:{cl.SteamId}", "plr_avatar" );
	//		//Plr_Name = Plr.Add.Label( $"{cl.Name}", "plr_name" );
	//
	//		Add.Label( "TESTING RENDER" );
	//	}
	//}
	//public partial class BrokenPlayers : Panel
	//{
	//	public Client client { get; set; }
	//	public Panel Players;
	//
	//	public BrokenPlayers()
	//	{
	//		StyleSheet.Load( "UI/Content/CurrentleyPlaying.scss" );
	//		//SetTemplate( "UI/Content/CurrentleyPlaying.html" );
	//
	//		Players = Add.Panel( "players" );
	//	}
	//
	//	public override void Tick()
	//	{
	//		base.Tick();
	//
	//		foreach ( var playerPanel_g in Players.Children.OfType<T_Player_Renderer>() )
	//		{
	//			if ( playerPanel_g.Client.IsValid() )
	//				continue;
	//			playerPanel_g.Delete();
	//		}
	//
	//		//Add any new clients that aren't already in the list
	//		foreach ( var client in Client.All )
	//		{
	//			if ( Players.Children.OfType<T_Player_Renderer>().Any( panel => panel.Client == client ) )
	//				continue;
	//
	//			var panel = new T_Player_Renderer( client );
	//			Players.AddChild( panel );
	//		}
	//	}
	//}
}
