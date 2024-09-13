// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.TitaniumShard
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class TitaniumShard : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_908";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 12;

    public virtual void SetDefaults() => this.Projectile.CloneDefaults(908);

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      player.FargoSouls();
      if (this.Projectile.owner == Main.myPlayer && Main.LocalPlayer.HasEffect<TitaniumEffect>())
        this.Projectile.Kill();
      if (this.Projectile.frameCounter == 0)
      {
        this.Projectile.frameCounter = 1;
        this.Projectile.frame = Main.rand.Next(12);
        this.Projectile.rotation = Utils.NextFloat(Main.rand) * 6.28318548f;
      }
      this.Projectile.rotation += (float) Math.PI / 200f;
      int index;
      int totalIndexesInGroup;
      this.AI_GetMyGroupIndexAndFillBlackList((List<int>) null, out index, out totalIndexesInGroup);
      double num1 = ((double) index / (double) totalIndexesInGroup + (double) player.miscCounterNormalized * 6.0) * 6.2831854820251465;
      float num2 = (float) (24.0 + (double) totalIndexesInGroup * 6.0);
      Vector2 vector2 = Vector2.op_Subtraction(((Entity) player).position, ((Entity) player).oldPosition);
      Projectile projectile = this.Projectile;
      ((Entity) projectile).Center = Vector2.op_Addition(((Entity) projectile).Center, vector2);
      Vector2 rotationVector2 = Utils.ToRotationVector2((float) num1);
      this.Projectile.localAI[0] = rotationVector2.Y;
      ((Entity) this.Projectile).Center = Vector2.Lerp(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Vector2.op_Multiply(rotationVector2, new Vector2(1f, 0.05f)), num2)), 0.3f);
    }

    private void AI_GetMyGroupIndexAndFillBlackList(
      List<int> blackListedTargets,
      out int index,
      out int totalIndexesInGroup)
    {
      index = 0;
      totalIndexesInGroup = 0;
      for (int index1 = 0; index1 < 1000; ++index1)
      {
        Projectile projectile = Main.projectile[index1];
        if (((Entity) projectile).active && projectile.owner == this.Projectile.owner && projectile.type == this.Projectile.type && (projectile.type != 759 || projectile.frame == Main.projFrames[projectile.type] - 1))
        {
          if (((Entity) this.Projectile).whoAmI > index1)
            ++index;
          ++totalIndexesInGroup;
        }
      }
    }
  }
}
