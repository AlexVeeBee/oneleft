using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace OneLeft.UI.Chat
{
	public partial class ChatEntry : Panel
	{
		public Label NameLabel { get; internal set; }
		public Panel Message_container { get; internal set; }
		public Label Message { get; internal set; }
		public Panel Avatar_container { get; internal set; }
		public Image Avatar { get; internal set; }

		public RealTimeSince TimeSinceBorn = 0;

		public ChatEntry()
		{
			Avatar_container = Add.Panel( "image-container" );
				Avatar = Avatar_container.Add.Image("", "image");

			Message_container = Add.Panel( "message_container" );
				Message_container.Add.Panel( "outline-top" );
				NameLabel = Message_container.Add.Label( "Name", "name" );
				Message = Message_container.Add.Label( "Message", "message" );
				Message_container.Add.Panel( "outline-bottom" );
		}

		public override void Tick()
		{
			base.Tick();

			if ( TimeSinceBorn > 10 )
			{
				AddClass( "old" );
				//if ( TimeSinceBorn > 5 ) {
				//	Delete();
				//}
			}
		}
	}
}
