// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.ModPlayers.EModePlayer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.ModPlayers
{
  public class EModePlayer : ModPlayer
  {
    public int MasomodeCrystalTimer;
    public int MasomodeFreezeTimer;
    public int MasomodeSpaceBreathTimer;
    public int MasomodeMinionNerfTimer;
    public const int MaxMasomodeMinionNerfTimer = 300;
    public bool ReduceMasomodeMinionNerf;
    public bool HasWhipBuff;
    public int HallowFlipCheckTimer;
    public int TorchGodTimer;
    public int ShorterDebuffsTimer;
    public const int MaxShorterDebuffsTimer = 60;
    public int MythrilHalberdTimer;
    public int CobaltHitCounter;
    public int LightningCounter;
    public int CrossNecklaceTimer;
    public static List<int> IronTiles;
    public static List<int> IronWalls;

    private int WeaponUseTimer => this.Player.FargoSouls().WeaponUseTimer;

    public virtual void ResetEffects()
    {
      this.ReduceMasomodeMinionNerf = false;
      this.HasWhipBuff = false;
    }

    public virtual void UpdateDead()
    {
      base.ResetEffects();
      this.MasomodeMinionNerfTimer = 0;
      this.ShorterDebuffsTimer = 0;
    }

    public virtual void OnEnterWorld()
    {
      // ISSUE: unable to decompile the method.
    }

    public virtual void PreUpdateBuffs() => this.MurderGreaterDangersense();

    public virtual void PostUpdate() => this.MurderGreaterDangersense();

    private void MurderGreaterDangersense()
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      Mod mod1;
      ModBuff modBuff1;
      if (Terraria.ModLoader.ModLoader.TryGetMod("AlchemistNPC", ref mod1) && Luminance.Common.Utilities.Utilities.AnyBosses() && mod1.TryFind<ModBuff>("GreaterDangersense", ref modBuff1))
        MurderBuff(modBuff1.Type);
      Mod mod2;
      ModBuff modBuff2;
      if (!Terraria.ModLoader.ModLoader.TryGetMod("AlchemistNPCLite", ref mod2) || !Luminance.Common.Utilities.Utilities.AnyBosses() || !mod2.TryFind<ModBuff>("GreaterDangersense", ref modBuff2))
        return;
      MurderBuff(modBuff2.Type);

      void MurderBuff(int type)
      {
        if (!this.Player.HasBuff(type))
          return;
        this.Player.DelBuff(this.Player.FindBuffIndex(type));
        this.Player.ClearBuff(type);
      }
    }

    public virtual void PreUpdate()
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      FargoSoulsPlayer fargoSoulsPlayer = this.Player.FargoSouls();
      if (!((Entity) this.Player).active || this.Player.dead || this.Player.ghost)
        return;
      if (!NPC.downedBoss3 && this.Player.ZoneDungeon && !NPC.AnyNPCs(68) && !Main.drunkWorld && !Main.zenithWorld)
        NPC.SpawnOnPlayer(((Entity) this.Player).whoAmI, 68);
      if (this.Player.ZoneUnderworldHeight && ((this.Player.HasEffect<AshWoodEffect>() ? 1 : (this.Player.HasEffect<ObsidianEffect>() ? 1 : 0)) != 0 || !this.Player.fireWalk && !fargoSoulsPlayer.PureHeart && this.Player.lavaMax <= 0))
        FargoSoulsUtil.AddDebuffFixedDuration(this.Player, 24, 2);
      if (this.Player.ZoneJungle && ((Entity) this.Player).wet && !((Entity) this.Player).lavaWet && !((Entity) this.Player).honeyWet && !fargoSoulsPlayer.MutantAntibodies)
        FargoSoulsUtil.AddDebuffFixedDuration(this.Player, 20, 2);
      if (this.Player.ZoneSnow)
      {
        if (((Entity) this.Player).wet && !((Entity) this.Player).lavaWet && !((Entity) this.Player).honeyWet && !fargoSoulsPlayer.MutantAntibodies && this.Player.chilled)
          this.Player.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 2, true, false);
        else
          this.MasomodeFreezeTimer = 0;
      }
      else
        this.MasomodeFreezeTimer = 0;
      if (this.Player.ZoneCorrupt)
      {
        if (!fargoSoulsPlayer.PureHeart)
          FargoSoulsUtil.AddDebuffFixedDuration(this.Player, 22, 2);
        if (((Entity) this.Player).wet && !((Entity) this.Player).lavaWet && !((Entity) this.Player).honeyWet && !fargoSoulsPlayer.MutantAntibodies)
          FargoSoulsUtil.AddDebuffFixedDuration(this.Player, 39, 2);
      }
      if (this.Player.ZoneCrimson)
      {
        if (!fargoSoulsPlayer.PureHeart)
          FargoSoulsUtil.AddDebuffFixedDuration(this.Player, 30, 2);
        if (((Entity) this.Player).wet && !((Entity) this.Player).lavaWet && !((Entity) this.Player).honeyWet && !fargoSoulsPlayer.MutantAntibodies)
          FargoSoulsUtil.AddDebuffFixedDuration(this.Player, 69, 2);
      }
      Tile tileSafely1;
      if (this.Player.ZoneHallow)
      {
        if (this.Player.ZoneRockLayerHeight && !fargoSoulsPlayer.PureHeart && ++this.HallowFlipCheckTimer > 6)
        {
          this.HallowFlipCheckTimer = 0;
          float num1 = ((Entity) this.Player).Center.Y - 800f;
          float num2 = ((Entity) this.Player).Center.Y + 800f;
          if ((double) num1 / 16.0 < (double) Main.maxTilesY && (double) num2 / 16.0 < (double) Main.maxTilesY && !Collision.CanHitLine(new Vector2(((Entity) this.Player).Left.X, num1), 0, 0, new Vector2(((Entity) this.Player).Left.X, num2), 0, 0) && !Collision.CanHitLine(new Vector2(((Entity) this.Player).Right.X, num1), 0, 0, new Vector2(((Entity) this.Player).Right.X, num2), 0, 0))
          {
            bool[] wallHouse1 = Main.wallHouse;
            tileSafely1 = Framing.GetTileSafely(((Entity) this.Player).Center);
            int index1 = (int) ((Tile) ref tileSafely1).WallType;
            if (!wallHouse1[index1])
            {
              bool[] wallHouse2 = Main.wallHouse;
              tileSafely1 = Framing.GetTileSafely(((Entity) this.Player).TopLeft);
              int index2 = (int) ((Tile) ref tileSafely1).WallType;
              if (!wallHouse2[index2])
              {
                bool[] wallHouse3 = Main.wallHouse;
                tileSafely1 = Framing.GetTileSafely(((Entity) this.Player).TopRight);
                int index3 = (int) ((Tile) ref tileSafely1).WallType;
                if (!wallHouse3[index3])
                {
                  bool[] wallHouse4 = Main.wallHouse;
                  tileSafely1 = Framing.GetTileSafely(((Entity) this.Player).BottomLeft);
                  int index4 = (int) ((Tile) ref tileSafely1).WallType;
                  if (!wallHouse4[index4])
                  {
                    bool[] wallHouse5 = Main.wallHouse;
                    tileSafely1 = Framing.GetTileSafely(((Entity) this.Player).BottomRight);
                    int index5 = (int) ((Tile) ref tileSafely1).WallType;
                    if (!wallHouse5[index5])
                      this.Player.AddBuff(ModContent.BuffType<FlippedHallowBuff>(), 90, true, false);
                  }
                }
              }
            }
          }
        }
        if (((Entity) this.Player).wet && !((Entity) this.Player).lavaWet && !((Entity) this.Player).honeyWet && !fargoSoulsPlayer.MutantAntibodies)
          this.Player.AddBuff(ModContent.BuffType<SmiteBuff>(), 2, true, false);
      }
      Vector2 center = ((Entity) this.Player).Center;
      center.X /= 16f;
      center.Y /= 16f;
      Tile tileSafely2 = Framing.GetTileSafely((int) center.X, (int) center.Y);
      if (!fargoSoulsPlayer.PureHeart && Main.raining && this.Player.ZoneOverworldHeight && this.Player.HeldItem.type != 946 && this.Player.HeldItem.type != 4707 && this.Player.armor[0].type != 1243 && this.Player.armor[0].type != 5101 && !this.Player.HasEffect<RainUmbrellaEffect>() && ((Tile) ref tileSafely2).WallType == (ushort) 0)
      {
        if (this.Player.ZoneSnow)
          this.Player.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 2, true, false);
        else
          this.Player.AddBuff(103, 2, true, false);
        ++this.LightningCounter;
        if (this.LightningCounter >= Luminance.Common.Utilities.Utilities.SecondsToFrames(WorldSavingSystem.MasochistModeReal ? 10f : 17f))
        {
          Point tileCoordinates = Utils.ToTileCoordinates(((Entity) this.Player).Top);
          tileCoordinates.X += Main.rand.Next(-25, 25);
          tileCoordinates.Y -= Main.rand.Next(4, 8);
          bool flag = false;
          if (WorldSavingSystem.MasochistModeReal)
            flag = true;
          if (Luminance.Common.Utilities.Utilities.AnyBosses() && !WorldSavingSystem.MasochistModeReal)
            this.LightningCounter = 0;
          else if (Utils.NextBool(Main.rand, 300) | flag)
          {
            float y = ((Entity) this.Player).Center.Y;
            int num = (Main.hardMode ? 120 : 60) / 4;
            Projectile.NewProjectile(((Entity) this.Player).GetSource_Misc(""), (float) (tileCoordinates.X * 16 + 8), (float) (tileCoordinates.Y * 16 + 17 - 900), 0.0f, 0.0f, ModContent.ProjectileType<RainLightning>(), num, 2f, Main.myPlayer, Utils.ToRotation(Vector2.UnitY), y, 0.0f);
            this.LightningCounter = 0;
          }
        }
      }
      if (((Entity) this.Player).wet && !((Entity) this.Player).lavaWet && !((Entity) this.Player).honeyWet && !((ExtraJumpState) ref this.Player.GetJumpState<ExtraJump>(ExtraJump.Flipper)).Enabled && !this.Player.gills && !fargoSoulsPlayer.MutantAntibodies)
        this.Player.AddBuff(ModContent.BuffType<LethargicBuff>(), 2, true, false);
      if (!fargoSoulsPlayer.PureHeart && !this.Player.buffImmune[68] && this.Player.ZoneSkyHeight && ((Entity) this.Player).whoAmI == Main.myPlayer && (Collision.DrownCollision(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, this.Player.gravDir, false) ? 1 : (this.Player.armor[0].IsAir ? 0 : (this.Player.armor[0].type == 250 ? 1 : (this.Player.armor[0].type == 4275 ? 1 : 0)))) == 0)
      {
        this.Player.breath -= 3;
        if (++this.MasomodeSpaceBreathTimer > 10)
        {
          this.MasomodeSpaceBreathTimer = 0;
          --this.Player.breath;
        }
        if (this.Player.breath == 0)
          SoundEngine.PlaySound(ref SoundID.Drown, new Vector2?(), (SoundUpdateCallback) null);
        if (this.Player.breath <= 0)
          this.Player.AddBuff(68, 2, true, false);
        if (this.Player.breath < -10)
          this.Player.breath = -10;
      }
      if (!fargoSoulsPlayer.PureHeart && !this.Player.buffImmune[149] && this.Player.stickyBreak > 0 && Tile.op_Inequality(tileSafely2, (ArgumentException) null) && ((Tile) ref tileSafely2).WallType == (ushort) 62)
      {
        this.Player.AddBuff(149, 30, true, false);
        this.Player.AddBuff(32, 90, true, false);
        this.Player.stickyBreak = 0;
        Vector2 vector2 = Collision.StickyTiles(((Entity) this.Player).position, ((Entity) this.Player).velocity, ((Entity) this.Player).width, ((Entity) this.Player).height);
        if ((double) vector2.X != -1.0 && (double) vector2.Y != -1.0)
        {
          int x = (int) vector2.X;
          int y = (int) vector2.Y;
          WorldGen.KillTile(x, y, false, false, false);
          if (Main.netMode == 1)
          {
            tileSafely1 = ((Tilemap) ref Main.tile)[x, y];
            if (!((Tile) ref tileSafely1).HasTile)
              NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) x, (float) y, 0.0f, 0, 0, 0);
          }
        }
      }
      if (Tile.op_Inequality(tileSafely2, (ArgumentException) null) && ((Tile) ref tileSafely2).TileType == (ushort) 80 && ((Tile) ref tileSafely2).HasUnactuatedTile && !fargoSoulsPlayer.CactusImmune)
      {
        int num = 10;
        if (WorldSavingSystem.MasochistModeReal)
        {
          if (this.Player.ZoneCorrupt)
          {
            num *= 2;
            this.Player.AddBuff(39, 150, true, false);
          }
          if (this.Player.ZoneCrimson)
          {
            num *= 2;
            this.Player.AddBuff(69, 150, true, false);
          }
          if (this.Player.ZoneHallow)
          {
            num *= 2;
            this.Player.AddBuff(31, 150, true, false);
          }
        }
        if (Main.hardMode)
          num *= 2;
        if (this.Player.hurtCooldowns[0] <= 0)
          this.Player.Hurt(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.FargowiltasSouls.DeathMessage.Cactus", (object) this.Player.name)), num, 0, false, false, 0, false, 0.0f, 0.0f, 4.5f);
      }
      if (fargoSoulsPlayer.PureHeart || !Main.bloodMoon)
        return;
      this.Player.AddBuff(86, 2, true, false);
    }

    public virtual void PostUpdateBuffs()
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      this.Player.pickSpeed -= 0.25f;
      this.Player.tileSpeed += 0.25f;
      this.Player.wallSpeed += 0.25f;
      this.Player.moveSpeed += 0.25f;
      this.Player.statManaMax2 += 50;
      this.Player.manaRegenDelay = Math.Min(this.Player.manaRegenDelay, 30f);
      this.Player.manaRegenBonus += 5;
      this.Player.wellFed = true;
    }

    public virtual void UpdateBadLifeRegen()
    {
      float frames = (float) Luminance.Common.Utilities.Utilities.SecondsToFrames(5f);
      if (this.Player.lifeRegen <= 0 || (double) this.Player.lifeRegenTime >= (double) frames)
        return;
      this.Player.lifeRegen = (int) ((double) this.Player.lifeRegen * (double) this.Player.lifeRegenTime / (double) frames);
    }

    public virtual void PostUpdateEquips()
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      if (this.Player.longInvince && !this.Player.immune)
      {
        if (this.CrossNecklaceTimer < 20)
        {
          this.Player.longInvince = false;
          ++this.CrossNecklaceTimer;
        }
      }
      else
        this.CrossNecklaceTimer = 0;
      if (this.Player.iceBarrier)
      {
        ref StatModifier local = ref this.Player.GetDamage(DamageClass.Generic);
        local = StatModifier.op_Subtraction(local, 0.1f);
      }
      if (!this.Player.setSquireT2 && !this.Player.setSquireT3 && !this.Player.setMonkT2 && !this.Player.setMonkT3 && !this.Player.setHuntressT2 && !this.Player.setHuntressT3 && !this.Player.setApprenticeT2 && !this.Player.setApprenticeT3 && !this.Player.setForbidden)
        return;
      this.ReduceMasomodeMinionNerf = true;
    }

    private void HandleTimersAlways()
    {
      if (this.MasomodeCrystalTimer > 0)
        --this.MasomodeCrystalTimer;
      if (DD2Event.Ongoing && !FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.betsyBoss, 551))
      {
        int firstNpc = NPC.FindFirstNPC(548);
        if (firstNpc != -1 && firstNpc != Main.maxNPCs && (double) ((Entity) this.Player).Distance(((Entity) Main.npc[firstNpc]).Center) < 3000.0)
        {
          this.MasomodeMinionNerfTimer -= 2;
          if (this.MasomodeMinionNerfTimer < 0)
            this.MasomodeMinionNerfTimer = 0;
        }
      }
      if (this.WeaponUseTimer > 0)
        ++this.ShorterDebuffsTimer;
      else if (this.ShorterDebuffsTimer > 0)
        --this.ShorterDebuffsTimer;
      if (this.WeaponUseTimer > 0 && this.Player.HeldItem.DamageType != DamageClass.Summon && this.Player.HeldItem.DamageType != DamageClass.SummonMeleeSpeed && this.Player.HeldItem.DamageType != DamageClass.Default)
        ++this.MasomodeMinionNerfTimer;
      else if (this.MasomodeMinionNerfTimer > 0)
        --this.MasomodeMinionNerfTimer;
      if (this.MasomodeMinionNerfTimer > 300)
        this.MasomodeMinionNerfTimer = 300;
      if (this.ShorterDebuffsTimer <= 60)
        return;
      this.ShorterDebuffsTimer = 60;
    }

    public virtual void PostUpdateMiscEffects()
    {
      this.HandleTimersAlways();
      if (!WorldSavingSystem.EternityMode)
        return;
      if (this.Player.HeldItem.shoot > 0 && ProjectileID.Sets.IsAWhip[this.Player.HeldItem.shoot])
        this.Player.GetAttackSpeed(DamageClass.Melee) = 1f;
      if (!this.Player.happyFunTorchTime || ++this.TorchGodTimer <= 60)
        return;
      this.TorchGodTimer = 0;
      float num1 = Utils.NextFloat(Main.rand, -2f, 2f);
      float num2 = Utils.NextFloat(Main.rand, -2f, 2f);
      Projectile.NewProjectile(((Entity) this.Player).GetSource_Misc("TorchGod"), Utils.NextVector2FromRectangle(Main.rand, ((Entity) this.Player).Hitbox), Vector2.Zero, ModContent.ProjectileType<TorchGodFlame>(), 20, 0.0f, Main.myPlayer, num1, num2, 0.0f);
    }

    public virtual void ModifyHitNPCWithProj(
      Projectile proj,
      NPC target,
      ref NPC.HitModifiers modifiers)
    {
      int num = WorldSavingSystem.EternityMode ? 1 : 0;
    }

    private void ShadowDodgeNerf()
    {
      if (!this.Player.shadowDodge)
        return;
      this.Player.AddBuff(ModContent.BuffType<HolyPriceBuff>(), 600, true, false);
    }

    public virtual void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      this.ShadowDodgeNerf();
      if (!this.Player.resistCold || !npc.coldDamage)
        return;
      ref StatModifier local1 = ref modifiers.SourceDamage;
      local1 = StatModifier.op_Multiply(local1, 1.3f);
      ref StatModifier local2 = ref modifiers.FinalDamage;
      local2 = StatModifier.op_Multiply(local2, 0.85f);
    }

    public virtual void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      this.ShadowDodgeNerf();
      if (!this.Player.resistCold || !proj.coldDamage)
        return;
      ref StatModifier local1 = ref modifiers.SourceDamage;
      local1 = StatModifier.op_Multiply(local1, 1.3f);
      ref StatModifier local2 = ref modifiers.FinalDamage;
      local2 = StatModifier.op_Multiply(local2, 0.85f);
    }

    public virtual void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
    {
      int num = WorldSavingSystem.EternityMode ? 1 : 0;
    }

    public virtual void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
    {
      int num = WorldSavingSystem.EternityMode ? 1 : 0;
    }

    public virtual void ModifyHurt(ref Player.HurtModifiers modifiers)
    {
      this.ShorterDebuffsTimer = 60;
      if (!WorldSavingSystem.EternityMode)
        base.ModifyHurt(ref modifiers);
      if (((Player.HurtModifiers) ref modifiers).DamageSource.SourceProjectileType == 108)
        this.Player.FargoSouls().AddBuffNoStack(ModContent.BuffType<StunnedBuff>(), 120);
      if (this.Player.brainOfConfusionItem != null && !this.Player.brainOfConfusionItem.IsAir && Utils.NextBool(Main.rand, 2))
        this.Player.brainOfConfusionItem = (Item) null;
      base.ModifyHurt(ref modifiers);
    }

    public virtual void Kill(
      double damage,
      int hitDirection,
      bool pvp,
      PlayerDeathReason damageSource)
    {
      if (WorldSavingSystem.MasochistModeReal && ((Entity) this.Player).whoAmI == Main.myPlayer)
      {
        foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (npc =>
        {
          if (!((Entity) npc).active)
            return false;
          return npc.boss || npc.type == 14 || npc.type == 13 || npc.type == 15;
        })))
        {
          int num = npc.lifeMax / 10;
          if (Main.netMode == 0)
          {
            npc.life += num;
            if (npc.life > npc.lifeMax)
              npc.life = npc.lifeMax;
            npc.HealEffect(num, true);
            npc.netUpdate = true;
          }
          else
          {
            ModPacket packet = ((ModType) this).Mod.GetPacket(256);
            ((BinaryWriter) packet).Write((byte) 12);
            ((BinaryWriter) packet).Write((byte) ((Entity) npc).whoAmI);
            ((BinaryWriter) packet).Write(num);
            packet.Send(-1, -1);
          }
        }
      }
      if ((!Main.snowMoon || NPC.waveNumber >= 15) && (!Main.pumpkinMoon || NPC.waveNumber >= 15) || !WorldSavingSystem.MasochistModeReal)
        return;
      if (NPC.waveNumber > 1)
        --NPC.waveNumber;
      NPC.waveKills /= 4f;
      FargoSoulsUtil.PrintLocalization("Mods.FargowiltasSouls.Message.MoonsDeathPenalty", new Color(175, 75, (int) byte.MaxValue));
    }

    public virtual void ModifyWeaponDamage(Item item, ref StatModifier damage)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      EmodeItemBalance.BalanceWeaponStats(this.Player, item, ref damage);
    }

    public float AttackSpeed
    {
      get => this.Player.FargoSouls().AttackSpeed;
      set => this.Player.FargoSouls().AttackSpeed = value;
    }

    public virtual bool ModifyNurseHeal(
      NPC nurse,
      ref int health,
      ref bool removeDebuffs,
      ref string chatText)
    {
      if (!WorldSavingSystem.EternityMode)
        return base.ModifyNurseHeal(nurse, ref health, ref removeDebuffs, ref chatText);
      if (!Main.LocalPlayer.HasBuff(ModContent.BuffType<RushJobBuff>()))
        return base.ModifyNurseHeal(nurse, ref health, ref removeDebuffs, ref chatText);
      chatText = Language.GetTextValue("Mods.FargowiltasSouls.Buffs.RushJobBuff.NurseChat");
      return false;
    }

    public virtual void PostNurseHeal(NPC nurse, int health, bool removeDebuffs, int price)
    {
      if (!WorldSavingSystem.EternityMode || !Luminance.Common.Utilities.Utilities.AnyBosses())
        return;
      Main.LocalPlayer.AddBuff(ModContent.BuffType<RushJobBuff>(), 10, true, false);
    }

    static EModePlayer()
    {
      List<int> intList1 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList1, 5);
      Span<int> span1 = CollectionsMarshal.AsSpan<int>(intList1);
      int num1 = 0;
      span1[num1] = 6;
      int num2 = num1 + 1;
      span1[num2] = 472;
      int num3 = num2 + 1;
      span1[num3] = 167;
      int num4 = num3 + 1;
      span1[num4] = 473;
      int num5 = num4 + 1;
      span1[num5] = 239;
      int num6 = num5 + 1;
      EModePlayer.IronTiles = intList1;
      List<int> intList2 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList2, 3);
      Span<int> span2 = CollectionsMarshal.AsSpan<int>(intList2);
      int num7 = 0;
      span2[num7] = 145;
      int num8 = num7 + 1;
      span2[num8] = 245;
      int num9 = num8 + 1;
      span2[num9] = 107;
      num6 = num9 + 1;
      EModePlayer.IronWalls = intList2;
    }
  }
}
