using System;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneLeft_UI.UI.Game.MainUI.Table
{
	public partial class GameTable : Panel
	{
		private string[][] CARDS_IDS = new string[][] 
		{
			new string[] { // RED
				"R-COLOR.png",
				"R-REVERSE.png",
				"R-COLOR4.png",
				"R-1.png",
				"R-2.png",
				"R-3.png",
				"R-4.png",
				"R-5.png",
				"R-6.png",
				"R-7.png",
				"R-8.png",
				"R-9.png",
			},
			new string[] {// YELLOW
				"Y-COLOR.png",
				"Y-REVERSE.png",
				"Y-COLOR4.png",
				"Y-1.png",
				"Y-2.png",
				"Y-3.png",
				"Y-4.png",
				"Y-5.png",
				"Y-6.png",
				"Y-7.png",
				"Y-8.png",
				"Y-9.png",
			},
			new string[] { // GREEN
				"G-COLOR.png",
				"G-REVERSE.png",
				"G-COLOR4.png",
				"G-1.png",
				"G-2.png",
				"G-3.png",
				"G-4.png",
				"G-5.png",
				"G-6.png",
				"G-7.png",
				"G-8.png",
				"G-9.png",
			},
			new string[] {
				"B-COLOR.png",
				"B-REVERSE.png",
				"B-COLOR4.png",
				"B-1.png",
				"B-2.png",
				"B-3.png",
				"B-4.png",
				"B-5.png",
				"B-6.png",
				"B-7.png",
				"B-8.png",
			},
		};

		private string[] Cards = { 
			//"BACK.png", 
				// RED
			"R-COLOR.png",
			"R-REVERSE.png",
			"R-COLOR4.png",
			"R-1.png",
			"R-2.png",
			"R-3.png",
			"R-4.png",
			"R-5.png",
			"R-6.png",
			"R-7.png",
			"R-8.png",
			"R-9.png",
				// YELLOW
			"Y-COLOR.png",
			"Y-REVERSE.png",
			"Y-COLOR4.png",
			"Y-1.png",
			"Y-2.png",
			"Y-3.png",
			"Y-4.png",
			"Y-5.png",
			"Y-6.png",
			"Y-7.png",
			"Y-8.png",
			"Y-9.png",
				// GREEN
			"G-COLOR.png",
			"G-REVERSE.png",
			"G-COLOR4.png",
			"G-1.png",
			"G-2.png",
			"G-3.png",
			"G-4.png",
			"G-5.png",
			"G-6.png",
			"G-7.png",
			"G-8.png",
			"G-9.png",
				// BLUE
			"B-COLOR.png",
			"B-REVERSE.png",
			"B-COLOR4.png",
			"B-1.png",
			"B-2.png",
			"B-3.png",
			"B-4.png",
			"B-5.png",
			"B-6.png",
			"B-7.png",
			"B-8.png",
		};
		public Client cl;


		public Panel LeftCard;
		public Panel RightCard;
		public GameTable()
		{
			StyleSheet.Load( "UI/Content/Table.scss" );

			LeftCard = Add.Panel( "cardL" );
			RightCard = Add.Panel( "cardR" );
			LeftCard.Style.Set( "background-image: url(UI/Images/Cards/BACK.png)" );
			RightCard.Style.Set( "background-image: url(UI/Images/Cards/BACK.png)" );

			//for ( int i = 0; i < 1; i++ )
			//{
			//	Random rand = new Random();
			//	int card = rand.Next( Cards.Length );
			//	RightCard.Style.Set( $"background-image: url(UI/Cards/{Cards[card]})" );
			//}


			LeftCard.AddEventListener( "onclick", ( obj ) => {
				Log.Info( "CLICK" );

				Random rand = new Random();
				int num_col = rand.Next( 0, 4 );
				int num_card = rand.Next( 0, 10 );

				Event.Run( "ADD_CARD", num_col , num_card );

				//OneLeft_game.OneLeftGAME.TossCard( num, num );
			} );

			var totalCards = 0;
			foreach(var c in Cards )
			{
				totalCards++;
				Log.Info( totalCards );
			}
		}

		public void StartGane()
		{
			LeftCard.Style.Set( "background-image: url(UI/Cards/BACK.png)" );
			RightCard.Style.Set( "background-image: url(UI/Cards/BACK.png)" );

			for ( int i = 0; i < 1; i++ )
			{
				Random rand = new Random();
				int card = rand.Next( Cards.Length );
				RightCard.Style.Set( $"background-image: url(UI/Cards/{Cards[card]})" );
			}
		}

		//[Event("UPDATE_TABLE_CARD")]
		//[ClientCmd( "UPDATE_TABLE_CARD" ), Event( "UPDATE_TABLE_CARD" )]
		//public void UpdateCard()
		public override void Tick()
		{
			//base.Tick();
			//Log.Info( "RUN" );
			//Log.Info( Local.Client.GetValue( "UI.UPDATE", false ) );
			if ( Local.Client.GetValue( "UI.UPDATE", Local.Client.GetValue( "UI.UPDATE", false ) ) == true )
			{
				RightCard.Style.Set( $"background-image: url(UI/images/Cards/{CARDS_IDS[Local.Client.GetValue<int>( "UI.CardView_co", Local.Client.GetValue<int>( "UI.CardView_co" ) )][Local.Client.GetValue<int>( "UI.CardView_ca" , Local.Client.GetValue<int>( "UI.CardView_ca" ) )]})" );
				Local.Client.SetValue( "UI.UPDATE", false );
				//Log.Info( "RUN" );
			}
		}
	}
}
