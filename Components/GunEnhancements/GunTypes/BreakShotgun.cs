using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using TerrariaOverhaul.Common.EntityEffects;
using TerrariaOverhaul.Common.Guns;
using TerrariaOverhaul.Common.Recoil;
using TerrariaOverhaul.Content.Gores;
using TerrariaOverhaul.Core.ItemComponents;
using TerrariaOverhaul.Core.Time;
using TerrariaOverhaulAddon.Common;

namespace TerrariaOverhaulAddon.Components.GunEnhancements;
// No herustics, will just be used as a base for the 2 vanilla has
internal class BreakShotgun : GunOverhaul
{
	public SoundStyle? OpenSound { get; set; }
	public SoundStyle? LoadSound { get; set; }
	public SoundStyle? CloseSound { get; set; }
    private int? _casingCount { get; set; }
	public int CasingCount {
        get => _casingCount ?? 1;
        set => _casingCount = value;
    }

    private uint openTime;
    private uint unloadTime;
    private uint loadTime;
	private uint closeTime;
	private uint endTime;
    //private int shotsLeft = 0;

	public override bool ShouldApplyItemOverhaul(Item item)
        => GunChecks.IsBreakShotgun(item);
	public override void SetDefaults(Item item)
	{
		OpenSound = SoundAssets.BreakShotgunOpenSound;
		LoadSound = SoundAssets.BreakShotgunLoadSound;
		CloseSound = SoundAssets.BreakShotgunCloseSound;
		ShootSound = SoundAssets.BreakShotgunShootSound;

		if (!Main.dedServ)
        {
            item.EnableComponent<ItemAimRecoil>();
            item.EnableComponent<ItemBulletCasings>(c =>
            {
                c.CasingGoreType = ModContent.GoreType<ShellCasing>();
                c.SpawnOnUse = false;
            });
        }

        base.SetDefaults(item);
    }

    public override float UseSpeedMultiplier(Item item, Player player)
    {
        float result = base.UseSpeedMultiplier(item, player);
        result /= 2f;
        return result;
    }

    public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
    {
        base.ModifyWeaponDamage(item, player, ref damage);
        damage *= 2f;
    }

    public override bool? UseItem(Item item, Player player)
    {
        bool? baseResult = base.UseItem(item, player);

		if (baseResult == false)
        {
            return false;
		}
		/*
        // This is just a test for now.
        // Would most likely desync on multiplayer.
		if (--shotsLeft <= 0)
		{
			item.reuseDelay = item.useAnimation * 8;
            float timeDelay = player.itemAnimationMax;
            float reuseDelay = item.reuseDelay / CombinedHooks.TotalUseSpeedMultiplier(player, item);
			float timeFract = reuseDelay / 4f;
			openTime = (uint)(Main.GameUpdateCount + timeDelay + timeFract * 1.0);
			if (timeFract >= 10)
			{
				unloadTime = openTime + 10;
				loadTime = openTime + 20;
			}
			else
			{
				unloadTime = (uint)(Main.GameUpdateCount + timeDelay + timeFract * 1.5);
				loadTime = (uint)(Main.GameUpdateCount + timeDelay + timeFract * 2.0);
			}
			closeTime = (uint)(Main.GameUpdateCount + timeDelay + timeFract * 3.0);
			endTime = (uint)(Main.GameUpdateCount + timeDelay + reuseDelay);
			shellsLoaded = 0;
            shotsLeft = 2;
		}
        else
        {
            item.reuseDelay = 0;
		}
        */

		float timeFract = player.itemAnimationMax / 4f;
		openTime = (uint)(Main.GameUpdateCount + timeFract * 1.0);
		if (timeFract >= 10)
		{
			unloadTime = openTime + 10;
			loadTime = openTime + 20;
		}
		else
		{
			unloadTime = (uint)(Main.GameUpdateCount + timeFract * 1.5);
			loadTime = (uint)(Main.GameUpdateCount + timeFract * 2.0);
		}
		closeTime = (uint)(Main.GameUpdateCount + timeFract * 3.0);
		endTime = (uint)(Main.GameUpdateCount + player.itemAnimationMax);
		shellsLoaded = 0;

		return baseResult;
    }

    private int shellsLoaded;
    public override void HoldItem(Item item, Player player)
    {
        base.HoldItem(item, player);

        if (!Main.dedServ)
		{
			if (openTime != 0 && closeTime != 0)
			{
				// This is a dumb way to do reloading animations, but at least I can directly set offset angles.
				// Offset angles are divided roughly by 2 because of lerping by 10. I can't be bothered to think about it more.
				var holdout = player.GetModPlayer<PlayerHoldOutAnimation>();
				if (Main.GameUpdateCount >= openTime && Main.GameUpdateCount < closeTime)
				{
					holdout.VisualRecoil = MathHelper.Lerp(holdout.VisualRecoil, -90f, 10f * TimeSystem.LogicDeltaTime);
				}
				else if (Main.GameUpdateCount >= closeTime && Main.GameUpdateCount < closeTime + 10)
				{
					holdout.VisualRecoil = MathHelper.Lerp(holdout.VisualRecoil, 45f, 10f * TimeSystem.LogicDeltaTime);
				}
			}

			if (OpenSound != null && openTime != 0 && Main.GameUpdateCount == openTime)
            {
                SoundEngine.PlaySound(OpenSound.Value, player.Center);
            }
            if (unloadTime != 0 && Main.GameUpdateCount == unloadTime)
            {
                item.GetGlobalItem<ItemBulletCasings>().SpawnCasings(item, player, CasingCount);
            }
            if (LoadSound != null && loadTime != 0)
            {
                float timeFract = (float)Math.Max(1.0, (closeTime - loadTime) / (float)CasingCount);
                if (Main.GameUpdateCount == loadTime + (int)(timeFract * shellsLoaded) && shellsLoaded < CasingCount)
                {
                    shellsLoaded += 2;
                    SoundEngine.PlaySound(LoadSound.Value, player.Center);
                }
            }
            if (CloseSound != null && closeTime != 0 && Main.GameUpdateCount == closeTime)
            {
                SoundEngine.PlaySound(CloseSound.Value, player.Center);
            }
        }
    }
}
