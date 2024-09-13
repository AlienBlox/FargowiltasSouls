// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantEyeHoming
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantEyeHoming : MutantEye
  {
    public override string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "Terraria/Images/Projectile_452" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantEye_April";
      }
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft = 900;
    }

    public override void AI()
    {
      float num1 = WorldSavingSystem.MasochistModeReal ? 15f : 10f;
      bool flag = false;
      NPC npc = FargoSoulsUtil.NPCExists(EModeGlobalNPC.mutantBoss, new int[1]
      {
        ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()
      });
      int[] source = new int[9]
      {
        4,
        5,
        6,
        13,
        14,
        15,
        21,
        22,
        23
      };
      if ((npc == null || !((IEnumerable<int>) source).Contains<int>((int) npc.ai[0])) && (!WorldSavingSystem.MasochistModeReal || (double) npc.ai[0] <= 10.0) && !Main.getGoodWorld)
      {
        this.Projectile.ai[1] = -600f;
        flag = true;
      }
      --this.Projectile.ai[1];
      Player player = FargoSoulsUtil.PlayerExists(npc == null ? this.Projectile.ai[0] : (float) npc.target);
      if (flag || (double) this.Projectile.ai[1] > 0.0 && player != null && (double) ((Entity) this.Projectile).Distance(((Entity) player).Center) < 240.0)
      {
        if (player != null)
        {
          double num2 = (double) Utils.ToRotation(((Entity) this.Projectile).DirectionFrom(((Entity) player).Center)) - (double) Utils.ToRotation(((Entity) this.Projectile).velocity);
          if (num2 > Math.PI)
            num2 -= 2.0 * Math.PI;
          if (num2 < -1.0 * Math.PI)
            num2 += 2.0 * Math.PI;
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, num2 * 0.05, new Vector2());
        }
        if (this.Projectile.timeLeft > 180)
          this.Projectile.timeLeft = 180;
      }
      else if ((double) this.Projectile.ai[1] < 0.0 && (double) this.Projectile.ai[1] > -600.0 && player != null)
      {
        float num3 = num1;
        if (npc != null && ((double) npc.ai[0] == 21.0 || (double) npc.ai[0] == 22.0 || (double) npc.ai[0] == 23.0))
          num3 *= 2f;
        if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < (double) num3)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.02f);
        }
        Vector2 center = ((Entity) player).Center;
        float num4 = WorldSavingSystem.MasochistModeReal ? 360f : 480f;
        if ((double) ((Entity) this.Projectile).Distance(center) > (double) num4)
        {
          double num5 = (double) Utils.ToRotation(Vector2.op_Subtraction(center, ((Entity) this.Projectile).Center)) - (double) Utils.ToRotation(((Entity) this.Projectile).velocity);
          if (num5 > Math.PI)
            num5 -= 2.0 * Math.PI;
          if (num5 < -1.0 * Math.PI)
            num5 += 2.0 * Math.PI;
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, num5 * 0.1, new Vector2());
        }
        else
          this.Projectile.ai[1] = -600f;
      }
      if ((double) this.Projectile.ai[1] < -600.0 && !Main.getGoodWorld && (double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() > (double) num1)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.96f);
      }
      base.AI();
    }
  }
}
