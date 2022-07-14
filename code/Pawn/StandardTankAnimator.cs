
using Sandbox;
using Tanks;
using Sandbox.UI;

namespace Tanks
{
	public partial class StandardTankAnimator : PawnAnimator
	{
		//Having Net and Change in the brackets means its only called clientside
		[Net, Change] public float BarrelRotation { get; set; } = 30f;
		public StandardTankAnimator()
		{
			
			SetAnimParameter( "BarrelRotation", 0f );
			GameBar.BarrelRotated += OnBarrelRotationChanged;
		}

		public override void Simulate()
		{
			if ( Host.IsServer ) {
			Log.Info( "Value of BarrelRotation  " + BarrelRotation );
			SetAnimParameter( "BarrelRotation", BarrelRotation );
		}
			base.Simulate();
		}

		[ConCmd.Server]
		public void OnBarrelRotationChanged( float obj )
		{
				BarrelRotation = obj;
		
		}
		
	}
}
