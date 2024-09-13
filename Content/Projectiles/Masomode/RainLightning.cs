// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.RainLightning
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class RainLightning : LightningArc
  {
    private int telegraphTimer;

    public override string Texture => "Terraria/Images/Projectile_466";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.DamageType = DamageClass.Default;
      this.Projectile.tileCollide = false;
      this.Projectile.friendly = true;
      this.Projectile.hostile = true;
    }

    public int TelegraphTime
    {
      get => (WorldSavingSystem.MasochistModeReal ? 110 : 190) * (1 + this.Projectile.extraUpdates);
    }

    public bool Telegraphing
    {
      get => this.telegraphTimer >= 0 && this.telegraphTimer < this.TelegraphTime;
    }

    public virtual bool PreAI()
    {
      if (this.Telegraphing)
      {
        this.Projectile.timeLeft = 300 * (this.Projectile.extraUpdates + 1);
        ++this.telegraphTimer;
        if (Utils.NextBool(Main.rand, 30))
          new SparkParticle(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.UnitY, (float) (900 - Main.rand.Next(30, 300)))), Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedByRandom(Vector2.UnitY, 0.31415927410125732)), Utils.NextFloat(Main.rand, 3f, 13f)), Color.Cyan, Utils.NextFloat(Main.rand, 0.3f, 0.7f), Main.rand.Next(10, 25)).Spawn();
        return false;
      }
      if (this.telegraphTimer < this.TelegraphTime)
        return base.PreAI();
      this.telegraphTimer = -1000;
      ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.op_UnaryNegation(Vector2.UnitY), 14f);
      this.Projectile.tileCollide = true;
      return false;
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.SourceDamage;
      local = StatModifier.op_Multiply(local, 10f);
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(144, 120, false);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(144, 120, true, false);
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
      if (Main.hardMode)
      {
        SoundEngine.PlaySound(ref SoundID.Item62, new Vector2?(), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<RainExplosion>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      }
      return base.OnTileCollide(oldVelocity);
    }

    public override bool PreDraw(ref Color lightColor)
    {
      return !this.Telegraphing && base.PreDraw(ref lightColor);
    }

    public virtual bool CanHitPlayer(Player target)
    {
      return !this.Telegraphing && base.CanHitPlayer(target);
    }
  }
}
