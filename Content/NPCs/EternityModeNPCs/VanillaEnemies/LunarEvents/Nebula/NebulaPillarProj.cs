// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Nebula.NebulaPillarProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Nebula
{
  public class NebulaPillarProj : ModProjectile
  {
    private int Timer;
    private const int Distance = 500;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Masomode/CelestialPillar";
    }

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 4;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 120;
      ((Entity) this.Projectile).height = 120;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 600;
      this.CooldownSlot = 1;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool? CanDamage() => new bool?(this.Projectile.alpha == 0);

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        short num = 242;
        for (int index = 0; index < 50; ++index)
        {
          Dust dust1 = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, (int) num, 0.0f, 0.0f, 0, new Color(), 1f)];
          Dust dust2 = dust1;
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 10f);
          dust1.fadeIn = 1f;
          dust1.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
          if (!Utils.NextBool(Main.rand, 3))
          {
            dust1.noGravity = true;
            Dust dust3 = dust1;
            dust3.velocity = Vector2.op_Multiply(dust3.velocity, 3f);
            dust1.scale *= 2f;
          }
        }
      }
      ref float local1 = ref this.Projectile.ai[0];
      ref float local2 = ref this.Projectile.ai[1];
      ref float local3 = ref this.Projectile.ai[2];
      if (!((Entity) Main.npc[(int) local3]).active)
      {
        this.Projectile.Kill();
      }
      else
      {
        Player player = Main.player[Main.npc[(int) local3].target];
        if (!((Entity) player).active || player.ghost)
          return;
        if (this.Timer <= 120)
        {
          if (this.Projectile.alpha > 105)
          {
            this.Projectile.rotation = Utils.ToRotation(Vector2.UnitY);
            this.Projectile.alpha -= 10;
          }
          else
            this.Projectile.alpha = 105;
          float num = (float) ((double) this.Timer / 60.0 * 1.5707963705062866 + 1.5707963705062866 * (double) local1);
          Vector2 vector2 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Utils.ToRotationVector2(num), 500f));
          ((Entity) this.Projectile).Center = vector2;
          if (this.Timer == 0)
            this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo(vector2, ((Entity) player).Center));
          this.RotateTowards(((Entity) player).Center, 6f);
        }
        else if ((double) local2 == 1.0)
          this.Projectile.Kill();
        if (this.Timer > 120 && (double) local2 != 1.0)
        {
          if (this.Timer < 160)
          {
            float speed = WorldSavingSystem.MasochistModeReal ? 1f : 0.2f;
            this.RotateTowards(((Entity) player).Center, speed);
          }
          if (this.Timer == 121)
          {
            SoundEngine.PlaySound(ref SoundID.Item4, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
            this.Projectile.alpha = 0;
          }
          if (this.Timer == 160)
          {
            SoundStyle zombie104 = SoundID.Zombie104;
            ((SoundStyle) ref zombie104).Volume = 0.5f;
            SoundEngine.PlaySound(ref zombie104, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              float num = this.Projectile.rotation + 3.14159274f;
              Vector2 vector2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Utils.ToRotationVector2(num), (float) ((Entity) this.Projectile).height), 2f));
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), vector2, Utils.ToRotationVector2(num), ModContent.ProjectileType<NebulaDeathray>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
            }
          }
          if (this.Timer > 280)
            this.Projectile.Kill();
        }
        ++this.Timer;
        this.Projectile.frame = 0;
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      short num = 242;
      for (int index = 0; index < 80; ++index)
      {
        Dust dust1 = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, (int) num, 0.0f, 0.0f, 0, new Color(), 1f)];
        Dust dust2 = dust1;
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 10f);
        dust1.fadeIn = 1f;
        dust1.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
        if (!Utils.NextBool(Main.rand, 3))
        {
          dust1.noGravity = true;
          Dust dust3 = dust1;
          dust3.velocity = Vector2.op_Multiply(dust3.velocity, 3f);
          dust1.scale *= 2f;
        }
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue - this.Projectile.alpha));
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
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 3)
      {
        Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }

    private void RotateTowards(Vector2 target, float speed)
    {
      Vector2 vector2 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, target), 3.1415927410125732, new Vector2());
      Vector2 rotationVector2 = Utils.ToRotationVector2(this.Projectile.rotation);
      float num = (float) Math.Atan2((double) vector2.Y * (double) rotationVector2.X - (double) vector2.X * (double) rotationVector2.Y, (double) rotationVector2.X * (double) vector2.X + (double) rotationVector2.Y * (double) vector2.Y);
      this.Projectile.rotation = Utils.ToRotation(Utils.RotatedBy(Utils.ToRotationVector2(this.Projectile.rotation), (double) Math.Sign(num) * (double) Math.Min(Math.Abs(num), (float) ((double) speed * 3.1415927410125732 / 180.0)), new Vector2()));
    }
  }
}
