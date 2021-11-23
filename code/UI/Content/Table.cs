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

		public Client cl;

		public bool Open = false;
		public TimeSince LastOpen = 0;


		// CARDS
		public Panel LeftCard;
		public Panel RightCard;

		public Panel TurnDirection;

		// COLOR RING
		public Panel _4ColorPicker;
		public Panel _4ColorWheel_b;

		public Panel _4ColorPicker_b_cl;
		public Panel _4ColorPicker_b_cr;

		public Panel _4ColorPicker_b_cl_r;
		public Panel _4ColorPicker_b_cl_b;

		public Panel _4ColorPicker_b_cr_y;
		public Panel _4ColorPicker_b_cr_g;

		// DEBUG
		public Panel Debug_Container;
		public Label Debug_val_0;
		public Label Debug_val_1;
		public Label Debug_val_2;
		public Label Debug_val_3;
		public Label Debug_val_4;
		public Label Debug_val_5;

		public GameTable()
		{

			StyleSheet.Load( "UI/Content/Table.scss" );

			LeftCard = Add.Panel( "cardL" );
			RightCard = Add.Panel( "cardR" );
			LeftCard.Style.Set( "background-image: url(UI/Images/Cards/BACK.png)" );
			RightCard.Style.Set( "background-image: url(UI/Images/Cards/BACK.png)" );

			TurnDirection = Add.Panel( "TurnDirec" );

			Debug_Container = Add.Panel();
			Debug_Container.Style.Set( "font-size: 24px; flex-direction: column; left: -300px; top: -150px; background-color: black; position: absolute;" );

			//Debug_val_0 = Debug_Container.Add.Label( "EMPTY", "" );
			//Debug_val_0.Style.Set( "padding: 5px; color: white; " );

			// COLOR RING
			_4ColorPicker = Add.Panel( "PickerBackground" );
			_4ColorWheel_b = _4ColorPicker.Add.Panel( "PickerBackground-inner shown" );
			// {
				// LEFT
				_4ColorPicker_b_cl = _4ColorWheel_b.Add.Panel("col");
				// {
					_4ColorPicker_b_cl_r = _4ColorPicker_b_cl.Add.Panel("cr_l_r");
					_4ColorPicker_b_cl_b = _4ColorPicker_b_cl.Add.Panel( "cr_l_B" );
				// }
			
				// RIGHT
				_4ColorPicker_b_cr = _4ColorWheel_b.Add.Panel( "col" );
				// {
					_4ColorPicker_b_cr_y = _4ColorPicker_b_cr.Add.Panel( "cr_r_y");
					_4ColorPicker_b_cr_g = _4ColorPicker_b_cr.Add.Panel( "cr_r_g" );
				// }
			
			// }

			// CARD PICLUP

			LeftCard.AddEventListener( "onclick", ( obj ) => {
				Log.Info( "CLICK" );

				Random rand = new Random();
				int num_col = rand.Next( 0, 4 );
				int num_card = rand.Next( 0, 12 );

				Event.Run( "ADD_CARD", num_col , num_card);
			} );

			_4ColorPicker_b_cl_r.AddEventListener("onclick", () => OneLeft_game.OneLeftGAME.ColorSwitch(0));
			_4ColorPicker_b_cl_b.AddEventListener("onclick", () => OneLeft_game.OneLeftGAME.ColorSwitch(3));
			_4ColorPicker_b_cr_y.AddEventListener("onclick", () => OneLeft_game.OneLeftGAME.ColorSwitch(1));
			_4ColorPicker_b_cr_g.AddEventListener("onclick", () => OneLeft_game.OneLeftGAME.ColorSwitch(2));

			//var totalCards = 0;
			//foreach(var c in CARDS_IDS )
			//{
			//	Log.Info( c );
			//	foreach(var ca in c)
			//	{
			//		Log.Info( "|_" + ca );
			//		totalCards++;
			//	}
			//}
		}

		public void StartGane()
		{
			LeftCard.Style.Set( "background-image: url(UI/Cards/BACK.png)" );
			RightCard.Style.Set( "background-image: url(UI/Cards/BACK.png)" );

			for ( int i = 0; i < 1; i++ )
			{
				Random rand = new Random();
				int num_col = rand.Next( 0, 4 );
				int num_card = rand.Next( 0, 10 );
				RightCard.Style.Set( $"background-image: url(UI/Cards/{CARDS_IDS[num_col][num_card]})" );
			}
		}

		//[Event("UPDATE_TABLE_CARD")]
		//[ClientCmd( "UPDATE_TABLE_CARD" ), Event( "UPDATE_TABLE_CARD" )]
		//public void UpdateCard()
		public override void Tick()
		{
			Event.Run( "ChangeBackground" );
			//base.Tick();
			//Log.Info( "RUN" );
			//Log.Info( Local.Client.GetValue( "UI.UPDATE", false ) );
			if ( Local.Client.GetValue( "UI.UPDATE", Local.Client.GetValue( "UI.UPDATE", false ) ) == true )
			{
				RightCard.Style.Set( $"background-image: url(UI/images/Cards/{CARDS_IDS[Local.Client.GetValue<int>( "UI.CardView_co", Local.Client.GetValue<int>( "UI.CardView_co" ) )][Local.Client.GetValue<int>( "UI.CardView_ca" , Local.Client.GetValue<int>( "UI.CardView_ca" ) )]})" );
				Local.Client.SetValue( "UI.UPDATE", false );
			}

			if ( Input.Pressed( InputButton.Menu ) && LastOpen >= .1f )
			{
				Open = !Open;
				LastOpen = 0;
			}
			_4ColorWheel_b.SetClass( "shown", Open );

		}
	}
}
