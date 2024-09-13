// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.DeviBoss.DeviRitual
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.DeviBoss
{
  public class DeviRitual : BaseArena
  {
    public DeviRitual()
      : base((float) Math.PI / 140f, 1000f, ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>(), 86, 3)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    protected override void Movement(NPC npc)
    {
      if ((double) npc.ai[0] > 10.0)
        return;
      this.Projectile.Kill();
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      base.OnHitPlayer(target, info);
      target.AddBuff(ModContent.BuffType<LovestruckBuff>(), 240, true, false);
    }

    public override bool PreDraw(ref Color lightColor)
    {
      Color white = Color.White;
      ((Color) ref white).A = (byte) 0;
      float num = 6.28318548f + this.Projectile.ai[0];
      Main.EntitySpriteDraw(FargosTextureRegistry.DeviBorderTexture.Value, Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Rectangle?(), Color.op_Multiply(white, 0.95f), num, Vector2.op_Multiply(Utils.Size(FargosTextureRegistry.DeviBorderTexture.Value), 0.5f), 0.67f * this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
