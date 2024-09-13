// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.FriendHeart
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class FriendHeart : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/FakeHeart";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 12;
      ((Entity) this.Projectile).height = 12;
      this.Projectile.timeLeft = 900;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
    }

    public virtual void AI()
    {
      float num1 = (float) ((double) Main.rand.Next(90, 111) * 0.0099999997764825821 * ((double) Main.essScale * 0.5));
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.5f * num1, 0.1f * num1, 0.1f * num1);
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.ai[0] = -1f;
      }
      NPC npc1 = FargoSoulsUtil.NPCExists(this.Projectile.ai[0]);
      if (npc1 != null)
      {
        if (npc1.CanBeChasedBy((object) null, false))
        {
          if ((double) ((Entity) this.Projectile).Distance(((Entity) npc1).Center) > 40.0)
            ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 16f), Vector2.op_Multiply(17f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc1).Center))), 17f);
          else if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 17.0)
          {
            Projectile projectile = this.Projectile;
            ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.05f);
          }
        }
        else
        {
          this.Projectile.ai[0] = -1f;
          this.Projectile.netUpdate = true;
        }
      }
      else
      {
        if ((double) --this.Projectile.ai[1] < 0.0)
        {
          this.Projectile.ai[1] = 6f;
          float num2 = 1700f;
          int num3 = -1;
          for (int index = 0; index < 200; ++index)
          {
            NPC npc2 = Main.npc[index];
            if (npc2.CanBeChasedBy((object) null, false))
            {
              float num4 = ((Entity) this.Projectile).Distance(((Entity) npc2).Center);
              if ((double) num4 < (double) num2)
              {
                num2 = num4;
                num3 = index;
              }
            }
          }
          this.Projectile.ai[0] = (float) num3;
          this.Projectile.netUpdate = true;
        }
        this.Projectile.localAI[1] = 0.0f;
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(119, 600, false);
      if (this.Projectile.owner != Main.myPlayer)
        return;
      int num = 2;
      Main.player[Main.myPlayer].HealEffect(num, true);
      Main.player[Main.myPlayer].statLife += num;
      if (Main.player[Main.myPlayer].statLife <= Main.player[Main.myPlayer].statLifeMax2)
        return;
      Main.player[Main.myPlayer].statLife = Main.player[Main.myPlayer].statLifeMax2;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color((int) byte.MaxValue, (int) ((Color) ref lightColor).G, (int) ((Color) ref lightColor).B, (int) ((Color) ref lightColor).A));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
