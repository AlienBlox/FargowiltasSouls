// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Timber.TimberSquirrel
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Timber
{
  internal class TimberSquirrel : ModProjectile
  {
    public int Counter;
    private const int baseTimeleft = 120;
    private NPC npc;
    private bool spawned;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Items/Weapons/Misc/TophatSquirrelWeapon";
    }

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 15;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 19;
      ((Entity) this.Projectile).height = 19;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 120;
      this.Projectile.penetrate = -1;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual bool? CanDamage() => new bool?(false);

    private bool EvilSqurrl => (double) this.Projectile.ai[0] != 0.0;

    public virtual void OnSpawn(IEntitySource source)
    {
      this.npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1]);
    }

    public virtual void AI()
    {
      if (!this.spawned)
      {
        this.spawned = true;
        for (int index1 = 0; index1 < 50; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 7, 0.0f, 0.0f, 0, new Color(), 3f);
          Main.dust[index2].noGravity = true;
          Dust dust1 = Main.dust[index2];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 3f);
          Dust dust2 = Main.dust[index2];
          dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, Utils.NextFloat(Main.rand, 9f)));
        }
      }
      this.Projectile.spriteDirection = Math.Sign(((Entity) this.Projectile).velocity.X);
      this.Projectile.rotation += 0.2f * (float) this.Projectile.spriteDirection;
      if (++this.Counter >= 45)
        this.Projectile.scale += 0.1f;
      if (!this.EvilSqurrl)
        return;
      ++this.Projectile.timeLeft;
      FargoSoulsUtil.AuraDust((Entity) this.Projectile, 1000f, 156, Color.White, true, (float) (1.0 - (double) this.Projectile.localAI[1] / 20.0));
      if (this.Counter == 120 && FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(Entity.InheritSource(this.npc != null ? (Entity) (object) this.npc : (Entity) (object) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<TimberLightningOrb>(), this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      if (this.Counter <= 120)
        return;
      this.Projectile.hide = true;
      Projectile projectile = this.Projectile;
      ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
      ((Entity) this.Projectile).velocity = Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX);
      this.Projectile.rotation += 0.1349578f * (float) Math.Sign(((Entity) this.Projectile).velocity.X);
      if ((double) --this.Projectile.localAI[0] >= 0.0)
        return;
      this.Projectile.localAI[0] = 15f;
      SoundEngine.PlaySound(ref SoundID.Item157, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      int num1 = (int) this.Projectile.localAI[1];
      if (num1 >= 10)
        num1 = 20 - num1;
      int num2 = 3 * num1;
      for (int index = 0; index < num2; ++index)
      {
        float num3 = this.npc != null ? (float) ((Entity) this.npc).whoAmI : -1f;
        float num4 = 6.28318548f / (float) num2;
        Vector2 vector2_1 = Vector2.op_Multiply(4f, Utils.ToRotationVector2(this.Projectile.rotation + num4 * (float) index));
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(Entity.InheritSource(this.npc != null ? (Entity) (object) this.npc : (Entity) (object) this.Projectile), ((Entity) this.Projectile).Center, vector2_1, ModContent.ProjectileType<TimberLaser>(), this.Projectile.damage, 0.0f, Main.myPlayer, num3, 0.0f, 0.0f);
        if (this.npc != null)
        {
          float num5 = this.Projectile.rotation + num4 * ((float) index + 0.5f);
          Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(1000f, Utils.ToRotationVector2(num5)));
          Vector2 vector2_3 = Utils.SafeNormalize(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(((Entity) Main.player[this.npc.target]).Center, Vector2.op_Multiply(((Entity) Main.player[this.npc.target]).velocity, Utils.NextFloat(Main.rand, 30f))), Utils.NextVector2Circular(Main.rand, 128f, 128f)), vector2_2), Vector2.UnitY);
          float num6 = MathHelper.WrapAngle(Utils.ToRotation(vector2_3) - num5);
          if ((double) Math.Abs(num6) > 1.5707963705062866)
            vector2_3 = Utils.ToRotationVector2(num5 + 1.57079637f * (float) Math.Sign(num6));
          Vector2 vector2_4 = Vector2.op_Multiply(vector2_3, 12f);
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.npc), vector2_2, vector2_4, ModContent.ProjectileType<TimberLaser>(), this.Projectile.damage, 0.0f, Main.myPlayer, num3, 0.0f, 0.0f);
        }
      }
      if ((double) ++this.Projectile.localAI[1] < 20.0)
        return;
      this.Projectile.Kill();
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      if (this.EvilSqurrl)
      {
        for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 3)
        {
          Color color;
          // ISSUE: explicit constructor call
          ((Color) ref color).\u002Ector(93, (int) byte.MaxValue, 241, 0);
          color = Color.op_Multiply(color, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          Vector2 oldPo = this.Projectile.oldPos[index];
          float num3 = this.Projectile.oldRot[index];
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale * 1.1f, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
