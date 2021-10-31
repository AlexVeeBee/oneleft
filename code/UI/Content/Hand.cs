using System;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace OneLeft_UI.UI.Game.MainUI.Hand
{
	partial class Hand : Panel
	{
		public Client client { get; set; }
		public int HandID;
		//TURN
		public bool IsTurn = false;
		public TimeSince LastOpen = 0;
		//PANEL
		public Panel LocalCardsCon { get; set; }

		private string CardsPath = "UI/images/Cards/";

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
		private string[] MyHand = {
		};
		public Hand()
		{
			StyleSheet.Load( "UI/Content/Hand.scss" );
			SetTemplate( "UI/Content/Hand.html" );

			//var i = 0;
			//foreach ( var CurHand in Cards )
			//{
			//	Log.Info( CurHand );
			//	CreateCard( client, i );
			//	i++;
			//	//Panel Hand = LocalCardsCon.Add.Panel( "HandCard" );
			//	//Hand.Style.Set( $"background-image: url({CardsPath + CurHand}); background-size: cover;" );
			//}
			//hand;
			for(int i = 0; i < 0; i++ )
			{	
				Random rand = new Random();
				int card = rand.Next( Cards.Length );
				CreateCard( card );
			//	ConsoleSystem.Run( "CreateCard", client, card );
			}

			//Panel Hand = LocalCardsCon.Add.Label( "Draw" , "HandCard" );
			//Hand.Style.Set( "background-image: url(UI/images/Cards/HIDDEN.png); background-size: cover;" );

			//Hand.AddEventListener( "onclick",() =>
			//{
			//	Random rand = new Random();
			//	int num = rand.Next( 0, 47 );
			//	CreateCard( num );
			//} );
		}

		[Event( "ADD_CARD")]
		private void CreateCard( int Card_colr = 0 , int Card_id = 0 )
		{

			var file = CARDS_IDS[Card_colr][Card_id];

			HandID++;
			LocalCardsCon.SetClass( "turn", false );
			Panel Hand = LocalCardsCon.Add.Panel( $"HandCard hand-id_{HandID}" );
			//Hand.SetProperty( "id", CardID.ToString() );
			//Hand.SetContent( "card-id_" + CardID );
			Hand.SetClass( "hand-id_" + HandID, true );
			Hand.Style.Set( $"background-image: url({CardsPath + file}); background-size: cover;" );

			Hand.AddEventListener( "onclick", () =>
			{
				Log.Info( $"CARD_COLR: {Card_colr}  ");
				Log.Info( "TOSS" );
				OneLeft_game.OneLeftGAME.TossCard( Card_colr, Card_id );
				Hand.Delete();
				//IsTurn = false;
			} );
		}
		public override void Tick()
		{
			base.Tick();

			if ( Input.Pressed( InputButton.Forward ) && LastOpen >= .1f )
			{
				IsTurn = !IsTurn;
				LastOpen = 0;
			}
			LocalCardsCon.SetClass( "turn", IsTurn );
		}

		[Event( "game.startgame" ), ClientCmd( "START_GAME" )]
		public void StartGane()
		{
			Log.Info( "CARDS START" );
			foreach ( var deletecards in LocalCardsCon.Children )
			{
				deletecards.Delete();
			}
			for ( int i = 0; i < 7; i++ )
			{
				Random rand = new Random();
				int card = rand.Next( Cards.Length );
				CreateCard( card );
				//	ConsoleSystem.Run( "CreateCard", client, card );
			}
		}
	}
}
