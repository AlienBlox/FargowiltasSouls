// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.PlanteraTentacle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class PlanteraTentacle : ModProjectile
  {
    private int counter;
    private const int attackTime = 150;

    public virtual string Texture => "Terraria/Images/NPC_264";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.npcFrameCount[264];
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = 2400;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 18;
      ((Entity) this.Projectile).height = 18;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.hostile = true;
      this.Projectile.extraUpdates = 0;
      this.Projectile.timeLeft = 360 * (this.Projectile.extraUpdates + 1);
      this.Projectile.hide = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
      this.Projectile.FargoSouls().GrazeCheck = (Func<Projectile, bool>) (projectile =>
      {
        float num = 0.0f;
        bool? nullable = base.CanDamage();
        bool flag = true;
        return nullable.GetValueOrDefault() == flag & nullable.HasValue && Collision.CheckAABBvLineCollision(Utils.TopLeft(((Entity) Main.LocalPlayer).Hitbox), Utils.Size(((Entity) Main.LocalPlayer).Hitbox), new Vector2(this.Projectile.localAI[0], this.Projectile.localAI[1]), ((Entity) this.Projectile).Center, (float) (22.0 * (double) this.Projectile.scale + (double) Main.LocalPlayer.FargoSouls().GrazeRadius * 2.0 + 42.0), ref num);
      });
    }

    public virtual bool? CanDamage() => new bool?(this.counter >= 150);

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], 262);
      if (npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).DirectionFrom(((Entity) npc).Center)) + 3.14159274f;
        this.Projectile.localAI[0] = ((Entity) npc).Center.X;
        this.Projectile.localAI[1] = ((Entity) npc).Center.Y;
        if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        {
          this.Projectile.frame = 0;
        }
        else
        {
          if (this.counter == 0)
            SoundEngine.PlaySound(ref SoundID.Item5, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          if (++this.counter < 150)
          {
            Projectile projectile1 = this.Projectile;
            ((Entity) projectile1).position = Vector2.op_Addition(((Entity) projectile1).position, Vector2.op_Division(((Entity) npc).velocity, 3f));
            Vector2 vector2 = Vector2.op_Subtraction(Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply((float) (150.0 + (double) this.counter * 1.5), Utils.ToRotationVector2(this.Projectile.ai[1]))), ((Entity) this.Projectile).Center);
            if ((double) ((Vector2) ref vector2).Length() > 10.0)
            {
              vector2 = Vector2.op_Division(vector2, 8f);
              ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 23f), vector2), 24f);
            }
            else if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 12.0)
            {
              Projectile projectile2 = this.Projectile;
              ((Entity) projectile2).velocity = Vector2.op_Multiply(((Entity) projectile2).velocity, 1.05f);
            }
          }
          else if (this.counter == 150)
          {
            ((Entity) this.Projectile).velocity = Vector2.op_Multiply(32f, Utils.ToRotationVector2(this.Projectile.ai[1]));
            SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          }
          else if (npc.HasPlayerTarget && (double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) > (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center))
          {
            Tile tileSafely = Framing.GetTileSafely(((Entity) this.Projectile).Center);
            if (((Tile) ref tileSafely).HasUnactuatedTile && Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] && !Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType])
            {
              for (int index1 = 0; index1 < 10; ++index1)
              {
                int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 157, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.10000000149011612), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.10000000149011612), 0, new Color(), 2.5f);
                Main.dust[index2].noGravity = true;
                Dust dust = Main.dust[index2];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
              }
              ((Entity) this.Projectile).velocity = Vector2.Zero;
            }
          }
          if (++this.Projectile.frameCounter <= 3 * (this.Projectile.extraUpdates + 1))
            return;
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame < Main.projFrames[this.Projectile.type])
            return;
          this.Projectile.frame = 0;
        }
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item5, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if ((double) this.Projectile.localAI[0] == 0.0 || (double) this.Projectile.localAI[1] == 0.0)
        return;
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector(this.Projectile.localAI[0], this.Projectile.localAI[1]);
      int num = (int) ((Entity) this.Projectile).Distance(vector2);
      for (int index = 0; index < num; index += 512)
      {
        if (!Main.dedServ)
          Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, vector2), (float) index + Utils.NextFloat(Main.rand, 512f))), Vector2.Zero, Utils.NextBool(Main.rand) ? 386 : 387, this.Projectile.scale);
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<IvyVenomBuff>(), 240, true, false);
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      return new bool?(Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), new Vector2(this.Projectile.localAI[0], this.Projectile.localAI[1]), ((Entity) this.Projectile).Center));
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      if (!this.Projectile.hide)
        return;
      behindNPCs.Add(index);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      if ((double) this.Projectile.localAI[0] != 0.0 && (double) this.Projectile.localAI[1] != 0.0)
      {
        Texture2D texture2D = TextureAssets.Chain27.Value;
        Vector2 vector2_1 = ((Entity) this.Projectile).Center;
        Vector2 vector2_2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_2).\u002Ector(this.Projectile.localAI[0], this.Projectile.localAI[1]);
        Rectangle? nullable = new Rectangle?();
        Vector2 vector2_3;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_3).\u002Ector((float) texture2D.Width * 0.5f, (float) texture2D.Height * 0.5f);
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
      Texture2D texture2D1 = TextureAssets.Npc[264].Value;
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
