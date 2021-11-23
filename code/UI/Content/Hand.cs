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
		public int totalCards = 0;
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
				"R-2PLUS.png",
				"R-0.png",
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
				"Y-2PLUS.png",
				"Y-0.png",
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
				"G-2PLUS.png",
				"G-0.png",
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
				"B-2PLUS.png",
				"B-0.png",
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
		public Panel Debug_Container;
		public Label Debug_val_0;
		public Label Debug_val_1;
		public Label Debug_val_2;
		public Label Debug_val_3;
		public Label Debug_val_4;
		public Label Debug_val_5;
		private string[] MyHand = {
		};
		public Hand()
		{
			StyleSheet.Load( "UI/Content/Hand.scss" );
			SetTemplate( "UI/Content/Hand.html" );
			Debug_Container = Add.Panel();
			Debug_Container.Style.Set( "font-size: 24px; flex-direction: column; left: 12px; bottom: 300px; background-color: black; position: absolute;" );

			Debug_val_0 = Debug_Container.Add.Label( "E" , "");
			Debug_val_0.Style.Set( "padding: 5px; color: white; " );

			Debug_val_1 = Debug_Container.Add.Label( "E", "" );
			Debug_val_1.Style.Set( "padding: 5px; color: white; " );

			Debug_val_2 = Debug_Container.Add.Label( "E" , "");
			Debug_val_2.Style.Set( "padding: 5px; color: white; " );
			
			//Debug_val_3 = Debug_Container.Add.Label( "E", "" );
			//Debug_val_3.Style.Set( "padding: 5px; color: white; " );
			
			Debug_val_4 = Debug_Container.Add.Label( "E", "" );
			Debug_val_4.Style.Set( "padding: 5px; color: white; " );

			Debug_val_5 = Debug_Container.Add.Label( "false", "" );
			Debug_val_5.Style.Set( "padding: 5px; color: white; " );

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
			for ( int i = 0; i < 0; i++ )
			{
				Random rand = new Random();
				int num_col = rand.Next( 0, 4 );
				int num_card = rand.Next( 0, 12 );
				CreateCard( num_col, num_card );
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
			totalCards++;
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
				//ocal.Client.GetValue<int>( "UI.CardView_co", Local.Client.GetValue<int>( "UI.CardView_co" ) )
				//Local.Client.GetValue<int>( "UI.CardView_ca", Local.Client.GetValue<int>( "UI.CardView_ca" ) )
				if ( Local.Client.GetValue<int>( "UI.CardView_co", Local.Client.GetValue<int>( "UI.CardView_co" ) ) == Card_colr ||
					Local.Client.GetValue<int>( "UI.CardView_ca", Local.Client.GetValue<int>( "UI.CardView_ca" ) ) == Card_id
				)
				{
					totalCards--;
					Hand.Delete();
					Hand.SetClass( "NotClickable", true );
					OneLeft_game.OneLeftGAME.TossCard( Card_colr, Card_id );
				} else if ( Card_id == 0 || Card_id == 2  )
				{
					totalCards--;
					Hand.Delete();
					Hand.SetClass( "NotClickable", true );
					OneLeft_game.OneLeftGAME.TossCard( Card_colr, Card_id );
				} else
				{
					switch( Local.Client.GetValue<int>( "UI.CardView_ca", Local.Client.GetValue<int>( "UI.CardView_ca" ) ))
					{
						case 0:
							totalCards--;
							Hand.Delete();
							Hand.SetClass( "NotClickable", true );
							OneLeft_game.OneLeftGAME.TossCard( Card_colr, Card_id );
						break;
						case 2:
							totalCards--;
							Hand.Delete();
							Hand.SetClass( "NotClickable", true );
							OneLeft_game.OneLeftGAME.TossCard( Card_colr, Card_id );
						break;
					}
				}

				//switch()
				//{
				//	case 2:
				//
				//		break; case 
				//}
				//IsTurn = false;
			} );
		}
		public override void Tick()
		{
			base.Tick();
			//Cards_Debug.Text = totalCards.ToString();
			//Cards_Debug_val.Text = Local.Client.GetValue<int>( "plr.totalCards", Local.Client.GetValue<int>( "plr.totalCards", 0 ) ).ToString();
			var debug_val_1 = Local.Client.GetValue<int>( "UI.CardView_co", Local.Client.GetValue<int>( "UI.CardView_co" ) );
			var debug_val_2 = Local.Client.GetValue<int>( "UI.CardView_ca", Local.Client.GetValue<int>( "UI.CardView_ca" ) );
			var debug_val_3 = Local.Client.GetValue<int>( "UI.CardView_co_cw", Local.Client.GetValue<int>( "UI.CardView_co_b" ) );
			//var debug_val_4 = Local.Client.GetValue<int>( "UI.CardView_ca_b", Local.Client.GetValue<int>( "UI.CardView_ca_b" ) );
			var debug_val_5 = Local.Client.GetValue<bool>( "UI.COLORCARDS", Local.Client.GetValue<bool>( "UI.COLORCARDS" ) );

			Debug_val_0.Text = "Card View: Colors: " + Local.Client.GetValue<int>( "UI.CardView_co", Local.Client.GetValue<int>( "UI.CardView_co" ) ).ToString();
			Debug_val_1.Text = "Card View: ID: " + Local.Client.GetValue<int>( "UI.CardView_ca", Local.Client.GetValue<int>( "UI.CardView_ca" ) ).ToString();
			Debug_val_2.Text = "Color Wheel: Card View: Color: " + debug_val_3.ToString();
			//Debug_val_3.Text = "Background Card View: ID: " + debug_val_4.ToString();
			Debug_val_4.Text = "FILE: " + CARDS_IDS[debug_val_1][debug_val_2];
			if ( debug_val_5 )
			{
				Debug_val_5.Text = "COLOR SWITCH MODE: TRUE";
			} else								  
			{									  
				Debug_val_5.Text = "COLOR SWITCH MODE: FALSE";
			}



			Local.Client.SetValue( "plr.totalCards", totalCards );

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
				int num_col = rand.Next( 0, 4 );
				int num_card = rand.Next( 0, 10 );
				CreateCard( num_col, num_card );
				//	ConsoleSystem.Run( "CreateCard", client, card );
			}
		}
	}
}
