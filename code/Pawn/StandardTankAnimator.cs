
using Sandbox;
using Tanks;
using Sandbox.UI;

namespace Tanks
{
	public partial class StandardTankAnimator : PawnAnimator
	{
		//Having Net and Change in the brackets means its only called clientside
		
		public StandardTankAnimator()
		{
			
			SetAnimParameter( "BarrelRotation", 0f );
		}

		public override void Simulate()
		{
			var BarrelRotation = (Pawn as TanksPlayer).PlayerBarrelRotation;
			//if(Host.IsServer)
			//	Log.Info( "Value from server pawn " + (Pawn as TanksPlayer).PlayerBarrelRotation);
			//if ( Host.IsClient )
			//	Log.Info( "Value from client pawn " + (Pawn as TanksPlayer).PlayerBarrelRotation );
			SetAnimParameter( "BarrelRotation", 40f );
			base.Simulate();
		}


		
	}
}
