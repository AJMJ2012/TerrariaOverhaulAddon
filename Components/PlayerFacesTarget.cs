using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using TerrariaOverhaul.Common.Movement;
using TerrariaOverhaul.Utilities;

namespace TerrariaOverhaulAddon.Components;

internal class PlayerFacesTarget : ModPlayer
{
	public override void PreUpdate()
		=> UpdateDirection();

	public override void PostUpdate()
		=> UpdateDirection();

	public override bool PreItemCheck()
	{
		UpdateDirection();
		return true;
	}

	public override void PostItemCheck()
	{
		UpdateDirection();
	}

	public void UpdateDirection()
	{
		Vector2 lookPosition = Vector2.Zero;
		bool forcedLooking = false;

		// Look at Smart Cursor
		if (Main.SmartCursorIsUsed)
		{
			Point smartCursor = new(Main.SmartCursorX, Main.SmartCursorY);
			if (Main.SmartCursorShowing)
			{
				lookPosition = (smartCursor.ToVector2() * 16f) + new Vector2(8, 8) + (Player.velocity * 2f);
				forcedLooking = true;
			}
			/*
			Point smartInteract = new Point(Main.SmartInteractX, Main.SmartInteractY);
			if (smartInteract.X > 0 && smartInteract.Y > 0) {
				lookPosition = (smartInteract.ToVector2() * 16f) + new Vector2(8, 8) + (Player.velocity * 2f);
				forcedLooking = true;
			}
			*/
		}

		// Look at Locked On target
		if (LockOnHelper.AimedTarget != null)
		{
			lookPosition = LockOnHelper.AimedTarget.position;
			forcedLooking = true;
		}

		Direction1D lookDirection = (Direction1D)(lookPosition.X >= Player.Center.X ? 1 : -1);

		if (forcedLooking)
		{
			var playerDirection = Player.GetModPlayer<PlayerDirectioning>();
			playerDirection.SetLookPositionOverride(lookPosition, 1);
			playerDirection.SetDirectionOverride(lookDirection, 1);
		}
	}
}