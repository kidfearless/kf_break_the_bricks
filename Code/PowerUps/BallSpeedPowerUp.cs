using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.PowerUps;
public class PowerUp : Component
{
	public IEnumerable<KfBall> Balls => Scene.Children.First(t => t.Name == "Balls").GetComponentsInChildren<KfBall>();
	public KfPaddle Paddle => Scene.Children.First(t => t.Name == "Paddle").GetComponent<KfPaddle>();



	public virtual void OnActivate() { }
	public virtual void OnDeactivate() { }
}

public class BallSpeedPowerUp : PowerUp
{
	[Property] public float Multiplier { get; set; } = 1.5f;
	public override void OnActivate()
	{
		Balls.ToList().ForEach(t => t.Speed *= Multiplier);
	}

	public override void OnDeactivate()
	{
		Balls.ToList().ForEach(t => t.Speed /= Multiplier);
	}
}