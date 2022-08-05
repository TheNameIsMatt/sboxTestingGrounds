using System;
using Sandbox;

namespace Tanks
{
	public partial class TankController : BasePlayerController
	{
		[Net] public float TankSpeed { get; set; } = 100f;
		[Net] public float Acceleration { get; set; } = 10f;
		[Net] public bool b_isAiming { get; set; } = false;


		public TankController()
		{

		}
		public override void Simulate()
		{


			DebugOverlay.ScreenText( $"        Tank Speed: {TankSpeed}", 1 );
			DebugOverlay.ScreenText( $"        Is Aiming?: {b_isAiming}", 2 );
			Move();
			base.Simulate();
		}
		public virtual void Move()
		{
			if ( !b_isAiming && Input.Down( InputButton.Right ) )
			{
				Vector3 move = Pawn.Position;
				move.x += (TankSpeed * Acceleration * Time.Delta);
				move.x = MathX.Lerp( Position.x, move.x, 0.1f );
				Position = move;
			}

			if ( !b_isAiming && Input.Down( InputButton.Left ) )
			{
				Vector3 move = Pawn.Position;
				move.x -= (TankSpeed * Acceleration * Time.Delta);
				move.x = MathX.Lerp( Position.x, move.x, 0.1f );
				Position = move;
			}
		}

	}
}
