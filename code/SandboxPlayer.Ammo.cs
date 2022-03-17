using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace Sandbox
{
	partial class SandboxPlayer
	{
		[Net]
		public IList<int> Ammo { get; set; }

		public bool GiveAmmo( AmmoType type, int amount )
		{
			if ( !Host.IsServer ) return false;
			if ( Ammo == null ) return false;

			SetAmmo( type, AmmoCount( type ) + amount );
			return true;
		}

		public bool SetAmmo( AmmoType type, int amount )
		{
			var iType = (int)type;
			if ( !Host.IsServer ) return false;
			if ( Ammo == null ) return false;

			while ( Ammo.Count <= iType )
			{
				Ammo.Add( 0 );
			}

			Ammo[(int)type] = amount;
			return true;
		}

		public int AmmoCount( AmmoType type )
		{
			var iType = (int)type;
			if ( Ammo == null ) return 0;
			if ( Ammo.Count <= iType ) return 0;

			return Ammo[(int)type];
		}

		public enum AmmoType
		{
			Pistol,
			Buckshot,
			Crossbow
		}
	}
}
