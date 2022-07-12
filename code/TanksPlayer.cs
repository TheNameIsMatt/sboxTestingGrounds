using Sandbox;
using Tanks;
using System.Linq;

namespace Sandbox { 
	public partial class TanksPlayer : Player
	{
		TankInventory PlayerInventory;
		public TanksPlayer() : base() 
		{
			PlayerInventory = new TankInventory(this);
			SetAnimGraph( "animations/tank.vanmgrph" );
		}
		

		[Net, Predicted, Change(nameof( MoveByPointerPosition ) )]
		private float MousePositionX { get; set; }


		[Net, Predicted, Change( nameof( MoveByPointerPosition ) )]
		private float MousePositionY { get; set; }




		public override void Respawn()
		{
			
			SetModel( "models/Tankv1/tank.vmdl" );

			//Because it inherits these controllers and animators you can just call Controller rather than this.controller
			Controller = new TankController();

			Animator = new StandardTankAnimator();

			CameraMode = new ThirdPersonCamera();


			if ( DevController is NoclipController )
			{
				DevController = null;
			}
	
			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			base.Respawn();
		}


		public override void Simulate( Client cl )
		{
			// Because I create an instance of sandbox player after inheriting from Player I have to call base.similate

			//Within this base class simulate, (player in this case) there is a Lifecheck to see if the pawn is alive, if not they will be respawned after 3 seconds
			
			
			
			// The Input.Pressed (InputButton class is linked to the bindings set in the menu, the .View field is linked to the C key in the bindings
			//ChangeCamera();
			SpawnModel();
			//PrintCursorPosition();
			//ClickTest();


			//MoveByPointerPosition();
			base.Simulate( cl );
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
			if ( IsServer && Input.Pressed( InputButton.PrimaryAttack) )
			{

				var p = new ModelEntity();
				p.SetModel( "models/rockets/sboxrocket.vmdl");
				p.Position = EyePosition + EyeRotation.Forward * 40;

			}
		}
		public void ChangeCamera()
		{
			if ( IsServer && Input.Pressed( InputButton.View ) )
			{
				if ( CameraMode is FirstPersonCamera)
				{

					CameraMode = new ThirdPersonCamera();
				}
				else
				{
					CameraMode = new FirstPersonCamera();
				}

			}
		}



	}
}
