// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.StyxArmorScythe
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class StyxArmorScythe : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 40;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.penetrate = -1;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.aiStyle = -1;
      this.Projectile.timeLeft = 300;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
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
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero) || Utils.HasNaNs(((Entity) this.Projectile).velocity))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      Player player = Main.player[this.Projectile.owner];
      if (!this.Projectile.friendly || this.Projectile.hostile || !((Entity) player).active || player.dead || player.ghost || !player.FargoSouls().StyxSet)
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.timeLeft = 240;
        this.Projectile.damage = (int) (100.0 * (double) ((StatModifier) ref player.GetDamage(DamageClass.Melee)).Additive);
        ((Entity) this.Projectile).Center = ((Entity) player).Center;
        if (player.ownedProjectileCounts[this.Projectile.type] > 0)
        {
          if (((Entity) player).whoAmI == Main.myPlayer)
          {
            if ((double) ++this.Projectile.localAI[0] > ((double) this.Projectile.localAI[1] == 0.0 ? 300.0 : 5.0))
            {
              this.Projectile.localAI[0] = 0.0f;
              this.Projectile.localAI[1] = 0.0f;
              for (int index = 0; index < Main.maxProjectiles; ++index)
              {
                if (((Entity) Main.projectile[index]).active && Main.projectile[index].type == this.Projectile.type && Main.projectile[index].owner == this.Projectile.owner && ((Entity) this.Projectile).whoAmI != index)
                {
                  if ((double) this.Projectile.localAI[0] == (double) Main.projectile[index].localAI[0])
                    this.Projectile.localAI[0] += 5f;
                  if ((double) this.Projectile.ai[0] == (double) Main.projectile[index].ai[0])
                  {
                    ++this.Projectile.ai[0];
                    this.Projectile.localAI[1] = 1f;
                  }
                }
              }
              this.Projectile.netUpdate = true;
            }
            if ((double) this.Projectile.ai[0] >= (double) player.ownedProjectileCounts[this.Projectile.type])
            {
              this.Projectile.ai[0] = 0.0f;
              this.Projectile.localAI[1] = 1f;
            }
          }
          ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(-150f, Utils.RotatedBy(Vector2.UnitY, 6.2831854820251465 / (double) player.ownedProjectileCounts[this.Projectile.type] * (double) this.Projectile.ai[0], new Vector2())), 0.1f);
        }
        ++this.Projectile.rotation;
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item71, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 70, 0.0f, 0.0f, 0, new Color(), 2f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 6f);
        Main.dust[index2].noGravity = true;
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(153, 300, false);
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 300, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.75f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      Color color = Color.op_Multiply((double) this.Projectile.ai[0] < 0.0 || Main.player[this.Projectile.owner].ownedProjectileCounts[this.Projectile.type] >= 12 ? Color.Yellow : Color.Purple, this.Projectile.Opacity);
      ((Color) ref color).A = (byte) 0;
      return new Color?(color);
    }
  }
}
