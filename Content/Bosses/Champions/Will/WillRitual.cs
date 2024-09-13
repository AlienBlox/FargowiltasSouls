// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Will.WillRitual
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Will
{
  public class WillRitual : BaseArena
  {
    public virtual string Texture => "FargowiltasSouls/Content/Bosses/Champions/Will/WillTyphoon";

    public WillRitual()
      : base((float) Math.PI / 140f, 1200f, ModContent.NPCType<WillChampion>(), 87, 5)
    {
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 22;
    }

    protected override void Movement(NPC npc)
    {
      if (((double) npc.ai[0] != 2.0 || (double) npc.ai[1] >= 30.0) && ((double) npc.ai[0] != -1.0 || (double) npc.ai[1] >= 10.0))
        return;
      this.Projectile.Kill();
    }

    public override void AI()
    {
      base.AI();
      this.Projectile.rotation -= MathHelper.ToRadians(1.5f);
      if (++this.Projectile.frameCounter <= 2)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame < Main.projFrames[this.Projectile.type])
        return;
      this.Projectile.frame = 0;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300, true, false);
        target.AddBuff(ModContent.BuffType<MidasBuff>(), 300, true, false);
      }
      target.AddBuff(30, 300, true, false);
    }
  }
}
