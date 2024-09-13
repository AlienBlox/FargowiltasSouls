// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.MiniSaucer
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
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class MiniSaucer : ModProjectile
  {
    private int rotation;
    private int syncTimer;
    private Vector2 mousePos;
    private int rayCounter;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 25;
      ((Entity) this.Projectile).height = 25;
      this.Projectile.scale = 1f;
      this.Projectile.timeLeft *= 5;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
      this.Projectile.scale = 1.5f;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.mousePos.X);
      writer.Write(this.mousePos.Y);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      Vector2 vector2;
      vector2.X = reader.ReadSingle();
      vector2.Y = reader.ReadSingle();
      if (this.Projectile.owner == Main.myPlayer)
        return;
      this.mousePos = vector2;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (((Entity) player).active && !player.dead && player.FargoSouls().MiniSaucer)
        this.Projectile.timeLeft = 2;
      if (((Entity) player).whoAmI == Main.myPlayer)
      {
        this.mousePos = Main.MouseWorld;
        this.mousePos.Y -= 250f;
      }
      if ((double) ((Entity) this.Projectile).Distance(((Entity) Main.player[this.Projectile.owner]).Center) > 2000.0)
      {
        ((Entity) this.Projectile).Center = ((Entity) player).Center;
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI), 12f);
      }
      Vector2 vector2_1 = Vector2.op_Subtraction(this.mousePos, ((Entity) this.Projectile).Center);
      if ((double) ((Vector2) ref vector2_1).Length() > 10.0)
      {
        Vector2 vector2_2 = Vector2.op_Division(vector2_1, 18f);
        ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 23f), vector2_2), 24f);
      }
      else if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 12.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.05f);
      }
      if (((Entity) player).whoAmI == Main.myPlayer)
      {
        if (++this.syncTimer > 20)
        {
          this.syncTimer = 0;
          this.Projectile.netUpdate = true;
        }
        if (player.controlUseTile)
        {
          if (++this.rayCounter > 20)
          {
            this.rayCounter = 0;
            if (((Entity) player).whoAmI == Main.myPlayer)
              Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.UnitY, ModContent.ProjectileType<SaucerDeathray>(), this.Projectile.damage / 2, this.Projectile.knockBack / 2f, Main.myPlayer, 0.0f, (float) this.Projectile.identity, 0.0f);
          }
        }
        else
          this.rayCounter = 0;
        if (player.controlUseItem)
        {
          if ((double) ++this.Projectile.localAI[0] > 5.0)
          {
            this.Projectile.localAI[0] = 0.0f;
            if (((Entity) player).whoAmI == Main.myPlayer)
            {
              Vector2 vector2_3 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, Main.MouseWorld), 16f);
              SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
              Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2.5f)), Utils.RotatedBy(vector2_3, (Main.rand.NextDouble() - 0.5) * 0.785398185253143 / 3.0, new Vector2()), ModContent.ProjectileType<SaucerLaser>(), this.Projectile.damage / 2, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
            }
          }
          if ((double) ++this.Projectile.localAI[1] > 20.0)
          {
            this.Projectile.localAI[1] = 0.0f;
            float num1 = 500f;
            int num2 = -1;
            for (int index = 0; index < Main.maxNPCs; ++index)
            {
              NPC npc = Main.npc[index];
              if (npc.CanBeChasedBy((object) this.Projectile, false) && Collision.CanHitLine(((Entity) this.Projectile).Center, 0, 0, ((Entity) npc).Center, 0, 0))
              {
                float num3 = ((Entity) this.Projectile).Distance(((Entity) npc).Center);
                if ((double) num3 < (double) num1)
                {
                  num1 = num3;
                  num2 = index;
                }
              }
            }
            if (num2 >= 0)
            {
              Vector2 vector2_4 = Utils.RotatedBy(new Vector2(0.0f, -10f), (Main.rand.NextDouble() - 0.5) * Math.PI, new Vector2());
              Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2_4, ModContent.ProjectileType<SaucerRocket>(), this.Projectile.damage, this.Projectile.knockBack * 4f, this.Projectile.owner, (float) num2, 20f, 0.0f);
            }
          }
        }
      }
      if ((double) ((Entity) this.Projectile).velocity.X > 32.0)
        ((Entity) this.Projectile).velocity.X = 32f;
      if ((double) ((Entity) this.Projectile).velocity.X < -32.0)
        ((Entity) this.Projectile).velocity.X = -32f;
      if ((double) ((Entity) this.Projectile).velocity.Y > 32.0)
        ((Entity) this.Projectile).velocity.Y = 32f;
      if ((double) ((Entity) this.Projectile).velocity.Y < -32.0)
        ((Entity) this.Projectile).velocity.Y = -32f;
      if (Main.player[this.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<SaucerDeathray>()] > 0)
      {
        this.Projectile.rotation = 0.0f;
        this.rotation = 0;
      }
      else
      {
        this.Projectile.rotation = (float) (Math.Sin(2.0 * Math.PI * (double) this.rotation++ / 90.0) * 3.1415927410125732 / 8.0);
        if (this.rotation <= 180)
          return;
        this.rotation = 0;
      }
    }

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(144, 360, false);
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
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
