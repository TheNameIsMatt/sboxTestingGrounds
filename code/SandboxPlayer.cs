using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox { 
	partial class SandboxPlayer : Player
	{


		[Net, Predicted, Change(nameof( MoveByPointerPosition ) )]
		private float MousePosition { get; set; }

	
		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );
	
			//Because it inherits these controllers and animators you can just call Controller rather than this.controller
			Controller = new NoclipController();
			Animator = new StandardPlayerAnimator();
	
	
	
			if ( DevController is NoclipController )
			{
				DevController = null;
			}
	
			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;
	
	
			CameraMode = new FirstPersonCamera();


			base.Respawn();
		}
		public override void Simulate( Client cl )
		{
			// Because I create an instance of sandbox player after inheriting from Player I have to call base.similate

			//Within this base class simulate, (player in this case) there is a Lifecheck to see if the pawn is alive, if not they will be respawned after 3 seconds
			base.Simulate( cl );



			// The Input.Pressed (InputButton class is linked to the bindings set in the menu, the .View field is linked to the C key in the bindings
			ChangeCamera();
			SpawnModel();
			PrintCursorPosition();
			MoveByPointerPosition();
		}

		[ClientRpc]
		private void MoveByPointerPosition()
		{
			if ( IsClient )
			{
				MousePosition = Mouse.Position.x;
			}

			if ( IsServer )
			{
				if ( MousePosition < 200f )
				{
					Log.Info( MousePosition );
					Log.Info( "Server Call" );

					Position = Vector3.Lerp( Transform.Position, new Vector3( Transform.Position.x + 0.3f, Transform.Position.y, Transform.Position.z ), 1 );
				}
			}

			if (IsClient)
			{	
				if ( MousePosition < 200f)
				{
					Log.Info( "Mouse Position Client Call");

					 Position = Vector3.Lerp(Transform.Position, new Vector3( Transform.Position.x + 0.3f, Transform.Position.y, Transform.Position.z), 1  );
				}
			}


		}

		public void SpawnModel()
		{
			if ( IsServer && Input.Pressed( InputButton.Attack1 ) )
			{

				var p = new ModelEntity();
				p.SetModel( "weapons/rust_pistol/rust_pistol.vmdl" );
				p.Position = EyePosition + EyeRotation.Forward * 40;
				p.Rotation = Rotation.LookAt( Vector3.Random.Normal );
				p.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
				p.PhysicsGroup.Velocity = EyeRotation.Forward * 1000;

			}
		}
		public void ChangeCamera()
		{
			if ( IsServer && Input.Pressed( InputButton.View ) )
			{
				if ( CameraMode.GetType() == typeof( FirstPersonCamera ) )
				{

					CameraMode = new ThirdPersonCamera();
				}
				else
				{
					CameraMode = new FirstPersonCamera();
				}
				Log.Info( "Camera mode GetType() is: " + CameraMode.GetType() );
				Log.Info( "Camera mode TypeOf() is: " + typeof( FirstPersonCamera ) );
			}
		}

		public void PrintCursorPosition()
		{
			if (IsClient && Input.Pressed( InputButton.Forward ) )
			{
				Log.Info( "Mouse Position X is: " + Mouse.Position.x);
				Log.Info( "Mouse Position Y is: " + Mouse.Position.y);

			}
		}

	}
}
