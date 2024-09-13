// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.GladiatorStandard
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class GladiatorStandard : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 13;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 74;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.penetrate = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.timeLeft = 900;
    }

    private ref float hits => ref this.Projectile.ai[2];

    public virtual bool? CanDamage()
    {
      return new bool?(Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero) && (double) this.hits < 5.0);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => ++this.hits;

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer1 = player.FargoSouls();
      FargoSoulsPlayer fargoSoulsPlayer2 = Main.LocalPlayer.FargoSouls();
      int distance = fargoSoulsPlayer1.ForceEffect<GladiatorEnchant>() ? 800 : 400;
      FargoSoulsUtil.AuraDust((Entity) this.Projectile, (float) distance, 246, new Color());
      if ((double) Utils.Distance(FargoSoulsUtil.ClosestPointInHitbox(((Entity) Main.LocalPlayer).Hitbox, ((Entity) this.Projectile).Center), ((Entity) this.Projectile).Center) < (double) distance && player.HasEffect<GladiatorBanner>() && !fargoSoulsPlayer2.Purified)
        Main.LocalPlayer.AddBuff(ModContent.BuffType<GladiatorBuff>(), 2, true, false);
      if (++this.Projectile.frameCounter <= 2)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame < Main.projFrames[this.Projectile.type])
        return;
      this.Projectile.frame = 0;
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = false;
      return true;
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      SoundEngine.PlaySound(ref SoundID.Dig, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      Projectile projectile1 = this.Projectile;
      ((Entity) projectile1).position = Vector2.op_Addition(((Entity) projectile1).position, ((Entity) this.Projectile).velocity);
      Projectile projectile2 = this.Projectile;
      ((Entity) projectile2).position = Vector2.op_Addition(((Entity) projectile2).position, Vector2.op_Multiply(Vector2.UnitY, 8f));
      ((Entity) this.Projectile).velocity = Vector2.Zero;
      for (int index = 0; index < 30; ++index)
        Dust.NewDust(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitY, (float) ((Entity) this.Projectile).height), 2f)), 0, 0, 10, Utils.NextFloat(Main.rand, -30f, 30f), -Utils.NextFloat(Main.rand, 4f, 8f), 0, new Color(), 1f);
      this.Projectile.tileCollide = false;
      return false;
    }
  }
}
