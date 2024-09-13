// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.BigStinger
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class BigStinger : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(374);
      this.AIType = 14;
      this.Projectile.minion = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.timeLeft = 240;
      ((Entity) this.Projectile).width = 12;
      ((Entity) this.Projectile).height = 12;
      this.Projectile.scale *= 3f;
      this.Projectile.extraUpdates = 2;
      this.Projectile.penetrate = 1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.ai[0] == 1.0)
      {
        this.Projectile.extraUpdates = 0;
        this.Projectile.aiStyle = -1;
        this.Projectile.ignoreWater = true;
        this.Projectile.tileCollide = false;
        int num = 15;
        bool flag = false;
        ++this.Projectile.localAI[0];
        int index = (int) this.Projectile.ai[1];
        if ((double) this.Projectile.localAI[0] >= (double) (60 * num))
          flag = true;
        else if (index < 0 || index >= Main.maxNPCs)
          flag = true;
        else if (((Entity) Main.npc[index]).active && !Main.npc[index].dontTakeDamage)
        {
          ((Entity) this.Projectile).Center = Vector2.op_Subtraction(((Entity) Main.npc[index]).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2f));
          this.Projectile.gfxOffY = Main.npc[index].gfxOffY;
        }
        else
          flag = true;
        if (!flag)
          return;
        this.Projectile.Kill();
      }
      else
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.5f));
        if (!Utils.NextBool(Main.rand))
          return;
        int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 18, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index].noGravity = true;
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
      }
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      ((NPC.HitModifiers) ref modifiers).DisableCrit();
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      this.Projectile.penetrate = -1;
      this.Projectile.maxPenetrate = -1;
      for (int index1 = 0; index1 < Main.maxProjectiles; ++index1)
      {
        Projectile proj = Main.projectile[index1];
        if (((Entity) proj).active && proj.type == this.Projectile.type && proj != this.Projectile)
        {
          Rectangle hitbox = ((Entity) this.Projectile).Hitbox;
          if (((Rectangle) ref hitbox).Intersects(((Entity) proj).Hitbox))
          {
            target.SimpleStrikeNPC(this.Projectile.damage / 2, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
            target.AddBuff(70, 600, false);
            BigStinger.DustRing(proj, 24);
            proj.Kill();
            SoundStyle soundStyle = SoundID.Item27;
            ((SoundStyle) ref soundStyle).Pitch = -0.4f;
            SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
            if (this.Projectile.owner == Main.myPlayer)
            {
              Vector2 vector2_1 = Utils.RotatedByRandom(Vector2.UnitX, Math.PI / 4.0);
              for (int index2 = 0; index2 < 8; ++index2)
              {
                int index3 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) proj).Center, Vector2.op_Multiply(10f, Utils.RotatedBy(vector2_1, Math.PI / 4.0 * (double) index2, new Vector2())), ModContent.ProjectileType<Stinger>(), this.Projectile.damage / 2, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
                if (index3 != Main.maxProjectiles)
                  Main.projectile[index3].timeLeft = 22;
              }
              Vector2 screenPosition = Main.screenPosition;
              if (((Entity) Main.player[this.Projectile.owner]).direction < 0)
                screenPosition.X += (float) Main.screenWidth;
              screenPosition.Y += (float) Main.rand.Next(Main.screenHeight);
              Vector2 position = ((Entity) target).position;
              position.X += Utils.NextFloat(Main.rand, (float) ((Entity) target).width);
              position.Y += Utils.NextFloat(Main.rand, (float) ((Entity) target).height);
              Vector2 vector2_2 = Vector2.op_Multiply(22f, Vector2.Normalize(Vector2.op_Subtraction(position, screenPosition)));
              Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), screenPosition, vector2_2, ModContent.ProjectileType<BigSting22>(), this.Projectile.damage / 2, this.Projectile.knockBack * 2f, this.Projectile.owner, -1f, 0.0f, 0.0f);
              break;
            }
            break;
          }
        }
      }
      this.Projectile.ai[0] = 1f;
      this.Projectile.ai[1] = (float) ((Entity) target).whoAmI;
      this.Projectile.aiStyle = -1;
      ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.op_Subtraction(((Entity) Main.npc[((Entity) target).whoAmI]).Center, ((Entity) this.Projectile).Center), 1f);
      this.Projectile.damage = 0;
      this.Projectile.timeLeft = 300;
      this.Projectile.netUpdate = true;
    }

    public virtual void OnKill(int timeLeft)
    {
      if ((double) this.Projectile.ai[0] == 1.0)
        ((Entity) this.Projectile).velocity = Vector2.Zero;
      for (int index1 = 0; index1 < 10; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 18, ((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y, 0, new Color(), 0.9f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.25f);
        Main.dust[index2].fadeIn = 1.3f;
      }
      SoundEngine.PlaySound(ref SoundID.Item10, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }

    private static void DustRing(Projectile proj, int max)
    {
      for (int index1 = 0; index1 < max; ++index1)
      {
        Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitY, 5f), (double) (index1 - (max / 2 - 1)) * 6.2831854820251465 / (double) max, new Vector2()), ((Entity) proj).Center);
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) proj).Center);
        int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 87, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity = vector2_2;
      }
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      SpriteEffects spriteEffects1 = (SpriteEffects) 0;
      Color color1 = Lighting.GetColor((int) ((double) ((Entity) this.Projectile).position.X + (double) ((Entity) this.Projectile).width * 0.5) / 16, (int) (((double) ((Entity) this.Projectile).position.Y + (double) ((Entity) this.Projectile).height * 0.5) / 16.0));
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      int num3 = 7;
      float num4 = 0.0f;
      for (int index = 0; (double) this.Projectile.ai[0] != 1.0 && index < num3; ++index)
      {
        Color color2 = Color.op_Multiply(this.Projectile.GetAlpha(color1), (float) (num3 - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float rotation = this.Projectile.rotation;
        SpriteEffects spriteEffects2 = spriteEffects1;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, rotation + (float) ((double) this.Projectile.rotation * (double) num4 * (double) (index - 1) * -(double) Utils.ToDirectionInt(((Enum) (object) spriteEffects1).HasFlag((Enum) (object) (SpriteEffects) 1))), vector2, this.Projectile.scale * 0.8f, spriteEffects2, 0.0f);
      }
      Color alpha = this.Projectile.GetAlpha(color1);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects1, 0.0f);
      return false;
    }
  }
}
