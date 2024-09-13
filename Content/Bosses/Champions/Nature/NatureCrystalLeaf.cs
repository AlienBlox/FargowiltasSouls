// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Nature.NatureCrystalLeaf
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Nature
{
  public class NatureCrystalLeaf : MutantCrystalLeaf
  {
    public override string Texture => "Terraria/Images/Projectile_226";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft = 300;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public override void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        for (int index1 = 0; index1 < 30; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 157, 0.0f, 0.0f, 0, new Color(), 2f);
          Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
        }
      }
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.1f, 0.4f, 0.2f);
      this.Projectile.scale = (float) (((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.20000000298023224 + 0.949999988079071);
      this.Projectile.scale *= 2f;
      int index = (int) this.Projectile.ai[0];
      Vector2 vector2 = Utils.RotatedBy(new Vector2(125f, 0.0f), (double) this.Projectile.ai[1], new Vector2());
      ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) Main.npc[index]).Center, vector2);
      this.Projectile.ai[1] += 0.09f;
      this.Projectile.rotation = this.Projectile.ai[1] + 1.57079637f;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 30; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 157, 0.0f, 0.0f, 0, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
      }
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(20, 300, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<InfestedBuff>(), 300, true, false);
    }
  }
}
