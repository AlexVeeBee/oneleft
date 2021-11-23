using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

using OneLeft;
//using OneLeft.UI.Chat;
using OneLeft.UI.Menu;
using OneLeft.UI.Menu.Background;

public partial class MyHud : HudEntity<RootPanel>
{
	public MyHud(bool showfinalScore = false)
	{
		if ( !IsClient ) return;

		RootPanel.AddChild<Custom_Scoreboard<Custom_ScoreboardEntry>>();
		RootPanel.AddChild<MainMenu>();
		RootPanel.AddChild<Background>();
		RootPanel.AddChild<OneLeft.UI.Chat.ChatBox>();
		if ( showfinalScore )
		{
			Log.Info( "FINAL SCORE SHOW" );
		}
	}
}
