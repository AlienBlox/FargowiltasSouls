// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MimicDagger
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
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MimicDagger : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_93";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(93);
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.friendly = false;
      this.Projectile.DamageType = DamageClass.Generic;
      this.Projectile.timeLeft = 300;
      this.Projectile.tileCollide = false;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if ((double) this.Projectile.ai[0] < 60.0)
      {
        this.Projectile.hostile = false;
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.95f);
      }
      else
      {
        this.Projectile.hostile = true;
        if ((double) this.Projectile.ai[0] == 60.0)
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation - 1.57079637f), 9f);
        if ((double) this.Projectile.ai[0] > 80.0)
          this.Projectile.rotation += 0.6f;
        if ((double) this.Projectile.ai[0] > 180.0)
        {
          ((Entity) this.Projectile).velocity.X *= 0.95f;
          ((Entity) this.Projectile).velocity.Y += 0.3f;
        }
      }
      if (Utils.NextBool(Main.rand, 12))
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 246, 0.0f, 0.0f, 0, new Color(), 0.5f);
      if (this.Projectile.timeLeft < 180)
        this.Projectile.tileCollide = true;
      ++this.Projectile.ai[0];
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<MidasBuff>(), 600, true, false);
    }
  }
}
