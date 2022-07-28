using Sandbox;
using Tanks;
using Tanks.AmmoTypes;
using System.Linq;

namespace Sandbox { 
	public partial class TanksPlayer : Player
	{
		TankInventory PlayerInventory;

		
		public float PlayerBarrelRotation;

		public TanksPlayer() : base() 
		{
			PlayerInventory = new TankInventory(this);
			SetAnimGraph( "animations/tank.vanmgrph" );
			PlayerBarrelRotation = 0f;
			
		}
		



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


		public void SpawnModel()
		{
			if ( IsClient && Input.Pressed( InputButton.PrimaryAttack) )
			{
				var p = new ModelEntity();
				//Resource library goes off of where you saved it in your file structure, as I saved mine in assettypes this is where I call it from.
				p.SetModel( ResourceLibrary.Get<TankAmmo>( "assettypes/regularmissile.amtype" ).Model);
				p.Position = GetAttachment("endOfBarrel").Value.Position;

			}
		}

	}
}
