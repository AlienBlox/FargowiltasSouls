// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.RefractorBlaster2Held
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class RefractorBlaster2Held : ModProjectile
  {
    private int syncTimer;
    private Vector2 mousePos;
    public int timer;
    public float lerp = 0.12f;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Items/Weapons/SwarmDrops/RefractorBlaster2";
    }

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 7;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 76;
      ((Entity) this.Projectile).height = 38;
      this.Projectile.alpha = 0;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.netImportant = true;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.mousePos.X);
      writer.Write(this.mousePos.Y);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      Vector2 vector2;
      vector2.X = reader.ReadSingle();
      vector2.Y = reader.ReadSingle();
      if (this.Projectile.owner == Main.myPlayer)
        return;
      this.mousePos = vector2;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (player.dead || !((Entity) player).active)
        this.Projectile.Kill();
      if (Main.player[this.Projectile.owner].HeldItem.type == ModContent.ItemType<RefractorBlaster2>())
      {
        this.Projectile.damage = Main.player[this.Projectile.owner].GetWeaponDamage(Main.player[this.Projectile.owner].HeldItem, false);
        this.Projectile.CritChance = player.GetWeaponCrit(player.HeldItem);
        this.Projectile.knockBack = Main.player[this.Projectile.owner].GetWeaponKnockback(Main.player[this.Projectile.owner].HeldItem, Main.player[this.Projectile.owner].HeldItem.knockBack);
      }
      ((Entity) this.Projectile).Center = player.MountedCenter;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      float num1 = (double) ((Entity) this.Projectile).direction * (double) player.gravDir < 0.0 ? 3.14159274f : 0.0f;
      float num2 = ((Entity) this.Projectile).direction < 0 ? 3.14159274f : 0.0f;
      player.itemRotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + num2;
      player.itemRotation = MathHelper.WrapAngle(player.itemRotation);
      player.ChangeDir(((Entity) this.Projectile).direction);
      player.heldProj = ((Entity) this.Projectile).whoAmI;
      player.itemTime = 10;
      player.itemAnimation = 10;
      Vector2 vector2 = Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 3), 0.0f), (double) MathHelper.WrapAngle(Utils.ToRotation(((Entity) this.Projectile).velocity)), new Vector2());
      Projectile projectile1 = this.Projectile;
      ((Entity) projectile1).Center = Vector2.op_Addition(((Entity) projectile1).Center, vector2);
      this.Projectile.spriteDirection = ((Entity) this.Projectile).direction * (int) player.gravDir;
      this.Projectile.rotation -= num1;
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter > 3)
      {
        ++this.Projectile.frame;
        if (this.Projectile.frame > Main.projFrames[this.Projectile.type] - 1)
          this.Projectile.frame = 0;
        this.Projectile.frameCounter = 0;
      }
      ((Entity) this.Projectile).velocity = Vector2.Lerp(Vector2.Normalize(((Entity) this.Projectile).velocity), Vector2.Normalize(Vector2.op_Subtraction(this.mousePos, player.MountedCenter)), this.lerp);
      ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
      if (this.Projectile.owner == Main.myPlayer)
      {
        this.mousePos = Main.MouseWorld;
        if (++this.syncTimer > 20)
        {
          this.syncTimer = 0;
          this.Projectile.netUpdate = true;
        }
        if (player.channel)
        {
          ++this.timer;
          SoundStyle soundStyle;
          if (this.timer % 6 == 0)
          {
            if (player.inventory[player.selectedItem].UseSound.HasValue)
            {
              soundStyle = player.inventory[player.selectedItem].UseSound.Value;
              SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
            }
            if (!player.CheckMana(player.inventory[player.selectedItem].mana, true, false))
              this.Projectile.Kill();
          }
          if (this.timer > 60)
          {
            int num3 = ModContent.ProjectileType<MechElectricOrbFriendly>();
            double num4 = 0.078539818525314331;
            int num5 = this.Projectile.damage / 3;
            for (int index = -10; index <= 10; ++index)
              Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 2f)), Vector2.op_Multiply(22f, Utils.RotatedBy(((Entity) this.Projectile).velocity, num4 * (double) index, new Vector2())), num3, num5, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
            soundStyle = SoundID.Item105;
            ((SoundStyle) ref soundStyle).Pitch = -0.3f;
            SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
            this.timer = 0;
          }
          ++this.Projectile.timeLeft;
          if ((double) this.Projectile.ai[1] == 0.0)
          {
            int num6 = ModContent.ProjectileType<PrimeDeathray>();
            int index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, num6, this.Projectile.damage, this.Projectile.knockBack, ((Entity) player).whoAmI, 0.0f, (float) this.Projectile.identity, 0.0f);
            if (index < Main.maxProjectiles)
              RefractorBlaster2Held.SplitProj(Main.projectile[index], 17);
            ++this.Projectile.ai[1];
          }
          else if (player.ownedProjectileCounts[ModContent.ProjectileType<PrimeDeathray>()] < 12)
            this.Projectile.Kill();
        }
        Projectile projectile2 = this.Projectile;
        ((Entity) projectile2).Center = Vector2.op_Addition(((Entity) projectile2).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 20f));
        if (player.channel)
          return;
        this.Projectile.Kill();
      }
      else
      {
        Projectile projectile3 = this.Projectile;
        ((Entity) projectile3).Center = Vector2.op_Addition(((Entity) projectile3).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 20f));
      }
    }

    public static void SplitProj(Projectile Projectile, int number)
    {
      if (number % 2 != 0)
        --number;
      double num1 = 1.5707963705062866 / (double) number;
      for (int index1 = 2; index1 < number / 2; ++index1)
      {
        for (int index2 = 0; index2 < 2; ++index2)
        {
          int num2 = index2 == 0 ? 1 : -1;
          float num3 = Projectile.type == ModContent.ProjectileType<PrimeDeathray>() ? (float) ((index1 + 1) * num2) : 0.0f;
          Projectile.NewProjectile(((Entity) Projectile).GetSource_FromThis((string) null), ((Entity) Projectile).Center, Utils.RotatedBy(((Entity) Projectile).velocity, (double) num2 * num1 * (double) (index1 + 1), new Vector2()), Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, num3, Projectile.ai[1], 0.0f);
        }
      }
      ((Entity) Projectile).active = false;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int width = TextureAssets.Projectile[this.Projectile.type].Value.Width;
      int num2 = num1 * this.Projectile.frame;
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, width, num1);
      Main.spriteBatch.Draw(texture2D, Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Rectangle?(rectangle), lightColor, this.Projectile.rotation, new Vector2((float) (width / 2), (float) (num1 / 2)), this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    public virtual void PostDraw(Color lightColor)
    {
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Items/Weapons/SwarmDrops/RefractorBlaster2_glow", (AssetRequestMode) 1).Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int width = TextureAssets.Projectile[this.Projectile.type].Value.Width;
      int num2 = num1 * this.Projectile.frame;
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, width, num1);
      Main.spriteBatch.Draw(texture2D, Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Rectangle?(rectangle), Color.White, this.Projectile.rotation, new Vector2((float) (width / 2), (float) (num1 / 2)), this.Projectile.scale, spriteEffects, 0.0f);
    }
  }
}
