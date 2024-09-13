// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.StyxArmorScythe2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class StyxArmorScythe2 : StyxArmorScythe
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/StyxArmorScythe";

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.penetrate = 1;
      this.Projectile.usesLocalNPCImmunity = false;
      this.Projectile.localNPCHitCooldown = 0;
      this.Projectile.FargoSouls().CanSplit = true;
      this.Projectile.FargoSouls().TimeFreezeImmune = false;
    }

    public override void AI()
    {
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero) || Utils.HasNaNs(((Entity) this.Projectile).velocity))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      Player player = Main.player[this.Projectile.owner];
      this.Projectile.damage = (int) ((double) (100 * player.ownedProjectileCounts[this.Projectile.type]) * (double) ((StatModifier) ref player.GetDamage(DamageClass.Magic)).Additive);
      if ((double) ++this.Projectile.ai[0] > 10.0)
      {
        this.Projectile.ai[0] = 0.0f;
        this.Projectile.ai[1] = (float) FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 2000f);
        this.Projectile.netUpdate = true;
      }
      if ((double) this.Projectile.ai[0] >= 0.0 && (double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 24.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.06f);
      }
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1]);
      if (npc != null)
      {
        double num = (double) Utils.ToRotation(Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center)) - (double) Utils.ToRotation(((Entity) this.Projectile).velocity);
        if (num > Math.PI)
          num -= 2.0 * Math.PI;
        if (num < -1.0 * Math.PI)
          num += 2.0 * Math.PI;
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, num * 0.20000000298023224, new Vector2());
      }
      else
      {
        this.Projectile.ai[1] = -1f;
        this.Projectile.netUpdate = true;
      }
      ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X < 0.0 ? -1 : 1;
      this.Projectile.rotation += (float) this.Projectile.spriteDirection * 1f;
    }

    public override void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.NPCDeath52, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 40; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 70, 0.0f, 0.0f, 0, new Color(), 3f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 12f);
        Main.dust[index2].noGravity = true;
      }
      this.Projectile.timeLeft = 0;
      this.Projectile.penetrate = -1;
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 200;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      if (timeLeft > 0)
        this.Projectile.Damage();
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index3 = 0; index3 < 20; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
        Dust dust = Main.dust[index4];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index5 = 0; index5 < 10; ++index5)
      {
        int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index6].noGravity = true;
        Dust dust1 = Main.dust[index6];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index7 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index7];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      float num = 0.5f;
      for (int index8 = 0; index8 < 4; ++index8)
      {
        int index9 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore = Main.gore[index9];
        gore.velocity = Vector2.op_Multiply(gore.velocity, num);
        ++Main.gore[index9].velocity.X;
        ++Main.gore[index9].velocity.Y;
      }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(153, 300, false);
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 300, false);
    }

    public override bool PreDraw(ref Color lightColor)
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
        Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    public override Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, 0, 0), this.Projectile.Opacity));
    }
  }
}
