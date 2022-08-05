
using Sandbox;
using Sandbox.UI.Construct;
using Sandbox.UI;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tanks;
using Tanks.UI;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace Tanks
{
	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// </summary>
	/// 
	[Library( "Tanks", Title = "Tanks" )]
	public partial class MyGame : Sandbox.Game
	{
		public MyGame()
		{
			if ( IsServer )
			{
				new TanksHUD();
			}
		}

		public override void Simulate( Client cl )
		{
			ChangePlayerController( cl );
			base.Simulate( cl );

		}
		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			// Create a pawn for this client to play with
			var player = new TanksPlayer();
			player.Respawn();


			client.Pawn = player;

			// Get all of the spawnpoints
			var spawnpoints = Entity.All.OfType<SpawnPoint>();

			// chose a random one
			var randomSpawnPoint = spawnpoints.OrderBy( x => Guid.NewGuid() ).FirstOrDefault();

			// if it exists, place the pawn there
			if ( randomSpawnPoint != null )
			{
				var tx = randomSpawnPoint.Transform;
				tx.Position = tx.Position + Vector3.Up * 50.0f; // raise it up
				player.Transform = tx;
			}

		}
		public void ChangePlayerController( Client cl )
		{
			if ( cl.Pawn is TanksPlayer theirPlayer )
			{
				if ( Input.Pressed( InputButton.Zoom ) )
				{
					if ( theirPlayer.DevController is NoclipController )
					{
						Log.Info( "Noclip Mode Off" );
						theirPlayer.DevController = null;
					}
					else
					{
						Log.Info( "Noclip Mode On" );
						theirPlayer.DevController = new NoclipController();
					}
				}
			}
		}

	}

}
