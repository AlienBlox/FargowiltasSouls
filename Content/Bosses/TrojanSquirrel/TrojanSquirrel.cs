// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.BossBars;
using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Items.Placables.Relics;
using FargowiltasSouls.Content.Items.Placables.Trophies;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Content.Items.Weapons.Challengers;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.TrojanSquirrel
{
  [AutoloadBossHead]
  public class TrojanSquirrel : TrojanSquirrelPart
  {
    private const float BaseWalkSpeed = 4f;
    private string TownNPCName;
    public NPC head;
    public NPC arms;
    public int lifeMaxHead;
    public int lifeMaxArms;
    private bool spawned;
    public bool Jumping;

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> bestiaryDrawOffset = NPCID.Sets.NPCBestiaryDrawOffset;
      int type = this.NPC.type;
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers1;
      // ISSUE: explicit constructor call
      ((NPCID.Sets.NPCBestiaryDrawModifiers) ref bestiaryDrawModifiers1).\u002Ector();
      bestiaryDrawModifiers1.CustomTexturePath = "FargowiltasSouls/Content/Bosses/TrojanSquirrel/" + ((ModType) this).Name + "_Still";
      bestiaryDrawModifiers1.Position = new Vector2(64f, 64f);
      bestiaryDrawModifiers1.PortraitPositionXOverride = new float?(24f);
      bestiaryDrawModifiers1.PortraitPositionYOverride = new float?(48f);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      bestiaryDrawOffset.Add(type, bestiaryDrawModifiers2);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.NPC.lifeMax = 800;
      ((Entity) this.NPC).width = this.baseWidth = 100;
      ((Entity) this.NPC).height = this.baseHeight = 120;
      this.NPC.value = (float) Item.buyPrice(0, 0, 75, 0);
      this.NPC.boss = true;
      Mod mod;
      this.Music = Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod) ? MusicLoader.GetMusicSlot(mod, "Assets/Music/TrojanSquirrel") : 81;
      this.SceneEffectPriority = (SceneEffectPriority) 6;
      this.NPC.BossBar = (IBigProgressBar) ModContent.GetInstance<CompositeBossBar>();
    }

    public override void SendExtraAI(BinaryWriter writer)
    {
      base.SendExtraAI(writer);
      writer.Write(this.NPC.localAI[0]);
      writer.Write(this.NPC.localAI[1]);
      writer.Write(this.NPC.localAI[2]);
      writer.Write(this.NPC.localAI[3]);
      writer.Write(this.head != null ? ((Entity) this.head).whoAmI : -1);
      writer.Write(this.arms != null ? ((Entity) this.arms).whoAmI : -1);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
      base.ReceiveExtraAI(reader);
      this.NPC.localAI[0] = reader.ReadSingle();
      this.NPC.localAI[1] = reader.ReadSingle();
      this.NPC.localAI[2] = reader.ReadSingle();
      this.NPC.localAI[3] = reader.ReadSingle();
      this.head = FargoSoulsUtil.NPCExists(reader.ReadInt32(), Array.Empty<int>());
      this.arms = FargoSoulsUtil.NPCExists(reader.ReadInt32(), Array.Empty<int>());
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      ModNPC modNpc;
      if (ModContent.TryFind<ModNPC>("Fargowiltas", "Squirrel", ref modNpc))
      {
        int firstNpc = NPC.FindFirstNPC(modNpc.Type);
        if (firstNpc != -1 && firstNpc != Main.maxNPCs)
        {
          ((Entity) this.NPC).Bottom = ((Entity) Main.npc[firstNpc]).Bottom;
          this.TownNPCName = Main.npc[firstNpc].GivenName;
          Main.npc[firstNpc].life = 0;
          ((Entity) Main.npc[firstNpc]).active = false;
          if (Main.netMode == 2)
            NetMessage.SendData(23, -1, -1, (NetworkText) null, firstNpc, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
      }
      int closest = (int) Player.FindClosest(((Entity) this.NPC).Center, 0, 0);
      if (!closest.IsWithinBounds((int) byte.MaxValue))
        return;
      Player player = Main.player[closest];
      if (player == null || !((Entity) player).active || player.dead || (double) ((Entity) this.NPC).Distance(((Entity) player).Center) >= 400.0)
        return;
      ((Entity) this.NPC).Center = Vector2.op_Subtraction(((Entity) player).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, 1000f), (float) ((Entity) player).direction));
    }

    private void TileCollision(bool fallthrough = false, bool dropDown = false)
    {
      bool flag1 = false;
      for (int x = (int) ((Entity) this.NPC).position.X; (double) x <= (double) ((Entity) this.NPC).position.X + (double) ((Entity) this.NPC).width; x += 16)
      {
        Tile tileSafely = Framing.GetTileSafely(new Vector2((float) x, ((Entity) this.NPC).Bottom.Y + 2f));
        if (((Tile) ref tileSafely).TileType == (ushort) 19)
        {
          flag1 = true;
          break;
        }
      }
      bool flag2 = Collision.SolidCollision(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height);
      if (dropDown)
        ((Entity) this.NPC).velocity.Y += 0.5f;
      else if (flag2 || flag1 && !fallthrough)
      {
        if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
          ((Entity) this.NPC).velocity.Y = 0.0f;
        if ((double) ((Entity) this.NPC).velocity.Y > -0.20000000298023224)
          ((Entity) this.NPC).velocity.Y -= 0.025f;
        else
          ((Entity) this.NPC).velocity.Y -= 0.2f;
        if ((double) ((Entity) this.NPC).velocity.Y < -4.0)
          ((Entity) this.NPC).velocity.Y = -4f;
        if (this.Jumping)
        {
          SoundStyle soundStyle = SoundID.Item14;
          ((SoundStyle) ref soundStyle).Pitch = 0.4f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.NPC).Bottom), (SoundUpdateCallback) null);
          for (int index = 0; index < 4; ++index)
          {
            int num1 = index % 2 == 0 ? 1 : -1;
            float num2 = Utils.NextFloat(Main.rand, 4f, 6f);
            Vector2 vector2 = Utils.RotatedByRandom(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) num1), num2), 0.28559935092926025);
            Gore.NewGoreDirect(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Subtraction(((Entity) this.NPC).Bottom, Vector2.op_Multiply(Vector2.UnitY, 10f)), vector2, Main.rand.Next(11, 14), 2f);
          }
          this.Jumping = false;
        }
      }
      else
      {
        if ((double) ((Entity) this.NPC).velocity.Y < 0.0)
          ((Entity) this.NPC).velocity.Y = 0.0f;
        if ((double) ((Entity) this.NPC).velocity.Y < 0.10000000149011612)
          ((Entity) this.NPC).velocity.Y += 0.025f;
        else
          ((Entity) this.NPC).velocity.Y += 0.5f;
      }
      if ((double) ((Entity) this.NPC).velocity.Y > 10.0)
        ((Entity) this.NPC).velocity.Y = 10f;
      Player player = Main.player[this.NPC.target];
      if (!flag2 || player == null || !((Entity) player).active || player.dead || (double) ((Entity) player).Center.Y <= (double) ((Entity) this.NPC).Center.Y + (double) ((Entity) this.NPC).height * 1.5 || (double) Math.Abs(((Entity) player).Center.X - ((Entity) this.NPC).Center.X) >= 400.0)
        return;
      ((Entity) this.NPC).velocity.Y += 3f;
    }

    private void Movement(Vector2 target, bool goFast = false)
    {
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).Center.X < (double) target.X ? 1 : -1;
      if ((double) Math.Abs(target.X - ((Entity) this.NPC).Center.X) < (double) (((Entity) this.NPC).width / 2))
      {
        ((Entity) this.NPC).velocity.X *= 0.9f;
        if ((double) Math.Abs(((Entity) this.NPC).velocity.X) < 0.10000000149011612)
          ((Entity) this.NPC).velocity.X = 0.0f;
      }
      else
      {
        float num1 = 4f * this.NPC.scale;
        if (this.head == null)
          num1 *= 1.2f;
        if (this.arms == null)
          num1 *= 1.2f;
        if (goFast)
        {
          num1 *= 3f;
          if (!WorldSavingSystem.EternityMode)
            num1 *= 0.75f;
        }
        else if (!WorldSavingSystem.MasochistModeReal)
        {
          num1 *= 0.75f;
          if (this.head != null && (double) this.head.ai[0] != 0.0 || this.arms != null && (double) this.arms.ai[0] != 0.0)
            num1 *= 0.5f;
        }
        if (this.NPC.dontTakeDamage)
          num1 *= 0.75f;
        int num2 = WorldSavingSystem.EternityMode ? 30 : 40;
        if (WorldSavingSystem.MasochistModeReal || this.arms == null || this.head == null)
          num2 = 20;
        if (((Entity) this.NPC).direction > 0)
          ((Entity) this.NPC).velocity.X = (((Entity) this.NPC).velocity.X * (float) num2 + num1) / (float) (num2 + 1);
        else
          ((Entity) this.NPC).velocity.X = (((Entity) this.NPC).velocity.X * (float) num2 - num1) / (float) (num2 + 1);
      }
      this.TileCollision((double) target.Y > (double) ((Entity) this.NPC).Bottom.Y, (double) Math.Abs(target.X - ((Entity) this.NPC).Center.X) < (double) (((Entity) this.NPC).width / 2) && (double) ((Entity) this.NPC).Bottom.Y < (double) target.Y);
    }

    public virtual void AI()
    {
      if (!this.spawned)
      {
        this.spawned = true;
        this.NPC.TargetClosest(false);
        if (FargoSoulsUtil.HostCheck)
        {
          this.head = FargoSoulsUtil.NPCExists(FargoSoulsUtil.NewNPCEasy(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, ModContent.NPCType<TrojanSquirrelHead>(), ((Entity) this.NPC).whoAmI, target: this.NPC.target, velocity: new Vector2()), Array.Empty<int>());
          this.arms = FargoSoulsUtil.NPCExists(FargoSoulsUtil.NewNPCEasy(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, ModContent.NPCType<TrojanSquirrelArms>(), ((Entity) this.NPC).whoAmI, target: this.NPC.target, velocity: new Vector2()), Array.Empty<int>());
        }
        if (WorldSavingSystem.EternityMode && !WorldSavingSystem.DownedBoss[9] && FargoSoulsUtil.HostCheck)
          Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) Main.player[this.NPC.target]).Hitbox, ModContent.ItemType<SquirrelCoatofArms>(), 1, false, 0, false, false);
        this.NPC.ai[0] = 1f;
        this.NPC.ai[3] = 1f;
        for (int index1 = 0; index1 < 80; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 31, ((Entity) this.NPC).velocity.X, ((Entity) this.NPC).velocity.Y, 50, new Color(), 4f);
          Main.dust[index2].velocity.Y -= 1.5f;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.5f);
          Main.dust[index2].noGravity = true;
        }
        FargoSoulsUtil.GrossVanillaDodgeDust((Entity) this.NPC);
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) Main.player[this.NPC.target]).Center), (SoundUpdateCallback) null);
      }
      Player player = Main.player[this.NPC.target];
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1 : -1;
      bool flag1 = false;
      switch ((int) this.NPC.ai[0])
      {
        case 0:
          Vector2 target = Vector2.op_Subtraction(((Entity) player).Bottom, Vector2.UnitY);
          if ((double) this.NPC.localAI[0] > 0.0)
          {
            --this.NPC.localAI[0];
            if ((double) this.NPC.localAI[0] % 10.0 == 0.0)
            {
              SoundEngine.PlaySound(ref SoundID.Run, new Vector2?(), (SoundUpdateCallback) null);
              Vector2 vector2 = Vector2.op_Division(Utils.RotatedByRandom(Vector2.op_UnaryNegation(((Entity) this.NPC).velocity), 0.28559935092926025), 2f);
              Gore.NewGoreDirect(((Entity) player).GetSource_FromThis((string) null), Vector2.op_Subtraction(((Entity) this.NPC).Bottom, Vector2.op_Multiply(Vector2.UnitY, 10f)), vector2, Main.rand.Next(11, 14), Utils.NextFloat(Main.rand, 1.5f, 2f)).timeLeft /= 2;
            }
            float num = ((Entity) this.NPC).Center.X - target.X;
            if ((double) Math.Sign(num) == (double) this.NPC.localAI[1] && (double) Math.Abs(num) > 160.0)
              this.NPC.localAI[0] = 0.0f;
            // ISSUE: explicit constructor call
            ((Vector2) ref target).\u002Ector(((Entity) this.NPC).Center.X + 256f * this.NPC.localAI[1], target.Y);
            if ((double) this.NPC.localAI[0] == 0.0)
              this.NPC.TargetClosest(false);
            if (WorldSavingSystem.EternityMode && this.head == null && (double) this.NPC.localAI[0] % 3.0 == 0.0 && FargoSoulsUtil.HostCheck)
            {
              int index = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Top.X, ((Entity) this.NPC).Top.Y, Utils.NextFloat(Main.rand, -5f, 5f), Utils.NextFloat(Main.rand, -3f), Main.rand.Next(326, 329), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              if (index != Main.maxProjectiles)
                Main.projectile[index].timeLeft = 90;
            }
          }
          else if (!this.NPC.HasValidTarget || (double) ((Entity) this.NPC).Distance(((Entity) player).Center) > 2400.0)
          {
            target = Vector2.op_Addition(((Entity) this.NPC).Center, new Vector2(256f * (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) player).Center.X), (float) sbyte.MinValue));
            this.NPC.TargetClosest(false);
            flag1 = true;
          }
          if ((double) Math.Abs(((Entity) this.NPC).velocity.Y) < 0.05000000074505806 && (double) this.NPC.localAI[3] >= 2.0)
          {
            if ((double) this.NPC.localAI[3] == 2.0)
            {
              this.NPC.localAI[3] = 0.0f;
            }
            else
            {
              --this.NPC.localAI[3];
              this.NPC.ai[0] = 1f;
              this.NPC.ai[3] = 1f;
            }
            if (WorldSavingSystem.MasochistModeReal)
            {
              SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              this.ExplodeAttack();
            }
          }
          bool goFast = flag1 || (double) this.NPC.localAI[0] > 0.0;
          this.Movement(target, goFast);
          if (this.arms != null && ((double) this.NPC.localAI[3] == -1.0 || (double) this.NPC.localAI[3] == 1.0))
            ((Entity) this.NPC).direction = this.NPC.spriteDirection = (int) this.NPC.localAI[3];
          if ((!WorldSavingSystem.EternityMode ? 0 : (!goFast ? 1 : 0)) != 0)
          {
            float num1 = 1f;
            if (this.head == null)
              num1 += 0.5f;
            if (this.arms == null)
              num1 += 0.5f;
            if (WorldSavingSystem.MasochistModeReal)
              ++num1;
            if (this.NPC.dontTakeDamage)
              num1 /= 2f;
            if ((double) target.Y > (double) ((Entity) this.NPC).Top.Y)
              this.NPC.ai[1] += num1;
            else
              this.NPC.ai[2] += num1;
            if ((double) Math.Abs(((Entity) this.NPC).velocity.Y) < 0.05000000074505806)
            {
              bool flag2 = (this.head == null || (double) this.head.ai[0] == 0.0) && (this.arms == null || (double) this.arms.ai[0] == 0.0);
              int num2 = 300;
              if ((double) this.NPC.ai[1] > (double) num2)
              {
                if (flag2)
                {
                  this.NPC.ai[0] = 1f;
                  this.NPC.ai[1] = 0.0f;
                  this.NPC.ai[3] = 0.0f;
                  this.NPC.localAI[0] = 0.0f;
                  this.NPC.netUpdate = true;
                }
                else
                  this.NPC.ai[1] -= 10f;
              }
              if ((double) this.NPC.ai[2] > (double) num2)
              {
                if (flag2)
                {
                  this.NPC.ai[0] = 1f;
                  this.NPC.ai[2] = 0.0f;
                  this.NPC.ai[3] = 1f;
                  this.NPC.localAI[0] = 0.0f;
                  this.NPC.netUpdate = true;
                  break;
                }
                this.NPC.ai[2] -= 10f;
                break;
              }
              break;
            }
            break;
          }
          break;
        case 1:
          ((Entity) this.NPC).velocity.X = 0.0f;
          this.TileCollision((double) ((Entity) player).Bottom.Y - 1.0 > (double) ((Entity) this.NPC).Bottom.Y, (double) Math.Abs(((Entity) player).Center.X - ((Entity) this.NPC).Center.X) < (double) (((Entity) this.NPC).width / 2) && (double) ((Entity) this.NPC).Bottom.Y < (double) ((Entity) player).Bottom.Y - 1.0);
          int num3 = 105;
          if (WorldSavingSystem.EternityMode)
          {
            if (this.head == null)
              num3 -= 20;
            if (this.arms == null)
              num3 -= 20;
            if (this.head == null && this.arms == null)
              num3 -= 30;
          }
          if (WorldSavingSystem.MasochistModeReal || (double) this.NPC.localAI[3] >= 2.0)
            num3 -= 20;
          if ((double) this.NPC.ai[3] != 0.0)
            ((Entity) this.NPC).position.X += (float) (((double) this.NPC.localAI[0] % 2.0 == 0.0 ? 1 : -1) * 8) * (this.NPC.localAI[0] / (float) num3);
          if ((double) ++this.NPC.localAI[0] > (double) num3)
          {
            this.NPC.localAI[0] = 0.0f;
            this.NPC.netUpdate = true;
            if ((double) this.NPC.ai[3] == 0.0)
            {
              this.NPC.ai[0] = 0.0f;
              this.NPC.localAI[0] = 300f;
              this.NPC.localAI[1] = (float) Math.Sign(((Entity) player).Center.X - ((Entity) this.NPC).Center.X);
              this.NPC.localAI[2] = ((Entity) player).Center.X;
              break;
            }
            this.NPC.ai[0] = 2f;
            break;
          }
          break;
        case 2:
          float num4 = !WorldSavingSystem.EternityMode || this.arms != null ? 90f : 60f;
          if ((double) this.NPC.localAI[0]++ == 0.0)
          {
            Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) player).Top, ((Entity) this.NPC).Bottom);
            if (WorldSavingSystem.EternityMode && this.arms == null)
            {
              vector2_1.X += (float) (((Entity) this.NPC).width * Math.Sign(((Entity) player).Center.X - ((Entity) this.NPC).Center.X));
              if ((double) this.NPC.localAI[3] < 2.0)
              {
                this.NPC.localAI[3] = 2f;
                if (this.head == null)
                  this.NPC.localAI[3] += 2f;
              }
              this.ExplodeAttack();
            }
            vector2_1.X /= num4;
            vector2_1.Y = (float) ((double) vector2_1.Y / (double) num4 - 0.20000000298023224 * (double) num4);
            ((Entity) this.NPC).velocity = vector2_1;
            this.NPC.netUpdate = true;
            SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.NPC).Bottom), (SoundUpdateCallback) null);
            for (int index = 0; index < 4; ++index)
            {
              int num5 = index % 2 == 0 ? 1 : -1;
              float num6 = Utils.NextFloat(Main.rand, 4f, 6f);
              Vector2 vector2_2 = Utils.RotatedByRandom(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) num5), num6), 0.28559935092926025);
              Gore.NewGoreDirect(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Subtraction(((Entity) this.NPC).Bottom, Vector2.op_Multiply(Vector2.UnitY, 10f)), vector2_2, Main.rand.Next(11, 14), 2f);
            }
            this.Jumping = true;
          }
          else
            ((Entity) this.NPC).velocity.Y += 0.4f;
          if ((double) this.NPC.localAI[0] > (double) num4)
          {
            this.NPC.TargetClosest(false);
            ((Entity) this.NPC).velocity.X = Utils.Clamp<float>(((Entity) this.NPC).velocity.X, -20f, 20f);
            ((Entity) this.NPC).velocity.Y = Utils.Clamp<float>(((Entity) this.NPC).velocity.Y, -10f, 10f);
            this.NPC.ai[0] = 0.0f;
            this.NPC.localAI[0] = 0.0f;
            this.NPC.netUpdate = true;
            break;
          }
          break;
        default:
          this.NPC.ai[0] = 0.0f;
          goto case 0;
      }
      if (flag1)
      {
        if (this.NPC.timeLeft > 60)
          this.NPC.timeLeft = 60;
      }
      else if (this.NPC.timeLeft < 600)
        this.NPC.timeLeft = 600;
      if (this.head == null)
      {
        Vector2 top = ((Entity) this.NPC).Top;
        top.X += 32f * (float) ((Entity) this.NPC).direction;
        top.Y -= 8f;
        int num7 = 64;
        int num8 = 32;
        top.X -= (float) num7 / 2f;
        top.Y -= (float) num8 / 2f;
        for (int index3 = 0; index3 < 3; ++index3)
        {
          int index4 = Dust.NewDust(top, num7, num8, 31, ((Entity) this.NPC).velocity.X, ((Entity) this.NPC).velocity.Y, 50, new Color(), 2.5f);
          Main.dust[index4].velocity.Y -= 1.5f;
          Dust dust = Main.dust[index4];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.5f);
          Main.dust[index4].noGravity = true;
        }
        if (Utils.NextBool(Main.rand, 3))
        {
          int index = Dust.NewDust(top, num7, num8, 6, ((Entity) this.NPC).velocity.X * 0.4f, ((Entity) this.NPC).velocity.Y * 0.4f, 100, new Color(), 2.5f);
          Main.dust[index].noGravity = true;
          Main.dust[index].velocity.Y -= 3f;
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.5f);
        }
      }
      else
      {
        this.lifeMaxHead = this.head.lifeMax;
        this.head = FargoSoulsUtil.NPCExists(((Entity) this.head).whoAmI, new int[1]
        {
          ModContent.NPCType<TrojanSquirrelHead>()
        });
      }
      if (this.arms == null)
      {
        Vector2 center = ((Entity) this.NPC).Center;
        center.X -= 16f * (float) ((Entity) this.NPC).direction;
        center.Y -= 48f;
        int num9 = 32;
        int num10 = 32;
        center.X -= (float) num9 / 2f;
        center.Y -= (float) num10 / 2f;
        for (int index5 = 0; index5 < 2; ++index5)
        {
          int index6 = Dust.NewDust(center, num9, num10, 31, ((Entity) this.NPC).velocity.X, ((Entity) this.NPC).velocity.Y, 50, new Color(), 1.5f);
          Main.dust[index6].noGravity = true;
        }
        if (Utils.NextBool(Main.rand))
        {
          int index = Dust.NewDust(center, num9, num10, 6, ((Entity) this.NPC).velocity.X * 0.4f, ((Entity) this.NPC).velocity.Y * 0.4f, 100, new Color(), 3f);
          Main.dust[index].noGravity = true;
        }
      }
      else
      {
        this.lifeMaxArms = this.arms.lifeMax;
        this.arms = FargoSoulsUtil.NPCExists(((Entity) this.arms).whoAmI, new int[1]
        {
          ModContent.NPCType<TrojanSquirrelArms>()
        });
      }
      if (this.NPC.life < this.NPC.lifeMax / 2 && Utils.NextBool(Main.rand, 3))
      {
        int index = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 31, ((Entity) this.NPC).velocity.X, ((Entity) this.NPC).velocity.Y, 50, new Color(), 4f);
        Main.dust[index].velocity.Y -= 1.5f;
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.5f);
        Main.dust[index].noGravity = true;
      }
      if (WorldSavingSystem.EternityMode)
      {
        int num11 = this.NPC.dontTakeDamage ? 1 : 0;
        this.NPC.dontTakeDamage = this.NPC.life < this.NPC.lifeMax / 2 && (this.head != null || this.arms != null);
        int num12 = this.NPC.dontTakeDamage ? 1 : 0;
        if (num11 != num12)
        {
          for (int index = 0; index < 6; ++index)
            this.ExplodeDust(Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width), (float) Main.rand.Next(((Entity) this.NPC).height))));
        }
      }
      else
        this.NPC.dontTakeDamage = false;
      if (!WorldSavingSystem.MasochistModeReal || !Main.getGoodWorld || !FargoSoulsUtil.HostCheck)
        return;
      int[] source = new int[18]
      {
        30,
        635,
        321,
        311,
        191,
        322,
        253,
        157,
        159,
        208,
        5,
        634,
        171,
        323,
        170,
        596,
        616,
        384
      };
      for (float x = ((Entity) this.NPC).position.X; (double) x < (double) ((Entity) this.NPC).BottomRight.X; x += 16f)
      {
        for (float y = ((Entity) this.NPC).position.Y; (double) y < (double) ((Entity) this.NPC).BottomRight.Y; y += 16f)
        {
          Tile tileSafely = Framing.GetTileSafely(new Vector2(x, y));
          if (Tile.op_Inequality(tileSafely, (ArgumentException) null) && ((IEnumerable<int>) source).Contains<int>((int) ((Tile) ref tileSafely).TileType))
          {
            int num13 = (int) x / 16;
            int num14 = (int) y / 16;
            WorldGen.KillTile(num13, num14, false, false, true);
            if (Main.netMode == 2)
              NetMessage.SendTileSquare(-1, num13, num14, 1, (TileChangeType) 0);
            this.NPC.scale += 0.01f;
            this.NPC.netUpdate = true;
            if (this.head != null)
            {
              this.head.scale += 0.01f;
              this.head.netUpdate = true;
            }
            if (this.arms != null)
            {
              this.arms.scale += 0.01f;
              this.arms.netUpdate = true;
            }
          }
        }
      }
    }

    private void ExplodeAttack()
    {
      if (!FargoSoulsUtil.HostCheck)
        return;
      float width = (float) ((Entity) this.NPC).width;
      int num = WorldSavingSystem.MasochistModeReal ? 4 : 2;
      for (int index = -num; index <= num; ++index)
      {
        Projectile projectile = Projectile.NewProjectileDirect(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Bottom, new Vector2(width * (float) index, -65f)), Vector2.Zero, 696, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        projectile.friendly = false;
        projectile.hostile = true;
      }
    }

    private void ExplodeDust(Vector2 center)
    {
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(center), (SoundUpdateCallback) null);
      Vector2 vector2 = Vector2.op_Subtraction(center, Vector2.op_Division(new Vector2(32f, 32f), 2f));
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(vector2, 32, 32, 31, 0.0f, 0.0f, 100, new Color(), 3f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 15; ++index3)
      {
        int index4 = Dust.NewDust(vector2, 32, 32, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index5 = Dust.NewDust(vector2, 32, 32, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      float num = 0.5f;
      for (int index6 = 0; index6 < 3; ++index6)
      {
        int index7 = Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore = Main.gore[index7];
        gore.velocity = Vector2.op_Multiply(gore.velocity, num);
        ++Main.gore[index7].velocity.X;
        ++Main.gore[index7].velocity.Y;
      }
    }

    public virtual void FindFrame(int frameHeight)
    {
      switch ((int) this.NPC.ai[0])
      {
        case 1:
          this.NPC.frame.Y = frameHeight * 6;
          break;
        case 2:
          this.NPC.frame.Y = frameHeight * 7;
          break;
        default:
          this.NPC.frameCounter += 0.25 / (double) this.NPC.scale * (double) Math.Abs(((Entity) this.NPC).velocity.X);
          if (this.NPC.frameCounter > 2.5)
          {
            this.NPC.frameCounter = 0.0;
            this.NPC.frame.Y += frameHeight;
          }
          if (this.NPC.frame.Y >= frameHeight * 6)
            this.NPC.frame.Y = 0;
          if (this.arms != null && (double) this.arms.ai[0] == 1.0 && (double) this.arms.ai[3] == 1.0)
            this.NPC.frame.Y = frameHeight * 6;
          if ((double) ((Entity) this.NPC).velocity.X == 0.0)
            this.NPC.frame.Y = frameHeight;
          if ((double) ((Entity) this.NPC).velocity.Y <= 4.0)
            break;
          this.NPC.frame.Y = frameHeight * 7;
          break;
      }
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      for (int index = 3; index <= 7; ++index)
      {
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).width), Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).height)));
        if (!Main.dedServ)
        {
          IEntitySource sourceFromThis = ((Entity) this.NPC).GetSource_FromThis((string) null);
          Vector2 vector2_2 = vector2_1;
          Vector2 velocity = ((Entity) this.NPC).velocity;
          string name = ((ModType) this).Mod.Name;
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
          interpolatedStringHandler.AppendLiteral("TrojanSquirrelGore");
          interpolatedStringHandler.AppendFormatted<int>(index);
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          int type = ModContent.Find<ModGore>(name, stringAndClear).Type;
          double scale = (double) this.NPC.scale;
          Gore.NewGore(sourceFromThis, vector2_2, velocity, type, (float) scale);
        }
      }
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 28;

    public virtual void OnKill()
    {
      NPC.SetEventFlagCleared(ref WorldSavingSystem.DownedBoss[9], -1);
      ModNPC modNpc;
      if (!ModContent.TryFind<ModNPC>("Fargowiltas", "Squirrel", ref modNpc) || NPC.AnyNPCs(modNpc.Type))
        return;
      int index = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, modNpc.Type, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
      if (index == Main.maxNPCs)
        return;
      Main.npc[index].homeless = true;
      if (this.TownNPCName != null)
        Main.npc[index].GivenName = this.TownNPCName;
      if (Main.netMode != 2)
        return;
      NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.BossBag(ModContent.ItemType<TrojanSquirrelBag>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(ModContent.ItemType<TrojanSquirrelTrophy>(), 10, 1, 1));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<TrojanSquirrelRelic>()));
      LeadingConditionRule leadingConditionRule = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.NotExpert());
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.OneFromOptions(1, new int[4]
      {
        ModContent.ItemType<TreeSword>(),
        ModContent.ItemType<MountedAcornGun>(),
        ModContent.ItemType<SnowballStaff>(),
        ModContent.ItemType<KamikazeSquirrelStaff>()
      }), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.OneFromOptions(1, new int[2]
      {
        2018,
        3563
      }), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(4759, 1, 1, 1), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(2334, 1, 1, 5), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(3093, 1, 1, 5), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(27, 1, 100, 100), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(ModContent.Find<ModItem>("Fargowiltas", "LumberJaxe").Type, 10, 1, 1), false);
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) leadingConditionRule);
    }

    public virtual void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref spriteEffects = ((Entity) this.NPC).direction < 0 ? 0 : 1;
    }
  }
}
