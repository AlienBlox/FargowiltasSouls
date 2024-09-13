// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Vortex.LightningTelegraph
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Will;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Vortex
{
  public class LightningTelegraph : ModProjectile
  {
    private static readonly SoundStyle LightningSound = new SoundStyle("FargowiltasSouls/Assets/Sounds/LightningStrike", (SoundType) 0);

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 12;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 14;
      ((Entity) this.Projectile).height = 44;
      this.Projectile.DamageType = DamageClass.Default;
      this.Projectile.friendly = false;
      this.Projectile.hostile = false;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 80;
      this.Projectile.scale = 1f;
      this.AIType = 0;
      this.Projectile.aiStyle = 0;
    }

    public virtual void AI()
    {
    }

    public virtual void OnKill(int timeLeft)
    {
      if ((double) this.Projectile.ai[0] != 13.0)
        return;
      SoundStyle lightningSound = LightningTelegraph.LightningSound;
      ((SoundStyle) ref lightningSound).MaxInstances = 4;
      ((SoundStyle) ref lightningSound).Volume = 0.2f;
      SoundEngine.PlaySound(ref lightningSound, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
      if (!FargoSoulsUtil.HostCheck)
        return;
      Vector2 unitY = Vector2.UnitY;
      Vector2 vector2 = Vector2.Normalize(unitY);
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 6f), ModContent.ProjectileType<VortexLightningDeathray>(), this.Projectile.damage, 0.0f, Main.myPlayer, Utils.ToRotation(unitY), 1f, 0.0f);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Color red = Color.Red;
      ((Color) ref red).A = (byte) 0;
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = texture2D.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), red, this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
