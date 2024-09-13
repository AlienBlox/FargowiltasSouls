// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.FlameburstMinion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class FlameburstMinion : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 44;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.timeLeft = 900;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (((Entity) player).whoAmI == Main.myPlayer && (player.dead || !player.FargoSouls().DarkArtistEnchantActive || !player.HasEffect<DarkArtistMinion>()))
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.netUpdate = true;
        ((Entity) this.Projectile).position.X = (float) (int) ((Entity) this.Projectile).position.X;
        ((Entity) this.Projectile).position.Y = (float) (int) ((Entity) this.Projectile).position.Y;
        this.Projectile.scale = (float) ((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.2f + 0.95f;
        if ((double) this.Projectile.ai[0] != 0.0)
          return;
        ((Entity) this.Projectile).position.X = ((Entity) player).Center.X - (float) (((Entity) this.Projectile).width / 2);
        ((Entity) this.Projectile).position.Y = (float) ((double) ((Entity) player).Center.Y - (double) (((Entity) this.Projectile).height / 2) + (double) player.gfxOffY - 50.0);
        if (((Entity) player).whoAmI == Main.myPlayer)
        {
          if ((double) Main.MouseWorld.X > (double) ((Entity) this.Projectile).Center.X)
          {
            this.Projectile.spriteDirection = 1;
            this.Projectile.rotation = Utils.AngleLerp(this.Projectile.rotation, Utils.ToRotation(Vector2.op_Subtraction(new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y), ((Entity) this.Projectile).Center)), 0.08f);
          }
          else
          {
            this.Projectile.spriteDirection = -1;
            this.Projectile.rotation = Utils.AngleLerp(this.Projectile.rotation, Utils.ToRotation(Vector2.op_Subtraction(new Vector2(Main.MouseWorld.X - (float) (((double) Main.MouseWorld.X - (double) ((Entity) this.Projectile).Center.X) * 2.0), Main.MouseWorld.Y - (float) (((double) Main.MouseWorld.Y - (double) ((Entity) this.Projectile).Center.Y) * 2.0)), ((Entity) this.Projectile).Center)), 0.08f);
          }
        }
        int num = 60;
        ++this.Projectile.ai[1];
        if (!player.controlUseItem || (double) this.Projectile.ai[1] < (double) num)
          return;
        Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) this.Projectile).Center)), 10f);
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2, ModContent.ProjectileType<MegaFlameburst>(), FargoSoulsUtil.HighestDamageTypeScaling(player, 85), 4f, this.Projectile.owner, (float) ((Entity) this.Projectile).whoAmI, 0.0f, 0.0f);
        SoundEngine.PlaySound(ref SoundID.DD2_FlameburstTowerShot, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        this.Projectile.ai[1] = 0.0f;
      }
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 12; ++index1)
      {
        Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.rotation, new Vector2()), 6f), (double) (index1 - 5) * 6.2831854820251465 / 12.0, new Vector2()), ((Entity) this.Projectile).Center);
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
        int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 270, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity = vector2_2;
      }
    }
  }
}
