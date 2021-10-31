using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
namespace OneLeft.UI.Game.MainUI
{
	/// <summary>
	/// The main content of the game
	/// </summary>
	public partial class UiGameContent : Panel 
	{
		public UiGameContent()
		{
			StyleSheet.Load( "UI/Content/Game.scss" );
			SetTemplate( "UI/Content/Game.html" );
		}
	}
}
