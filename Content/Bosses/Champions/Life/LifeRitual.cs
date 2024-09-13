// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Life.LifeRitual
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
namespace FargowiltasSouls.Content.Bosses.Champions.Life
{
  public class LifeRitual : BaseArena
  {
    public virtual string Texture => "Terraria/Images/Projectile_467";

    public LifeRitual()
      : base((float) Math.PI / 140f, 1000f, ModContent.NPCType<LifeChampion>(), 87)
    {
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 4;
    }

    protected override void Movement(NPC npc)
    {
      if ((double) npc.ai[0] != 2.0 && (double) npc.ai[0] != 8.0)
      {
        ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center), 30f);
      }
      else
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.95f);
      }
    }

    public override void AI()
    {
      base.AI();
      this.Projectile.rotation += 0.77f;
      if (++this.Projectile.frameCounter <= 6)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame <= 3)
        return;
      this.Projectile.frame = 0;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      base.OnHitPlayer(target, info);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<PurifiedBuff>(), 300, true, false);
    }
  }
}
