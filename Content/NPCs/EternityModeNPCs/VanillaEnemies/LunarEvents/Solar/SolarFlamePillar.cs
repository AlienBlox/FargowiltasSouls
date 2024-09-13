// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar.SolarFlamePillar
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar
{
  public class SolarFlamePillar : ModProjectile
  {
    private int child = -1;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 12;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual string Texture => FargoSoulsUtil.EmptyTexture;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 64;
      ((Entity) this.Projectile).height = 1;
      this.Projectile.DamageType = DamageClass.Default;
      this.Projectile.friendly = false;
      this.Projectile.hostile = false;
      this.Projectile.tileCollide = true;
      this.Projectile.timeLeft = 1800;
      this.Projectile.scale = 1f;
      this.AIType = 0;
      this.Projectile.aiStyle = 0;
    }

    public virtual void AI()
    {
      ref float local = ref this.Projectile.ai[0];
      if ((double) local == 0.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).Center = Vector2.op_Subtraction(((Entity) projectile).Center, Vector2.op_Multiply(Vector2.UnitY, 400f));
        ((Entity) this.Projectile).height = 400;
      }
      if ((double) local == 120.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item45, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 velocity = Vector2.op_Multiply(Vector2.UnitY, -30f);
          this.child = FargoSoulsUtil.NewNPCEasy(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitY, 400f), 2f)), 519, velocity: velocity);
          if (((Entity) Main.npc[this.child]).active && this.child != Main.maxNPCs)
            Main.npc[this.child].damage = this.Projectile.damage;
        }
      }
      if ((double) local < 120.0)
        new ExpandingBloomParticle(Utils.NextVector2FromRectangle(Main.rand, ((Entity) this.Projectile).Hitbox), Vector2.op_Multiply(Vector2.UnitY, Utils.NextFloat(Main.rand, -20f, 0.0f)), Color.Goldenrod, Vector2.op_Multiply(Vector2.One, 1f), Vector2.op_Multiply(Vector2.One, 0.0f), 60).Spawn();
      else
        ((Entity) Main.npc[this.child]).velocity.Y = -30f;
      if ((double) local > 150.0)
        this.Projectile.Kill();
      ++local;
    }
  }
}
