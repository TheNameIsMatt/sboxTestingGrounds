using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
	public class TankInventory : BaseInventory
	{


		public TankInventory( Entity owner ) : base( owner )
		{
			
		}

		public override bool Add( Entity ent, bool makeActive = false )
		{
			var player = Owner as SandboxPlayer;
			//var weapon = ent as TankWeapon;

			return base.Add( ent, makeActive );
		}


	}
}
