using Sandbox;
using Tanks;
using Tanks.AmmoTypes;
using System.Linq;

namespace Tanks
{
	public partial class TanksPlayer : Player
	{
		TankInventory PlayerInventory;

		public Vector3 TankBarrelPostion;
		public Vector3 TankBarrelRotation;

		public float PlayerBarrelRotation;

		public TanksPlayer() : base()
		{
			PlayerInventory = new TankInventory( this );
			SetAnimGraph( "animations/tank.vanmgrph" );
			PlayerBarrelRotation = 0f;

			// Debug Code


		}




		public override void Respawn()
		{

			SetModel( "models/Tankv1/tank.vmdl" );

			//Because it inherits these controllers and animators you can just call Controller rather than this.controller
			Controller = new TankController();

			Animator = new StandardTankAnimator();

			CameraMode = new Camera();


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
			var tankBarrel = GetAttachment( "endOfBarrel" ).Value;



			// The Input.Pressed (InputButton class is linked to the bindings set in the menu, the .View field is linked to the C key in the bindings
			SpawnModel();
			DrawDebugNormals( tankBarrel.Position, (Rotation)tankBarrel.Rotation );

			base.Simulate( cl );
		}


		public void SpawnModel()
		{
			if ( IsClient && Input.Pressed( InputButton.PrimaryAttack ) )
			{
				var tankBarrel = GetAttachment( "endOfBarrel" ).Value;
				var barrelBone = GetBoneTransform( "BarrelBone" ).Rotation;

				var p = new Prop()
				{
					Position = tankBarrel.Position,
					Rotation = GetAttachment( "endOfBarrel" ).Value.Rotation
				};
				//Resource library goes off of where you saved it in your file structure, as I saved mine in assettypes this is where I call it from.
				p.SetModel( ResourceLibrary.Get<TankAmmo>( "assettypes/regularmissile.amtype" ).Model );

				//
				//
				// Need to update it so Props Rotation.Up faces the same direction as the attachments Rotaiton.left
				//
				//
				p.Velocity = GetAttachment( "endOfBarrel" ).Value.Rotation.Left * 320f;

			}
		}

		public void DrawDebugNormals( Vector3 position, Rotation rotation )
		{
			//DebugOverlay.Line( position, rotation. * 100f, Color.Red );
			//DebugOverlay.Line( position, rotation.Right * 100f, Color.Green ) ;
			//DebugOverlay.Line( position, rotation.Up * 100f, Color.Blue );
			DebugOverlay.Axis( position, rotation );
		}

	}
}
