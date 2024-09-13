// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Sasha.FishMinion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Minions;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Sasha
{
  public class FishMinion : HoverShooter
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
      Main.projFrames[this.Projectile.type] = 4;
      Main.projPet[this.Projectile.type] = true;
      ProjectileID.Sets.MinionSacrificable[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.minionSlots = 1f;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 18000;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1.5f;
      this.Projectile.extraUpdates = 1;
      this.Shoot = ModContent.ProjectileType<WaterStream>();
      this.ChaseAccel = 12f;
      this.Inertia = 20f;
      this.ShootSpeed = 20f;
      this.ShootCool = 45f;
      this.ViewDist = 600f;
    }

    public override void CheckActive()
    {
      Player player = Main.player[this.Projectile.owner];
      PatreonPlayer modPlayer = player.GetModPlayer<PatreonPlayer>();
      if (player.dead)
        modPlayer.FishMinion = false;
      if (!modPlayer.FishMinion)
        return;
      this.Projectile.timeLeft = 2;
    }

    public override void CreateDust()
    {
      Lighting.AddLight((int) ((double) ((Entity) this.Projectile).Center.X / 16.0), (int) ((double) ((Entity) this.Projectile).Center.Y / 16.0), 0.6f, 0.9f, 0.3f);
    }

    public override void SelectFrame()
    {
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter < 4)
        return;
      this.Projectile.frameCounter = 0;
      this.Projectile.frame = (this.Projectile.frame + 1) % 4;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 10;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), 0.75f), 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
