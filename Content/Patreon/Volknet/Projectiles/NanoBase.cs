// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Volknet.Projectiles.NanoBase
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Volknet.Projectiles
{
  public class NanoBase : ModProjectile
  {
    public int AtkTimer;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 4;
      ((Entity) this.Projectile).height = 4;
      this.Projectile.hide = true;
      this.Projectile.friendly = false;
      this.Projectile.hostile = false;
      this.Projectile.timeLeft = 2;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.damage = 1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().CanSplit = false;
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      behindProjectiles.Add(index);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Player player = Main.player[this.Projectile.owner];
      if (player.channel && player.GetModPlayer<NanoPlayer>().NanoCoreMode == 1 && NPCUtils.AnyProj(ModContent.ProjectileType<NanoProbe>(), ((Entity) player).whoAmI))
      {
        bool flag = true;
        foreach (Projectile projectile in Main.projectile)
        {
          if (((Entity) projectile).active && projectile.type == ModContent.ProjectileType<NanoProbe>() && projectile.owner == ((Entity) player).whoAmI && (double) projectile.ai[1] == 0.0)
            flag = false;
        }
        if (flag)
        {
          Vector2 rotationVector2 = Utils.ToRotationVector2(this.Projectile.rotation);
          Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(rotationVector2, 40f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(rotationVector2) + 1.57079637f), 30f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(rotationVector2) + 2.3561945f), 60f));
          Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(rotationVector2, 40f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(rotationVector2) - 1.57079637f), 30f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(rotationVector2) - 2.3561945f), 60f));
          Utils.DrawLine(Main.spriteBatch, vector2_1, vector2_2, Color.LightGreen, Color.DarkGreen, 3f);
        }
      }
      return false;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      if (((Entity) Main.player[this.Projectile.owner]).active)
      {
        Player owner = Main.player[this.Projectile.owner];
        if (!owner.dead && owner.HeldItem.type == ModContent.ItemType<NanoCore>())
        {
          this.Projectile.damage = owner.GetWeaponDamage(owner.HeldItem, false);
          this.Projectile.CritChance = owner.GetWeaponCrit(owner.HeldItem);
          this.Projectile.timeLeft = 2;
          ((Entity) this.Projectile).Center = ((Entity) owner).Center;
          this.Projectile.rotation = Utils.ToRotation(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) owner).Center));
          if (owner.channel)
          {
            owner.itemTime = 2;
            owner.itemAnimation = 2;
          }
          if (owner.ownedProjectileCounts[ModContent.ProjectileType<NanoProbe>()] < 7)
          {
            int num = 7 - owner.ownedProjectileCounts[ModContent.ProjectileType<NanoProbe>()];
            for (int index = 0; index < num; ++index)
              Projectile.NewProjectile(owner.GetSource_ItemUse(owner.HeldItem, (string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.ToRotationVector2((float) ((double) this.Projectile.rotation + (double) Utils.NextFloat(Main.rand) * 3.1415927410125732 / 3.0 * 2.0 - 1.0471975803375244)), 14f), ModContent.ProjectileType<NanoProbe>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
          }
          int num1 = 0;
          foreach (Projectile projectile in Main.projectile)
          {
            if (((Entity) projectile).active && projectile.type == ModContent.ProjectileType<NanoProbe>() && projectile.owner == this.Projectile.owner)
            {
              projectile.ai[0] = (float) num1;
              ++num1;
            }
          }
          if (owner.GetModPlayer<NanoPlayer>().NanoCoreMode == 0 && NanoBase.AllSet(owner) && !NPCUtils.AnyProj(ModContent.ProjectileType<NanoBlade>(), ((Entity) owner).whoAmI))
            Projectile.NewProjectile(owner.GetSource_ItemUse(owner.HeldItem, (string) null), ((Entity) owner).Center, Vector2.Zero, ModContent.ProjectileType<NanoBlade>(), 0, this.Projectile.knockBack, ((Entity) owner).whoAmI, 0.0f, 1.5f, 0.0f);
          if (owner.GetModPlayer<NanoPlayer>().NanoCoreMode == 1)
          {
            if (this.AtkTimer > 0)
              --this.AtkTimer;
            if (NanoBase.AllSet(owner) && this.AtkTimer == 0)
            {
              this.AtkTimer = 6;
              SoundEngine.PlaySound(ref SoundID.Item75, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
              bool flag1 = Utils.NextBool(Main.rand, 4);
              bool flag2 = true;
              int num2;
              float num3;
              int num4;
              float num5;
              int num6;
              if (owner.PickAmmo(owner.HeldItem, ref num2, ref num3, ref num4, ref num5, ref num6, !flag1))
              {
                float num7 = num3 * 4f + 64f;
                if (Utils.NextBool(Main.rand, 4))
                {
                  num2 = ModContent.ProjectileType<PlasmaArrow>();
                  num4 = (int) ((double) num4 * 2.0);
                  num7 = 3f;
                }
                int num8 = (int) ((double) num4 / 1.75);
                if (flag2)
                {
                  Projectile.NewProjectile(owner.GetSource_ItemUse(owner.HeldItem, (string) null), Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(((Entity) owner).Center, Utils.NextVector2Circular(Main.rand, 8f, 8f)), Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation + 1.57079637f), 15f)), Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 35f)), Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), num7), 0.8f), num2, num8, num5, ((Entity) owner).whoAmI, 0.0f, 0.0f, 0.0f);
                  Projectile.NewProjectile(owner.GetSource_ItemUse(owner.HeldItem, (string) null), Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(((Entity) owner).Center, Utils.NextVector2Circular(Main.rand, 8f, 8f)), Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation - 1.57079637f), 15f)), Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 35f)), Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), num7), 0.8f), num2, num8, num5, ((Entity) owner).whoAmI, 0.0f, 0.0f, 0.0f);
                  Projectile.NewProjectile(owner.GetSource_ItemUse(owner.HeldItem, (string) null), Vector2.op_Addition(Vector2.op_Addition(((Entity) owner).Center, Utils.NextVector2Circular(Main.rand, 8f, 8f)), Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 35f)), Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), num7), num2, num8, num5, ((Entity) owner).whoAmI, 0.0f, 0.0f, 0.0f);
                }
              }
            }
          }
          if (owner.GetModPlayer<NanoPlayer>().NanoCoreMode == 2)
          {
            if (owner.channel)
            {
              if (NanoBase.AllSet(owner))
              {
                if (!owner.CheckMana(2, true, false))
                {
                  owner.channel = false;
                  this.AtkTimer = 180;
                  return;
                }
                owner.manaRegenDelay = 10f;
                if (!NPCUtils.AnyProj(ModContent.ProjectileType<PlasmaDeathRay>(), ((Entity) owner).whoAmI))
                {
                  Vector2 vector2 = Vector2.op_Addition(((Entity) owner).Center, Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) owner).Center)), 130f));
                  float num9 = 0.33f;
                  for (int index = 0; index < 9; ++index)
                  {
                    if ((double) Utils.NextFloat(Main.rand) >= (double) num9)
                    {
                      float num10 = Utils.NextFloat(Main.rand) * 6.28318548f;
                      float num11 = Utils.NextFloat(Main.rand);
                      Dust dust = Dust.NewDustPerfect(Vector2.op_Addition(vector2, Vector2.op_Multiply(Utils.ToRotationVector2(num10), (float) (110.0 + 200.0 * (double) num11))), 157, new Vector2?(Vector2.op_Multiply(Utils.ToRotationVector2(num10 - 3.14159274f), (float) (14.0 + 8.0 * (double) num11))), 0, new Color(), 1f);
                      dust.scale = 0.9f;
                      dust.fadeIn = (float) (1.1499999761581421 + (double) num11 * 0.30000001192092896);
                      dust.noGravity = true;
                      dust.customData = (object) owner;
                    }
                  }
                }
                if (this.AtkTimer > 0)
                  --this.AtkTimer;
                if (this.AtkTimer == 0)
                {
                  this.AtkTimer = 180;
                  if (!Main.dedServ)
                  {
                    SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Zombie_104", (SoundType) 0);
                    SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
                  }
                  Projectile.NewProjectile(owner.GetSource_ItemUse(owner.HeldItem, (string) null), ((Entity) owner).Center, Vector2.Zero, ModContent.ProjectileType<PlasmaDeathRay>(), (int) ((double) this.Projectile.damage * 2.5), this.Projectile.knockBack, ((Entity) owner).whoAmI, 0.0f, 0.0f, 0.0f);
                }
              }
              foreach (Dust dust1 in Main.dust)
              {
                if (dust1.active && dust1.type == 157 && dust1.customData != null && dust1.customData is Player customData)
                {
                  Dust dust2 = dust1;
                  dust2.position = Vector2.op_Addition(dust2.position, Vector2.op_Subtraction(((Entity) customData).position, ((Entity) customData).oldPosition));
                }
              }
            }
            else
              this.AtkTimer = 120;
          }
          if (owner.GetModPlayer<NanoPlayer>().NanoCoreMode != 3)
            return;
          if (owner.channel)
          {
            if (!owner.CheckMana(2, true, false))
            {
              owner.channel = false;
              this.AtkTimer = 0;
            }
            else
            {
              owner.manaRegenDelay = 10f;
              this.AtkTimer = (this.AtkTimer + 1) % 30;
              if (this.AtkTimer % 5 != 3)
                return;
              foreach (Projectile projectile in Main.projectile)
              {
                if (((Entity) projectile).active && projectile.type == ModContent.ProjectileType<NanoProbe>() && projectile.owner == ((Entity) owner).whoAmI && (double) projectile.ai[1] != 0.0 && ((double) projectile.ai[0] == (double) (this.AtkTimer / 5) || (double) projectile.ai[0] == (double) (this.AtkTimer / 5 + 1) || (double) projectile.ai[0] == 6.0))
                {
                  SoundEngine.PlaySound(ref SoundID.Item91, new Vector2?(((Entity) projectile).Center), (SoundUpdateCallback) null);
                  int num12 = (int) ((double) this.Projectile.damage * 1.05 / 2.0);
                  Projectile.NewProjectile(owner.GetSource_ItemUse(owner.HeldItem, (string) null), ((Entity) projectile).Center, Vector2.op_Multiply(Utils.RotatedByRandom(Utils.ToRotationVector2(projectile.rotation), (double) MathHelper.ToRadians(2f)), 36f), ModContent.ProjectileType<PlasmaProj>(), num12, projectile.knockBack, ((Entity) owner).whoAmI, 0.0f, 0.0f, 0.0f);
                }
              }
            }
          }
          else
            this.AtkTimer = 0;
        }
        else
          this.Projectile.Kill();
      }
      else
        this.Projectile.Kill();
    }

    public static bool AllSet(Player owner)
    {
      bool channel = owner.channel;
      bool flag1 = NPCUtils.AnyProj(ModContent.ProjectileType<NanoProbe>(), ((Entity) owner).whoAmI);
      bool flag2 = true;
      foreach (Projectile projectile in Main.projectile)
      {
        if (((Entity) projectile).active && projectile.type == ModContent.ProjectileType<NanoProbe>() && projectile.owner == ((Entity) owner).whoAmI && (double) projectile.ai[1] == 0.0)
          flag2 = false;
      }
      return channel & flag1 & flag2;
    }
  }
}
