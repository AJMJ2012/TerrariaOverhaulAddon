using Terraria;
using TerrariaOverhaul.Common.Items;
using TerrariaOverhaul.Common.Recoil;
using TerrariaOverhaul.Core.ItemComponents;
using TerrariaOverhaulAddon.Common;

namespace TerrariaOverhaulAddon.Components.GunEnhancements;

public class Nailgun : GunOverhaul
{
	public override bool ShouldApplyItemOverhaul(Item item)
		=> GunChecks.IsNailgun(item);

	public override void SetDefaults(Item item)
	{
		ShootSound = SoundAssets.NailgunShootSound;

		if (!Main.dedServ)
		{
			item.EnableComponent<ItemAimRecoil>();
			item.EnableComponent<ItemPlaySoundOnEveryUse>();
		}

		base.SetDefaults(item);
	}
}
