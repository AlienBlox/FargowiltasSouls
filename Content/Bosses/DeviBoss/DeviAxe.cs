// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.DeviBoss.DeviAxe
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.DeviBoss
{
  public class DeviAxe : ModProjectile
  {
    public virtual string Texture => FargoSoulsUtil.EmptyTexture;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 46;
      ((Entity) this.Projectile).height = 46;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 180;
      this.Projectile.hide = true;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.CooldownSlot = 1;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (((Rectangle) ref projHitbox).Intersects(targetHitbox))
        return new bool?(true);
      Rectangle rectangle = projHitbox;
      rectangle.X = (int) ((Entity) this.Projectile).oldPosition.X;
      rectangle.Y = (int) ((Entity) this.Projectile).oldPosition.Y;
      if (((Rectangle) ref rectangle).Intersects(targetHitbox))
        return new bool?(true);
      rectangle = projHitbox;
      rectangle.X = (int) MathHelper.Lerp(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).oldPosition.X, 0.5f);
      rectangle.Y = (int) MathHelper.Lerp(((Entity) this.Projectile).position.Y, ((Entity) this.Projectile).oldPosition.Y, 0.5f);
      return ((Rectangle) ref rectangle).Intersects(targetHitbox) ? new bool?(true) : new bool?(false);
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>());
      if (npc != null)
      {
        if ((double) this.Projectile.localAI[0] == 0.0)
        {
          this.Projectile.localAI[0] = 1f;
          this.Projectile.localAI[1] = Utils.ToRotation(((Entity) this.Projectile).DirectionFrom(((Entity) npc).Center));
        }
        Vector2 vector2 = Utils.RotatedBy(new Vector2(this.Projectile.ai[1], 0.0f), (double) npc.ai[3] + (double) this.Projectile.localAI[1], new Vector2());
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, vector2);
      }
      else
        this.Projectile.Kill();
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.NPCDeath6, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      ((Entity) target).velocity.X = (double) ((Entity) target).Center.X < (double) ((Entity) Main.npc[(int) this.Projectile.ai[0]]).Center.X ? -15f : 15f;
      ((Entity) target).velocity.Y = -10f;
      target.AddBuff(ModContent.BuffType<LovestruckBuff>(), 240, true, false);
    }
  }
}
