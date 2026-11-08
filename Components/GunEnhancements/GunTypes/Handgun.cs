using Terraria;

namespace TerrariaOverhaulAddon.Components.GunEnhancements;
internal class Handgun : TerrariaOverhaul.Common.Guns.Handgun
{
	public override bool ShouldApplyItemOverhaul(Item item)
		=> GunChecks.IsHandgun(item);
}
