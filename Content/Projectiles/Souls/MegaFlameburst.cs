// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.MegaFlameburst
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class MegaFlameburst : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_668";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 56;
      ((Entity) this.Projectile).height = 28;
      this.Projectile.aiStyle = 1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.penetrate = 1;
      this.Projectile.timeLeft = 300;
      this.AIType = 14;
    }

    public virtual void AI()
    {
      this.Projectile.scale = 2f;
      if ((double) this.Projectile.ai[1] == 0.0)
      {
        this.Projectile.ai[1] = 1f;
        for (int index1 = 0; index1 < 12; ++index1)
        {
          Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.rotation, new Vector2()), 6f), (double) (index1 - 5) * 6.2831854820251465 / 12.0, new Vector2()), ((Entity) this.Projectile).Center);
          Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
          int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 270, 0.0f, 0.0f, 0, new Color(), 1.5f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].velocity = vector2_2;
        }
      }
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] <= 0.0)
        return;
      this.Projectile.ai[0] = 0.0f;
      NPC npc = FargoSoulsUtil.NPCExists(FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 1000f), Array.Empty<int>());
      if (!npc.Alive())
        return;
      ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), 8f), 0.04f);
    }

    public virtual void OnKill(int timeleft)
    {
      int index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Zero, 296, this.Projectile.damage, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      if (index == Main.maxProjectiles)
        return;
      Main.projectile[index].timeLeft = 15;
    }
  }
}
