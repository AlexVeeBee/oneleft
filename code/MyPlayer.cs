using Sandbox;
public class FloatingCamera : Camera
{
	public override void Update()
	{
		var Pawn = Local.Pawn;

		Pos = new Vector3( 0,128,60 );

		FieldOfView = 80;

		Viewer = Pawn;
	}
}
partial class MyPlayer : Player
{
	public override void Respawn()
	{
		SetModel( "models/citizen/citizen.vmdl" );

		// Use WalkController for movement (you can make your own PlayerController for 100% control)
		//Controller = new WalkController();

		// Use StandardPlayerAnimator  (you can make your own PlayerAnimator for 100% control)
		//Animator = new StandardPlayerAnimator();

		// Use ThirdPersonCamera (you can make your own Camera for 100% control)
		//Camera = new ThirdPersonCamera();

		Camera = new FloatingCamera();

		EnableAllCollisions = true;
		EnableDrawing = true;
		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;

		base.Respawn();
	}
}
