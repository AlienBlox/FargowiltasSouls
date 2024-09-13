// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BloomLine
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Lifelight;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class BloomLine : ModProjectile
  {
    public Color color = Color.White;
    private int counter;
    private readonly int drawLayers = 1;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = 2400;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 36;
      ((Entity) this.Projectile).height = 1024;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.hostile = true;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.hide = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      behindProjectiles.Add(index);
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.counter);
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.Projectile.localAI[1]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.counter = reader.ReadInt32();
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.Projectile.localAI[1] = reader.ReadSingle();
    }

    public virtual void AI()
    {
      int num1 = 60;
      float num2 = 3f;
      switch (this.Projectile.ai[0])
      {
        case -2f:
          this.color = Color.DeepPink;
          num2 = 1f;
          this.Projectile.scale = 0.6f;
          num1 = 60;
          NPC npc1 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<LifeChallenger>());
          Player player = Main.player[npc1.target];
          if (npc1 != null && ((Entity) npc1).active && player != null && ((Entity) player).active)
          {
            this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc1, ((Entity) player).Center));
            ((Entity) this.Projectile).Center = ((Entity) npc1).Center;
            break;
          }
          break;
        case -1f:
          this.color = Color.Goldenrod;
          num2 = 1f;
          this.Projectile.scale = 0.6f;
          num1 = 60;
          this.Projectile.rotation = this.Projectile.ai[1];
          break;
        case 0.0f:
          this.color = Color.Cyan;
          num2 = 1f;
          this.Projectile.scale = 0.6f;
          num1 = 40;
          this.Projectile.rotation = this.Projectile.ai[1];
          break;
        case 1f:
          this.color = Color.DeepPink;
          num2 = 1f;
          this.Projectile.scale = 0.6f;
          num1 = 60;
          this.Projectile.rotation = this.Projectile.ai[1];
          break;
        case 2f:
          NPC npc2 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron>());
          if (npc2.Alive())
          {
            this.Projectile.rotation = npc2.rotation;
            ((Entity) this.Projectile).Center = ((Entity) npc2).Center;
          }
          this.color = Color.Gray;
          num2 = 1f;
          this.Projectile.scale = 1f;
          num1 = 70;
          break;
        case 3f:
          NPC npc3 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron>());
          if (npc3.Alive())
          {
            this.Projectile.rotation = npc3.rotation;
            ((Entity) this.Projectile).Center = ((Entity) npc3).Center;
          }
          this.color = Color.Gray;
          num2 = 1f;
          this.Projectile.scale = 1f;
          num1 = 30;
          break;
        case 4f:
          NPC npc4 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron>());
          if (npc4.Alive())
          {
            this.Projectile.rotation = npc4.rotation;
            ((Entity) this.Projectile).Center = ((Entity) npc4).Center;
          }
          this.color = Color.Gray;
          num2 = 1f;
          this.Projectile.scale = 1f;
          num1 = 29;
          break;
        case 5f:
          this.Projectile.rotation = this.Projectile.ai[1];
          this.color = Color.DeepPink;
          num2 = 1f;
          this.Projectile.scale = 1f;
          num1 = (int) this.Projectile.ai[2];
          break;
        case 6f:
          ref float local = ref this.Projectile.ai[1];
          num1 = (int) this.Projectile.ai[2];
          float num3 = (float) this.counter / (float) num1;
          float num4 = local * MathHelper.ToRadians(30f);
          float radians = MathHelper.ToRadians(150f);
          this.Projectile.rotation = Utils.ToRotation(Utils.RotatedBy(Utils.RotatedBy(Vector2.op_UnaryNegation(Vector2.UnitY), (double) num4, new Vector2()), (double) local * (double) radians * (double) num3, new Vector2()));
          this.color = Color.DeepPink;
          this.Projectile.scale = 1f;
          break;
        case 7f:
          NPC npc5 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron>());
          if (npc5.Alive())
          {
            this.Projectile.rotation = npc5.rotation;
            ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc5).Center, Vector2.op_Division(Vector2.op_Multiply(Utils.ToRotationVector2(npc5.rotation), (float) ((Entity) npc5).width), 3f));
          }
          this.color = Color.DeepPink;
          num2 = 1f;
          this.Projectile.scale = 1f;
          num1 = (int) this.Projectile.ai[2];
          break;
        default:
          Main.NewText("bloom line: you shouldnt be seeing this text, show terry or javyz", byte.MaxValue, byte.MaxValue, byte.MaxValue);
          break;
      }
      if (++this.counter > num1)
      {
        this.Projectile.Kill();
      }
      else
      {
        if ((double) num2 >= 0.0)
        {
          this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * Math.Sin(Math.PI / (double) num1 * (double) this.counter) * (double) num2);
          if (this.Projectile.alpha < 0)
            this.Projectile.alpha = 0;
        }
        ((Color) ref this.color).A = (byte) 0;
      }
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (((Rectangle) ref projHitbox).Intersects(targetHitbox))
        return new bool?(true);
      float num = 0.0f;
      return Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), ((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 3000f)), 16f * this.Projectile.scale, ref num) ? new bool?(true) : new bool?(false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(this.color, this.Projectile.Opacity), (float) Main.mouseTextColor / (float) byte.MaxValue), 0.9f));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      Rectangle rectangle1;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle1).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle1), 2f);
      Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 3000f), 2f);
      Vector2 vector2_3 = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenLastPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), vector2_2);
      Rectangle rectangle2;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle2).\u002Ector((int) vector2_3.X, (int) vector2_3.Y, 3000, (int) ((double) rectangle1.Height * (double) this.Projectile.scale));
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index = 0; index < this.drawLayers; ++index)
        Main.EntitySpriteDraw(new DrawData(texture2D, rectangle2, new Rectangle?(rectangle1), alpha, this.Projectile.rotation, vector2_1, (SpriteEffects) 0, 0.0f));
      return false;
    }
  }
}
