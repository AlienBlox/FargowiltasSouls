// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomRitual
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.AbomBoss
{
  public class AbomRitual : BaseArena
  {
    private const float realRotation = 0.0174532924f;

    public virtual string Texture => "Terraria/Images/Projectile_274";

    public AbomRitual()
      : base((float) Math.PI / 180f, 1400f, ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>(), 87, visualCount: 64)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    protected override void Movement(NPC npc)
    {
      if ((double) npc.ai[0] < 9.0)
      {
        ((Entity) this.Projectile).velocity = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center);
        if ((double) npc.ai[0] != 8.0)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Division(((Entity) projectile).velocity, 40f);
        }
        this.rotationPerTick = (float) Math.PI / 180f;
      }
      else
      {
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        this.rotationPerTick = -0.00174532924f;
      }
    }

    public override void AI()
    {
      base.AI();
      ++this.Projectile.rotation;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      base.OnHitPlayer(target, info);
      if (WorldSavingSystem.EternityMode)
      {
        target.AddBuff(ModContent.BuffType<AbomFangBuff>(), 300, true, false);
        target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 120, true, false);
      }
      target.AddBuff(30, 600, true, false);
    }
  }
}
