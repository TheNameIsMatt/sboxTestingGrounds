using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
	public class CustomCamera : CameraMode
	{
		// https://github.com/apetavern/sbox-grubs/blob/master/code/Pawn/Camera.cs reference
		public Range DistanceRange => new Range( new Index( 128 ), new Index( 2048, true ) );


		public override void Update()
		{

			var currentMousePosition = Mouse.Position;
			throw new NotImplementedException();
		}
	}
}
