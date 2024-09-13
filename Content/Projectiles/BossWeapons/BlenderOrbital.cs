// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.BlenderOrbital
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  internal class BlenderOrbital : ModProjectile
  {
    public int Counter;
    private int soundtimer;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 46;
      ((Entity) this.Projectile).height = 46;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.scale = 1f;
      this.Projectile.extraUpdates = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 15;
      this.Projectile.aiStyle = -1;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.Projectile.localAI[1]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.Projectile.localAI[1] = reader.ReadSingle();
    }

    public virtual void AI()
    {
      int projectileByIdentity = FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, (int) this.Projectile.localAI[0], new int[1]
      {
        ModContent.ProjectileType<BlenderYoyoProj>()
      });
      if (projectileByIdentity == -1)
      {
        if (this.Projectile.owner == Main.myPlayer)
        {
          this.Projectile.Kill();
          return;
        }
      }
      else
      {
        Projectile projectile1 = Main.projectile[projectileByIdentity];
        Vector2 vector2_1 = Utils.RotatedBy(new Vector2((float) (150.0 + 150.0 * (1.0 - Math.Cos((double) this.Projectile.localAI[1]))), 0.0f), (double) this.Projectile.ai[1], new Vector2());
        Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, vector2_1);
        Vector2 vector2_3 = Vector2.op_Addition(((Entity) projectile1).Center, vector2_1);
        Projectile projectile2 = this.Projectile;
        ((Entity) projectile2).position = Vector2.op_Addition(((Entity) projectile2).position, Vector2.op_Subtraction(Vector2.Lerp(vector2_2, ((Entity) projectile1).Center, 0.05f), vector2_2));
        Projectile projectile3 = this.Projectile;
        ((Entity) projectile3).position = Vector2.op_Addition(((Entity) projectile3).position, Vector2.op_Division(Vector2.op_Subtraction(((Entity) Main.player[this.Projectile.owner]).position, ((Entity) Main.player[this.Projectile.owner]).oldPosition), 2f));
        ((Entity) this.Projectile).Center = Vector2.Lerp(((Entity) this.Projectile).Center, vector2_3, 0.05f);
        this.Projectile.ai[1] += (float) (0.15707963705062866 / (double) this.Projectile.MaxUpdates * 0.75);
        if ((double) this.Projectile.ai[1] > 3.1415927410125732)
        {
          this.Projectile.ai[1] -= 6.28318548f;
          this.Projectile.netUpdate = true;
        }
        this.Projectile.localAI[1] += (float) Math.PI / 60f / (float) this.Projectile.MaxUpdates;
        if ((double) this.Projectile.localAI[1] > 3.1415927410125732)
          this.Projectile.localAI[1] -= 6.28318548f;
        this.Projectile.damage = (int) ((double) projectile1.damage * 0.5);
        this.Projectile.knockBack = projectile1.knockBack;
      }
      ++this.Projectile.timeLeft;
      this.Projectile.rotation += 0.25f / (float) this.Projectile.MaxUpdates;
      if (this.soundtimer <= 0)
        return;
      --this.soundtimer;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (this.soundtimer != 0)
        return;
      this.soundtimer = 15;
      SoundStyle soundStyle = SoundID.Item22;
      ((SoundStyle) ref soundStyle).Volume = 1.5f;
      ((SoundStyle) ref soundStyle).Pitch = 1f;
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.5f)
      {
        Color color1 = Color.op_Multiply(Color.op_Multiply(Color.LightGreen, this.Projectile.Opacity), 0.5f);
        ((Color) ref color1).A = (byte) 100;
        Color color2 = Color.op_Multiply(color1, ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          float num3 = this.Projectile.oldRot[index2];
          Vector2 vector2_2 = Vector2.op_Addition(Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, num3, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
