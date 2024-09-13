// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Timber.TimberChampionHead
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.TrojanSquirrel;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Placables.Relics;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ItemDropRules;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Timber
{
  [AutoloadBossHead]
  public class TimberChampionHead : ModNPC
  {
    private bool haveGottenInRange;
    private bool noHurt;

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 3;
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 8;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 6);
      Span<int> span = CollectionsMarshal.AsSpan<int>(debuffs);
      int num1 = 0;
      span[num1] = 31;
      int num2 = num1 + 1;
      span[num2] = 46;
      int num3 = num2 + 1;
      span[num3] = 24;
      int num4 = num3 + 1;
      span[num4] = 68;
      int num5 = num4 + 1;
      span[num5] = ModContent.BuffType<LethargicBuff>();
      int num6 = num5 + 1;
      span[num6] = ModContent.BuffType<ClippedWingsBuff>();
      int num7 = num6 + 1;
      npc.AddDebuffImmunities(debuffs);
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 100;
      ((Entity) this.NPC).height = 100;
      this.NPC.damage = 140;
      this.NPC.defense = 50;
      this.NPC.lifeMax = 160000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit7);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      this.NPC.value = (float) Item.buyPrice(1, 0, 0, 0);
      this.NPC.boss = true;
      Mod mod;
      this.Music = Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod) ? MusicLoader.GetMusicSlot(mod, "Assets/Music/Champions") : 81;
      this.SceneEffectPriority = (SceneEffectPriority) 6;
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * (double) balance);
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot)
    {
      CooldownSlot = 1;
      return !this.noHurt;
    }

    public virtual void AI()
    {
      if ((double) this.NPC.localAI[2] == 0.0)
      {
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.NPC.TargetClosest(false);
        this.NPC.localAI[2] = 1f;
      }
      EModeGlobalNPC.championBoss = ((Entity) this.NPC).whoAmI;
      Player player = Main.player[this.NPC.target];
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1 : -1;
      if ((double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 1200.0)
        this.haveGottenInRange = true;
      this.noHurt = false;
      switch ((int) this.NPC.ai[0])
      {
        case 0:
        case 2:
        case 4:
        case 9:
        case 11:
        case 13:
          if (this.haveGottenInRange && (!((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 2400.0))
          {
            this.NPC.TargetClosest(false);
            if (this.NPC.timeLeft > 30)
              this.NPC.timeLeft = 30;
            this.NPC.noTileCollide = true;
            this.NPC.noGravity = true;
            --((Entity) this.NPC).velocity.Y;
            break;
          }
          Vector2 targetPos1 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), 400f));
          if ((double) ((Entity) this.NPC).Distance(targetPos1) > 50.0)
            this.Movement(targetPos1, 0.2f, 24f);
          if ((double) ++this.NPC.ai[1] <= 60.0)
            break;
          this.NPC.TargetClosest(true);
          ++this.NPC.ai[0];
          this.NPC.ai[1] = 0.0f;
          this.NPC.ai[2] = 0.0f;
          this.NPC.ai[3] = 0.0f;
          this.NPC.netUpdate = true;
          break;
        case 1:
          if ((double) this.NPC.ai[1] == 0.0)
          {
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Division(Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center), 120f), ModContent.ProjectileType<TimberSquirrel>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, this.NPC.ai[3], (float) ((Entity) this.NPC).whoAmI, 0.0f);
          }
          if ((double) this.NPC.ai[1] < 60.0)
          {
            Vector2 center = ((Entity) player).Center;
            center.X += (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? -200f : 200f;
            center.Y -= 200f;
            if ((double) ((Entity) this.NPC).Distance(center) > 50.0)
              this.Movement(center, 0.2f, 24f);
          }
          else
          {
            NPC npc = this.NPC;
            ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.9f);
          }
          if ((double) ++this.NPC.ai[1] < 120.0)
          {
            if ((double) this.NPC.ai[3] != 0.0)
              break;
            if ((double) this.NPC.ai[1] == 90.0)
            {
              ((Entity) this.NPC).velocity = Vector2.Zero;
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(Vector2.UnitY), ModContent.ProjectileType<GlowLine>(), 0, 0.0f, Main.myPlayer, 19f, 0.0f, 0.0f);
            }
            if ((double) this.NPC.ai[1] <= 90.0 || (double) this.NPC.ai[1] % 3.0 != 0.0)
              break;
            float num = 192f * ((this.NPC.ai[1] - 90f) / 3f);
            for (int index = -1; index <= 1; index += 2)
            {
              Vector2 vector2;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2).\u002Ector(((Entity) this.NPC).Center.X + num * (float) index, ((Entity) player).Center.Y + 1500f);
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2, Vector2.op_UnaryNegation(Vector2.UnitY), ModContent.ProjectileType<GlowLine>(), 0, 0.0f, Main.myPlayer, 19f, 0.0f, 0.0f);
            }
            break;
          }
          if ((double) this.NPC.ai[1] == 120.0)
            break;
          if ((double) this.NPC.ai[1] < 270.0)
          {
            if ((double) this.NPC.ai[3] != 0.0)
              break;
            if ((double) this.NPC.ai[1] % 3.0 == 0.0)
              SoundEngine.PlaySound(ref SoundID.Item157, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            for (int index = 0; index < 2; ++index)
            {
              Vector2 center = ((Entity) player).Center;
              center.X += Utils.NextFloat(Main.rand, -1000f, 1000f);
              center.Y -= Utils.NextFloat(Main.rand, 600f, 800f);
              Vector2 vector2 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.NextFloat(Main.rand, 7.5f, 12.5f), Vector2.UnitY), 1f);
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), center, vector2, ModContent.ProjectileType<TimberLaser>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
            }
            break;
          }
          if (((IEnumerable<Projectile>) Main.projectile).Any<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.type == ModContent.ProjectileType<TimberSquirrel>() && (double) ((Entity) this.NPC).whoAmI == (double) p.ai[1])))
            break;
          this.NPC.TargetClosest(true);
          ++this.NPC.ai[0];
          this.NPC.ai[1] = 0.0f;
          this.NPC.ai[2] = 0.0f;
          this.NPC.ai[3] = 0.0f;
          this.NPC.netUpdate = true;
          break;
        case 3:
        case 12:
          Vector2 center1 = ((Entity) player).Center;
          center1.X += (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? -400f : 400f;
          center1.Y -= 100f;
          if ((double) ((Entity) this.NPC).Distance(center1) > 50.0)
            this.Movement(center1, 0.25f, 32f);
          ++this.NPC.ai[2];
          if ((double) this.NPC.ai[3] > 2.0 && WorldSavingSystem.EternityMode)
          {
            bool flag = (double) this.NPC.ai[2] == 1.0;
            NPC npc = this.NPC;
            ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.9f);
            this.NPC.ai[1] -= 0.5f;
            if ((double) this.NPC.ai[2] > 60.0)
            {
              this.NPC.ai[2] -= 15f;
              Vector2 vector2 = Vector2.op_Multiply(20f, ((Entity) player).DirectionFrom(((Entity) this.NPC).Center));
              int num = (int) this.NPC.ai[3]++ - 2;
              for (int index = -num; index <= num; ++index)
              {
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(75f) / (double) num * (double) index, new Vector2()), ModContent.ProjectileType<TimberSnowball>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, 0.0f, 0.0f);
              }
              flag = true;
            }
            if (flag)
            {
              SoundEngine.PlaySound(ref SoundID.Item36, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              SoundEngine.PlaySound(ref SoundID.Item11, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              for (int index1 = 0; index1 < 20; ++index1)
              {
                int index2 = Dust.NewDust(((Entity) this.NPC).Center, 0, 0, 51, 0.0f, 0.0f, 0, new Color(), 3f);
                Main.dust[index2].noGravity = true;
                Dust dust = Main.dust[index2];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
                Main.dust[index2].velocity.X += (float) ((Entity) this.NPC).direction * Utils.NextFloat(Main.rand, 6f, 24f);
              }
            }
          }
          else if ((double) this.NPC.ai[2] > 35.0)
          {
            this.NPC.ai[2] = 0.0f;
            ++this.NPC.ai[3];
            float num = WorldSavingSystem.MasochistModeReal ? 40f : 30f;
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center);
            if (WorldSavingSystem.MasochistModeReal)
              vector2.X += ((Entity) player).velocity.X * num;
            vector2.X /= num;
            vector2.Y = (float) ((double) vector2.Y / (double) num - 0.10000000149011612 * (double) num);
            for (int index = 0; index < 20; ++index)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Addition(vector2, Vector2.op_Multiply(Utils.NextVector2Square(Main.rand, -0.5f, 0.5f), 3f)), ModContent.ProjectileType<TimberAcorn>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
          if ((double) ++this.NPC.ai[1] <= 200.0)
            break;
          this.NPC.TargetClosest(true);
          ++this.NPC.ai[0];
          this.NPC.ai[1] = 0.0f;
          this.NPC.ai[2] = 0.0f;
          this.NPC.ai[3] = 0.0f;
          this.NPC.netUpdate = true;
          break;
        case 5:
          Vector2 targetPos2 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), 300f));
          if ((double) targetPos2.Y > (double) ((Entity) player).position.Y - 100.0)
            targetPos2.Y = ((Entity) player).position.Y - 100f;
          if ((double) ((Entity) this.NPC).Distance(targetPos2) > 50.0)
            this.Movement(targetPos2, 0.1f);
          if ((double) --this.NPC.ai[2] < 0.0)
          {
            this.NPC.ai[2] = 70f;
            if ((double) this.NPC.ai[1] < 300.0)
            {
              for (int index3 = 0; index3 < 5; ++index3)
              {
                Vector2 center2 = ((Entity) player).Center;
                center2.X += Utils.NextFloat(Main.rand, -1500f, 1500f) + ((Entity) player).velocity.X * 75f;
                center2.Y -= Utils.NextFloat(Main.rand, 300f);
                for (int index4 = 0; index4 < 100; ++index4)
                {
                  Tile tile = ((Tilemap) ref Main.tile)[(int) center2.X / 16, (int) center2.Y / 16];
                  if (Tile.op_Equality(tile, (ArgumentException) null))
                    tile = new Tile();
                  if (!((Tile) ref tile).HasUnactuatedTile || !Main.tileSolid[(int) ((Tile) ref tile).TileType] && !Main.tileSolidTop[(int) ((Tile) ref tile).TileType])
                    center2.Y += 16f;
                  else
                    break;
                }
                for (int index5 = 0; index5 < 50; ++index5)
                {
                  Tile tile = ((Tilemap) ref Main.tile)[(int) center2.X / 16, (int) center2.Y / 16];
                  if (Tile.op_Equality(tile, (ArgumentException) null))
                    tile = new Tile();
                  if (((Tile) ref tile).HasUnactuatedTile && (Main.tileSolid[(int) ((Tile) ref tile).TileType] || Main.tileSolidTop[(int) ((Tile) ref tile).TileType]))
                    center2.Y -= 16f;
                  else
                    break;
                }
                float num = 90f;
                Vector2 vector2 = Vector2.op_Subtraction(center2, ((Entity) this.NPC).Center);
                vector2.X /= num;
                vector2.Y = (float) ((double) vector2.Y / (double) num - 0.10000000149011612 * (double) num);
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<TimberTreeAcorn>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) this.NPC.target, 0.0f, 0.0f);
              }
            }
          }
          if ((double) ++this.NPC.ai[1] <= 390.0)
            break;
          this.NPC.TargetClosest(true);
          ++this.NPC.ai[0];
          this.NPC.ai[1] = 0.0f;
          this.NPC.ai[2] = 0.0f;
          this.NPC.ai[3] = 0.0f;
          this.NPC.netUpdate = true;
          break;
        case 6:
          if (!WorldSavingSystem.MasochistModeReal)
          {
            this.NPC.ai[1] -= 0.5f;
            goto case 0;
          }
          else
            goto case 0;
        case 7:
          this.noHurt = true;
          int num1 = 240;
          int num2 = 60;
          if ((double) this.NPC.ai[1] < (double) num1)
          {
            NPC npc = this.NPC;
            ((Entity) npc).position = Vector2.op_Addition(((Entity) npc).position, Vector2.op_Division(Vector2.op_Subtraction(((Entity) player).position, ((Entity) player).oldPosition), 2f));
            if (WorldSavingSystem.EternityMode)
            {
              Vector2 targetPos3 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(150f, Utils.RotatedBy(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), (double) MathHelper.ToRadians(10f), new Vector2())));
              ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, targetPos3), ((Vector2) ref ((Entity) this.NPC).velocity).Length());
              this.Movement(targetPos3, 0.25f, 24f);
            }
            else
            {
              Vector2 targetPos4 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(150f, ((Entity) this.NPC).DirectionFrom(((Entity) player).Center)));
              this.Movement(targetPos4, 0.25f, 24f);
              if ((double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 150.0)
                this.Movement(targetPos4, 0.5f, 24f);
            }
            if ((double) ++this.NPC.ai[2] > 8.0)
            {
              this.NPC.ai[2] = 0.0f;
              SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              Vector2 vector2 = Vector2.op_Multiply(32f, Utils.RotatedByRandom(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), Math.PI / 2.0));
              float num3 = (float) (num1 + num2) - this.NPC.ai[1];
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<TimberHook2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, num3, 0.0f);
            }
          }
          else
          {
            NPC npc = this.NPC;
            ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.9f);
          }
          if ((double) ++this.NPC.ai[1] <= (double) (num1 + num2))
            break;
          this.NPC.TargetClosest(true);
          ++this.NPC.ai[0];
          this.NPC.ai[1] = 0.0f;
          this.NPC.ai[2] = 0.0f;
          this.NPC.ai[3] = 0.0f;
          this.NPC.netUpdate = true;
          break;
        case 8:
          ((Entity) this.NPC).velocity = Vector2.Zero;
          if ((double) this.NPC.ai[1] == 0.0)
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if ((double) ++this.NPC.ai[1] <= 120.0)
            break;
          this.NPC.TargetClosest(true);
          ++this.NPC.ai[0];
          this.NPC.ai[1] = 0.0f;
          this.NPC.ai[2] = 0.0f;
          this.NPC.ai[3] = 0.0f;
          this.NPC.netUpdate = true;
          break;
        case 10:
          this.NPC.ai[3] = 1f;
          goto case 1;
        case 14:
          if ((double) ++this.NPC.ai[3] < 180.0)
          {
            Vector2 center3 = ((Entity) player).Center;
            center3.Y -= 200f;
            this.Movement(center3, 0.6f, 32f);
            if ((double) ((Entity) this.NPC).Center.Y - (double) ((Entity) player).Center.Y >= -200.0)
              break;
            this.NPC.ai[3] = 180f;
            NPC npc = this.NPC;
            ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.5f);
            break;
          }
          int num4 = 6;
          if (WorldSavingSystem.MasochistModeReal)
          {
            Vector2 center4 = ((Entity) player).Center;
            center4.X += ((Entity) player).velocity.X * 45f;
            center4.Y -= 200f;
            this.Movement(center4, 0.5f, 32f);
            if ((double) ++this.NPC.ai[2] > 5.0 && (double) this.NPC.ai[1] < 420.0)
            {
              this.NPC.ai[2] = 0.0f;
              if (FargoSoulsUtil.HostCheck)
              {
                int num5 = (double) this.NPC.ai[1] > 120.0 ? FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage) : 0;
                for (int index = -2; index <= 2; ++index)
                {
                  Vector2 vector2;
                  // ISSUE: explicit constructor call
                  ((Vector2) ref vector2).\u002Ector(5f * (float) index, -20f);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<TimberSnowball2>(), num5, 0.0f, Main.myPlayer, (float) this.NPC.target, (float) ((Entity) this.NPC).whoAmI, 0.0f);
                }
              }
            }
            if ((double) ++this.NPC.ai[1] <= 510.0)
              break;
            this.NPC.TargetClosest(true);
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            this.NPC.netUpdate = true;
            break;
          }
          Vector2 center5 = ((Entity) player).Center;
          center5.X += (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? -200f : 200f;
          center5.Y -= 200f;
          this.Movement(center5, 0.25f, 32f);
          NPC npc1 = this.NPC;
          ((Entity) npc1).velocity = Vector2.op_Multiply(((Entity) npc1).velocity, 0.9f);
          ++this.NPC.ai[1];
          int num6 = WorldSavingSystem.MasochistModeReal ? 180 : 240;
          if ((double) this.NPC.ai[1] % (double) num4 == 0.0)
          {
            Vector2 vector2;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2).\u002Ector(((Entity) this.NPC).Center.X, ((Entity) Main.player[this.NPC.target]).Center.Y);
            for (int index = -3; index <= 3; ++index)
            {
              if ((double) this.NPC.ai[1] * (double) (Math.Abs(index) + 1) >= 60.0)
              {
                Vector2 target = vector2;
                if (index != 0)
                {
                  target.X += (float) (240 * index);
                  float num7 = (float) (1.0 - (double) this.NPC.ai[1] / (double) num6);
                  target.X += (float) (1200.0 * (double) index / 2.0) * num7;
                }
                this.ShootSquirrelAt(target);
              }
            }
          }
          if ((double) this.NPC.ai[1] <= (double) num6)
            break;
          if ((double) this.NPC.ai[1] > (double) (num6 + num4))
            this.NPC.ai[1] -= (float) num4;
          if ((double) ++this.NPC.ai[2] <= 60.0)
            break;
          this.NPC.TargetClosest(true);
          ++this.NPC.ai[0];
          this.NPC.ai[1] = 0.0f;
          this.NPC.ai[2] = 0.0f;
          this.NPC.ai[3] = 0.0f;
          this.NPC.netUpdate = true;
          break;
        default:
          this.NPC.ai[0] = 0.0f;
          goto case 0;
      }
    }

    private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f)
    {
      if ((double) ((Entity) this.NPC).Center.X < (double) targetPos.X)
      {
        ((Entity) this.NPC).velocity.X += speedModifier;
        if ((double) ((Entity) this.NPC).velocity.X < 0.0)
          ((Entity) this.NPC).velocity.X += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.NPC).velocity.X -= speedModifier;
        if ((double) ((Entity) this.NPC).velocity.X > 0.0)
          ((Entity) this.NPC).velocity.X -= speedModifier * 2f;
      }
      if ((double) ((Entity) this.NPC).Center.Y < (double) targetPos.Y)
      {
        ((Entity) this.NPC).velocity.Y += speedModifier;
        if ((double) ((Entity) this.NPC).velocity.Y < 0.0)
          ((Entity) this.NPC).velocity.Y += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.NPC).velocity.Y -= speedModifier;
        if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
          ((Entity) this.NPC).velocity.Y -= speedModifier * 2f;
      }
      if ((double) Math.Abs(((Entity) this.NPC).velocity.X) > (double) cap)
        ((Entity) this.NPC).velocity.X = cap * (float) Math.Sign(((Entity) this.NPC).velocity.X);
      if ((double) Math.Abs(((Entity) this.NPC).velocity.Y) <= (double) cap)
        return;
      ((Entity) this.NPC).velocity.Y = cap * (float) Math.Sign(((Entity) this.NPC).velocity.Y);
    }

    private void ShootSquirrelAt(Vector2 target)
    {
      float num1 = 0.6f;
      float num2 = 60f;
      if (WorldSavingSystem.MasochistModeReal)
        num2 /= 2f;
      float num3 = num1 * (75f / num2);
      Vector2 vector2 = Vector2.op_Subtraction(target, ((Entity) this.NPC).Center);
      vector2.X += Utils.NextFloat(Main.rand, -32f, 32f);
      vector2.X /= num2;
      vector2.Y = (float) ((double) vector2.Y / (double) num2 - 0.5 * (double) num3 * (double) num2);
      SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      if (!FargoSoulsUtil.HostCheck)
        return;
      float num4 = (float) ((double) num2 + (double) Main.rand.Next(-10, 11) - 1.0);
      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<TrojanSquirrelProj>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, num3, num4, 0.0f);
    }

    public virtual void FindFrame(int frameHeight)
    {
      if (++this.NPC.frameCounter <= 3.0)
        return;
      this.NPC.frameCounter = 0.0;
      this.NPC.frame.Y += frameHeight;
      if (this.NPC.frame.Y < frameHeight * Main.npcFrameCount[this.NPC.type])
        return;
      this.NPC.frame.Y = 0;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<GuiltyBuff>(), 600, true, false);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life <= 0)
      {
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).width), Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).height)));
        if (!Main.dedServ)
          Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_1, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>(((ModType) this).Mod.Name, "TimberGore1").Type, this.NPC.scale);
        Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).width), Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).height)));
        if (!Main.dedServ)
          Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_2, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>(((ModType) this).Mod.Name, "TimberGore2").Type, this.NPC.scale);
      }
      if (!Main.remixWorld || !Utils.NextBool(Main.rand, 10))
        return;
      int num = 5394;
      if (!FargoSoulsUtil.HostCheck)
        return;
      Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) this.NPC).position, ((Entity) this.NPC).Size, num, 1, false, 0, false, false);
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 3544;

    public virtual void OnKill()
    {
      NPC.SetEventFlagCleared(ref WorldSavingSystem.DownedBoss[0], -1);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) new ChampionEnchDropRule(BaseForce.EnchantsIn<TimberForce>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<TimberChampionRelic>()));
    }

    public virtual void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref spriteEffects = ((Entity) this.NPC).direction < 0 ? 0 : 1;
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Timber/TimberChampionHead_Glow", (AssetRequestMode) 1).Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
      SpriteEffects spriteEffects = this.NPC.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Color color1 = Color.op_Multiply(Color.Red, this.NPC.Opacity);
      ((Color) ref color1).A = (byte) 20;
      for (float index1 = 0.0f; (double) index1 < (double) NPCID.Sets.TrailCacheLength[this.NPC.type]; index1 += 0.25f)
      {
        Color color2 = Color.op_Multiply(color1, 0.4f);
        float num = ((float) NPCID.Sets.TrailCacheLength[this.NPC.type] - index1) / (float) NPCID.Sets.TrailCacheLength[this.NPC.type];
        Color color3 = Color.op_Multiply(color2, num * num);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          float rotation = this.NPC.rotation;
          Vector2 vector2_2 = Vector2.op_Addition(Vector2.Lerp(this.NPC.oldPos[(int) index1], this.NPC.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(((Entity) this.NPC).Size, 2f));
          Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), color3, rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.White, this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
