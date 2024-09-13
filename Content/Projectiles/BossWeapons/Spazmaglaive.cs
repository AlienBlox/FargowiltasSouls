// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.Spazmaglaive
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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
  public class Spazmaglaive : ModProjectile
  {
    private bool empowered;
    private bool hitSomething;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.empowered);
      writer.Write(this.hitSomething);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.empowered = reader.ReadBoolean();
      this.hitSomething = reader.ReadBoolean();
    }

    public virtual void SetDefaults()
    {
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.friendly = true;
      this.Projectile.light = 0.4f;
      this.Projectile.tileCollide = false;
      ((Entity) this.Projectile).width = 50;
      ((Entity) this.Projectile).height = 50;
      this.Projectile.penetrate = -1;
      this.Projectile.aiStyle = -1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.ai[0] == (double) ModContent.ProjectileType<Retiglaive>())
      {
        this.empowered = true;
        this.Projectile.ai[0] = 0.0f;
      }
      else if ((double) this.Projectile.ai[0] == (double) ModContent.ProjectileType<Spazmaglaive>())
        this.Projectile.ai[0] = 0.0f;
      if ((double) this.Projectile.ai[1] == 0.0)
        this.Projectile.ai[1] = Utils.NextFloat(Main.rand, -0.5235988f, 0.5235988f);
      this.Projectile.rotation += (float) ((Entity) this.Projectile).direction * -0.4f;
      ++this.Projectile.ai[0];
      Vector2 vector2_1 = Utils.RotatedBy(Utils.RotatedBy(new Vector2(950f * (float) Math.Sin((double) this.Projectile.ai[0] * Math.PI / 45.0), 0.0f), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()), (double) this.Projectile.ai[1] - (double) this.Projectile.ai[1] * (double) this.Projectile.ai[0] / 22.0, new Vector2());
      ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) Main.player[this.Projectile.owner]).Center, vector2_1);
      if ((double) this.Projectile.ai[0] > 45.0)
        this.Projectile.Kill();
      if (!this.empowered || (double) this.Projectile.ai[0] != 22.0 || this.Projectile.owner != Main.myPlayer)
        return;
      Vector2 vector2_2 = Utils.NextVector2CircularEdge(Main.rand, 1f, 1f);
      for (int index1 = 0; index1 < 24; ++index1)
      {
        SoundStyle soundStyle = SoundID.Item105;
        ((SoundStyle) ref soundStyle).Pitch = -0.3f;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        Vector2 vector2_3 = Utils.RotatedBy(vector2_2, (double) index1 * 6.2831854820251465 / 24.0, new Vector2());
        int index2 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Division(vector2_3, 2f), ModContent.ProjectileType<MechElectricOrbFriendly>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
        if (index2 < Main.maxProjectiles)
        {
          Main.projectile[index2].DamageType = DamageClass.Melee;
          Main.projectile[index2].timeLeft = 30;
          Main.projectile[index2].netUpdate = true;
        }
      }
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      return new bool?((double) ((Entity) this.Projectile).Distance(new Vector2((float) ((Rectangle) ref targetHitbox).Center.X, (float) ((Rectangle) ref targetHitbox).Center.Y)) < 150.0);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(39, 120, false);
      if (this.hitSomething)
        return;
      this.hitSomething = true;
      if (this.Projectile.owner == Main.myPlayer)
      {
        SoundEngine.PlaySound(ref SoundID.Item74, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        Vector2 vector2_1 = Utils.NextVector2CircularEdge(Main.rand, 1f, 1f);
        float num = 78f;
        for (int index = 0; index < 5; ++index)
        {
          Vector2 vector2_2 = Utils.RotatedBy(vector2_1, (double) index * 6.2831854820251465 / 5.0, new Vector2());
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) target).Center, vector2_2, ModContent.ProjectileType<SpazmaglaiveExplosion>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, num, (float) ((Entity) target).whoAmI, 0.0f);
        }
      }
      this.Projectile.netUpdate = true;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.33f)
      {
        int index2 = Math.Max((int) index1 - 1, 0);
        Vector2 vector2_2 = Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0));
        if ((double) index1 < 4.0)
        {
          Color color = Color.op_Multiply(alpha, ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, this.Projectile.oldRot[(int) index1], vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
        }
        if (this.empowered)
        {
          Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/BossWeapons/HentaiSpearSpinGlow", (AssetRequestMode) 1).Value;
          Color color;
          // ISSUE: explicit constructor call
          ((Color) ref color).\u002Ector(142, 250, 176);
          color = Color.Lerp(color, Color.Transparent, 0.6f);
          float num3 = this.Projectile.scale * ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
          Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(), color, 0.0f, Vector2.op_Division(Utils.Size(texture2D2), 2f), num3, (SpriteEffects) 0, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
