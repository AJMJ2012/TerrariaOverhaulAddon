using Terraria;
using TerrariaOverhaul.Common.Items;
using TerrariaOverhaul.Common.Recoil;
using TerrariaOverhaul.Core.ItemComponents;
using TerrariaOverhaulAddon.Common;

namespace TerrariaOverhaulAddon.Components.GunEnhancements;

public class LaserGun : GunOverhaul
{

	public override bool ShouldApplyItemOverhaul(Item item)
		=> GunChecks.IsLaserGun(item);

	public override void SetDefaults(Item item)
	{
		ShootSound = SoundAssets.LaserGunShootSound;

		if (!Main.dedServ)
		{
			item.EnableComponent<ItemAimRecoil>();
			item.EnableComponent<ItemPlaySoundOnEveryUse>();
		}

		base.SetDefaults(item);
	}
}