using Sandbox;
using Tanks.Utils;

namespace Tanks
{
	// https://github.com/apetavern/sbox-grubs/blob/master/code/Pawn/Camera.cs
	public class Camera : Sandbox.CameraMode
	{
		public Range DistanceRange { get; } = new Range( 128f, 2048f );
		public float Distance { get; set; } = 1024f;
		private float DistanceScrollRate => 32f;

		private TimeSince TimeSinceMousePan { get; set; }
		private int SecondsBeforeReturnFromPan => 3;

		private bool CenterOnPawn { get; set; } = true;

		public Vector3 Center { get; set; }

		protected Entity LookTarget { get; private set; }

		public override void Activated()
		{
			Position = Vector3.Right * Distance;
			Rotation = Rotation.FromYaw( 90 );
		}

		public void SetLookTarget(Entity target )
		{
			LookTarget = target;
		}

		public override void Update()
		{
			var pawn = LookTarget;

			if ( pawn == null )
				return;

			// Distance Scrolling
			Distance += -Input.MouseWheel * DistanceScrollRate;
			Distance = DistanceRange.Clamp( Distance );

			// If we havent moved the camera, center it on the pawn;

			Vector3 cameraCenter = (CenterOnPawn) ? pawn.Position : Center;


			// Lerp to our target position
			Vector3 targetPosition = cameraCenter + Vector3.Right * Distance;
			Position = Position.LerpTo( targetPosition, 5 * Time.Delta );

			// Rotate towards the target position
			Vector3 lookDir = (cameraCenter + Vector3.Right) * Distance;
			Rotation = Rotation.LookAt( lookDir, Vector3.Up );

			if ( Input.Down( InputButton.SecondaryAttack ) )
				MoveCamera( pawn );

			if ( !Input.Down( InputButton.SecondaryAttack ) && TimeSinceMousePan > SecondsBeforeReturnFromPan )
				CenterOnPawn = true;


			//
			// Camera Properties
			FieldOfView = 65;
			ZNear = 8;
			ZFar = 25000;
			Viewer = null;
		}

		private void MoveCamera (Entity pawn )
		{
			var Delta = new Vector3( -Mouse.Delta.x, 0, Mouse.Delta.y );
			TimeSinceMousePan = 0;

			if ( CenterOnPawn )
			{
				Center = pawn.Position;

				if ( !Delta.LengthSquared.AlmostEqual( 0, 0.1f ) )
					CenterOnPawn = false;
			}

			Center += Delta;
		}
	}
}
