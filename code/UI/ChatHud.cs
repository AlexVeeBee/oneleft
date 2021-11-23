using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public partial class ChatUI : HudEntity<RootPanel>
{
	public ChatUI()
	{
		if ( !IsClient ) return;
	}
}
