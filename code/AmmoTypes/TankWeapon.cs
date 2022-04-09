using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sandbox
{
	[Library("weapon"), AutoGenerate]
	public partial class TankWeapon : Asset
	{

		public virtual string BombName { get; set; }

		[ResourceType("vmdl")]
		public virtual Model BombModel { get; set; }

		// Replaced with a Key Value Pair in the Inventory Class
		// public virtual int Amount { get; set; }
		
		public virtual int ProjectileCount { get; set; }

		public virtual float ExplosionRadius { get; set; }

		public virtual bool Bounce { get; set; }


	}
}
