// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.RainbowSlime
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class RainbowSlime : ModProjectile
  {
    public int counter;

    public virtual string Texture => "Terraria/Images/Projectile_266";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 6;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      this.Projectile.alpha = 75;
      ((Entity) this.Projectile).width = 24;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.timeLeft *= 5;
      this.Projectile.aiStyle = 26;
      this.AIType = 266;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.penetrate = -1;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      this.Projectile.timeLeft = 2;
      if (((Entity) player).whoAmI == Main.myPlayer && (!((Entity) player).active || player.dead || player.ghost || !player.FargoSouls().RainbowSlime))
      {
        this.Projectile.Kill();
      }
      else
      {
        if (++this.counter <= 150)
          return;
        this.counter = 0;
        if (this.Projectile.owner != Main.myPlayer || FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 600f, true) == -1)
          return;
        for (int index = 0; index < 15; ++index)
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(Utils.NextFloat(Main.rand, -6f, 6f), Utils.NextFloat(Main.rand, -8f, -5f)), ModContent.ProjectileType<RainbowSlimeSpikeFriendly>(), this.Projectile.damage / 10, this.Projectile.knockBack, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity) => false;

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = (double) ((Entity) Main.player[this.Projectile.owner]).Center.Y > (double) ((Entity) this.Projectile).position.Y + (double) ((Entity) this.Projectile).height;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(ModContent.BuffType<FlamesoftheUniverseBuff>(), 120, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, this.Projectile.alpha));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY - 4f)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
