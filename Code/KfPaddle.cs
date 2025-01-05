using Sandbox;

public class KfPaddle : Component
{
	public CharacterController Controller => this.Components.Get<CharacterController>();
	MeshComponent Mesh => this.Components.Get<MeshComponent>();
	[Property] public float Speed { get; set; } = 10f;
	[Property] public float Friction { get; set; } = 5;
	[Property] public float StopSpeed { get; set; } = 140;

	protected override void OnUpdate()
	{
		if (Input.Down("Left"))
		{
			Controller.Accelerate(Speed * Vector2.Left);
		}
		else if (Input.Down("Right"))
		{
			Controller.Accelerate(Speed * Vector2.Right);
		}
		else
		{
			Controller.ApplyFriction(this.Friction, this.StopSpeed);
		}

		Controller.Move();
		this.LocalPosition = LocalPosition with
		{
			y = 0,
			z = 0,
		};
	}
}
