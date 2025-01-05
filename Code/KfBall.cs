using Sandbox;

using System;



public class KfBall : Component, Component.ICollisionListener
{
	[AttributeUsage(AttributeTargets.Property)]
	[CodeGenerator(CodeGeneratorFlags.WrapPropertySet | CodeGeneratorFlags.Instance, "OnWrapSet")]
	public class WrapSet : Attribute { }

	[Property] public Vector3 Velocity { get; set; }
	[WrapSet][Property] public float Radius { get; set; } = 32f;
	[Property] public float Speed { get; set; } = 300f;
	[Property, Range(0, 1)] public float BounceAngleScale { get; set; } = 0.5f;
	private Guid LastTouchedObject;

	public void OnWrapSet(WrappedPropertySet<float> setter)
	{
		this.LocalScale = setter.Value / 32f;
		setter.Setter(setter.Value);
	}

	GameObject Paddle => Scene.Children.First(t => t.Name == "Paddle");

	protected override void OnFixedUpdate()
	{
		var nextPosition = this.LocalPosition + (Velocity * Time.Delta);

		var result = Scene.Trace.Sphere(Radius, this.LocalPosition, nextPosition)
			.WithAnyTags("bounds", "paddle", "box")
			.WithoutTags("ball")
			.Run();

		if (result.Hit && result.GameObject.Id != LastTouchedObject)
		{
			this.Velocity = Vector3.Reflect(Velocity, result.Normal);
			var other = result.GameObject.GetComponentInParent<CharacterController>()?.Velocity ?? Vector3.Zero;

			this.Velocity += other;
			nextPosition = result.EndPosition + Velocity * Time.Delta;


			result.GameObject.As<KfBox>()?.Damage();
			LastTouchedObject = result.GameObject.Id;
		}

		this.LocalPosition = nextPosition.WithY(0);
	}

	/// <summary>
	/// returns the relative angle from Vector3.Up in the 2d plane. where negatives are to the right, and positives are to the left.
	/// </summary>
	/// <param name="velocity"></param>
	/// <returns></returns>
	public static float CalculateAngleInDegrees(Vector3 velocity)
	{
		velocity = velocity.Normal;
		// Calculate the angle in radians
		var angleRadians = Math.Atan2(velocity.x, velocity.z);

		// Convert to degrees
		var angleDegrees = angleRadians * (180 / Math.PI);

		return (float)angleDegrees;
	}


	public static Vector3 GetDirectionFromAngle(float angleInDegrees)
	{
		// Convert angle from degrees to radians
		var angleRadians = float.DegreesToRadians(angleInDegrees);

		// Calculate the direction vector components
		var xComponent = MathF.Cos(angleRadians);
		var yComponent = 0.0f; // Assuming you're working in a 2D plane (X/Z)
		var zComponent = MathF.Sin(angleRadians);

		return new Vector3(xComponent, yComponent, zComponent);
	}

	protected override void OnUpdate()
	{
		if (Input.Down("jump") && GameObject.Parent.Tags.Has("paddle"))
		{
			this.GameObject.SetParent(Scene.Children.First(t => t.Name == "Balls"));
			this.Velocity = Vector3.Up * Speed;
		}

		if (this.Velocity.Length < Speed && this.Velocity.Length > 0)
		{
			this.Velocity = this.Velocity.Normal * Speed;
		}

		if (this.Velocity.Length > this.Speed)
		{
			var temp = this.Velocity.LerpTo(this.Velocity.Normal * Speed, Time.Delta);
			this.Velocity = temp;
		}
	}
}
