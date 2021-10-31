using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Linq;

using OneLeft.UI.Menu.ReadyPlayers;

namespace OneLeft.UI.Menu
{
	public partial class MainMenu : Panel
	{
		private Client cl;


		private Panel logo_container { get; set; }
		private Panel menu_container { get; set; }
		public Panel players_container { get; set; }
		public Panel controls_container { get; set; }

		public Label StartGane { get; set; }
		public Label readyButton { get; set; }

		public MainMenu()
		{
			StyleSheet.Load( "UI/MainMenu/Menu.scss" );
			SetTemplate( "UI/MainMenu/Menu.html" );

			logo_container.Add.Image( "UI/Images/logo.png" , "image_logo");
			logo_container.Add.Image( "UI/Images/LamaProductions_Logo.png", "image_org_logo");
				StartGane = controls_container.Add.Label( "Start Game", "button" );
				StartGane.AddEventListener( "onclick", () =>
				{
					StartGame();
				} );
			readyButton = controls_container.Add.Label( "readyButton CS", "button" );
			readyButton.AddEventListener( "onclick", () =>
			{
				ReadyGame();
			} );
		}

		public Panel MenuContainer_main { get; set; }

		public void ReadyGame()
		{ OneLeft_game.OneLeftGAME.Ready(); }
		public void StartGame()
		{ OneLeft_game.OneLeftGAME.StartGame(); }

		[Event( "UI_START" )]
		public void GAMEStartGame()
		{
			Log.Info( "GAME START FROM EVENTS" );
			MenuContainer_main.SetClass( "HideToLeft", true );
		}

		public override void Tick()
		{
			base.Tick();

			foreach(var cl in Client.All)
			{
				if ( cl == Local.Client )
				{
					if ( cl.GetValue( "ready", false ) == true )
					{
						readyButton.Text = "Unready";
					}
					else
					{
						readyButton.Text = "Ready!";
					}
					if ( cl.UserId != 1 )
					{
						StartGane.Delete();
					}
				}
			}
		}
	}
}


//var pnl = PlayersContainers.Children.First();
//pnl.SetClass( "ready", !pnl.HasClass( "ready" ) );

