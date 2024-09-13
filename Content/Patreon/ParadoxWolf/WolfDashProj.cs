// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.ParadoxWolf.WolfDashProj
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
namespace FargowiltasSouls.Content.Patreon.ParadoxWolf
{
  public class WolfDashProj : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 8;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 0;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 42;
      ((Entity) this.Projectile).height = 42;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 20;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().CanSplit = false;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (!((Entity) player).active || player.dead)
      {
        this.Projectile.Kill();
      }
      else
      {
        player.GetModPlayer<PatreonPlayer>().WolfDashing = true;
        if (player.FargoSouls().IsDashingTimer < 2)
          player.FargoSouls().IsDashingTimer = 2;
        this.Projectile.FargoSouls().TimeFreezeImmune = player.HasEffect<StardustEffect>();
        ((Entity) player).Center = ((Entity) this.Projectile).Center;
        this.Projectile.spriteDirection = -((Entity) this.Projectile).direction;
      }
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity) => false;

    public virtual bool PreDraw(ref Color lightColor)
    {
      SpriteEffects spriteEffects = this.Projectile.spriteDirection >= 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector((float) TextureAssets.Projectile[this.Projectile.type].Value.Width * 0.5f, (float) ((Entity) this.Projectile).height * 0.5f);
      for (int index = 0; index < this.Projectile.oldPos.Length; ++index)
      {
        Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Subtraction(this.Projectile.oldPos[index], Main.screenPosition), vector2_1), new Vector2(0.0f, this.Projectile.gfxOffY));
        Color color = Color.op_Multiply(this.Projectile.GetAlpha(lightColor), (float) (this.Projectile.oldPos.Length - index) / (float) this.Projectile.oldPos.Length);
        Main.EntitySpriteDraw(TextureAssets.Projectile[this.Projectile.type].Value, vector2_2, new Rectangle?(), color, this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      }
      return true;
    }
  }
}
