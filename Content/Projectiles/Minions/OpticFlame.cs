// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.OpticFlame
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class OpticFlame : ModProjectile
  {
    public int targetID = -1;
    public int searchTimer = 3;

    public virtual string Texture => "Terraria/Images/Projectile_101";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SendExtraAI(BinaryWriter writer) => writer.Write(this.targetID);

    public virtual void ReceiveExtraAI(BinaryReader reader) => this.targetID = reader.ReadInt32();

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(101);
      this.AIType = 101;
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.tileCollide = false;
      this.Projectile.penetrate = -1;
      this.Projectile.ignoreWater = true;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 8;
      target.AddBuff(39, 600, false);
    }
  }
}
