using Sandbox;
using System;
using System.Linq;

namespace Sandbox
{
	partial class Pawn : AnimatedEntity
	{
		/// <summary>
		/// Called when the entity is first created 
		/// </summary>
		public override void Spawn()
		{
			base.Spawn();

			//
			// Use a watermelon model
			//
			SetModel( "models/citizen/citizen.vmdl" );

			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

		}

		//it's very important that in your Pawn's BuildInput you also call ActiveChild?.BuildInput( input ); and that you do it before base.BuildInput( input ); because BuildInput on entities is NOT called by default.
		//It is called for a player's Pawn by default in the base Game class, you need to pass it down from there.

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			
			base.Simulate( cl );

			Rotation = Input.Rotation;
			EyeRotation = Rotation;

			// build movement from the input values
			var movement = new Vector3( Input.Forward, Input.Left, 0 ).Normal;

			// rotate it to the direction we're facing
			Velocity = Rotation * movement;

			// apply some speed to it
			Velocity *= Input.Down( InputButton.Run ) ? 1000 : 200;

			// apply it to our position using MoveHelper, which handles collision
			// detection and sliding across surfaces for us
			MoveHelper helper = new MoveHelper( Position, Velocity );
			helper.Trace = helper.Trace.Size( 16 );
			if ( helper.TryMove( Time.Delta ) > 0 )
			{
				Position = helper.Position;
			}

			// If we're running serverside and Attack1 was just pressed, spawn a ragdoll

		}



		/// <summary>
		/// Called every frame on the client
		/// </summary>
		public override void FrameSimulate( Client cl )
		{
			base.FrameSimulate( cl );

			// Update rotation every frame, to keep things smooth
			Rotation = Input.Rotation;
			EyeRotation = Rotation;
		}
	}
}
