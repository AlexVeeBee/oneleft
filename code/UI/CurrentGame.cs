using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

// OneLeft
using OneLeft;
using OneLeft.UI.Chat;
using OneLeft.UI.Menu;
using OneLeft.UI.Menu.Background;
using OneLeft.UI.Game.MainUI;

public partial class CurrentGame : HudEntity<RootPanel>
{
	public CurrentGame()
	{
		if ( !IsClient ) return;

		RootPanel.AddChild<Custom_Scoreboard<Custom_ScoreboardEntry>>();
		RootPanel.AddChild<Background>();
		RootPanel.AddChild<C_ChatBox>();
		RootPanel.AddChild<UiGameContent>();
	}
}
