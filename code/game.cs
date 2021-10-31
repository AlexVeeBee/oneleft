using Sandbox;
using System.Collections.Generic;

using OneLeft.UI.Chat;
using OneLeft_UI.UI.Game.MainUI.Table;

namespace OneLeft_game
{
	public partial class OneLeft : Sandbox.Game
	{
		[Net] public static bool GameRunning { get; set;  }

		public MyHud Myhud;
		public CurrentGame CurrentGame;

		public OneLeft()
		{
			if ( GameRunning == true)
			{
				if ( Myhud.IsValid() ) Myhud.Delete();
				if ( CurrentGame.IsValid() ) CurrentGame.Delete();

				if ( IsClient ) { CurrentGame = new CurrentGame(); }
			} else
			{
				if ( Myhud.IsValid() ) Myhud.Delete();
				if ( CurrentGame.IsValid() ) CurrentGame.Delete();

				if ( IsClient ) { Myhud = new MyHud(); }
			}
		}

		[Event.Hotload]
		public void HotloadUpdate()
		{
			Log.Info( "HOT LOAD" );
			Log.Info( "GAME RUNNING: " + GameRunning );
			if ( GameRunning == true)
			{	
				if ( !IsClient ) return;
				if ( Myhud.IsValid() ) Myhud.Delete();
				if ( CurrentGame.IsValid() ) CurrentGame.Delete();
				CurrentGame = new CurrentGame();
			}
			else
			{
				if ( !IsClient ) return;
				if ( Myhud.IsValid() ) Myhud.Delete();
				if ( CurrentGame.IsValid() ) CurrentGame.Delete();
				Myhud = new MyHud();
			}
		}

		[ClientRpc]
		public void EventHotLoad(bool start = false)
		{
			Log.Info( "SCRIPTED HOT LOAD" );
			Log.Info( "BOOL start: " + start );
			GameRunning = start;
			if ( start == true )
			{
				if ( !IsClient ) return;
				if ( Myhud.IsValid() ) Myhud.Delete();
				if ( CurrentGame.IsValid() ) CurrentGame.Delete();
				CurrentGame = new CurrentGame();
			} else
			{
				if ( !IsClient ) return;
				if ( Myhud.IsValid() ) Myhud.Delete();
				if ( CurrentGame.IsValid() ) CurrentGame.Delete();
				Myhud = new MyHud(true);
			}
		}

		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			// Create a pawn and assign it to the client.
			var player = new MyPlayer();
			client.Pawn = player;

			player.Respawn();
		}
	}	

	public partial class OneLeftGAME : Game
	{
		[Net] public static bool GameRunning { get; set; } = false;

		[ServerCmd]
		public static void StartGame()
		{
			Host.AssertServer();
			var ReadyPlayers = 0;
			foreach (var cl in Client.All)
			{
				if ( cl.GetValue( "ready", false ) == true)
				{
					Log.Info($"{cl.Name} Is Ready" );
					ReadyPlayers++;
				}
			}
			Log.Info( "PLAYERS: "+Client.All.Count );
			Log.Info( "READY PLAYERS: "+ ReadyPlayers );
			if ( Client.All.Count >= 0 )
			{
				if ( Client.All.Count <= ReadyPlayers )
				{
					Log.Info( $"Game is ready to start" );
					GameRunning=true;
					((OneLeft)Current).EventHotLoad( true);

					foreach ( var cl in Client.All )
					{
						Log.Info( $"VALUE UPDATE TO {cl.Name}" );
						cl.SetValue( "UI.CardView_co", 0 );
						cl.SetValue( "UI.CardView_ca", 0 );
						cl.SetValue( "UI.UPDATE", true );
						Log.Info( $"VALUE UPDATE TO {cl.Name} SUCCESS" );
						cl.SendCommandToClient( "UPDATE_TABLE_CARD" );
						Log.Info( $"=================" );
					}
					Event.Run( "UPDATE_TABLE_CARD" );
				}
				else
				{
					Log.Info( $"Not enough ready players" );

					//Event.Run( "DEGUB_POPUP.PLR1.Debug_start" );
				}
			}
			else
			{
				Log.Info( $"Not enough players" );
			}

			Log.Info( $"{(Host.IsServer ? "Server:" : "Client:")} {GameRunning}" );
		}
		
		[ServerCmd]
		public static void DEBUG_force_stop()
		{
			((OneLeft)Current).EventHotLoad( false);
		}

		[ServerCmd]
		public static void Ready()
		{
			var client = ConsoleSystem.Caller;
			if ( client == null ) return;

			client.SetValue( "ready", !client.GetValue<bool>( "ready", false ) );

			// TODO: move this to a tick or some shit
			Log.Info( client.Name + " IS " + client.GetValue<bool>( "ready", false ) );
			foreach ( var cl in Client.All )
			{
				if ( !cl.GetValue<bool>( "ready", false ) )
					return;
			}
		}

		[ServerCmd]
		public static void TossCard( int cardColorId, int cardNumer_spacialId)
		{
			Log.Info( "CARD COLOR" + cardColorId );
			Log.Info( "CARD NUMVER/SPECIAL" + cardNumer_spacialId );
			foreach(var cl in Client.All)
			{
				Log.Info( $"VALUE UPDATE TO {cl.Name}");
				cl.SetValue( "UI.CardView_co", cardColorId );
				cl.SetValue( "UI.CardView_ca", cardNumer_spacialId );

				Log.Info( "UI.CardView_co: " + cl.GetValue( "UI.CardView_co", cardColorId ) );
				Log.Info( "UI.CardView_ca: " + cl.GetValue( "UI.CardView_ca", cardNumer_spacialId ) );

				cl.SetValue( "UI.UPDATE", true );

				Log.Info( $"VALUE UPDATE TO {cl.Name} SUCCESS");
				cl.SendCommandToClient( "UPDATE_TABLE_CARD" );
				Log.Info( $"=================");
			}
			Event.Run( "UPDATE_TABLE_CARD" );
		}
	}
}
