// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.PlanteraMushroomThing
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class PlanteraMushroomThing : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Type] = 3;
      ProjectileID.Sets.TrailCacheLength[this.Type] = 3;
      ProjectileID.Sets.TrailingMode[this.Type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(276);
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = 0;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 24;
    }

    public virtual void AI()
    {
      if ((!SoulConfig.Instance.BossRecolors ? 0 : (WorldSavingSystem.EternityMode ? 1 : 0)) != 0)
        Lighting.AddLight(((Entity) this.Projectile).Center, 0.09803922f, 0.184313729f, 0.2509804f);
      else
        Lighting.AddLight(((Entity) this.Projectile).Center, 0.1f, 0.4f, 0.2f);
      if ((double) this.Projectile.localAI[0] != 1.0)
      {
        SoundEngine.PlaySound(ref SoundID.NPCDeath13, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        this.Projectile.localAI[0] = 1f;
      }
      if (++this.Projectile.frameCounter > 4)
      {
        if (++this.Projectile.frame >= Main.projFrames[this.Type])
          this.Projectile.frame = 0;
        this.Projectile.frameCounter = 0;
      }
      if ((double) this.Projectile.ai[0] < 20.0)
        this.Projectile.ai[0] += 0.5f;
      float num1 = this.Projectile.ai[0] + 4f;
      int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 1, 1);
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[closest]).Center, ((Entity) this.Projectile).Center);
      ((Vector2) ref vector2_1).Normalize();
      Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, num1);
      int num2 = 70;
      ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, (float) (num2 - 1)), vector2_2), (float) num2);
      if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() != (double) this.Projectile.ai[0])
      {
        ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, this.Projectile.ai[0]);
      }
      this.Projectile.tileCollide = false;
      if (this.Projectile.timeLeft > 180)
        this.Projectile.timeLeft = 180;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<IvyVenomBuff>(), 240, true, false);
      target.AddBuff(20, 300, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      bool flag = SoulConfig.Instance.BossRecolors && WorldSavingSystem.EternityMode;
      Texture2D texture = flag ? TextureAssets.Projectile[this.Type].Value : ModContent.Request<Texture2D>(this.Texture + "Vanilla", (AssetRequestMode) 2).Value;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color lightColor1 = Color.op_Multiply(Color.op_Multiply(flag ? Color.Blue : Color.op_Multiply(Color.Pink, 0.75f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]), 0.5f);
        Vector2 vector2 = Vector2.op_Addition(this.Projectile.oldPos[index], Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
        float num = this.Projectile.oldRot[index];
        FargoSoulsUtil.GenericProjectileDraw(this.Projectile, lightColor1, texture, new Vector2?(vector2), new float?(num));
      }
      FargoSoulsUtil.GenericProjectileDraw(this.Projectile, Color.White, texture);
      return false;
    }
  }
}
