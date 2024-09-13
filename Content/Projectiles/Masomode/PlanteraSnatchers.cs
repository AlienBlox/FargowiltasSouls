// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.PlanteraSnatchers
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public abstract class PlanteraSnatchers : ModProjectile
  {
    private int arenaDistance = Main.rand.Next(980, 1050);
    private const int defaultDistance = 2600;

    public abstract string VineTexture { get; }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 18;
      ((Entity) this.Projectile).height = 18;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.hostile = true;
      this.Projectile.extraUpdates = 0;
      this.Projectile.timeLeft = 216000;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], 262);
      if (npc == null || !npc.HasValidTarget)
      {
        this.Projectile.Kill();
      }
      else
      {
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.ai[1]), 2600f));
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).DirectionFrom(vector2_1)) + 3.14159274f;
        this.Projectile.localAI[0] = vector2_1.X;
        this.Projectile.localAI[1] = vector2_1.Y;
        if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        {
          this.Projectile.frame = 0;
        }
        else
        {
          Projectile projectile1 = this.Projectile;
          ((Entity) projectile1).position = Vector2.op_Subtraction(((Entity) projectile1).position, Vector2.op_Division(Vector2.op_Multiply(((Entity) npc).velocity, 2f), 3f));
          int closest = (int) Player.FindClosest(((Entity) npc).Center, 1, 1);
          if (!closest.IsWithinBounds((int) byte.MaxValue))
            return;
          Player player = Main.player[closest];
          if (player == null || !((Entity) player).active)
            return;
          float num1 = Math.Min(((Entity) npc).Distance(((Entity) player).Center), (float) (2600 - this.arenaDistance));
          Vector2 vector2_2 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[npc.target]).Center), num1));
          this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, vector2_1));
          Vector2 center = ((Entity) this.Projectile).Center;
          Vector2 vector2_3 = Vector2.op_Subtraction(vector2_2, center);
          double num2 = (double) ((Vector2) ref vector2_3).Length();
          vector2_3 = Vector2.op_Division(vector2_3, 8f);
          ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 9f), vector2_3), 10f), 0.2f);
          Projectile projectile2 = this.Projectile;
          ((Entity) projectile2).velocity = Vector2.op_Multiply(((Entity) projectile2).velocity, MathHelper.Lerp(0.7f, 1f, MathHelper.Clamp(((Entity) this.Projectile).Distance(((Entity) player).Center), 0.0f, (float) (2600 - this.arenaDistance)) / (float) (2600 - this.arenaDistance)));
          if (++this.Projectile.frameCounter <= 3 * (this.Projectile.extraUpdates + 1))
            return;
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame < Main.projFrames[this.Projectile.type])
            return;
          this.Projectile.frame = 0;
        }
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<IvyVenomBuff>(), 240, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      if ((double) this.Projectile.localAI[0] != 0.0 && (double) this.Projectile.localAI[1] != 0.0)
      {
        Texture2D texture2D = ModContent.Request<Texture2D>(this.VineTexture, (AssetRequestMode) 1).Value;
        Vector2 vector2_1 = ((Entity) this.Projectile).Center;
        Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitX, this.Projectile.localAI[0]), Vector2.op_Multiply(Vector2.UnitY, this.Projectile.localAI[1]));
        Rectangle? nullable = new Rectangle?();
        Vector2 vector2_3 = Vector2.op_Addition(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) texture2D.Width), 0.5f), Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, (float) texture2D.Height), 0.5f));
        float height = (float) texture2D.Height;
        Vector2 vector2_4 = Vector2.op_Subtraction(vector2_2, vector2_1);
        float num = (float) Math.Atan2((double) vector2_4.Y, (double) vector2_4.X) - 1.57f;
        bool flag = true;
        if (float.IsNaN(vector2_1.X) && float.IsNaN(vector2_1.Y))
          flag = false;
        if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
          flag = false;
        while (flag)
        {
          if ((double) ((Vector2) ref vector2_4).Length() < (double) height + 1.0)
          {
            flag = false;
          }
          else
          {
            Vector2 vector2_5 = vector2_4;
            ((Vector2) ref vector2_5).Normalize();
            vector2_1 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(vector2_5, height));
            vector2_4 = Vector2.op_Subtraction(vector2_2, vector2_1);
            Color alpha = this.Projectile.GetAlpha(Lighting.GetColor((int) vector2_1.X / 16, (int) ((double) vector2_1.Y / 16.0)));
            Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(vector2_1, Main.screenPosition), nullable, alpha, num, vector2_3, 1f, (SpriteEffects) 0, 0.0f);
          }
        }
      }
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha1 = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha1, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
