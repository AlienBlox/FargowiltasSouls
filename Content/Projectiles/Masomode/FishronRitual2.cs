// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.FishronRitual2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class FishronRitual2 : BaseArena
  {
    public virtual string Texture => "Terraria/Images/Projectile_409";

    public FishronRitual2()
      : base((float) Math.PI / 140f, 1600f, 370)
    {
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 3;
    }

    protected override void Movement(NPC npc)
    {
      ((Entity) this.Projectile).velocity = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center);
      Projectile projectile = this.Projectile;
      ((Entity) projectile).velocity = Vector2.op_Division(((Entity) projectile).velocity, 20f);
    }

    public override void AI()
    {
      base.AI();
      this.Projectile.rotation += 0.2f;
      ++this.Projectile.frame;
      if (this.Projectile.frame <= 2)
        return;
      this.Projectile.frame = 0;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      base.OnHitPlayer(target, info);
      target.FargoSouls().MaxLifeReduction += 50;
      target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 1200, true, false);
    }

    public override Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color(150, 50 + (int) (100.0 * (double) Main.DiscoG / (double) byte.MaxValue), (int) byte.MaxValue, 150), (double) this.targetPlayer == (double) Main.myPlayer ? 1f : 0.2f));
    }
  }
}
