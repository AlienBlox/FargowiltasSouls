// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.DeviBoss.DeviButterfly
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
namespace FargowiltasSouls.Content.Bosses.DeviBoss
{
  public class DeviButterfly : ModProjectile
  {
    public bool drawLoaded;
    public int drawBase;

    public virtual string Texture => "Terraria/Images/NPC_205";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.npcFrameCount[205];
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 2;
      ((Entity) this.Projectile).height = 2;
      this.Projectile.timeLeft = 420;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.hide = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>());
      if (npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        if (!this.drawLoaded)
        {
          this.drawLoaded = true;
          this.drawBase = Main.rand.Next(8);
          this.Projectile.hide = false;
        }
        Vector2 vector2_1;
        vector2_1.X = ((Entity) npc).Center.X;
        vector2_1.Y = ((Entity) Main.player[npc.target]).Center.Y;
        vector2_1.X += (float) (1100.0 * Math.Sin(Math.PI / 300.0 * (double) this.Projectile.ai[1]++));
        vector2_1.Y -= 420f;
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
        if ((double) ((Vector2) ref vector2_2).Length() > 25.0)
        {
          vector2_2 = Vector2.op_Division(vector2_2, 8f);
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 23f), vector2_2), 24f);
        }
        else if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 12.0)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.05f);
        }
        if ((double) ++this.Projectile.localAI[0] > 90.0)
        {
          int num = 12;
          if ((double) npc.localAI[3] <= 1.0)
          {
            num = 3;
            if ((double) this.Projectile.localAI[0] > 105.0)
              this.Projectile.localAI[0] = 45f;
          }
          if ((double) ++this.Projectile.localAI[1] > (double) num)
          {
            this.Projectile.localAI[1] = 0.0f;
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.UnitY, 3f), ModContent.ProjectileType<DeviLightBall2>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
          }
        }
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = Math.Sign(((Entity) this.Projectile).velocity.X);
        ++this.Projectile.frameCounter;
        if (this.Projectile.frameCounter < 4)
          this.Projectile.frame = 0;
        else if (this.Projectile.frameCounter < 8)
          this.Projectile.frame = 1;
        else if (this.Projectile.frameCounter < 12)
          this.Projectile.frame = 2;
        else if (this.Projectile.frameCounter < 16)
          this.Projectile.frame = 1;
        else
          this.Projectile.frameCounter = 0;
      }
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.NPCDeath1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if (!Main.dedServ)
      {
        Gore.NewGore(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, 270, this.Projectile.scale);
        Gore.NewGore(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, 271, this.Projectile.scale);
        Gore.NewGore(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, 271, this.Projectile.scale);
        Gore.NewGore(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, 272, this.Projectile.scale);
      }
      for (int index1 = 0; index1 < 10; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 86, 0.0f, 0.0f, 0, new Color(), 2.5f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 8f);
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
