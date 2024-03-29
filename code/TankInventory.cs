﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.AmmoTypes;
using Sandbox;

namespace Tanks
{
	public class TankInventory : BaseInventory, IBaseInventory
	{
		// BombType and Amount
		public Dictionary<TankAmmo, int> PlayerAmmo = new Dictionary<TankAmmo, int>();

		public TankInventory( Entity owner ) : base( owner )
		{
			//PlayerAmmo.Add( new SmallMissile(), 1 );
		}

		public override bool Add( Entity ent, bool makeActive = false )
		{
			var player = Owner as TanksPlayer;
			//var weapon = ent as TankWeapon;

			return base.Add( ent, makeActive );
		}


	}
}
