using Terraria;

namespace TerrariaOverhaulAddon.Components.GunEnhancements;
internal class RocketLauncher : TerrariaOverhaul.Common.Guns.RocketLauncher
{
	public override bool ShouldApplyItemOverhaul(Item item)
		=> GunChecks.IsRocketLauncher(item);
}
