using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;


namespace Tanks.AmmoTypes
{
	[GameResource("TankAmmo", "amtype", "Various ammo types that can be purchased and used by the player's tanks")]
	public partial class TankAmmo : GameResource
	{
		public static IReadOnlyList<TankAmmo> All => _all;
		internal static List<TankAmmo> _all = new();


		[ResourceType( "vmdl" )]
		public string Model { get; set; }

		public string BombName { get; set; }

		public float ExplosionRadius { get; set; }

		public  float BombMass { get; set; }

		public int ProjectileCount { get; set; }

		public bool Bounce { get; set; }

		public bool ApplyGravity { get; set; }



		//When Each Asset is loaded automatically upon gamestart it will call this method to add itself to the the read only list
		protected override void PostLoad()
		{
			base.PostLoad();

			if ( !_all.Contains( this ) )
				_all.Add( this );
		}

	}
}
