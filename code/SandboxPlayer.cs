using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox { 
	partial class SandboxPlayer : Player
	{
		public TankWeapon tankWeapon;
		public SandboxPlayer() : base() 
		{
			Inventory = new TankInventory(this);
		}


		[Net, Predicted, Change(nameof( MoveByPointerPosition ) )]
		private float MousePositionX { get; set; }


		[Net, Predicted, Change( nameof( MoveByPointerPosition ) )]
		private float MousePositionY { get; set; }




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
	
	
			CameraMode = new ThirdPersonCamera();


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


		//	MoveByPointerPosition();
		}

		[ClientRpc]
		private void MoveByPointerPosition()
		{
			Vector3 CarriableVector = Position;

			if ( IsClient )
			{
				MousePositionX = Mouse.Position.x;
				MousePositionY = Mouse.Position.y;
			}

			if ( IsServer )
			{
				
				if ( MousePositionX < Screen.Width / 4 )
				{
					// Move player Left
					CarriableVector.x = CarriableVector.x + 0.3f;
				}

				if ( MousePositionX > Screen.Width - (Screen.Width / 4))
				{
					//Move Player Right
					CarriableVector.x = CarriableVector.x - 0.3f;
				}

				if ( MousePositionY > Screen.Height - (Screen.Height / 4))
				{
					//Move Player Down
					CarriableVector.z = CarriableVector.z - 0.3f;
				}

				if ( MousePositionY < Screen.Height / 4)
				{
					//Move Player Up -- Remove later for scroll feature instead
					CarriableVector.z = CarriableVector.z + 0.3f;
				}

				Position = Vector3.Lerp( Position, CarriableVector, 1 );
			}

			//Repeat IsServer
			if (IsClient)
			{
				if ( MousePositionX < Screen.Width / 4 )
				{

					CarriableVector.x = CarriableVector.x + 0.3f;
				}

				if ( MousePositionX > Screen.Width - (Screen.Width / 4) )
				{

					CarriableVector.x = CarriableVector.x - 0.3f;
				}

				if ( MousePositionY > Screen.Height - (Screen.Height / 4) )
				{

					CarriableVector.z = CarriableVector.z - 0.3f;
				}

				if ( MousePositionY < Screen.Height / 4 )
				{

					CarriableVector.z = CarriableVector.z + 0.3f;
				}

				Position = Vector3.Lerp( Position, CarriableVector, 1 );
			}
		}

		public void SpawnModel()
		{
			if ( IsServer && Input.Pressed( InputButton.Attack1 ) )
			{

				var p = new ModelEntity();
				p.SetModel( "rockets/sboxrocket.vmdl" );
				p.Position = EyePosition + EyeRotation.Forward * 40;
				p.Rotation = Rotation.LookAt( Vector3.Random.Normal );



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

		public void ClickTest()
		{
			if ( IsServer && Input.Pressed( InputButton.Attack1 ) )
			{
				//
				
				//
			}
		}

	}
}
