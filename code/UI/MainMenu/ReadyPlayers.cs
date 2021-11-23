using System;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OneLeft;

namespace OneLeft.UI.Menu.ReadyPlayers
{
	public partial class ReadyScreenPlayer : Panel
	{
		public Client Client { get; set; }
		public Label NameLabel;
		public Image Avatar;
		public Label FR;
		private Panel MainPanel;

		public ReadyScreenPlayer( Client cl )
		{
			Client = cl;

			MainPanel = Add.Panel( $"player plr-{cl.Name}" );
			Avatar = MainPanel.Add.Image( $"avatarbig:{cl.PlayerId }", "plrImage" );
			NameLabel = MainPanel.Add.Label( $"{cl.Name}", "plrname" );
		}

		public override void Tick()
		{
			base.Tick();

			if ( !Client.IsValid() )
				return;

			MainPanel.SetClass( "ready", Client.GetValue( "ready", false ) );
		}
	}
	public partial class WaitingScreenPlayer : Panel
	{
		public Label W_NameLabel;
		private Panel W_MainPanel;
		private Panel MainPanel;
	
		public WaitingScreenPlayer()
		{
			MainPanel = Add.Panel( $"player plr-WAITING" );
			W_MainPanel = MainPanel.Add.Label( $"Waiting for more players", "plrname" );
		}
	}
	public partial class readyscreen : Panel
	{
		private Panel W_MainPanel;

		Panel PlayersContainers;
		Label PlayersText { get; set; }
		//Label ReadyCount { get; set; }

		public readyscreen()
		{
			StyleSheet.Load( "/UI/MainMenu/ReadyPlayers.scss" );
			//SetTemplate( "/UI/MainMenu/ReadyPlayers.html" );

			PlayersContainers = Add.Panel("Player_container_style");
		}
		// Copied code
		// Credit: Layla / matt :3
		public override void Tick()
		{
		//	base.Tick();
		//	// Remove any invalid clients (disconnected)
			var PlayersCount = 0;
			foreach ( var playerPanel in PlayersContainers.Children.OfType<ReadyScreenPlayer>() )
			{
				if ( playerPanel.Client.IsValid() )
					continue;
		
				playerPanel.Delete();
				PlayersCount--;
			}
		
			//Add any new clients that aren't already in the list
			foreach ( var client in Client.All )
			{
				if ( PlayersContainers.Children.OfType<ReadyScreenPlayer>().Any( panel => panel.Client == client ) )
					continue;
		
				var panel = new ReadyScreenPlayer( client );
				panel.Parent = PlayersContainers;
				PlayersCount++;
			}


			//foreach ( var client in Client.All )
			//{
			//	PlayersContainers = Add.Panel( $"player plr-WAITING" );
			//	W_MainPanel = PlayersContainers.Add.Label( $"Waiting for more players", "plrname" );
			//}

			//	//
			//	var readyPlayers = 0;
			//	foreach ( var client in Client.All )
			//	{
			//		if ( !client.GetValue<bool>( "ready", false ) )
			//			continue;
			//		readyPlayers++;
			//		ReadyButton.Text = "Unready";
			//	}
			//	if ( Client.All.Count <= 0 )
			//	{
			//		PlayersText.Text = $"Waiting for players {Client.All.Count}/2";
			//		ReadyButton.SetClass( "not-showing", true );
			//	}
			//	else if ( Client.All.Count >= 5 )
			//	{
			//		ReadyButton.SetClass( "not-showing", true );
			//		PlayersText.Text = $"Max Players";
			//	}
			//	else
			//	{
			//		ReadyButton.SetClass( "not-showing", false );
			//		if ( readyPlayers >= Client.All.Count )
			//		{
			//			PlayersText.Text = $"Waiting for Host | Ready {readyPlayers}/{Client.All.Count}";
			//		}
			//		else
			//		{
			//			PlayersText.Text = $"Waiting for players {readyPlayers}/{Client.All.Count}";
			//		}
			//	}
		}
	}
}
