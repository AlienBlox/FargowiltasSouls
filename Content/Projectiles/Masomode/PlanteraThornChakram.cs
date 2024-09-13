// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.PlanteraThornChakram
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class PlanteraThornChakram : ModProjectile
  {
    public virtual string Texture => FargoSoulsUtil.VanillaTextureProjectile(33);

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Type] = 4;
      ProjectileID.Sets.TrailingMode[this.Type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 600;
      this.Projectile.scale = 1f;
    }

    private ref float Timer => ref this.Projectile.ai[2];

    public virtual void AI()
    {
      if ((!SoulConfig.Instance.BossRecolors ? 0 : (WorldSavingSystem.EternityMode ? 1 : 0)) != 0)
        Lighting.AddLight(((Entity) this.Projectile).Center, 0.09803922f, 0.184313729f, 0.2509804f);
      else
        Lighting.AddLight(((Entity) this.Projectile).Center, 0.4f, 1.2f, 0.4f);
      if ((double) this.Projectile.localAI[0] == 0.0)
        this.Projectile.localAI[0] = Utils.NextBool(Main.rand) ? 1f : -1f;
      this.Projectile.rotation += 0.3f * this.Projectile.localAI[0];
      float num = this.Projectile.ai[0];
      if ((double) this.Timer <= 20.0 && (double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < (double) num)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.08f);
      }
      ++this.Timer;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      bool flag = SoulConfig.Instance.BossRecolors && WorldSavingSystem.EternityMode;
      Texture2D texture = flag ? ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/Masomode/PlanteraThornChakram", (AssetRequestMode) 2).Value : TextureAssets.Projectile[this.Type].Value;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color lightColor1 = Color.op_Multiply(flag ? Color.DimGray : Color.op_Multiply(Color.LimeGreen, 0.75f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 vector2 = Vector2.op_Addition(this.Projectile.oldPos[index], Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
        float num = this.Projectile.oldRot[index];
        FargoSoulsUtil.GenericProjectileDraw(this.Projectile, lightColor1, texture, new Vector2?(vector2), new float?(num));
      }
      FargoSoulsUtil.GenericProjectileDraw(this.Projectile, lightColor, texture);
      return false;
    }
  }
}
