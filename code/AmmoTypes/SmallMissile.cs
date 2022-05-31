using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.AmmoTypes
{

	[Library( "weapons", Title = "SmallMissile" )]
	public class SmallMissile : TankWeapon
	{
		public static Model worldModel => Model.Load( "/models/sboxrocket.vmdl" );
		public override string BombName => "Small Missile";
		public override int ProjectileCount => 1;

		public override float ExplosionRadius => 1f;

		public override bool Bounce => false;

		public SmallMissile ()
		{
			
		}
	}
}
