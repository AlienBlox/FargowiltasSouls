// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.RainCloud
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class RainCloud : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_238";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 6;

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(238);
      this.AIType = 238;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.timeLeft = 300;
      this.Projectile.penetrate = -1;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.ownedProjectileCounts[this.Projectile.type] > 1 || this.Projectile.owner == Main.myPlayer && !player.HasEffect<RainUmbrellaEffect>())
        this.Projectile.Kill();
      else
        this.Projectile.timeLeft = 2;
      if (player == Main.LocalPlayer)
      {
        Vector2 mouseWorld = Main.MouseWorld;
        ((Entity) this.Projectile).Center = new Vector2(mouseWorld.X, mouseWorld.Y - 30f);
      }
      if (fargoSoulsPlayer.ForceEffect<RainEnchant>())
        this.Projectile.scale = 3f;
      this.Projectile.ai[0] = 0.0f;
      ++this.Projectile.localAI[1];
      if ((double) this.Projectile.scale > 3.0)
        this.Projectile.localAI[1] += 4f;
      else if ((double) this.Projectile.scale > 2.0)
        this.Projectile.localAI[1] += 3f;
      else if ((double) this.Projectile.scale > 1.5)
        this.Projectile.localAI[1] += 2f;
      else
        ++this.Projectile.localAI[1];
      if (!Collision.CanHitLine(((Entity) player).Center, 2, 2, ((Entity) this.Projectile).Center, 2, 2) || (double) this.Projectile.localAI[1] < 8.0)
        return;
      this.Projectile.localAI[1] = 0.0f;
      if (this.Projectile.owner != Main.myPlayer)
        return;
      int num1 = (int) ((double) ((Entity) this.Projectile).Center.X + (double) Main.rand.Next((int) (-20.0 * (double) this.Projectile.scale), (int) (20.0 * (double) this.Projectile.scale)));
      int num2 = (int) ((double) ((Entity) this.Projectile).position.Y + (double) ((Entity) this.Projectile).height + 4.0);
      int index1 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), (float) num1, (float) num2, 0.0f, 5f, 239, this.Projectile.damage / 4, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      if (index1 != Main.maxProjectiles)
      {
        Main.projectile[index1].penetrate = 1;
        Main.projectile[index1].timeLeft = 45 * Main.projectile[index1].MaxUpdates;
      }
      if (!Utils.NextBool(Main.rand, 10))
        return;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector(Utils.NextFloat(Main.rand, -2f, 2f), 5f);
      ((Vector2) ref vector2_1).Normalize();
      Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 10f);
      float num3 = (float) Main.rand.Next(80);
      int index2 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).position.X + Utils.NextFloat(Main.rand, (float) ((Entity) this.Projectile).width), ((Entity) this.Projectile).Center.Y + (float) (((Entity) this.Projectile).height / 2), vector2_2.X, vector2_2.Y, ModContent.ProjectileType<LightningArc>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, Utils.ToRotation(vector2_1), num3, 0.0f);
      if (index2 == Main.maxProjectiles)
        return;
      Main.projectile[index2].DamageType = DamageClass.Magic;
      Main.projectile[index2].usesIDStaticNPCImmunity = false;
      Main.projectile[index2].idStaticNPCHitCooldown = 0;
      Main.projectile[index2].FargoSouls().noInteractionWithNPCImmunityFrames = false;
    }
  }
}
