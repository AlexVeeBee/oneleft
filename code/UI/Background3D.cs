using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace OneLeft.UI.Menu.Background
{
	public class Background3D : Panel
	{
		readonly ScenePanel scene;

		Angles CamAngles = new( 90.0f, 0.0f, 180.0f );
		float CamDistance = 50;
		Vector3 CamPos => Vector3.Up * 400 + CamAngles.Direction * -CamDistance;

		Color TopColor			= Color.Random;
		Color BottomColor		= Color.Random;
		Color RightColor		= Color.Random;
		Color LeftColor			= Color.Random;

		public Background3D()
		{
			StyleSheet.Load( "UI/Background3D.scss" );
			Style.FlexWrap = Wrap.Wrap;
			Style.JustifyContent = Justify.Center;
			Style.AlignItems = Align.Center;
			Style.AlignContent = Align.Center;

			using ( SceneWorld.SetCurrent( new SceneWorld() ) )
			{
				SceneObject.CreateModel( "models/background_gravel.vmdl", Transform.Zero );

				Light.Point( Vector3.Up * 10.0f + Vector3.Forward * 150.0f, 200,	TopColor	* 15.0f );
				Light.Point( Vector3.Up * 10.0f + Vector3.Backward * 150.0f, 200,	BottomColor * 15f );
				Light.Point( Vector3.Up * 10.0f + Vector3.Right * 200.0f, 200,		RightColor	* 15.0f );
				Light.Point( Vector3.Up * 10.0f + Vector3.Left * 200.0f, 200,		LeftColor	* 15.0f );
				
				Light.Point( Vector3.Up * 10.0f + Vector3.Left * 0.0f, 200,	Color.Red * 15.0f );

				scene = Add.ScenePanel( SceneWorld.Current, CamPos, Rotation.From( CamAngles ), 45 );
			}
		}

		public override void OnMouseWheel( float value )
		{
			CamDistance += value;
			CamDistance = CamDistance.Clamp( 10, 200 );

			base.OnMouseWheel( value );
		}

		public override void OnButtonEvent( ButtonEvent e )
		{
			if ( e.Button == "mouseleft" )
			{
				SetMouseCapture( e.Pressed );
			}

			base.OnButtonEvent( e );
		}

		public override void Tick()
		{
			base.Tick();
			TopColor = Color.Random;
			BottomColor = Color.Random;
			RightColor = Color.Random;
			LeftColor = Color.Random;


			scene.CameraPosition = CamPos;
			scene.CameraRotation = Rotation.From( CamAngles );
		}
	}
}
