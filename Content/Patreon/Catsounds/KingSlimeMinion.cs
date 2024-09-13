// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Catsounds.KingSlimeMinion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Catsounds
{
  public class KingSlimeMinion : ModProjectile
  {
    public bool goingDown;
    public int spikeAttackCounter;
    public int slimeAttackCounter;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 6;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      this.Projectile.alpha = 75;
      ((Entity) this.Projectile).width = 38;
      ((Entity) this.Projectile).height = 40;
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
      if (((Entity) player).active && !player.dead && player.GetModPlayer<PatreonPlayer>().KingSlimeMinion)
        this.Projectile.timeLeft = 2;
      else
        this.Projectile.Kill();
      if (this.Projectile.frame >= 2)
        return;
      if (this.goingDown)
      {
        if ((double) ((Entity) this.Projectile).velocity.Y <= 0.0)
        {
          this.goingDown = false;
          ++this.spikeAttackCounter;
          if (this.spikeAttackCounter >= 10)
          {
            this.spikeAttackCounter = 0;
            if (this.Projectile.owner == Main.myPlayer && FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 400f, true, new Vector2()) != -1)
            {
              for (int index = 0; index < 25; ++index)
                Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), new Vector2(((Entity) this.Projectile).Center.X + (float) Main.rand.Next(-5, 5), ((Entity) this.Projectile).Center.Y - 15f), new Vector2(Utils.NextFloat(Main.rand, -6f, 6f), Utils.NextFloat(Main.rand, -8f, -5f)), ModContent.ProjectileType<KingSlimeSpike>(), this.Projectile.damage, this.Projectile.knockBack / 2f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
        }
      }
      else if ((double) ((Entity) this.Projectile).velocity.Y > 0.0)
        this.goingDown = true;
      if (++this.slimeAttackCounter <= 150)
        return;
      this.slimeAttackCounter = 0;
      int prioritizingMinionFocus = FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 800f, true, new Vector2());
      if (prioritizingMinionFocus == -1 || this.Projectile.owner != Main.myPlayer)
        return;
      for (int index = 0; index < 5; ++index)
      {
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) Main.npc[prioritizingMinionFocus]).Center, Vector2.op_Multiply(((Entity) Main.npc[prioritizingMinionFocus]).velocity, Utils.NextFloat(Main.rand, 15f)));
        vector2_1.X += (float) Main.rand.Next(-50, 51);
        vector2_1.Y -= (float) Main.rand.Next(600, 701);
        Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) Main.npc[prioritizingMinionFocus]).Center, vector2_1);
        ((Vector2) ref vector2_2).Normalize();
        Vector2 vector2_3 = Vector2.op_Multiply(vector2_2, Utils.NextFloat(Main.rand, 10f, 20f));
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_1, vector2_3, ModContent.ProjectileType<KingSlimeBallPiercing>(), this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
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
      target.AddBuff(137, 180, false);
    }
  }
}
