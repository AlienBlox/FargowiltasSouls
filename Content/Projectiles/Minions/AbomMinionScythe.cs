// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.AbomMinionScythe
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.AbomBoss;
using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class AbomMinionScythe : AbomScytheSplit
  {
    public override string Texture => "Terraria/Images/Projectile_274";

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.timeLeft = 120;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
      this.CooldownSlot = -1;
    }

    public override void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item71, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      ++this.Projectile.rotation;
      ++this.Projectile.ai[1];
      if ((double) this.Projectile.ai[1] <= 30.0)
        return;
      this.Projectile.ai[1] = 30f;
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0]);
      if (npc.Alive())
        ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), 70f), 0.1f);
      else
        this.Projectile.ai[0] = -1f;
    }

    public override void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 25; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 70, 0.0f, 0.0f, 0, new Color(), 3.5f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 6f);
        Main.dust[index2].noGravity = true;
      }
      if (this.Projectile.owner != Main.myPlayer)
        return;
      Vector2 vector2 = Vector2.op_Multiply(30f, Vector2.Normalize(((Entity) this.Projectile).velocity));
      for (int index = 0; index < 6; ++index)
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Utils.RotatedBy(vector2, 1.0471975803375244 * (double) index, new Vector2()), ModContent.ProjectileType<AbomMinionSickle>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 30f, 0.0f, 0.0f);
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 600, false);
      target.AddBuff(153, 600, false);
    }
  }
}
