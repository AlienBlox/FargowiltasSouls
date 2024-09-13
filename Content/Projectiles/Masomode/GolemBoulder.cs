// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.GolemBoulder
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
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
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class GolemBoulder : ModProjectile
  {
    public bool spawned;
    public float vel;

    public virtual string Texture => "Terraria/Images/Projectile_261";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(261);
      this.AIType = 261;
      this.Projectile.hostile = true;
      this.Projectile.friendly = false;
      this.Projectile.DamageType = DamageClass.Default;
      this.Projectile.tileCollide = false;
      this.Projectile.extraUpdates = 0;
    }

    public virtual void SendExtraAI(BinaryWriter writer) => writer.Write(this.vel);

    public virtual void ReceiveExtraAI(BinaryReader reader) => this.vel = reader.ReadSingle();

    public virtual void AI()
    {
      if (!this.spawned)
      {
        this.spawned = true;
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 20; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
        }
        for (int index3 = 0; index3 < 10; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
          Main.dust[index4].noGravity = true;
          Dust dust1 = Main.dust[index4];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
          int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust2 = Main.dust[index5];
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
        }
        float num = 0.5f;
        for (int index6 = 0; index6 < 3; ++index6)
        {
          int index7 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
          Gore gore = Main.gore[index7];
          gore.velocity = Vector2.op_Multiply(gore.velocity, num);
          ++Main.gore[index7].velocity.X;
          ++Main.gore[index7].velocity.Y;
        }
      }
      if (!this.Projectile.tileCollide)
      {
        Tile tileSafely = Framing.GetTileSafely(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.UnitY, 26f)));
        if (!((Tile) ref tileSafely).HasUnactuatedTile || !Main.tileSolid[(int) ((Tile) ref tileSafely).TileType])
          this.Projectile.tileCollide = true;
      }
      if ((double) ((Entity) this.Projectile).velocity.Y < 0.0 && (double) ((Entity) this.Projectile).velocity.X == 0.0 && (double) this.vel == 0.0)
      {
        int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
        if (closest != -1)
        {
          ((Entity) this.Projectile).velocity.X = this.vel = (double) ((Entity) this.Projectile).Center.X < (double) ((Entity) Main.player[closest]).Center.X ? 4f : -4f;
          ((Entity) this.Projectile).velocity.Y *= Utils.NextFloat(Main.rand, 1.9f, 2.1f);
        }
        else
          this.Projectile.timeLeft = 0;
      }
      if (Math.Sign(((Entity) this.Projectile).velocity.X) == Math.Sign(this.vel))
        ((Entity) this.Projectile).velocity.X = this.vel;
      else
        this.Projectile.timeLeft = 0;
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      if ((double) this.vel == 0.0)
        return true;
      if ((double) ((Entity) this.Projectile).velocity.Y != (double) oldVelocity.Y && (double) oldVelocity.Y > 1.0)
        ((Entity) this.Projectile).velocity.Y = (float) (-(double) oldVelocity.Y * 0.800000011920929);
      return false;
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      width = 26;
      height = 26;
      Tile tileSafely = Framing.GetTileSafely(((Entity) this.Projectile).Center);
      if ((!Tile.op_Inequality(tileSafely, (ArgumentException) null) ? 0 : (((Tile) ref tileSafely).WallType == (ushort) 87 ? 1 : 0)) == 0)
        fallThrough = (FargoSoulsUtil.NPCExists(NPC.golemBoss, new int[1]
        {
          245
        }) == null ? 1 : ((double) ((Entity) Main.player[Main.npc[NPC.golemBoss].target]).Bottom.Y > (double) ((Entity) this.Projectile).Bottom.Y ? 1 : 0)) != 0;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Dig, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index = 0; index < 5; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 148, 0.0f, 0.0f, 0, new Color(), 1f);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(36, 600, true, false);
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
      target.AddBuff(195, 600, true, false);
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Color color1 = Color.op_Multiply(Color.op_Multiply(Color.Orange, this.Projectile.Opacity), 0.75f);
      ((Color) ref color1).A = (byte) 20;
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.5f)
      {
        Color color2 = color1;
        float num3 = ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
        Color color3 = Color.op_Multiply(color2, num3 * num3);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          float num4 = this.Projectile.oldRot[index2];
          Vector2 vector2_2 = Vector2.op_Addition(Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color3, num4, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
