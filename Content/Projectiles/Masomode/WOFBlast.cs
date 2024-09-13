﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.WOFBlast
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class WOFBlast : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 12;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 80;
      ((Entity) this.Projectile).height = 80;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.penetrate = -1;
      this.Projectile.scale = 1.5f;
      this.Projectile.alpha = 0;
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 114);
      if (Utils.HasNaNs(((Entity) this.Projectile).position) || npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        Vector2 vector2;
        if ((double) this.Projectile.ai[0] == 0.0)
        {
          this.Projectile.rotation = npc.rotation + 3.14159274f;
          vector2 = Utils.RotatedBy(new Vector2((float) (((Entity) npc).width - 36), 6f), (double) this.Projectile.rotation, new Vector2());
        }
        else
        {
          this.Projectile.rotation = npc.rotation;
          vector2 = Utils.RotatedBy(new Vector2((float) (((Entity) npc).width - 36), -6f), (double) this.Projectile.rotation, new Vector2());
        }
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, vector2);
        if (++this.Projectile.frameCounter >= 3)
        {
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          {
            --this.Projectile.frame;
            this.Projectile.Kill();
          }
        }
        if ((double) this.Projectile.localAI[0] != 0.0)
          return;
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item88, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
