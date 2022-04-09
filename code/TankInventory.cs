using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.AmmoTypes;

namespace Sandbox
{
	public class TankInventory : BaseInventory, IBaseInventory
	{
		// BombType and Amount
		public Dictionary<TankWeapon, int> PlayerAmmo = new Dictionary<TankWeapon, int>();

		public TankInventory( Entity owner ) : base( owner )
		{
			PlayerAmmo.Add( new SmallMissile(), 1 );
		}

		public override bool Add( Entity ent, bool makeActive = false )
		{
			var player = Owner as SandboxPlayer;
			//var weapon = ent as TankWeapon;

			return base.Add( ent, makeActive );
		}

		public void testytest()
		{

		}

	}
}
