// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.ChallengerItems.BaronTuskShrapnel
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.ChallengerItems
{
  public class BaronTuskShrapnel : ModProjectile
  {
    public NPC EmbeddedNPC;
    public Vector2 embedOffset = Vector2.Zero;

    public virtual string Texture => "FargowiltasSouls/Content/Bosses/BanishedBaron/BaronShrapnel";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 10;
      ((Entity) this.Projectile).height = 10;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.timeLeft = 2400;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
      this.Projectile.FargoSouls().CanSplit = false;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      ref float local = ref this.Projectile.ai[1];
      if ((double) local != 0.0)
        return;
      this.EmbeddedNPC = target;
      this.embedOffset = Vector2.op_Subtraction(((Entity) this.Projectile).Center, ((Entity) target).Center);
      ((Entity) this.Projectile).velocity = Vector2.Zero;
      local = 1f;
    }

    public virtual void AI()
    {
      ref float local = ref this.Projectile.ai[1];
      this.Projectile.rotation = Utils.ToRotation(Utils.RotatedBy(((Entity) this.Projectile).velocity, 3.1415927410125732, new Vector2()));
      float num = local;
      if ((double) num != 0.0)
      {
        if ((double) num != 1.0)
        {
          if ((double) num != 2.0)
          {
            if ((double) num != 3.0)
              return;
            ((Entity) this.Projectile).velocity.Y += 0.25f;
          }
          else
          {
            ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.Normalize(this.embedOffset), 20f);
            this.Projectile.friendly = true;
            this.Projectile.tileCollide = true;
            this.Projectile.penetrate = 2;
            this.EmbeddedNPC = (NPC) null;
            this.embedOffset = Vector2.Zero;
            local = 3f;
          }
        }
        else
        {
          this.Projectile.friendly = false;
          this.Projectile.tileCollide = false;
          if (((Entity) this.EmbeddedNPC).active)
          {
            ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) this.EmbeddedNPC).Center, this.embedOffset);
            this.Projectile.rotation = Utils.ToRotation(this.embedOffset);
          }
          else
            local = 2f;
        }
      }
      else
        ((Entity) this.Projectile).velocity.Y += 0.25f;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 8, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
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
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
