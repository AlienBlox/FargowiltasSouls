// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Mounts.TrojanSquirrelMount
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Mounts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Mounts
{
  public class TrojanSquirrelMount : ModMount
  {
    public virtual void SetStaticDefaults()
    {
      this.MountData.buff = ModContent.BuffType<TrojanSquirrelMountBuff>();
      this.MountData.heightBoost = 20;
      this.MountData.fallDamage = 0.0f;
      this.MountData.runSpeed = 15f;
      this.MountData.dashSpeed = 8f;
      this.MountData.flightTimeMax = 0;
      this.MountData.fatigueMax = 0;
      this.MountData.jumpHeight = 50;
      this.MountData.acceleration = 0.2f;
      this.MountData.jumpSpeed = 15f;
      this.MountData.blockExtraJumps = true;
      this.MountData.totalFrames = 6;
      this.MountData.constantJump = true;
      int[] numArray = new int[this.MountData.totalFrames];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = 125;
      this.MountData.playerYOffsets = numArray;
      this.MountData.xOffset = 0;
      this.MountData.yOffset = -84;
      this.MountData.bodyFrame = 0;
      this.MountData.standingFrameCount = 1;
      this.MountData.standingFrameDelay = 12;
      this.MountData.standingFrameStart = 1;
      this.MountData.runningFrameCount = 6;
      this.MountData.runningFrameDelay = 25;
      this.MountData.runningFrameStart = 0;
      this.MountData.flyingFrameCount = 0;
      this.MountData.flyingFrameDelay = 0;
      this.MountData.flyingFrameStart = 0;
      this.MountData.inAirFrameCount = 1;
      this.MountData.inAirFrameDelay = 12;
      this.MountData.inAirFrameStart = 5;
      this.MountData.idleFrameCount = 1;
      this.MountData.idleFrameDelay = 12;
      this.MountData.idleFrameStart = 1;
      this.MountData.idleFrameLoop = true;
      this.MountData.swimFrameCount = this.MountData.inAirFrameCount;
      this.MountData.swimFrameDelay = this.MountData.inAirFrameDelay;
      this.MountData.swimFrameStart = this.MountData.inAirFrameStart;
      if (Main.netMode == 2)
        return;
      this.MountData.textureWidth = Utils.Width(this.MountData.backTexture);
      this.MountData.textureHeight = Utils.Height(this.MountData.backTexture);
    }

    public virtual bool Draw(
      List<DrawData> playerDrawData,
      int drawType,
      Player drawPlayer,
      ref Texture2D texture,
      ref Texture2D glowTexture,
      ref Vector2 drawPosition,
      ref Rectangle frame,
      ref Color drawColor,
      ref Color glowColor,
      ref float rotation,
      ref SpriteEffects spriteEffects,
      ref Vector2 drawOrigin,
      ref float drawScale,
      float shadow)
    {
      drawColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      return base.Draw(playerDrawData, drawType, drawPlayer, ref texture, ref glowTexture, ref drawPosition, ref frame, ref drawColor, ref glowColor, ref rotation, ref spriteEffects, ref drawOrigin, ref drawScale, shadow);
    }
  }
}
