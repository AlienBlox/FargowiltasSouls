// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Earth.EarthPalladOrb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Earth
{
  public class EarthPalladOrb : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Souls/PalladOrb";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 1200;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 2f;
      this.Projectile.extraUpdates = 3;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[1] == 0.0)
      {
        this.Projectile.localAI[1] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if (this.Projectile.timeLeft % (this.Projectile.extraUpdates + 1) == 0 && (double) ++this.Projectile.localAI[1] > 30.0)
      {
        if ((double) this.Projectile.localAI[1] < 90.0)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.035f);
        }
        if ((double) this.Projectile.localAI[1] > 60.0 && (double) this.Projectile.localAI[1] < 150.0 && FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.championBoss, ModContent.NPCType<EarthChampion>()) && Main.npc[EModeGlobalNPC.championBoss].HasValidTarget)
        {
          float rotation1 = Utils.ToRotation(((Entity) this.Projectile).velocity);
          float rotation2 = Utils.ToRotation(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) Main.player[Main.npc[EModeGlobalNPC.championBoss].target]).Center, Vector2.op_Multiply(((Entity) Main.player[Main.npc[EModeGlobalNPC.championBoss].target]).velocity, 10f)), ((Entity) this.Projectile).Center));
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(new Vector2(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), 0.0f), (double) Utils.AngleLerp(rotation1, rotation2, 0.03f), new Vector2());
        }
      }
      int index1 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, Utils.NextBool(Main.rand) ? 174 : 259, 0.0f, 0.0f, 100, new Color(), 2f);
      Main.dust[index1].noGravity = true;
      Dust dust1 = Main.dust[index1];
      dust1.velocity = Vector2.op_Multiply(dust1.velocity, 3f);
      int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, Utils.NextBool(Main.rand) ? 174 : 259, 0.0f, 0.0f, 100, new Color(), 1f);
      Dust dust2 = Main.dust[index2];
      dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      Main.dust[index2].noGravity = true;
      this.Projectile.rotation += 0.4f;
      if (++this.Projectile.frameCounter <= 3)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame < Main.projFrames[this.Projectile.type])
        return;
      this.Projectile.frame = 0;
    }

    public virtual void OnKill(int timeLeft)
    {
      if (timeLeft > 0)
      {
        this.Projectile.timeLeft = 0;
        ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
        ((Entity) this.Projectile).width = 500;
        ((Entity) this.Projectile).height = 500;
        ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
        this.Projectile.penetrate = -1;
        this.Projectile.Damage();
      }
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      for (int index4 = 0; index4 < 20; ++index4)
      {
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, Utils.NextBool(Main.rand) ? 174 : 259, 0.0f, 0.0f, 100, new Color(), 4f);
        Main.dust[index5].noGravity = true;
        Dust dust3 = Main.dust[index5];
        dust3.velocity = Vector2.op_Multiply(dust3.velocity, 21f * this.Projectile.scale);
        int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, Utils.NextBool(Main.rand) ? 174 : 259, 0.0f, 0.0f, 100, new Color(), 2.5f);
        Dust dust4 = Main.dust[index6];
        dust4.velocity = Vector2.op_Multiply(dust4.velocity, 12f);
        Main.dust[index6].noGravity = true;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.AddBuff(ModContent.BuffType<PurifiedBuff>(), 300, true, false);
        target.AddBuff(ModContent.BuffType<LethargicBuff>(), 300, true, false);
      }
      if (this.Projectile.timeLeft <= 0)
        return;
      this.Projectile.Kill();
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }
  }
}
