using Terraria;

namespace TerrariaOverhaulAddon.Components.GunEnhancements;
internal class GrenadeLauncher : TerrariaOverhaul.Common.Guns.GrenadeLauncher
{
	public override bool ShouldApplyItemOverhaul(Item item)
		=> GunChecks.IsGrenadeLauncher(item);
}
