using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

// I'm using my own detection system
namespace TerrariaOverhaulAddon.Components.GunEnhancements;
internal class DisableOriginalGunDetection : ModSystem
{
    static readonly Type[] guns =
    [
        typeof(TerrariaOverhaul.Common.Guns.AssaultRifle),
        typeof(TerrariaOverhaul.Common.Guns.GrenadeLauncher),
        typeof(TerrariaOverhaul.Common.Guns.Handgun),
        typeof(TerrariaOverhaul.Common.Guns.Minigun),
        typeof(TerrariaOverhaul.Common.Guns.Revolver),
        typeof(TerrariaOverhaul.Common.Guns.RocketLauncher),
        typeof(TerrariaOverhaul.Common.Guns.Shotgun)
    ];
    public override void Load()
    {
        foreach (Type gun in guns)
        {
            MonoModHooks.Add(gun.GetMethod("ShouldApplyItemOverhaul", BindingFlags.Public | BindingFlags.Instance), bool (Func<object, Item, bool> orig, object self, Item item) => { return false; });
        }
    }
}