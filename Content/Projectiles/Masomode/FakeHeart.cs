// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.FakeHeart
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class FakeHeart : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 12;
      ((Entity) this.Projectile).height = 12;
      this.Projectile.timeLeft = 600;
      this.Projectile.hostile = true;
      this.Projectile.aiStyle = -1;
      this.CooldownSlot = 0;
    }

    public virtual void AI()
    {
      float num1 = 0.1f;
      float num2 = 7f;
      ((Entity) this.Projectile).velocity.Y += num1;
      if ((double) ((Entity) this.Projectile).velocity.Y > (double) num2)
        ((Entity) this.Projectile).velocity.Y = num2;
      ((Entity) this.Projectile).velocity.X *= 0.95f;
      if ((double) ((Entity) this.Projectile).velocity.X < 0.10000000149011612 && (double) ((Entity) this.Projectile).velocity.X > -0.10000000149011612)
        ((Entity) this.Projectile).velocity.X = 0.0f;
      float num3 = (float) ((double) Main.rand.Next(90, 111) * 0.0099999997764825821 * ((double) Main.essScale * 0.5));
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.5f * num3, 0.1f * num3, 0.1f * num3);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity) => false;

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = false;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual bool CanHitPlayer(Player target)
    {
      if (this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) target).Hitbox))
      {
        if (target.FargoSouls().DevianttHeartItem == null)
        {
          target.hurtCooldowns[0] = 0;
          Player.DefenseStat statDefense = target.statDefense;
          float endurance = target.endurance;
          ref MultipliableFloat local = ref target.statDefense.FinalMultiplier;
          local = MultipliableFloat.op_Multiply(local, 0.0f);
          target.endurance = 0.0f;
          target.Hurt(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.FargowiltasSouls.DeathMessage.FakeHeart", (object) target.name)), this.Projectile.damage, 0, false, false, 0, false, 0.0f, 0.0f, 4.5f);
          target.statDefense = statDefense;
          target.endurance = endurance;
          if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.deviBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>()))
            target.AddBuff(ModContent.BuffType<LovestruckBuff>(), 240, true, false);
        }
        else
        {
          ++target.statLife;
          target.HealEffect(1, true);
        }
        this.Projectile.timeLeft = 0;
      }
      return false;
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
