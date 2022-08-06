using Sandbox;
using System.Linq;

namespace Tanks;

public class Camera : Sandbox.CameraMode
{
	public float Distance { get; set; } = 1024;
	public float DistanceScrollRate { get; set; } = 32f;
	public float MinDistance { get; set; } = 128f;
	public float MaxDistance { get; set; } = 2048f;

	private float LerpSpeed { get; set; } = 5f;
	private bool CenterOnPawn { get; set; } = true;
	private Vector3 Center { get; set; }
	private float CameraUpOffset { get; set; } = 32f;

	private TimeSince TimeSinceMousePan { get; set; }
	private static int SecondsBeforeReturnFromPan => 3;

	public Entity Target { get; set; }

	public override void Update()
	{
		if (Target == null )
		{
			Target = FindTargetEntity();
		}
		

		Distance -= Input.MouseWheel * DistanceScrollRate;
		Distance = Distance.Clamp( MinDistance, MaxDistance );

		// Get the center position, plus move the camera up a little bit.
		var cameraCenter = (CenterOnPawn) ? Target.Position : Center;
		cameraCenter += Vector3.Up * CameraUpOffset;

		var targetPosition = cameraCenter + Vector3.Right * Distance;
		Position = Position.LerpTo( targetPosition, Time.Delta * LerpSpeed );

		var lookDir = (cameraCenter - targetPosition).Normal;
		Rotation = Rotation.LookAt( lookDir, Vector3.Up );

		// Handle camera panning
		if ( Input.Down( InputButton.SecondaryAttack ) )
			MoveCamera();

		// Check the last time we panned the camera, update CenterOnPawn if greater than N.
		if ( !Input.Down( InputButton.SecondaryAttack ) && TimeSinceMousePan > SecondsBeforeReturnFromPan )
			CenterOnPawn = true;
	}

	private void MoveCamera()
	{
		var delta = new Vector3( -Mouse.Delta.x, 0, Mouse.Delta.y ) * 2;
		TimeSinceMousePan = 0;

		if ( CenterOnPawn )
		{
			Center = Target.Position;

			// Check if we've moved the camera, don't center on the pawn if we have
			if ( !delta.LengthSquared.AlmostEqual( 0, 0.1f ) )
				CenterOnPawn = false;
		}

		Center += delta;
	}

	private AnimatedEntity FindTargetEntity()
	{
		var localPawn = Local.Pawn;

		if ( localPawn is Player character )
		{
			return character;
		}
		else
		{
			var target = Client.All.Select( x => x.Pawn as TanksPlayer )
				.FirstOrDefault();

			if ( target.IsValid() )
			{
				return target;
			}
		}

		return null;
	}
}
