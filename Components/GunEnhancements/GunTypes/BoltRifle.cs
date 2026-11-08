using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaOverhaul.Common.Guns;
using TerrariaOverhaul.Common.Recoil;
using TerrariaOverhaul.Content.Gores;
using TerrariaOverhaul.Core.ItemComponents;
using TerrariaOverhaulAddon.Common;

namespace TerrariaOverhaulAddon.Components.GunEnhancements;

public class BoltRifle : GunOverhaul
{
	public SoundStyle? RackSound { get; set; }

	private uint rackTime;

	public override bool ShouldApplyItemOverhaul(Item item)
		=> GunChecks.IsBoltRifle(item) && item.type != ItemID.RedRyder;

	public override void SetDefaults(Item item)
	{
		ShootSound = SoundAssets.AssaultRifleFireSound;
		if (item.UseSound == SoundID.Item40) ShootSound = SoundAssets.SniperRifleShootSound;
		RackSound = SoundAssets.RifleRackSound;

		if (!Main.dedServ)
		{
			item.EnableComponent<ItemAimRecoil>();
			item.EnableComponent<ItemBulletCasings>(c => {
				c.CasingGoreType = ModContent.GoreType<BulletCasing>();
				c.SpawnOnUse = false;
			});
		}

		base.SetDefaults(item);
	}

	public override bool? UseItem(Item item, Player player)
	{
		bool? baseResult = base.UseItem(item, player);

		if (baseResult == false)
		{
			return false;
		}

		rackTime = (uint)(Main.GameUpdateCount + player.itemAnimationMax / 2);

		return baseResult;
	}

	public override void HoldItem(Item item, Player player)
	{
		base.HoldItem(item, player);

		if (!Main.dedServ && RackSound != null && rackTime != 0 && Main.GameUpdateCount == rackTime)
		{
			SoundEngine.PlaySound(RackSound, player.Center);

			item.GetGlobalItem<ItemBulletCasings>().SpawnCasings(item, player);
		}
	}
}
