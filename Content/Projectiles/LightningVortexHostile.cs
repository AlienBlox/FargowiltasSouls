// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.LightningVortexHostile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class LightningVortexHostile : ModProjectile
  {
    private Color DrawColor = Color.Cyan;
    private bool useVanillaLightning;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.friendly = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = (int) byte.MaxValue;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void OnSpawn(IEntitySource source)
    {
      base.OnSpawn(source);
      if (!(source is EntitySource_Parent entitySourceParent) || !(entitySourceParent.Entity is NPC entity) || entity.type != 398)
        return;
      this.useVanillaLightning = true;
    }

    public virtual void AI()
    {
      bool flag = SoulConfig.Instance.BossRecolors && WorldSavingSystem.EternityMode;
      ModNPC modNpc;
      if (NPC.AnyNPCs(134) & flag || FargowiltasSouls.FargowiltasSouls.Instance.TryFind<ModNPC>("MutantBoss", ref modNpc) && NPC.AnyNPCs(modNpc.Type))
        this.DrawColor = new Color(231, 174, 254);
      int num1 = Color.op_Equality(this.DrawColor, new Color(231, 174, 254)) ? 100 : 0;
      Player player = FargoSoulsUtil.PlayerExists(this.Projectile.localAI[1]);
      if (player == null && (double) this.Projectile.ai[0] == 0.0)
        this.TargetEnemies();
      if ((double) this.Projectile.localAI[0] < 90.0 && (double) this.Projectile.ai[0] != 0.0)
      {
        Vector2 rotationVector2 = Utils.ToRotationVector2(this.Projectile.ai[1]);
        ((Vector2) ref rotationVector2).Normalize();
        Vector2 vector2_1 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(rotationVector2, 1.5707963705062866, new Vector2()), (float) Utils.ToDirectionInt(Utils.NextBool(Main.rand))), (float) Main.rand.Next(10, 21));
        Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Subtraction(Vector2.op_Multiply(rotationVector2, (float) Main.rand.Next(-80, 81)), vector2_1), 10f);
        int num2 = 229;
        Dust dust = Main.dust[Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, num2, 0.0f, 0.0f, 0, new Color(), 1f)];
        dust.noGravity = true;
        dust.position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(rotationVector2, ((Vector2) ref vector2_1).Length()));
        dust.velocity = Vector2.op_Multiply(Vector2.op_Multiply(rotationVector2, ((Vector2) ref vector2_2).Length()), 3f);
        dust.scale = 1.5f;
      }
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] <= 50.0)
      {
        if (Utils.NextBool(Main.rand, 4))
        {
          Vector2 vector2 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, this.DrawColor, 1f)];
          dust.noGravity = true;
          dust.shader = GameShaders.Armor.GetSecondaryShader(num1, Main.LocalPlayer);
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, (float) Main.rand.Next(10, 21)));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, 1.5707963705062866, new Vector2()), 4f);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.fadeIn = 0.5f;
        }
        if (!Utils.NextBool(Main.rand, 4))
          return;
        Vector2 vector2_3 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
        Dust dust1 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_3, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, this.DrawColor, 1f)];
        dust1.noGravity = true;
        dust1.shader = GameShaders.Armor.GetSecondaryShader(num1, Main.LocalPlayer);
        dust1.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_3, 30f));
        dust1.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_3, -1.5707963705062866, new Vector2()), 2f);
        dust1.scale = 0.5f + Utils.NextFloat(Main.rand);
        dust1.fadeIn = 0.5f;
      }
      else if ((double) this.Projectile.localAI[0] <= 90.0)
      {
        this.Projectile.scale = (float) (((double) this.Projectile.localAI[0] - 50.0) / 40.0);
        this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale);
        this.Projectile.rotation -= 0.1570796f;
        if (Utils.NextBool(Main.rand))
        {
          Vector2 vector2 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, this.DrawColor, 1f)];
          dust.noGravity = true;
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, (float) Main.rand.Next(10, 21)));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, 1.5707963705062866, new Vector2()), 6f);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.shader = GameShaders.Armor.GetSecondaryShader(num1, Main.LocalPlayer);
          dust.fadeIn = 0.5f;
          dust.customData = (object) ((Entity) this.Projectile).Center;
        }
        if (Utils.NextBool(Main.rand))
        {
          Vector2 vector2 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, this.DrawColor, 1f)];
          dust.noGravity = true;
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, -1.5707963705062866, new Vector2()), 3f);
          dust.shader = GameShaders.Armor.GetSecondaryShader(num1, Main.LocalPlayer);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.fadeIn = 0.5f;
          dust.customData = (object) ((Entity) this.Projectile).Center;
        }
        if ((double) this.Projectile.localAI[0] != 90.0 || !FargoSoulsUtil.HostCheck)
          return;
        Vector2 vector2_4 = Vector2.op_Multiply(24f, player == null || (double) this.Projectile.ai[0] != 0.0 ? Utils.ToRotationVector2(this.Projectile.ai[1]) : Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center));
        float num3 = Utils.NextBool(Main.rand) ? 1f : -1f;
        int num4 = this.useVanillaLightning ? 580 : ModContent.ProjectileType<HostileLightning>();
        int index = Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2_4, num4, this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, Utils.ToRotation(vector2_4), num3 * 0.75f, 0.0f);
        if (index == Main.maxProjectiles || this.useVanillaLightning)
          return;
        Main.projectile[index].localAI[1] = (float) num1;
      }
      else if ((double) this.Projectile.localAI[0] <= 120.0)
      {
        this.Projectile.scale = 1f;
        this.Projectile.alpha = 0;
        this.Projectile.rotation -= (float) Math.PI / 60f;
        if (Utils.NextBool(Main.rand))
        {
          Vector2 vector2 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, this.DrawColor, 1f)];
          dust.noGravity = true;
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, (float) Main.rand.Next(10, 21)));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, 1.5707963705062866, new Vector2()), 6f);
          dust.shader = GameShaders.Armor.GetSecondaryShader(num1, Main.LocalPlayer);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.fadeIn = 0.5f;
          dust.customData = (object) ((Entity) this.Projectile).Center;
        }
        else
        {
          Vector2 vector2 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, this.DrawColor, 1f)];
          dust.noGravity = true;
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, -1.5707963705062866, new Vector2()), 3f);
          dust.shader = GameShaders.Armor.GetSecondaryShader(num1, Main.LocalPlayer);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.fadeIn = 0.5f;
          dust.customData = (object) ((Entity) this.Projectile).Center;
        }
      }
      else
      {
        this.Projectile.scale = (float) (1.0 - ((double) this.Projectile.localAI[0] - 120.0) / 60.0);
        this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale);
        this.Projectile.rotation -= (float) Math.PI / 30f;
        if (this.Projectile.alpha >= (int) byte.MaxValue)
          this.Projectile.Kill();
        for (int index = 0; index < 2; ++index)
        {
          switch (Main.rand.Next(3))
          {
            case 0:
              Vector2 vector2_5 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
              Dust dust2 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_5, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, this.DrawColor, 1f)];
              dust2.noGravity = true;
              dust2.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_5, (float) Main.rand.Next(10, 21)));
              dust2.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_5, 1.5707963705062866, new Vector2()), 6f);
              dust2.shader = GameShaders.Armor.GetSecondaryShader(num1, Main.LocalPlayer);
              dust2.scale = 0.5f + Utils.NextFloat(Main.rand);
              dust2.fadeIn = 0.5f;
              dust2.customData = (object) ((Entity) this.Projectile).Center;
              break;
            case 1:
              Vector2 vector2_6 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
              Dust dust3 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_6, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, this.DrawColor, 1f)];
              dust3.noGravity = true;
              dust3.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_6, 30f));
              dust3.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_6, -1.5707963705062866, new Vector2()), 3f);
              dust3.shader = GameShaders.Armor.GetSecondaryShader(num1, Main.LocalPlayer);
              dust3.scale = 0.5f + Utils.NextFloat(Main.rand);
              dust3.fadeIn = 0.5f;
              dust3.customData = (object) ((Entity) this.Projectile).Center;
              break;
          }
        }
      }
    }

    private void TargetEnemies()
    {
      float num1 = 1000f;
      int num2 = -1;
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        Player player = Main.player[index];
        if (((Entity) player).active && Collision.CanHitLine(((Entity) this.Projectile).Center, 0, 0, ((Entity) player).Center, 0, 0))
        {
          float num3 = ((Entity) this.Projectile).Distance(((Entity) player).Center);
          if ((double) num3 < (double) num1)
          {
            num1 = num3;
            num2 = index;
          }
        }
      }
      this.Projectile.localAI[1] = (float) num2;
      this.Projectile.netUpdate = true;
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(Color.Black, this.Projectile.Opacity), -this.Projectile.rotation, vector2, this.Projectile.scale * 1.25f, (SpriteEffects) 1, 0.0f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(this.DrawColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
