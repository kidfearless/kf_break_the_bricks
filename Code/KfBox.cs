using Sandbox;

using System.Diagnostics;

public class KfBox : Component
{
	[Property] public int Health { get; set; } = 1;

	protected override void OnUpdate()
	{
	}

	public void Damage(int damage = 1)
	{
		Health -= damage;
		if (Health <= 0)
		{
			Kill();
		}
	}

	public void Kill()
	{
		var position = this.LocalPosition;

		this.DestroyGameObject();

	}
}
