using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sandbox
{
	public class SandboxPlayerInventory : BaseInventory
	{
		public SandboxPlayerInventory( Player player ) : base( player )
		{

		}

		public override bool Add( Entity ent, bool makeActive = false )
		{
			var player = Owner as SandboxPlayer;
			var weapon = ent as BaseTGWeapon;
			//var notices = !player.SupressPickupNotices;

			//
			// We don't want to pick up the same weapon twice
			// But we'll take the ammo from it Winky Face
			//
			if ( weapon != null )
			{
				var ammo = weapon.AmmoClip;
				var ammoType = weapon.AmmoType;

				if ( ammo > 0 )
				{
					player.GiveAmmo( ammoType, ammo );

					//if ( notices )
					//{
					//	Sound.FromWorld( "dm.pickup_ammo", ent.Position );
					//	PickupFeed.OnPickup( To.Single( player ), $"+{ammo} {ammoType}" );
					//}
				}

				ItemRespawn.Taken( ent );

				// Despawn it
				ent.Delete();
				return false;
			}

			if ( weapon != null )
			{
				Sound.FromWorld( "dm.pickup_weapon", ent.Position );
				//PickupFeed.OnPickup( To.Single( player ), $"{ent.ClassInfo.Title}" );
			}


			ItemRespawn.Taken( ent );
			return base.Add( ent, makeActive );
		}

	}
}
