using Terraria;
using Terraria.ID;
using TerrariaOverhaulAddon.Common;

namespace TerrariaOverhaulAddon.Components.GunEnhancements.SpecificGuns;
// A specialized sound change.
// This weapon uses the game mecahnics of a rifle, but it's style is actually a BB gun.
// I've done my best to use some fitting sounds.
internal class RedRyder : BoltRifle
{
	public override bool ShouldApplyItemOverhaul(Item item)
		=> item.type == ItemID.RedRyder;

	public override void SetDefaults(Item item)
	{
		base.SetDefaults(item);
		ShootSound = SoundAssets.RedRyderShootSound;
		RackSound = SoundAssets.RedRyderPumpSound;
		// But this weapon pumps! It doesn't rack!
	}
}
