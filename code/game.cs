using System;
using Sandbox;
using System.Collections.Generic;

//using OneLeft.UI.Chat;
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

		private static string[] COLOR_CARDS_IDS = new string[]
		{
			"R",
			"Y",
			"G",
			"B",
		};

		[Net] public static int clID_turn{ get; set; } = 1;

		[Net] public static bool GameRunning { get; set; } = false;

		[Net] public static int gamecard_co { get; set; } = 0;
		[Net] public static int gamecard_ca { get; set; } = 0;

		[Net] public static bool ColorSWitchMode { get; set; } = false;
		[Net] public static bool RevesCard { get; set; } = false;

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

						Random rand = new Random();
						int num_col = rand.Next( 0, 4 );
						int num_card = rand.Next( 0, 13 );

						cl.SetValue( "UI.CardView_co", num_col );
						cl.SetValue( "UI.CardView_ca", num_card );

						gamecard_co = num_col;
						gamecard_ca = num_card;

						cl.SetValue( "UI.UPDATE", true );
						Log.Info( $"VALUE UPDATE TO {cl.Name} SUCCESS" );
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

//		-----------// PLACE CARD // -----------
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
				
				if ( cardNumer_spacialId == 0 || cardNumer_spacialId == 2 )
				{
					cl.SetValue( "UI.CardView_co_b", cardColorId );
					//cl.SetValue( "UI.CardView_ca_b", cardNumer_spacialId );

					// COLOR SWITCH
						
					cl.SetValue( "UI.CardView_co_cw", -1 );

					cl.SetValue( "UI.COLORCARDS", true);
					ColorSWitchMode = true;
				} else
				{
					cl.SetValue( "UI.COLORCARDS", false );
					ColorSWitchMode = false;
				}
				if ( cardNumer_spacialId == 1)
				{
					if ( RevesCard )
					{
						RevesCard = false;
						Log.Info( "Turn direction: NORMAL" );
					}
					else
					{
						RevesCard = true;
						Log.Info( "Turn direction: REVERSEED" );
					}
					Log.Info( "REVERSE: " + RevesCard );
					Log.Info( "UI.CardView_co: " + cl.GetValue( "UI.CardView_co", cardColorId ) );
				}

				Log.Info( "UI.CardView_co: " + cl.GetValue( "UI.CardView_co", cardColorId ) );
				Log.Info( "UI.CardView_ca: " + cl.GetValue( "UI.CardView_ca", cardNumer_spacialId ) );
				
				gamecard_co = cardColorId;
				gamecard_ca = cardNumer_spacialId;

				cl.SetValue( "UI.UPDATE", true );

				Log.Info( $"VALUE UPDATE TO {cl.Name} SUCCESS");
				Log.Info( $"=================");
			}
			Event.Run( "UPDATE_TABLE_CARD" );
		}

//		-----------// COLOR SWITCH // -----------
		[ServerCmd]
		public static void ColorSwitch( int cardColorId )
		{
			var cl_C = ConsoleSystem.Caller;
			Log.Info( "CALLER: "+ cl_C.UserId );
			Log.Info( "TURN: " + clID_turn );
			if ( cl_C.UserId == clID_turn ) {
				Log.Info( "CARD COLOR" + cardColorId );
				foreach ( var cl in Client.All )
				{
					if ( ColorSWitchMode == true ) {
						// COLOR SWITCH NOW
						cl.SetValue( "UI.CardView_co_cw", cardColorId );

						cl.SetValue( "UI.COLORCARDS", true );
						ColorSWitchMode = true;
						Log.Info( COLOR_CARDS_IDS[cardColorId] );
					}
					else
					{
						cl.SetValue( "UI.CardView_co_cw", -1 );
						cl.SetValue( "UI.COLORCARDS", false );
						ColorSWitchMode = false;
					}

					Log.Info( "UI.CardView_co_cw: " + cl.GetValue( "UI.CardView_co_cw", cardColorId ) );

					gamecard_co = cardColorId;

					cl.SetValue( "UI.UPDATE", true );

					Log.Info( $"VALUE UPDATE TO {cl.Name} SUCCESS" );
					Log.Info( $"=================" );
				}
				Event.Run( "UPDATE_TABLE_CARD" );
			} else
			{
				Log.Info( "Nothing was affected" );
				Log.Warning( $"its not {cl_C.Name} turn" );
			}
		}
	}
}
