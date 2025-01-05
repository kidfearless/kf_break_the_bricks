using Sandbox;

public sealed class Kfcamera : Component
{
	CameraComponent Camera => this.Components.Get<CameraComponent>();
	protected override void OnStart()
	{
		base.OnStart();
	}
	protected override void OnUpdate()
	{
		//Camera.CustomSize = 12.0f/9.0f;

	}
}
