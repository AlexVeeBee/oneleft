using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Linq;

using OneLeft.UI.Menu.ReadyPlayers;

namespace OneLeft.UI.Menu.Background
{
	public partial class Background : Panel
	{
		readonly ScenePanel scene;

		public Panel Background_image;
		public Background()
		{

			StyleSheet.Load( "UI/background.scss" );
			Background_image = Add.Panel("b");

			Style.Set( "background-image: url(\"UI/Images/background_darktheme.png\");" );
		}

		[Event("ChangeBackground")]
		public void ChangeBackground()
		{
			//Style.Set( "background-image: none;" );
		}
	}
}
