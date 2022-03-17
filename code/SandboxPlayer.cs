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
	  }
}
