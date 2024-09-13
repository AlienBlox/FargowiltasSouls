// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.Bloodshed
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class Bloodshed : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.tileCollide = false;
      this.Projectile.aiStyle = -1;
      this.Projectile.timeLeft = 600;
    }

    public virtual void AI()
    {
      Projectile projectile = this.Projectile;
      ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.95f);
      if ((double) this.Projectile.ai[0] == 0.0)
      {
        if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 2.0)
        {
          foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && !n.friendly && n.lifeMax > 5 && !n.immortal && n.damage > 0)))
          {
            if (this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) npc).Hitbox))
            {
              npc.AddBuff(ModContent.BuffType<BloodDrinkerBuff>(), 360, false);
              this.Projectile.ai[1] = 1f;
              this.Projectile.netUpdate = true;
              this.Projectile.Kill();
              return;
            }
          }
        }
      }
      else
      {
        this.Projectile.timeLeft -= 3;
        int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
        switch (closest)
        {
          case -1:
          case (int) byte.MaxValue:
            break;
          default:
            if (((Entity) Main.player[closest]).active && !Main.player[closest].dead && !Main.player[closest].ghost && (double) ((Entity) Main.player[closest]).Distance(((Entity) this.Projectile).Center) < 360.0)
            {
              ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[closest]).Center), 9f);
              ++this.Projectile.timeLeft;
              if (this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) Main.player[closest]).Hitbox))
              {
                Main.player[closest].AddBuff(ModContent.BuffType<BloodDrinkerBuff>(), 360, true, false);
                this.Projectile.ai[1] = 1f;
                this.Projectile.netUpdate = true;
                this.Projectile.Kill();
                return;
              }
              break;
            }
            break;
        }
      }
      if (Main.player[this.Projectile.owner].ownedProjectileCounts[this.Projectile.type] > 50)
      {
        --Main.player[this.Projectile.owner].ownedProjectileCounts[this.Projectile.type];
        this.Projectile.Kill();
      }
      else
      {
        if (++this.Projectile.frameCounter > 4)
        {
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
            this.Projectile.frame = 0;
        }
        Lighting.AddLight(((Entity) this.Projectile).Center, 19);
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      if ((double) this.Projectile.ai[1] == 1.0)
        SoundEngine.PlaySound(ref SoundID.NPCDeath11, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      int num = (double) this.Projectile.ai[1] == 1.0 ? 20 : 10;
      for (int index1 = 0; index1 < num; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 5, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, (double) this.Projectile.ai[1] == 1.0 ? 2.5f : 1.5f);
        Main.dust[index2].scale += (double) this.Projectile.ai[1] == 1.0 ? 1.5f : 0.5f;
      }
    }

    public virtual Color? GetAlpha(Color lightColor) => base.GetAlpha(lightColor);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
