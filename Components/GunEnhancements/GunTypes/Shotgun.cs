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

public class Shotgun : GunOverhaul
{
	public SoundStyle? PumpSound { get; set; }

	private uint pumpTime;

	public override bool ShouldApplyItemOverhaul(Item item)
		=> GunChecks.IsShotgun(item);

	public override void SetDefaults(Item item)
	{
		ShootSound = SoundAssets.ShotgunShootSound;
		PumpSound = SoundAssets.ShotgunPumpSound;
		if (item.UseSound == SoundID.Item38)
		{
			ShootSound = SoundAssets.CombatShotgunShootSound;
			PumpSound = SoundAssets.CombatShotgunPumpSound;
		}

		if (!Main.dedServ)
		{
			item.EnableComponent<ItemAimRecoil>();
			item.EnableComponent<ItemBulletCasings>(c => {
				c.CasingGoreType = ModContent.GoreType<ShellCasing>();
				c.SpawnOnUse = false;
			});
		}

		base.SetDefaults(item);
	}

	public override float UseSpeedMultiplier(Item item, Player player)
	{
		float result = base.UseSpeedMultiplier(item, player);
		result /= 1f;
		return result;
	}

	public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
	{
		base.ModifyWeaponDamage(item, player, ref damage);
		damage *= 1f;
	}

	public override bool? UseItem(Item item, Player player)
	{
		bool? baseResult = base.UseItem(item, player);

		if (baseResult == false)
		{
			return false;
		}

		pumpTime = (uint)(Main.GameUpdateCount + player.itemAnimationMax / 2.5);

		return baseResult;
	}

	public override void HoldItem(Item item, Player player)
	{
		base.HoldItem(item, player);

		if (!Main.dedServ && PumpSound != null && pumpTime != 0 && Main.GameUpdateCount == pumpTime)
		{
			SoundEngine.PlaySound(PumpSound, player.Center);

			item.GetGlobalItem<ItemBulletCasings>().SpawnCasings(item, player);
		}
	}
}
