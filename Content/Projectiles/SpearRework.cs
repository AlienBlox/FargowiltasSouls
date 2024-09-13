// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.SpearRework
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class SpearRework : GlobalProjectile
  {
    public int SwingDirection = 1;
    public static List<int> ReworkedSpears;

    public virtual bool InstancePerEntity => true;

    public virtual void PostAI(Projectile projectile)
    {
      if (!WorldSavingSystem.EternityMode || !SpearRework.ReworkedSpears.Contains(projectile.type))
        return;
      this.ReworkedSpearSwing(projectile);
    }

    public void ReworkedSpearSwing(Projectile projectile)
    {
      Vector2 vector2 = Utils.Size(Asset<Texture2D>.op_Explicit(TextureAssets.Projectile[projectile.type]));
      float num1 = ((Vector2) ref vector2).Length() * projectile.scale;
      Vector2 size = ((Entity) projectile).Size;
      float num2 = ((Vector2) ref size).Length();
      Player player = Main.player[projectile.owner];
      int num3 = (int) ((double) player.itemAnimationMax / 1.5);
      int num4 = player.itemAnimationMax / 5;
      player.heldProj = ((Entity) projectile).whoAmI;
      projectile.spriteDirection = ((Entity) player).direction;
      if ((double) projectile.ai[1] == 0.0)
        this.SwingDirection = Utils.NextBool(Main.rand, 2) ? 1 : -1;
      float num5 = 13f;
      projectile.usesLocalNPCImmunity = true;
      projectile.localNPCHitCooldown = player.itemAnimationMax;
      if (projectile.timeLeft > player.itemAnimationMax)
        projectile.timeLeft = player.itemAnimationMax;
      if ((double) projectile.ai[1] <= (double) (num3 / 2))
      {
        projectile.ai[0] = projectile.ai[1] / (float) (num3 / 2);
        ((Entity) projectile).velocity = Utils.RotatedBy(((Entity) projectile).velocity, (double) (this.SwingDirection * projectile.spriteDirection) * (-1.0 * Math.PI) / ((double) num5 * (double) player.itemAnimationMax), new Vector2());
      }
      else if ((double) projectile.ai[1] <= (double) (num3 / 2 + num4))
      {
        projectile.ai[0] = 1f;
        ((Entity) projectile).velocity = Utils.RotatedBy(((Entity) projectile).velocity, (double) (this.SwingDirection * projectile.spriteDirection) * (1.5 * (double) num3 / (double) num4) * Math.PI / ((double) num5 * (double) player.itemAnimationMax), new Vector2());
      }
      else
      {
        projectile.ai[0] = ((float) (num3 + num4) - projectile.ai[1]) / (float) (num3 / 2);
        ((Entity) projectile).velocity = Utils.RotatedBy(((Entity) projectile).velocity, (double) (this.SwingDirection * projectile.spriteDirection) * (-1.0 * Math.PI) / ((double) num5 * (double) player.itemAnimationMax), new Vector2());
      }
      ++projectile.ai[1];
      ((Entity) projectile).Center = Vector2.op_Addition(player.MountedCenter, Vector2.SmoothStep(Vector2.op_Multiply(Vector2.Normalize(((Entity) projectile).velocity), num2), Vector2.op_Multiply(Vector2.Normalize(((Entity) projectile).velocity), num1), projectile.ai[0]));
      Projectile projectile1 = projectile;
      ((Entity) projectile1).position = Vector2.op_Subtraction(((Entity) projectile1).position, ((Entity) projectile).velocity);
      projectile.rotation = Utils.ToRotation(((Entity) projectile).velocity);
      if (projectile.spriteDirection == -1)
        projectile.rotation += MathHelper.ToRadians(45f);
      else
        projectile.rotation += MathHelper.ToRadians(135f);
      if (projectile.type != 215 || (double) projectile.ai[1] != (double) (num3 / 2) && ((double) projectile.ai[1] != (double) (num3 / 2 + num4) || !FargoSoulsUtil.HostCheck))
        return;
      Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, Vector2.op_Multiply(Vector2.Normalize(((Entity) projectile).velocity), 5f), 221, projectile.damage / 2, projectile.knockBack / 2f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    static SpearRework()
    {
      List<int> intList = new List<int>();
      CollectionsMarshal.SetCount<int>(intList, 11);
      Span<int> span = CollectionsMarshal.AsSpan<int>(intList);
      int num1 = 0;
      span[num1] = 49;
      int num2 = num1 + 1;
      span[num2] = 66;
      int num3 = num2 + 1;
      span[num3] = 97;
      int num4 = num3 + 1;
      span[num4] = 64;
      int num5 = num4 + 1;
      span[num5] = 215;
      int num6 = num5 + 1;
      span[num6] = 212;
      int num7 = num6 + 1;
      span[num7] = 218;
      int num8 = num7 + 1;
      span[num8] = 47;
      int num9 = num8 + 1;
      span[num9] = 367;
      int num10 = num9 + 1;
      span[num10] = 368;
      int num11 = num10 + 1;
      span[num11] = 222;
      int num12 = num11 + 1;
      SpearRework.ReworkedSpears = intList;
    }
  }
}
