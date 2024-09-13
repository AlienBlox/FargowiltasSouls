// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.KamikazeSquirrel
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class KamikazeSquirrel : ModProjectile
  {
    public int counter;
    private float realFrameCounter;
    private int realFrame;

    public virtual string Texture => "FargowiltasSouls/Content/NPCs/Critters/TophatSquirrelCritter";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 6;
      ProjectileID.Sets.MinionSacrificable[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.MinionTargettingFeature[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 30;
      this.Projectile.timeLeft *= 5;
      this.Projectile.aiStyle = 26;
      this.AIType = 266;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.penetrate = -1;
      this.Projectile.minionSlots = 0.333333343f;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 1;
    }

    public virtual bool? CanDamage() => new bool?(this.Projectile.timeLeft <= 0);

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      this.Projectile.timeLeft = 2;
      if (((Entity) player).whoAmI == Main.myPlayer && (!((Entity) player).active || player.dead || player.ghost))
      {
        this.Projectile.Kill();
      }
      else
      {
        if ((double) ((Entity) this.Projectile).velocity.X == 0.0)
        {
          this.realFrame = 0;
        }
        else
        {
          this.realFrameCounter += Math.Abs(((Entity) this.Projectile).velocity.X);
          if ((double) ++this.realFrameCounter > 6.0)
          {
            this.realFrameCounter = 0.0f;
            ++this.realFrame;
          }
          if (this.realFrame < 1 || this.realFrame >= Main.projFrames[this.Projectile.type])
            this.realFrame = 1;
        }
        this.Projectile.frame = this.realFrame;
      }
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity) => false;

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = (double) ((Entity) Main.player[this.Projectile.owner]).Center.Y > (double) ((Entity) this.Projectile).Bottom.Y;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual bool PreKill(int timeLeft) => base.PreKill(timeLeft);

    public virtual void OnKill(int timeLeft)
    {
      if (timeLeft != 1)
        return;
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 5, 0.0f, -1f, 0, new Color(), 1f);
        Main.dust[index2].scale += 0.5f;
      }
      if (!Main.dedServ)
      {
        int index = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(0.5f, Utils.RotatedByRandom(((Entity) this.Projectile).velocity, 0.78539818525314331)), ModContent.Find<ModGore>(((ModType) this).Mod.Name, Utils.NextBool(Main.rand) ? "TrojanSquirrelGore2" : "TrojanSquirrelGore2_2").Type, this.Projectile.scale);
        Main.gore[index].rotation = Utils.NextFloat(Main.rand, 6.28318548f);
        Main.gore[index].velocity.Y -= 6f;
      }
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 128;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      if (this.Projectile.owner == Main.myPlayer)
        this.Projectile.Damage();
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      SoundEngine.PlaySound(ref SoundID.NPCDeath1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index3 = 0; index3 < 20; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust = Main.dust[index4];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index5 = 0; index5 < 10; ++index5)
      {
        int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 2.5f);
        Main.dust[index6].noGravity = true;
        Dust dust1 = Main.dust[index6];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 5f);
        int index7 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1f);
        Dust dust2 = Main.dust[index7];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      for (int index8 = 0; index8 < 4; ++index8)
      {
        int index9 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore1 = Main.gore[index9];
        gore1.velocity = Vector2.op_Multiply(gore1.velocity, 0.4f);
        Gore gore2 = Main.gore[index9];
        gore2.velocity = Vector2.op_Addition(gore2.velocity, Utils.RotatedBy(new Vector2(1f, 1f), 1.5707963705062866 * (double) index8, new Vector2()));
      }
    }

    public virtual bool PreDraw(ref Color lightColor) => true;
  }
}
