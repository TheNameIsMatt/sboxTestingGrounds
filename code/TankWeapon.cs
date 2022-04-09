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

		public string BombName { get; set; }

		[ResourceType("vmdl")]
		public string Model { get; set; }

		public int Amount { get; set; }
	}
}
