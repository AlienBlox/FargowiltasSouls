// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.PalmTreeSentry
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class PalmTreeSentry : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 80;
      ((Entity) this.Projectile).height = 82;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.penetrate = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.timeLeft = 7200;
      this.Projectile.FargoSouls().NinjaCanSpeedup = false;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!player.Alive() || !player.HasEffect<PalmwoodEffect>())
      {
        this.Projectile.Kill();
      }
      else
      {
        bool flag = fargoSoulsPlayer.ForceEffect<PalmWoodEnchant>();
        this.Projectile.scale = flag ? 2f : 1f;
        ((Entity) this.Projectile).height = 82 * (int) this.Projectile.scale;
        ((Entity) this.Projectile).width = 80 * (int) this.Projectile.scale;
        ((Entity) this.Projectile).velocity.Y = ((Entity) this.Projectile).velocity.Y + 0.2f;
        if ((double) ((Entity) this.Projectile).velocity.Y > 16.0)
          ((Entity) this.Projectile).velocity.Y = 16f;
        ++this.Projectile.ai[1];
        if ((double) this.Projectile.ai[1] < (flag ? 30.0 : 45.0))
          return;
        float num1 = 2000f;
        int index1 = -1;
        for (int index2 = 0; index2 < 200; ++index2)
        {
          float num2 = Vector2.Distance(((Entity) this.Projectile).Center, ((Entity) Main.npc[index2]).Center);
          if ((double) num2 < (double) num1 && (double) num2 < 300.0 && Main.npc[index2].CanBeChasedBy((object) this.Projectile, false))
          {
            index1 = index2;
            num1 = num2;
          }
        }
        if (index1 != -1)
        {
          NPC npc = Main.npc[index1];
          if (Collision.CanHit(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, ((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height))
          {
            Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center)), 10f);
            int index3 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2, 483, this.Projectile.damage, 2f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
            if (index3 != Main.maxProjectiles)
              Main.projectile[index3].DamageType = DamageClass.Summon;
          }
        }
        this.Projectile.ai[1] = 0.0f;
        if ((double) Vector2.Distance(((Entity) Main.player[this.Projectile.owner]).Center, ((Entity) this.Projectile).Center) <= 2000.0)
          return;
        this.Projectile.Kill();
      }
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

    public virtual bool PreDraw(ref Color lightColor)
    {
      Main.EntitySpriteDraw(TextureAssets.Projectile[this.Type].Value, Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Rectangle?(), lightColor, this.Projectile.rotation, Vector2.op_Division(Utils.Size(TextureAssets.Projectile[this.Type]), 2f), this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      Projectile projectile = this.Projectile;
      ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
      ((Entity) this.Projectile).velocity = Vector2.Zero;
      return false;
    }
  }
}
