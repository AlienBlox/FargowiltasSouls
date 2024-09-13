// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.BigSting22
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
  public class BigSting22 : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Assets/ExtraTextures/Resprites/NPC_222";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.npcFrameCount[222];
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 66;
      ((Entity) this.Projectile).height = 66;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 240;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.scale = 0.5f;
      this.Projectile.penetrate = -1;
      this.Projectile.extraUpdates = 1;
    }

    public virtual void AI()
    {
      this.Projectile.spriteDirection = -Math.Sign(((Entity) this.Projectile).velocity.X);
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      if (this.Projectile.spriteDirection > 0)
        this.Projectile.rotation += 3.14159274f;
      if (++this.Projectile.frameCounter > 4)
      {
        ++this.Projectile.frame;
        this.Projectile.frameCounter = 0;
      }
      if (this.Projectile.frame >= 4)
        this.Projectile.frame = 0;
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0]);
      if (npc != null && npc.CanBeChasedBy((object) null, false))
      {
        if ((double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) < (double) (Math.Max(((Entity) npc).width, ((Entity) npc).height) / 2))
        {
          this.Projectile.ai[0] = -1f;
          this.Projectile.netUpdate = true;
        }
        else
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center));
        this.Projectile.ai[1] = 1f;
      }
      else
      {
        if ((double) this.Projectile.ai[1] != 0.0 || (double) ++this.Projectile.localAI[0] != 22.0)
          return;
        this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1220f, center: new Vector2());
        this.Projectile.ai[1] = 1f;
        this.Projectile.netUpdate = true;
      }
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
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

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.NPCDeath1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 5, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
        Main.dust[index2].scale += 0.75f;
      }
    }
  }
}
