
using Sandbox;
using Sandbox.Hooks;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.UI
{
	public partial class Custom_Scoreboard<T> : Panel where T : Custom_ScoreboardEntry, new()
	{
		public Panel Canvas_contianer { get; protected set; }
		public Panel Canvas { get; protected set; }
		Dictionary<Client, T> Rows = new ();

		public Panel Header { get; protected set; }

		public Custom_Scoreboard()
		{
			StyleSheet.Load( "/ui/Custom_scoreboard/Scoreboard.scss" );
			AddClass( "scoreboard" );

			AddHeader();

			Canvas_contianer = Add.Panel( "Canvas_contianer" );
			Canvas = Canvas_contianer.Add.Panel( "canvas" );
		}

		public override void Tick()
		{
			base.Tick();

			SetClass( "open", Input.Down( InputButton.Score ) );

			if ( !IsVisible )
				return;

			//
			// Clients that were added
			//
			foreach ( var client in Client.All.Except( Rows.Keys ) )
			{
				var entry = AddClient( client );
				Rows[client] = entry;
			}

			foreach ( var client in Rows.Keys.Except( Client.All ) )
			{
				if ( Rows.TryGetValue( client, out var row ))
				{
					row?.Delete();
					Rows.Remove( client );
				}
			}
		}


		protected virtual void AddHeader() 
		{
			Header = Add.Panel( "header" );
			Header.Add.Label( "Name", "name" );
			Header.Add.Label( "Wins", "wins" );
			Header.Add.Label( "Losses", "losses" );
			Header.Add.Label( "Ping", "ping" );
		}

		protected virtual T AddClient( Client entry )
		{
			var p = Canvas.AddChild<T>();
			p.Client = entry;
			return p;
		}
	}
}
