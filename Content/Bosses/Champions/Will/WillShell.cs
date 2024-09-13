// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Will.WillShell
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Will
{
  public class WillShell : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 100;
      ((Entity) this.Projectile).height = 100;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 600;
      this.Projectile.scale = 1.5f;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<WillChampion>());
      if (npc != null && (double) npc.ai[0] == -1.0)
      {
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = ((Entity) npc).direction;
        this.Projectile.rotation = npc.rotation;
        if ((double) this.Projectile.localAI[0] == 0.0)
        {
          this.Projectile.alpha += 17;
          if (this.Projectile.alpha <= (int) byte.MaxValue)
            return;
          this.Projectile.alpha = (int) byte.MaxValue;
          this.Projectile.localAI[0] = 1f;
        }
        else
        {
          this.Projectile.alpha -= 17;
          if (this.Projectile.alpha >= 0)
            return;
          this.Projectile.alpha = 0;
          this.Projectile.localAI[0] = 0.0f;
        }
      }
      else
        this.Projectile.Kill();
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 80; ++index1)
      {
        Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitX, 40f), (double) (index1 - 39) * 6.2831854820251465 / 80.0, new Vector2()), ((Entity) this.Projectile).Center);
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
        int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 174, 0.0f, 0.0f, 0, new Color(), 3f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity = vector2_2;
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
