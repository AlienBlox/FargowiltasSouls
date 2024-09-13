// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Duck.RailgunBlast
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Duck
{
  public class RailgunBlast : MutantSpecialDeathray
  {
    public RailgunBlast()
      : base(20, 1.25f)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.CooldownSlot = -1;
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.hide = true;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      double num1 = (double) ((Entity) this.Projectile).Distance(FargoSoulsUtil.ClosestPointInHitbox(targetHitbox, ((Entity) this.Projectile).Center));
      Vector2 tipOffset = this.TipOffset;
      double num2 = (double) ((Vector2) ref tipOffset).Length();
      return num1 < num2 ? new bool?(true) : base.Colliding(projHitbox, targetHitbox);
    }

    private Vector2 TipOffset
    {
      get => Vector2.op_Multiply(18f * this.Projectile.scale, ((Entity) this.Projectile).velocity);
    }

    public override void AI()
    {
      base.AI();
      this.Projectile.frameCounter += 60;
      Player player = Main.player[this.Projectile.owner];
      if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
        FargoSoulsUtil.ScreenshakeRumble(6f);
      ((Entity) this.Projectile).velocity = Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.op_UnaryNegation(Vector2.UnitY));
      float num1 = 10f;
      this.Projectile.scale = (float) Math.Cos((double) this.Projectile.localAI[0] * 1.5707963705062866 / (double) this.maxTime) * num1;
      if ((double) this.Projectile.scale > (double) num1)
        this.Projectile.scale = num1;
      if (((Entity) player).active && !player.dead && !player.ghost)
      {
        ((Entity) this.Projectile).Center = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(50f, ((Entity) this.Projectile).velocity)), this.TipOffset), Utils.NextVector2Circular(Main.rand, 5f, 5f));
        if ((double) this.Projectile.localAI[0] == 0.0)
        {
          if (!Main.dedServ)
          {
            SoundStyle soundStyle1 = new SoundStyle("FargowiltasSouls/Assets/Sounds/Railgun", (SoundType) 0);
            SoundEngine.PlaySound(ref soundStyle1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
            SoundStyle soundStyle2 = new SoundStyle("FargowiltasSouls/Assets/Sounds/Thunder", (SoundType) 0);
            SoundEngine.PlaySound(ref soundStyle2, new Vector2?(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, Math.Min((float) (Main.screenWidth / 2), 900f)))), (SoundUpdateCallback) null);
          }
          Vector2 vector2 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 50f));
          for (int index1 = 0; index1 < 40; ++index1)
          {
            int index2 = Dust.NewDust(Vector2.op_Subtraction(vector2, new Vector2(16f, 16f)), 32, 32, 31, 0.0f, 0.0f, 100, new Color(), 4f);
            Dust dust1 = Main.dust[index2];
            dust1.velocity = Vector2.op_Subtraction(dust1.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2f));
            Dust dust2 = Main.dust[index2];
            dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
            Dust dust3 = Main.dust[index2];
            dust3.velocity = Vector2.op_Addition(dust3.velocity, Vector2.op_Division(((Entity) player).velocity, 2f));
          }
          for (int index3 = 0; index3 < 50; ++index3)
          {
            int index4 = Dust.NewDust(Vector2.op_Subtraction(vector2, new Vector2(16f, 16f)), 32, 32, 6, 0.0f, 0.0f, 100, new Color(), 4f);
            Main.dust[index4].scale *= Utils.NextFloat(Main.rand, 1f, 2.5f);
            Main.dust[index4].noGravity = true;
            Dust dust4 = Main.dust[index4];
            dust4.velocity = Vector2.op_Subtraction(dust4.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2f));
            Main.dust[index4].velocity = Vector2.op_Multiply(Utils.RotatedByRandom(Main.dust[index4].velocity, (double) MathHelper.ToRadians(40f)), 6f);
            Dust dust5 = Main.dust[index4];
            dust5.velocity = Vector2.op_Multiply(dust5.velocity, Utils.NextFloat(Main.rand, 1f, 3f));
            Dust dust6 = Main.dust[index4];
            dust6.velocity = Vector2.op_Addition(dust6.velocity, Vector2.op_Division(((Entity) player).velocity, 2f));
            int index5 = Dust.NewDust(Vector2.op_Subtraction(vector2, new Vector2(16f, 16f)), 32, 32, 6, 0.0f, 0.0f, 100, new Color(), 4f);
            Dust dust7 = Main.dust[index5];
            dust7.velocity = Vector2.op_Subtraction(dust7.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2f));
            Dust dust8 = Main.dust[index5];
            dust8.velocity = Vector2.op_Multiply(dust8.velocity, 5f);
            Dust dust9 = Main.dust[index5];
            dust9.velocity = Vector2.op_Multiply(dust9.velocity, Utils.NextFloat(Main.rand, 1f, 2f));
            Dust dust10 = Main.dust[index5];
            dust10.velocity = Vector2.op_Addition(dust10.velocity, Vector2.op_Division(((Entity) player).velocity, 2f));
          }
          float num2 = 2f;
          for (int index6 = 0; index6 < 20; ++index6)
          {
            int index7 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2, Vector2.op_UnaryNegation(((Entity) this.Projectile).velocity), Main.rand.Next(61, 64), num2);
            Gore gore1 = Main.gore[index7];
            gore1.velocity = Vector2.op_Subtraction(gore1.velocity, ((Entity) this.Projectile).velocity);
            Main.gore[index7].velocity.Y += 2f;
            Gore gore2 = Main.gore[index7];
            gore2.velocity = Vector2.op_Multiply(gore2.velocity, 4f);
            Gore gore3 = Main.gore[index7];
            gore3.velocity = Vector2.op_Addition(gore3.velocity, Vector2.op_Division(((Entity) player).velocity, 2f));
          }
        }
        if ((double) ++this.Projectile.localAI[0] >= (double) this.maxTime)
        {
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], 3000f, 0.5f);
          Projectile projectile = this.Projectile;
          ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
          for (int index8 = 0; index8 < 3000; index8 += 75)
          {
            float num3 = (float) index8 + Utils.NextFloat(Main.rand, -75f, 75f);
            if ((double) num3 < 0.0)
              num3 = 0.0f;
            if ((double) num3 > 3000.0)
              num3 = 3000f;
            float num4 = this.Projectile.scale * 32f;
            int index9 = Dust.NewDust(Vector2.op_Addition(Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, num3)), Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.Projectile).velocity, 1.5707963705062866, new Vector2()), Utils.NextFloat(Main.rand, -num4, num4))), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, 0.0f, 0.0f, 0, new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 150), Utils.NextFloat(Main.rand, 2f, 4f));
            Main.dust[index9].noGravity = true;
            Dust dust11 = Main.dust[index9];
            dust11.velocity = Vector2.op_Addition(dust11.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2f));
            Dust dust12 = Main.dust[index9];
            dust12.velocity = Vector2.op_Multiply(dust12.velocity, Utils.NextFloat(Main.rand, 12f, 24f) / 10f * this.Projectile.scale);
          }
        }
      }
      else
        this.Projectile.Kill();
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 12;
      target.AddBuff(144, 600, false);
      if (this.Projectile.owner != Main.myPlayer || Main.player[this.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<LightningArc>()] >= 60)
        return;
      int num1 = 3;
      foreach (NPC npc in Main.npc)
      {
        if (npc.CanBeChasedBy((object) null, false) && ((Entity) npc).whoAmI != ((Entity) target).whoAmI && (double) ((Entity) target).Distance(((Entity) npc).Center) < 1500.0 && Collision.CanHitLine(((Entity) target).Center, 0, 0, ((Entity) npc).Center, 0, 0))
        {
          if (--num1 >= 0)
          {
            Vector2 vector2 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 10f, 20f), Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) target, ((Entity) npc).Center));
            Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) target).Center, vector2, ModContent.ProjectileType<LightningArc>(), this.Projectile.damage / 10, this.Projectile.knockBack / 10f, this.Projectile.owner, Utils.ToRotation(vector2), (float) Main.rand.Next(80), 0.0f);
          }
          else
            break;
        }
      }
      int num2 = (3 + num1) / 2;
      for (int index = -num2; index <= num2; ++index)
      {
        Vector2 vector2 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 10f, 20f), Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) MathHelper.ToRadians(30f) / (double) num2 * ((double) index + (double) Utils.NextFloat(Main.rand, -0.5f, 0.5f)), new Vector2()));
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) target).Center, vector2, ModContent.ProjectileType<LightningArc>(), this.Projectile.damage / 10, this.Projectile.knockBack / 10f, this.Projectile.owner, Utils.ToRotation(vector2), (float) Main.rand.Next(80), 0.0f);
      }
      Main.player[this.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<LightningArc>()] += 6;
    }

    public override bool PreDraw(ref Color lightColor)
    {
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      GameShaders.Armor.GetShaderFromItemId(1040).Apply((Entity) this.Projectile, new DrawData?());
      int num = base.PreDraw(ref lightColor) ? 1 : 0;
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      return num != 0;
    }
  }
}
