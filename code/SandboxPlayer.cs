using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox { 
	partial class SandboxPlayer : Player
	{
		private TimeSince timeSinceDropped;
		private TimeSince timeSinceJumpReleased;
	
		private DamageInfo lastDamage;
	
		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );
	
			//Because it inherits these controllers and animators you can just call Controller rather than this.controller
			Controller = new WalkController();
			Animator = new StandardPlayerAnimator();
	
	
	
			if ( DevController is NoclipController )
			{
				DevController = null;
			}
	
			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;
	
	
			CameraMode = new ThirdPersonCamera();

			


			base.Respawn();
		}
		public override void Simulate( Client cl )
		{
			// Because I create an instance of sandbox player after inheriting from Player I have to call base.similate
			base.Simulate( cl );

			if ( IsServer && Input.Pressed( InputButton.Attack1 ) )
			{

				var p = new Pistol();
				p.SetModel( "weapons/rust_pistol/rust_pistol.vmdl" );
				p.AmmoClip = 10;
				p.Position = EyePosition + EyeRotation.Forward * 40;
				p.Rotation = Rotation.LookAt( Vector3.Random.Normal );
				p.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
				p.PhysicsGroup.Velocity = EyeRotation.Forward * 1000;

			}
		}
	}
}
