// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Cosmos.CosmosSphere
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
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
namespace FargowiltasSouls.Content.Bosses.Champions.Cosmos
{
  public class CosmosSphere : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_454";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 2;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 46;
      ((Entity) this.Projectile).height = 46;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.extraUpdates = 1;
      this.Projectile.timeLeft = 480;
      this.CooldownSlot = 1;
    }

    public virtual bool? CanDamage() => new bool?((double) this.Projectile.ai[0] <= 0.0);

    public virtual void AI()
    {
      for (int index1 = 0; index1 < 2; ++index1)
      {
        Vector2 vector2 = Utils.RotatedBy(Utils.RotatedBy(new Vector2((float) -((Entity) this.Projectile).width * 0.65f * this.Projectile.scale, 0.0f), (double) Utils.NextFloat(Main.rand, -0.5f, 0.5f) * 6.28318548202515, new Vector2()), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2());
        int index2 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.One, 5f)), 10, 10, 229, (float) (-(double) ((Entity) this.Projectile).velocity.X / 3.0), (float) (-(double) ((Entity) this.Projectile).velocity.Y / 3.0), 150, Color.Transparent, 0.7f);
        Main.dust[index2].velocity = Vector2.Zero;
        Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2);
        Main.dust[index2].noGravity = true;
      }
      if ((double) this.Projectile.localAI[0] == 0.0)
        this.Projectile.localAI[0] = (float) -Math.Sign(((Entity) this.Projectile).velocity.Y);
      if (this.Projectile.timeLeft % this.Projectile.MaxUpdates != 0)
        return;
      if (this.Projectile.alpha > 0)
      {
        this.Projectile.alpha -= 11;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
      }
      this.Projectile.scale = (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue);
      if (++this.Projectile.frameCounter >= 6)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame > 1)
          this.Projectile.frame = 0;
      }
      if ((double) --this.Projectile.ai[0] == 0.0)
      {
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        this.Projectile.netUpdate = true;
      }
      if ((double) --this.Projectile.ai[1] == 0.0)
      {
        ((Entity) this.Projectile).velocity.Y = 60f / (float) this.Projectile.MaxUpdates * this.Projectile.localAI[0];
        this.Projectile.netUpdate = true;
      }
      NPC npc = FargoSoulsUtil.NPCExists(EModeGlobalNPC.championBoss, new int[1]
      {
        ModContent.NPCType<CosmosChampion>()
      });
      if ((double) this.Projectile.ai[1] <= 0.0 || npc == null || !npc.HasValidTarget)
        return;
      float num = this.Projectile.ai[1] / 60f;
      if ((double) num < 0.0)
        num = 0.0f;
      if ((double) num > 1.0)
        num = 1f;
      ((Entity) this.Projectile).position.Y += (float) (((double) ((Entity) Main.player[npc.target]).position.Y - (double) ((Entity) Main.player[npc.target]).oldPosition.Y) * 0.60000002384185791) * num;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, true, false);
    }

    public virtual void OnKill(int timeleft)
    {
      SoundEngine.PlaySound(ref SoundID.NPCDeath6, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 208;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index2].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
      }
      for (int index3 = 0; index3 < 10; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
        Main.dust[index4].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 1f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index5].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 1f);
        Main.dust[index5].noGravity = true;
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
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
