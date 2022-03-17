using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sandbox
{
	partial class BaseTGWeapon : BaseWeapon, IRespawnableEntity
	{

		public virtual SandboxPlayer.AmmoType AmmoType => SandboxPlayer.AmmoType.Pistol;
		public virtual int ClipSize => 16;
		public virtual float ReloadTime => 3.0f;
		public virtual int Bucket => 1;
		public virtual int BucketWeight => 100;

		[Net, Predicted]
		public int AmmoClip { get; set; }

		[Net, Predicted]
		public TimeSince TimeSinceReload { get; set; }

		[Net, Predicted]
		public bool IsReloading { get; set; }

		[Net, Predicted]
		public TimeSince TimeSinceDeployed { get; set; }


		public PickupTrigger PickupTrigger { get; protected set; }

		[ClientRpc]
		protected virtual void ShootEffects()
		{
			Host.AssertClient();

			Particles.Create( "particles/pistol_muzzleflash.vpcf", EffectEntity, "muzzle" );

			if ( IsLocalPawn )
			{
				new Sandbox.ScreenShake.Perlin();
			}

			ViewModelEntity?.SetAnimParameter( "fire", true );
			CrosshairPanel?.CreateEvent( "fire" );
		}

		/// <summary>
		/// Shoot a single bullet
		/// </summary>
		public virtual void ShootBullet( float spread, float force, float damage, float bulletSize, int bulletCount = 1 )
		{
			//
			// Seed rand using the tick, so bullet cones match on client and server
			//
			Rand.SetSeed( Time.Tick );

			for ( int i = 0; i < bulletCount; i++ )
			{
				var forward = Owner.EyeRotation.Forward;
				forward += (Vector3.Random + Vector3.Random + Vector3.Random + Vector3.Random) * spread * 0.25f;
				forward = forward.Normal;

				//
				// ShootBullet is coded in a way where we can have bullets pass through shit
				// or bounce off shit, in which case it'll return multiple results
				//
				foreach ( var tr in TraceBullet( Owner.EyePosition, Owner.EyePosition + forward * 5000, bulletSize ) )
				{
					tr.Surface.DoBulletImpact( tr );

					if ( !IsServer ) continue;
					if ( !tr.Entity.IsValid() ) continue;

					var damageInfo = DamageInfo.FromBullet( tr.EndPosition, forward * 100 * force, damage )
						.UsingTraceResult( tr )
						.WithAttacker( Owner )
						.WithWeapon( this );

					tr.Entity.TakeDamage( damageInfo );
				}
			}
		}
		public bool TakeAmmo( int amount )
		{
			if ( AmmoClip < amount )
				return false;

			AmmoClip -= amount;
			return true;
		}

		[ClientRpc]
		public virtual void DryFire()
		{
			// CLICK
		}

	}
}
