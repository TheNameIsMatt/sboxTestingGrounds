using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Ammo : Panel
{
	public Label Word;
	public Button btn;

	[Library]
	public Ammo()
	{
		StyleSheet.Load( "/UI/Ammo.scss" );
		Word = Add.Label( "Sandbox", "ammo" );
		btn = Add.Button( "Button", "fire" );


	}

	public override void Tick()
	{

	}
}
