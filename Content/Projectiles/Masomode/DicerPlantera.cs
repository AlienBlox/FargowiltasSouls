// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.DicerPlantera
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Systems;
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
  public class DicerPlantera : ModProjectile
  {
    private const float range = 200f;

    public virtual string Texture => FargoSoulsUtil.VanillaTextureProjectile(277);

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 1200;
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

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      bool flag = SoulConfig.Instance.BossRecolors && WorldSavingSystem.EternityMode;
      if (flag)
        Lighting.AddLight(((Entity) this.Projectile).Center, 0.09803922f, 0.184313729f, 0.2509804f);
      else
        Lighting.AddLight(((Entity) this.Projectile).Center, 0.4f, 1.2f, 0.4f);
      if ((double) this.Projectile.localAI[0] == 0.0)
        this.Projectile.localAI[0] = Utils.NextBool(Main.rand) ? 1f : -1f;
      if ((double) this.Projectile.localAI[1] >= 0.0)
      {
        if ((double) ++this.Projectile.localAI[1] <= 25.0)
          return;
        this.Projectile.localAI[1] = -1f;
        if ((double) this.Projectile.ai[1] > 0.0)
        {
          SoundEngine.PlaySound(ref SoundID.Grass, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, this.Projectile.type, this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, this.Projectile.ai[0], this.Projectile.ai[1] - 1f, 0.0f);
            if ((double) this.Projectile.ai[0] == 1.0)
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) MathHelper.ToRadians(120f), new Vector2()), this.Projectile.type, this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, this.Projectile.ai[1] - 1f, 0.0f);
          }
        }
        for (int index1 = 0; index1 < 20; ++index1)
        {
          int num = flag ? (Utils.NextBool(Main.rand) ? 41 : 307) : (Utils.NextBool(Main.rand) ? 107 : 157);
          Vector2 vector2 = Utils.NextVector2Circular(Main.rand, 4f, 4f);
          int index2 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(((Entity) this.Projectile).Size, this.Projectile.scale), 2f)), (int) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale), (int) ((double) ((Entity) this.Projectile).height * (double) this.Projectile.scale), num, vector2.X, vector2.Y, 0, new Color(), 2f);
          Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
        }
        this.Projectile.localAI[0] = (float) (50.0 * ((double) this.Projectile.ai[1] % 3.0));
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        this.Projectile.netUpdate = true;
      }
      else
      {
        this.Projectile.tileCollide = true;
        --this.Projectile.localAI[0];
        if ((double) this.Projectile.localAI[0] >= -30.0)
          this.Projectile.scale = 1f;
        if ((double) this.Projectile.localAI[0] < -30.0 && (double) this.Projectile.localAI[0] > -120.0)
        {
          this.Projectile.scale += 0.06f;
          this.Projectile.rotation += 0.3f * this.Projectile.localAI[0];
        }
        else if ((double) this.Projectile.localAI[0] == -120.0)
        {
          for (int index3 = 0; index3 < 20; ++index3)
          {
            int num = flag ? (Utils.NextBool(Main.rand) ? 41 : 307) : (Utils.NextBool(Main.rand) ? 107 : 157);
            Vector2 vector2 = Utils.NextVector2Circular(Main.rand, 4f, 4f);
            int index4 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(((Entity) this.Projectile).Size, this.Projectile.scale), 2f)), (int) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale), (int) ((double) ((Entity) this.Projectile).height * (double) this.Projectile.scale), num, vector2.X, vector2.Y, 0, new Color(), 2f);
            Main.dust[index4].noGravity = true;
            Dust dust = Main.dust[index4];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
          }
        }
        else
        {
          if ((double) this.Projectile.localAI[0] >= -150.0)
            return;
          this.Projectile.localAI[0] = 0.0f;
          this.Projectile.netUpdate = true;
          SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          if (!FargoSoulsUtil.HostCheck)
            return;
          if ((NPC.plantBoss <= -1 || NPC.plantBoss >= Main.maxNPCs || !((Entity) Main.npc[NPC.plantBoss]).active ? 0 : (Main.npc[NPC.plantBoss].type == 262 ? 1 : 0)) == 0 || !Collision.CanHitLine(((Entity) this.Projectile).Center, 0, 0, Vector2.op_Addition(((Entity) Main.npc[NPC.plantBoss]).Center, Vector2.op_Division(Vector2.op_Subtraction(((Entity) this.Projectile).Center, ((Entity) Main.npc[NPC.plantBoss]).Center), 2f)), 0, 0))
          {
            this.Projectile.Kill();
          }
          else
          {
            float num1 = Utils.NextFloat(Main.rand, 6.28318548f);
            for (int index5 = 0; index5 < 16; ++index5)
            {
              int num2 = WorldSavingSystem.MasochistModeReal ? 276 : 275;
              int index6 = Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(12.5f, Utils.RotatedBy(Vector2.UnitX, Math.PI / 8.0 * (double) index5 + (double) num1, new Vector2())), num2, this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
              if (index6 != Main.maxProjectiles)
                Main.projectile[index6].timeLeft = 16;
            }
            if ((double) this.Projectile.localAI[1]-- >= -3.0)
              return;
            this.Projectile.Kill();
          }
        }
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.NPCDeath1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[277].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Color alpha = this.Projectile.GetAlpha(lightColor);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
