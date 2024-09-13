// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.FargoSoulsGlobalProjectile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Shadow;
using FargowiltasSouls.Content.Bosses.Champions.Timber;
using FargowiltasSouls.Content.Bosses.TrojanSquirrel;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
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
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class FargoSoulsGlobalProjectile : GlobalProjectile
  {
    public int counter;
    public bool Rainbow;
    public int GrazeCD;
    public bool CanSplit = true;
    public bool NinjaCanSpeedup = true;
    public int stormTimer;
    public float TungstenScale = 1f;
    public int AdamModifier;
    public bool tikiMinion;
    public int tikiTimer;
    public float shroomiteMushroomCD;
    public Vector2 shroomiteStorePosition;
    private int spookyCD;
    public bool FrostFreeze;
    public bool ChilledProj;
    public int ChilledTimer;
    public int NinjaSpeedup;
    public bool canUmbrellaReflect = true;
    public int HuntressProj = -1;
    public Func<Projectile, bool> GrazeCheck = (Func<Projectile, bool>) (projectile =>
    {
      if ((double) ((Entity) projectile).Distance(((Entity) Main.LocalPlayer).Center) < (double) (Math.Min(((Entity) projectile).width, ((Entity) projectile).height) / 2 + 42) + (double) Main.LocalPlayer.FargoSouls().GrazeRadius)
      {
        if (projectile.ModProjectile != null)
        {
          bool? nullable = projectile.ModProjectile.CanDamage();
          bool flag = false;
          if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
            goto label_4;
        }
        return Collision.CanHit(((Entity) projectile).Center, 0, 0, ((Entity) Main.LocalPlayer).Center, 0, 0);
      }
label_4:
      return false;
    });
    private bool firstTick = true;
    private readonly bool squeakyToy;
    public const int TimeFreezeMoveDuration = 10;
    public int TimeFrozen;
    public bool TimeFreezeImmune;
    public int DeletionImmuneRank;
    public float CirnoBurst;
    public bool IsAHeldProj;
    public bool canHurt = true;
    public bool noInteractionWithNPCImmunityFrames;
    private int tempIframe;
    public static List<int> ShroomiteBlacklist;
    public static List<int> ShroomiteNerfList;
    private const int MAX_TIKI_TIMER = 20;

    public virtual bool InstancePerEntity => true;

    public virtual void SetStaticDefaults()
    {
      A_SourceNPCGlobalProjectile.SourceNPCSync[696] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[596] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[657] = true;
      A_SourceNPCGlobalProjectile.DamagingSync[696] = true;
      A_SourceNPCGlobalProjectile.DamagingSync[756] = true;
      A_SourceNPCGlobalProjectile.DamagingSync[961] = true;
      A_SourceNPCGlobalProjectile.DamagingSync[496] = true;
    }

    public virtual void SetDefaults(Projectile projectile)
    {
      switch (projectile.type)
      {
        case 190:
          this.NinjaCanSpeedup = false;
          break;
        case 250:
        case 251:
          this.DeletionImmuneRank = 1;
          break;
        case 309:
        case 378:
        case 379:
        case 644:
        case 664:
        case 666:
        case 668:
        case 680:
        case 688:
        case 689:
        case 690:
        case 694:
        case 695:
        case 696:
        case 698:
          projectile.DamageType = DamageClass.Summon;
          break;
        case 384:
        case 386:
          this.DeletionImmuneRank = 1;
          break;
        case 408:
        case 433:
        case 614:
          ProjectileID.Sets.MinionShot[projectile.type] = true;
          break;
        case 447:
        case 455:
        case 537:
        case 657:
        case 658:
        case 923:
        case 961:
          this.DeletionImmuneRank = 1;
          break;
        case 460:
        case 461:
        case 632:
        case 633:
          this.DeletionImmuneRank = 1;
          this.TimeFreezeImmune = true;
          break;
        case 623:
        case 624:
        case 925:
          this.TimeFreezeImmune = true;
          break;
        case 642:
          projectile.DamageType = DamageClass.Summon;
          this.DeletionImmuneRank = 1;
          break;
        case 645:
          this.DeletionImmuneRank = 1;
          break;
        case 656:
          this.DeletionImmuneRank = 1;
          break;
        case 687:
          this.DeletionImmuneRank = 1;
          break;
        case 933:
          this.DeletionImmuneRank = 2;
          this.TimeFreezeImmune = true;
          break;
      }
    }

    public virtual void OnSpawn(Projectile projectile, IEntitySource source)
    {
      if (WorldGen.generatingWorld || projectile.owner < 0 || projectile.owner >= (int) byte.MaxValue)
        return;
      Player player = Main.player[projectile.owner];
      FargoSoulsPlayer modPlayer = player.FargoSouls();
      if (projectile.friendly && FargoSoulsUtil.IsSummonDamage(projectile, includeWhips: false) && source is EntitySource_Parent entitySourceParent1 && entitySourceParent1.Entity is Projectile entity1 && FargoSoulsUtil.IsSummonDamage(entity1, includeWhips: false) && entity1.FargoSouls().tikiMinion)
      {
        this.tikiMinion = true;
        this.tikiTimer = entity1.FargoSouls().tikiTimer;
      }
      if (player.HasEffect<NinjaEffect>() && FargoSoulsUtil.OnSpawnEnchCanAffectProjectile(projectile, true) && projectile.type != 651 && ((Entity) projectile).whoAmI != player.heldProj && this.NinjaCanSpeedup && projectile.aiStyle != 190 && !projectile.minion)
        NinjaEnchant.NinjaSpeedSetup(modPlayer, projectile, this);
      this.shroomiteStorePosition = ((Entity) projectile).Center;
      switch (projectile.type)
      {
        case 298:
          if (player.HasEffect<SpectreEffect>() && !modPlayer.TerrariaSoul)
          {
            projectile.extraUpdates = 1;
            projectile.timeLeft = 180 * projectile.MaxUpdates;
            break;
          }
          break;
        case 496:
          if (projectile.damage > 0 && source is EntitySource_Parent entitySourceParent2 && entitySourceParent2.Entity is NPC entity2 && ((Entity) entity2).active && entity2.type == ModContent.NPCType<ShadowChampion>())
          {
            projectile.DamageType = DamageClass.Default;
            projectile.friendly = false;
            projectile.hostile = true;
            break;
          }
          break;
        case 596:
          if (projectile.damage > 0 && source is EntitySource_Parent entitySourceParent3 && entitySourceParent3.Entity is NPC entity3 && ((Entity) entity3).active && entity3.type == ModContent.NPCType<ShadowChampion>())
          {
            projectile.damage = FargoSoulsUtil.ScaledProjectileDamage(entity3.defDamage);
            break;
          }
          break;
        case 657:
          if (projectile.damage > 0 && source is EntitySource_Parent entitySourceParent4 && entitySourceParent4.Entity is NPC entity4 && ((Entity) entity4).active)
          {
            if (entity4.type == ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>())
            {
              projectile.damage = FargoSoulsUtil.ScaledProjectileDamage(entity4.defDamage);
              projectile.timeLeft = (double) entity4.ai[0] != 5.0 ? 90 : Math.Min(projectile.timeLeft, 450 - (int) entity4.ai[1]);
              break;
            }
            if (entity4.type == ModContent.NPCType<ShadowChampion>())
            {
              projectile.damage = FargoSoulsUtil.ScaledProjectileDamage(entity4.defDamage);
              break;
            }
            break;
          }
          break;
        case 696:
          if (projectile.damage > 0 && source is EntitySource_Parent entitySourceParent5 && entitySourceParent5.Entity is NPC entity5 && ((Entity) entity5).active && (entity5.ModNPC is TrojanSquirrelPart || entity5.type == ModContent.NPCType<TimberChampion>()))
          {
            projectile.DamageType = DamageClass.Default;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.alpha = 0;
            this.DeletionImmuneRank = 1;
            break;
          }
          break;
        case 756:
        case 961:
          if (source is EntitySource_ItemUse entitySourceItemUse && (entitySourceItemUse.Item.type == ModContent.ItemType<Deerclawps>() || entitySourceItemUse.Item.type == ModContent.ItemType<LumpOfFlesh>() || entitySourceItemUse.Item.type == ModContent.ItemType<MasochistSoul>()))
          {
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.DamageType = DamageClass.Melee;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = false;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 10;
            projectile.FargoSouls().CanSplit = false;
            projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
            Mod mod;
            if (Terraria.ModLoader.ModLoader.TryGetMod("Fargowiltas", ref mod))
            {
              mod.Call(new object[2]
              {
                (object) "LowRenderProj",
                (object) projectile
              });
              break;
            }
            break;
          }
          break;
        case 931:
          if (source is EntitySource_Misc entitySourceMisc && entitySourceMisc.Context.Equals("Pearlwood"))
          {
            projectile.usesLocalNPCImmunity = false;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 10;
            this.noInteractionWithNPCImmunityFrames = true;
            break;
          }
          break;
      }
      if (player.HasEffect<TungstenEffect>())
        TungstenEffect.TungstenIncreaseProjSize(projectile, modPlayer, source);
      if (player.HasEffect<HuntressEffect>() && FargoSoulsUtil.IsProjSourceItemUseReal(projectile, source) && projectile.damage > 0 && projectile.friendly && !projectile.hostile && !projectile.trap && projectile.DamageType != DamageClass.Default && !ProjectileID.Sets.CultistIsResistantTo[projectile.type] && !FargoSoulsUtil.IsSummonDamage(projectile, includeWhips: false))
        this.HuntressProj = 1;
      if (player.HasEffect<AdamantiteEffect>() && FargoSoulsUtil.OnSpawnEnchCanAffectProjectile(projectile, false) && this.CanSplit && Array.IndexOf<int>(FargoSoulsGlobalProjectile.NoSplit, projectile.type) <= -1 && projectile.aiStyle != 19 && projectile.owner == Main.myPlayer && !((IEnumerable<int>) AdamantiteEffect.AdamIgnoreItems).Contains<int>(modPlayer.Player.HeldItem.type) && modPlayer.Player.heldProj != ((Entity) projectile).whoAmI && (FargoSoulsUtil.IsProjSourceItemUseReal(projectile, source) || source is EntitySource_Parent entitySourceParent6 && entitySourceParent6.Entity is Projectile entity6 && (entity6.aiStyle == 19 || entity6.minion || entity6.sentry || ProjectileID.Sets.IsAWhip[entity6.type] && !ProjectileID.Sets.IsAWhip[projectile.type])))
      {
        projectile.ArmorPenetration += projectile.damage / 2;
        AdamantiteEffect.AdamantiteSplit(projectile, modPlayer, 1 + (int) modPlayer.AdamantiteSpread);
        this.AdamModifier = modPlayer.ForceEffect<AdamantiteEnchant>() ? 3 : 2;
      }
      if (!projectile.bobber || !this.CanSplit || !(source is EntitySource_ItemUse) || ((Entity) player).whoAmI != Main.myPlayer || !modPlayer.FishSoul2)
        return;
      FargoSoulsGlobalProjectile.SplitProj(projectile, 11, 1.04719758f, 1f);
    }

    public static int[] NoSplit
    {
      get
      {
        return new int[13]
        {
          656,
          633,
          632,
          379,
          630,
          615,
          460,
          651,
          705,
          439,
          927,
          714,
          444
        };
      }
    }

    public virtual bool PreAI(Projectile projectile)
    {
      bool flag = true;
      Player player = Main.player[projectile.owner];
      FargoSoulsPlayer modPlayer = player.FargoSouls();
      ++this.counter;
      if (this.IsAHeldProj)
      {
        projectile.damage = player.GetWeaponDamage(player.HeldItem, false);
        projectile.CritChance = player.GetWeaponCrit(player.HeldItem);
      }
      if (this.spookyCD > 0)
        --this.spookyCD;
      if (projectile.owner == Main.myPlayer)
      {
        if ((double) this.TungstenScale != 1.0 && !player.HasEffect<TungstenEffect>())
        {
          ((Entity) projectile).position = ((Entity) projectile).Center;
          projectile.scale /= this.TungstenScale;
          ((Entity) projectile).width = (int) ((double) ((Entity) projectile).width / (double) this.TungstenScale);
          ((Entity) projectile).height = (int) ((double) ((Entity) projectile).height / (double) this.TungstenScale);
          ((Entity) projectile).Center = ((Entity) projectile).position;
          this.TungstenScale = 1f;
        }
        switch (projectile.type)
        {
          case 556:
          case 557:
          case 558:
          case 559:
          case 560:
          case 561:
            if (projectile.owner == Main.myPlayer && player.HeldItem.type == ModContent.ItemType<Blender>() && (double) ++projectile.localAI[0] > 60.0)
            {
              SoundStyle npcDeath11 = SoundID.NPCDeath11;
              ((SoundStyle) ref npcDeath11).Volume = 0.5f;
              SoundEngine.PlaySound(ref npcDeath11, new Vector2?(((Entity) projectile).Center), (SoundUpdateCallback) null);
              int num = ModContent.ProjectileType<BlenderProj3>();
              Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, Vector2.op_Multiply(((Entity) projectile).DirectionFrom(((Entity) player).Center), 8f), num, projectile.damage, projectile.knockBack, projectile.owner, 0.0f, 0.0f, 0.0f);
              projectile.Kill();
              break;
            }
            break;
        }
        if (player.HasEffect<MahoganyEffect>() && projectile.aiStyle == 7)
          MahoganyEffect.MahoganyHookAI(projectile, modPlayer);
        if (!projectile.hostile && !projectile.trap && !projectile.npcProj)
        {
          if (modPlayer.Jammed && projectile.CountsAsClass(DamageClass.Ranged) && projectile.type != 178)
          {
            Projectile.NewProjectile(Entity.InheritSource((Entity) projectile), ((Entity) projectile).Center, ((Entity) projectile).velocity, 178, 0, 0.0f, projectile.owner, 0.0f, 0.0f, 0.0f);
            ((Entity) projectile).active = false;
          }
          if (modPlayer.Atrophied && projectile.CountsAsClass(DamageClass.Throwing))
          {
            projectile.damage = 0;
            projectile.Kill();
          }
          if (player.HasEffect<ShroomiteShroomEffect>() && projectile.damage > 0 && !FargoSoulsGlobalProjectile.ShroomiteBlacklist.Contains(projectile.type) && (double) ((Vector2) ref ((Entity) projectile).velocity).Length() > 1.0 && (double) projectile.minionSlots == 0.0 && projectile.type != ModContent.ProjectileType<ShroomiteShroom>() && player.ownedProjectileCounts[ModContent.ProjectileType<ShroomiteShroom>()] < 75)
          {
            float num = 100f;
            if (FargoSoulsGlobalProjectile.ShroomiteNerfList.Contains(projectile.type))
              num = 800f;
            if ((double) this.shroomiteMushroomCD >= (double) num)
            {
              this.shroomiteMushroomCD = 0.0f;
              if (modPlayer.ForceEffect<ShroomiteEnchant>())
                this.shroomiteMushroomCD += num / 4f;
              if ((double) player.stealth == 0.0)
                this.shroomiteMushroomCD += num / 4f;
              int index = Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, ((Entity) projectile).velocity, ModContent.ProjectileType<ShroomiteShroom>(), projectile.damage / 4, projectile.knockBack / 2f, projectile.owner, 0.0f, 0.0f, 0.0f);
              if (index != Main.maxProjectiles)
                Main.projectile[index].FargoSouls().AdamModifier = this.AdamModifier;
            }
            this.shroomiteMushroomCD += Vector2.Distance(((Entity) projectile).Center, this.shroomiteStorePosition);
            this.shroomiteStorePosition = ((Entity) projectile).Center;
          }
          if (player.HasEffect<SpookyEffect>() && (double) projectile.minionSlots > 0.0 && this.spookyCD == 0)
          {
            float num = 500f;
            int index1 = -1;
            for (int index2 = 0; index2 < Main.maxNPCs; ++index2)
            {
              NPC npc = Main.npc[index2];
              if (((Entity) npc).active && (double) Vector2.Distance(((Entity) projectile).Center, ((Entity) npc).Center) < (double) num && Main.npc[index2].CanBeChasedBy((object) projectile, false))
              {
                index1 = index2;
                num = Vector2.Distance(((Entity) projectile).Center, ((Entity) npc).Center);
              }
            }
            if (index1 != -1)
            {
              NPC npc = Main.npc[index1];
              if (Collision.CanHit(((Entity) projectile).Center, 0, 0, ((Entity) npc).Center, 0, 0))
              {
                Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) projectile).Center)), 20f);
                Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, vector2, ModContent.ProjectileType<SpookyScythe>(), projectile.damage, 2f, projectile.owner, 0.0f, 0.0f, 0.0f);
                SoundStyle soundStyle = SoundID.Item62;
                ((SoundStyle) ref soundStyle).Volume = 0.5f;
                SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) projectile).Center), (SoundUpdateCallback) null);
                this.spookyCD = 30 + Main.rand.Next(player.maxMinions * 5);
                if (modPlayer.ForceEffect<SpookyEnchant>())
                  this.spookyCD -= 10;
              }
            }
          }
        }
        if (modPlayer.Asocial && FargoSoulsUtil.IsSummonDamage(projectile, includeWhips: false))
        {
          projectile.Kill();
          flag = false;
        }
      }
      if (this.ChilledTimer > 0)
      {
        --this.ChilledTimer;
        if (flag && this.ChilledTimer % 3 == 1)
        {
          flag = false;
          ((Entity) projectile).position = ((Entity) projectile).oldPosition;
          ++projectile.timeLeft;
        }
        if (this.ChilledTimer <= 0)
          this.ChilledProj = false;
      }
      if (this.TimeFrozen > 0 && !this.firstTick && !this.TimeFreezeImmune)
      {
        if (this.counter % projectile.MaxUpdates == 0)
          --this.TimeFrozen;
        if (this.counter > 10 * projectile.MaxUpdates)
        {
          ((Entity) projectile).position = ((Entity) projectile).oldPosition;
          if (projectile.frameCounter > 0)
            --projectile.frameCounter;
          if (flag)
          {
            flag = false;
            ++projectile.timeLeft;
          }
        }
      }
      if (this.Rainbow)
      {
        projectile.tileCollide = false;
        if (this.counter >= 5)
          ((Entity) projectile).velocity = Vector2.Zero;
        int num = 15;
        if (projectile.hostile)
          num = 60;
        if (this.counter >= num)
          projectile.Kill();
      }
      if (this.firstTick)
      {
        if (projectile.type == 290)
        {
          NPC sourceNpc = projectile.GetSourceNPC();
          if (sourceNpc != null && sourceNpc.type == ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>())
            projectile.timeLeft = WorldSavingSystem.MasochistModeReal ? 1200 : 420;
        }
        if (projectile.type == 696 && projectile.hostile)
        {
          NPC sourceNpc = projectile.GetSourceNPC();
          if (sourceNpc != null && (sourceNpc.type == ModContent.NPCType<FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel>() || sourceNpc.type == ModContent.NPCType<TimberChampion>()))
          {
            ((Entity) projectile).position = ((Entity) projectile).Bottom;
            ((Entity) projectile).height = 96;
            ((Entity) projectile).Bottom = ((Entity) projectile).position;
            projectile.hostile = true;
            projectile.friendly = false;
          }
        }
        this.firstTick = false;
      }
      switch (projectile.type)
      {
        case 699:
        case 708:
          Vector2 vector2_1 = player.RotatedRelativePoint(player.MountedCenter, false, true);
          ((Entity) projectile).direction = ((Entity) player).direction;
          player.heldProj = ((Entity) projectile).whoAmI;
          ((Entity) projectile).Center = vector2_1;
          if (player.dead)
          {
            projectile.Kill();
            return false;
          }
          if (!player.frozen)
          {
            if (projectile.type == 699)
            {
              projectile.spriteDirection = ((Entity) projectile).direction = ((Entity) player).direction;
              Vector2 vector2_2 = vector2_1;
              projectile.alpha -= (int) sbyte.MaxValue;
              if (projectile.alpha < 0)
                projectile.alpha = 0;
              if ((double) projectile.localAI[0] > 0.0)
                --projectile.localAI[0];
              float num1 = 1f - (float) player.itemAnimation / (float) player.itemAnimationMax;
              float rotation = Utils.ToRotation(((Entity) projectile).velocity);
              float num2 = ((Vector2) ref ((Entity) projectile).velocity).Length() * projectile.scale;
              float num3 = 22f * projectile.scale;
              Vector2 vector2_3 = Vector2.op_Multiply(Utils.RotatedBy(new Vector2(1f, 0.0f), 3.1415927410125732 + (double) num1 * 6.2831854820251465, new Vector2()), new Vector2(num2, projectile.ai[0] * projectile.scale));
              Projectile projectile1 = projectile;
              ((Entity) projectile1).position = Vector2.op_Addition(((Entity) projectile1).position, Vector2.op_Addition(Utils.RotatedBy(vector2_3, (double) rotation, new Vector2()), Utils.RotatedBy(new Vector2(num2 + num3, 0.0f), (double) rotation, new Vector2())));
              Vector2 vector2_4 = Vector2.op_Addition(Vector2.op_Addition(vector2_2, Utils.RotatedBy(vector2_3, (double) rotation, new Vector2())), Utils.RotatedBy(new Vector2((float) ((double) num2 + (double) num3 + 40.0), 0.0f), (double) rotation, new Vector2()));
              projectile.rotation = Utils.AngleTo(vector2_2, vector2_4) + 0.7853982f * (float) ((Entity) player).direction;
              if (projectile.spriteDirection == -1)
                projectile.rotation += 3.14159274f;
              Luminance.Common.Utilities.Utilities.SafeDirectionTo(vector2_2, ((Entity) projectile).Center);
              Vector2 vector2_5 = Luminance.Common.Utilities.Utilities.SafeDirectionTo(vector2_2, vector2_4);
              Vector2 vector2_6 = Utils.SafeNormalize(((Entity) projectile).velocity, Vector2.UnitY);
              float num4 = 2f;
              for (int index = 0; (double) index < (double) num4; ++index)
              {
                Dust dust1 = Dust.NewDustDirect(((Entity) projectile).Center, 14, 14, 228, 0.0f, 0.0f, 110, new Color(), 1f);
                dust1.velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo(vector2_2, dust1.position), 2f);
                dust1.position = Vector2.op_Addition(((Entity) projectile).Center, Vector2.op_Multiply(Utils.RotatedBy(vector2_6, (double) num1 * 6.2831854820251465 * 2.0 + (double) index / (double) num4 * 6.2831854820251465, new Vector2()), 10f));
                dust1.scale = (float) (1.0 + 0.60000002384185791 * (double) Utils.NextFloat(Main.rand));
                Dust dust2 = dust1;
                dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(vector2_6, 3f));
                dust1.noGravity = true;
              }
              for (int index = 0; index < 1; ++index)
              {
                if (Utils.NextBool(Main.rand, 3))
                {
                  Dust dust = Dust.NewDustDirect(((Entity) projectile).Center, 20, 20, 228, 0.0f, 0.0f, 110, new Color(), 1f);
                  dust.velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo(vector2_2, dust.position), 2f);
                  dust.position = Vector2.op_Addition(((Entity) projectile).Center, Vector2.op_Multiply(vector2_5, -110f));
                  dust.scale = (float) (0.44999998807907104 + 0.40000000596046448 * (double) Utils.NextFloat(Main.rand));
                  dust.fadeIn = (float) (0.699999988079071 + 0.40000000596046448 * (double) Utils.NextFloat(Main.rand));
                  dust.noGravity = true;
                  dust.noLight = true;
                }
              }
            }
            else if (projectile.type == 708)
            {
              Lighting.AddLight(((Entity) player).Center, 0.75f, 0.9f, 1.15f);
              projectile.spriteDirection = ((Entity) projectile).direction = ((Entity) player).direction;
              projectile.alpha -= (int) sbyte.MaxValue;
              if (projectile.alpha < 0)
                projectile.alpha = 0;
              float num5 = 1f - (float) player.itemAnimation / (float) player.itemAnimationMax;
              float rotation = Utils.ToRotation(((Entity) projectile).velocity);
              float num6 = ((Vector2) ref ((Entity) projectile).velocity).Length() * projectile.scale;
              float num7 = 22f * projectile.scale;
              Vector2 vector2_7 = Vector2.op_Multiply(Utils.RotatedBy(new Vector2(1f, 0.0f), 3.1415927410125732 + (double) num5 * 6.2831854820251465, new Vector2()), new Vector2(num6, projectile.ai[0] * projectile.scale));
              Projectile projectile2 = projectile;
              ((Entity) projectile2).position = Vector2.op_Addition(((Entity) projectile2).position, Vector2.op_Addition(Utils.RotatedBy(vector2_7, (double) rotation, new Vector2()), Utils.RotatedBy(new Vector2(num6 + num7, 0.0f), (double) rotation, new Vector2())));
              Vector2 vector2_8 = Vector2.op_Addition(Vector2.op_Addition(vector2_1, Utils.RotatedBy(vector2_7, (double) rotation, new Vector2())), Utils.RotatedBy(new Vector2((float) ((double) num6 + (double) num7 + 40.0), 0.0f), (double) rotation, new Vector2()));
              projectile.rotation = Utils.ToRotation(Utils.SafeNormalize(Vector2.op_Subtraction(vector2_8, vector2_1), Vector2.UnitX)) + 0.7853982f * (float) ((Entity) player).direction;
              if (projectile.spriteDirection == -1)
                projectile.rotation += 3.14159274f;
              Utils.SafeNormalize(Vector2.op_Subtraction(((Entity) projectile).Center, vector2_1), Vector2.Zero);
              Utils.SafeNormalize(Vector2.op_Subtraction(vector2_8, vector2_1), Vector2.Zero);
              Vector2 vector2_9 = Utils.SafeNormalize(((Entity) projectile).velocity, Vector2.UnitY);
              if ((player.itemAnimation == 2 || player.itemAnimation == 6 || player.itemAnimation == 10) && projectile.owner == Main.myPlayer)
              {
                Vector2 vector2_10 = Vector2.op_Multiply(Vector2.op_Addition(vector2_9, Utils.NextVector2Square(Main.rand, -0.2f, 0.2f)), 12f);
                switch (player.itemAnimation)
                {
                  case 2:
                    vector2_10 = Utils.RotatedBy(vector2_9, 0.38397246599197388, new Vector2());
                    break;
                  case 6:
                    vector2_10 = Utils.RotatedBy(vector2_9, -0.38397246599197388, new Vector2());
                    break;
                  case 10:
                    vector2_10 = Utils.RotatedBy(vector2_9, 0.0, new Vector2());
                    break;
                }
                Vector2 vector2_11 = Vector2.op_Multiply(vector2_10, 10f + (float) Main.rand.Next(4));
                Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, vector2_11, 709, projectile.damage, 0.0f, projectile.owner, 0.0f, 0.0f, 0.0f);
              }
              for (int index = 0; index < 3; index += 2)
              {
                float num8 = 1f;
                float num9 = 1f;
                switch (index - 1)
                {
                  case 0:
                    num9 = -1f;
                    break;
                  case 1:
                    num9 = 1.25f;
                    num8 = 0.5f;
                    break;
                  case 2:
                    num9 = -1.25f;
                    num8 = 0.5f;
                    break;
                }
                if (!Utils.NextBool(Main.rand, 6))
                {
                  float num10 = num9 * 1.2f;
                  Dust dust = Dust.NewDustDirect(((Entity) projectile).position, ((Entity) projectile).width, ((Entity) projectile).height, 226, 0.0f, 0.0f, 100, new Color(), 1f);
                  dust.velocity = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(vector2_9, (float) (4.0 + 4.0 * (double) Utils.NextFloat(Main.rand))), num10), num8);
                  dust.noGravity = true;
                  dust.noLight = true;
                  dust.scale = 0.75f;
                  dust.fadeIn = 0.8f;
                  dust.customData = (object) this;
                  if (Utils.NextBool(Main.rand, 3))
                  {
                    dust.noGravity = false;
                    dust.fadeIn = 0.0f;
                  }
                }
              }
            }
          }
          if (((Entity) player).whoAmI == Main.myPlayer && player.itemAnimation <= 2)
          {
            projectile.Kill();
            player.reuseDelay = 2;
          }
          return false;
        default:
          return flag;
      }
    }

    public virtual bool PreDraw(Projectile projectile, ref Color lightColor)
    {
      switch (projectile.type)
      {
        case 221:
          FargoSoulsUtil.GenericProjectileDraw(projectile, lightColor);
          return false;
        case 556:
        case 557:
        case 558:
        case 559:
        case 560:
        case 561:
          Player player = Main.player[projectile.owner];
          if (player.HeldItem.type == ModContent.ItemType<Blender>())
          {
            Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/PlanteraTentacle", (AssetRequestMode) 1).Value;
            Rectangle rectangle;
            // ISSUE: explicit constructor call
            ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
            Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
            SpriteEffects spriteEffects = projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
            float num = Utils.ToRotation(Vector2.op_Subtraction(((Entity) projectile).Center, ((Entity) player).Center)) + 3.14159274f;
            if (projectile.spriteDirection < 0)
              num += 3.14159274f;
            Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) projectile).Center, Main.screenPosition), new Vector2(0.0f, projectile.gfxOffY)), new Rectangle?(rectangle), projectile.GetAlpha(lightColor), num, vector2, projectile.scale * 0.8f, spriteEffects, 0.0f);
            return false;
          }
          break;
        case 595:
        case 735:
          FargoSoulsUtil.GenericProjectileDraw(projectile, lightColor);
          return false;
        case 711:
          NPC sourceNpc = projectile.GetSourceNPC();
          if (sourceNpc != null && sourceNpc.type == 551)
          {
            Texture2D texture = TextureAssets.Projectile[686].Value;
            FargoSoulsUtil.GenericProjectileDraw(projectile, lightColor, texture);
            return false;
          }
          break;
        case 927:
          if ((double) this.TungstenScale != 1.0)
          {
            float tungstenScale = this.TungstenScale;
            float num1 = this.TungstenScale * 1.25f;
            int num2 = 3;
            int num3 = 2;
            Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) projectile).Center, Vector2.op_Multiply(Utils.ToRotationVector2(projectile.rotation), (float) num3));
            float num4 = Utils.NextFloat(Main.rand);
            float num5 = Utils.GetLerpValue(0.0f, 0.3f, num4, true) * Utils.GetLerpValue(1f, 0.5f, num4, true);
            Color color1 = Color.op_Multiply(projectile.GetAlpha(Lighting.GetColor(Utils.ToTileCoordinates(((Entity) projectile).Center))), num5);
            Texture2D texture2D1 = TextureAssets.Item[4923].Value;
            Vector2 vector2_2 = Vector2.op_Division(Utils.Size(texture2D1), 2f);
            float num6 = Utils.NextFloatDirection(Main.rand);
            float num7 = (float) (8.0 + (double) MathHelper.Lerp(0.0f, 20f, num4) + (double) Utils.NextFloat(Main.rand) * 6.0) * tungstenScale;
            float num8 = projectile.rotation + (float) ((double) num6 * 6.2831854820251465 * 0.039999999105930328);
            float num9 = num8 + 0.7853982f;
            Vector2 vector2_3 = Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Utils.ToRotationVector2(num8), num7)), Utils.NextVector2Circular(Main.rand, 8f, 8f)), Main.screenPosition);
            SpriteEffects spriteEffects1 = (SpriteEffects) 0;
            if ((double) projectile.rotation < -1.5707963705062866 || (double) projectile.rotation > 1.5707963705062866)
            {
              num9 += 1.57079637f;
              spriteEffects1 = (SpriteEffects) (spriteEffects1 | 1);
            }
            Main.spriteBatch.Draw(texture2D1, vector2_3, new Rectangle?(), color1, num9, vector2_2, tungstenScale, spriteEffects1, 0.0f);
            for (int index = 0; index < num2; ++index)
            {
              float num10 = Utils.NextFloat(Main.rand);
              float num11 = Utils.GetLerpValue(0.0f, 0.3f, num10, true) * Utils.GetLerpValue(1f, 0.5f, num10, true);
              float num12 = MathHelper.Lerp(0.6f, 1f, Utils.GetLerpValue(0.0f, 0.3f, num10, true) * Utils.GetLerpValue(1f, 0.5f, num10, true)) * num1;
              Color queenWeaponsColor = projectile.GetFairyQueenWeaponsColor(0.25f, 0.0f, new float?((float) (((double) Utils.NextFloat(Main.rand) * 0.33000001311302185 + (double) Main.GlobalTimeWrappedHourly) % 1.0)));
              Texture2D texture2D2 = TextureAssets.Projectile[projectile.type].Value;
              Color color2 = Color.op_Multiply(queenWeaponsColor, num11 * 0.5f);
              Vector2 vector2_4 = Vector2.op_Division(Utils.Size(texture2D2), 2f);
              Color color3 = Color.op_Multiply(Color.White, num11);
              ref Color local = ref color3;
              ((Color) ref local).A = (byte) ((uint) ((Color) ref local).A / 2U);
              Color color4 = Color.op_Multiply(color3, 0.5f);
              float num13 = 1f;
              float num14 = Utils.NextFloat(Main.rand) * 2f;
              float num15 = Utils.NextFloatDirection(Main.rand);
              Vector2 vector2_5 = Vector2.op_Multiply(Vector2.op_Multiply(new Vector2(2.8f + num14, 1f), num13), num12);
              Vector2.op_Multiply(Vector2.op_Multiply(new Vector2((float) (1.5 + (double) num14 * 0.5), 1f), num13), num12);
              int num16 = 50;
              Vector2 vector2_6 = Vector2.op_Multiply(Utils.ToRotationVector2(projectile.rotation), index >= 1 ? 56f : 0.0f);
              float num17 = (float) (0.029999999329447746 - (double) index * 0.012000000104308128);
              float num18 = (float) (30.0 + (double) MathHelper.Lerp(0.0f, (float) num16, num10) + (double) num14 * 16.0) * num1;
              float num19 = projectile.rotation + num15 * 6.28318548f * num17;
              float num20 = num19;
              Vector2 vector2_7 = Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Utils.ToRotationVector2(num19), num18)), Utils.NextVector2Circular(Main.rand, 20f, 20f)), vector2_6), Main.screenPosition);
              Color color5 = Color.op_Multiply(color2, num13);
              Color color6 = Color.op_Multiply(color4, num13);
              SpriteEffects spriteEffects2 = (SpriteEffects) 0;
              Main.spriteBatch.Draw(texture2D2, vector2_7, new Rectangle?(), color5, num20, vector2_4, vector2_5, spriteEffects2, 0.0f);
              Main.spriteBatch.Draw(texture2D2, vector2_7, new Rectangle?(), color6, num20, vector2_4, Vector2.op_Multiply(vector2_5, 0.6f), spriteEffects2, 0.0f);
            }
            return false;
          }
          break;
      }
      return base.PreDraw(projectile, ref lightColor);
    }

    public static List<Projectile> SplitProj(
      Projectile projectile,
      int number,
      float maxSpread,
      float damageRatio,
      bool allowMoreSplit = false)
    {
      ModProjectile modProjectile;
      if (ModContent.TryFind<ModProjectile>("Fargowiltas", "SpawnProj", ref modProjectile) && projectile.type == modProjectile.Type)
        return (List<Projectile>) null;
      if (number % 2 != 0)
        --number;
      List<Projectile> projectileList = new List<Projectile>();
      double num1 = (double) maxSpread / (double) number;
      for (int index1 = 0; index1 < number / 2; ++index1)
      {
        for (int index2 = 0; index2 < 2; ++index2)
        {
          int num2 = index2 == 0 ? 1 : -1;
          Projectile projectile1 = FargoSoulsUtil.NewProjectileDirectSafe(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, Utils.RotatedBy(((Entity) projectile).velocity, (double) num2 * num1 * (double) (index1 + 1), new Vector2()), projectile.type, (int) ((double) projectile.damage * (double) damageRatio), projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1]);
          if (projectile1 != null)
          {
            projectile1.ai[2] = projectile.ai[2];
            projectile1.localAI[0] = projectile.localAI[0];
            projectile1.localAI[1] = projectile.localAI[1];
            projectile1.localAI[2] = projectile.localAI[2];
            projectile1.friendly = projectile.friendly;
            projectile1.hostile = projectile.hostile;
            projectile1.timeLeft = projectile.timeLeft;
            projectile1.DamageType = projectile.DamageType;
            if (!allowMoreSplit)
              projectile1.FargoSouls().CanSplit = false;
            projectile1.FargoSouls().TungstenScale = projectile.FargoSouls().TungstenScale;
            projectileList.Add(projectile1);
          }
        }
      }
      return projectileList;
    }

    private static void KillPet(
      Projectile projectile,
      Player player,
      int buff,
      bool toggle,
      bool minion = false)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.FindBuffIndex(buff) != -1 || !player.dead && toggle && (minion ? (!player.HasEffect<StardustEffect>() ? 1 : 0) : (!fargoSoulsPlayer.VoidSoul ? 1 : 0)) == 0 && (fargoSoulsPlayer.PetsActive || minion))
        return;
      projectile.Kill();
    }

    public virtual void AI(Projectile projectile)
    {
      Player player = Main.player[projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      switch (projectile.type)
      {
        case 16:
        case 34:
        case 79:
          if ((double) projectile.ai[0] != -1.0 && (double) projectile.ai[1] != -1.0 && this.counter > 900 && Main.player[projectile.owner].ownedProjectileCounts[projectile.type] > 1)
          {
            projectile.Kill();
            --Main.player[projectile.owner].ownedProjectileCounts[projectile.type];
            break;
          }
          break;
        case 129:
          if ((double) projectile.ai[0] == 1.0)
          {
            if ((double) projectile.localAI[0] == 0.0)
            {
              projectile.localAI[0] = ((Entity) projectile).Center.X;
              projectile.localAI[1] = ((Entity) projectile).Center.Y;
            }
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) projectile).Center, new Vector2(projectile.localAI[0], projectile.localAI[1]));
            if (Vector2.op_Inequality(vector2, Vector2.Zero) && (double) ((Vector2) ref vector2).Length() >= 300.0)
            {
              ((Entity) projectile).velocity = Utils.RotatedBy(vector2, Math.PI / 2.0, new Vector2());
              ((Vector2) ref ((Entity) projectile).velocity).Normalize();
              Projectile projectile1 = projectile;
              ((Entity) projectile1).velocity = Vector2.op_Multiply(((Entity) projectile1).velocity, 8f);
              break;
            }
            break;
          }
          break;
        case 623:
          FargoSoulsGlobalProjectile.KillPet(projectile, player, 187, player.HasEffect<StardustMinionEffect>(), true);
          break;
      }
      if (this.stormTimer > 0)
      {
        --this.stormTimer;
        int index = Dust.NewDust(((Entity) projectile).position, ((Entity) projectile).width, ((Entity) projectile).height, 228, ((Entity) projectile).velocity.X, ((Entity) projectile).velocity.Y, 100, new Color(), 1.5f);
        Main.dust[index].noGravity = true;
      }
      if (this.ChilledProj)
      {
        int index = Dust.NewDust(((Entity) projectile).position, ((Entity) projectile).width, ((Entity) projectile).height, 76, ((Entity) projectile).velocity.X, ((Entity) projectile).velocity.Y, 100, new Color(), 1f);
        Main.dust[index].noGravity = true;
        Projectile projectile2 = projectile;
        ((Entity) projectile2).position = Vector2.op_Subtraction(((Entity) projectile2).position, Vector2.op_Multiply(((Entity) projectile).velocity, 0.5f));
      }
      if (this.NinjaSpeedup > 0 && player.heldProj != ((Entity) projectile).whoAmI)
      {
        projectile.extraUpdates = Math.Max(projectile.extraUpdates, this.NinjaSpeedup);
        if (projectile.owner == Main.myPlayer && !player.HasEffect<NinjaEffect>())
          projectile.Kill();
      }
      if (projectile.bobber && fargoSoulsPlayer.FishSoul1 && ((Entity) projectile).wet && (double) projectile.ai[0] == 0.0 && (double) projectile.ai[1] == 0.0 && (double) projectile.localAI[1] < 655.0)
        projectile.localAI[1] = 655f;
      if (ProjectileID.Sets.IsAWhip[projectile.type] && projectile.owner == Main.myPlayer && Main.player[projectile.owner].HasEffect<TikiEffect>())
      {
        foreach (Projectile projectile3 in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && !p.hostile && p.owner == Main.myPlayer && FargoSoulsUtil.IsSummonDamage(p, includeWhips: false) && !ProjectileID.Sets.IsAWhip[p.type] && projectile.Colliding(((Entity) projectile).Hitbox, ((Entity) p).Hitbox))))
        {
          projectile3.FargoSouls().tikiMinion = true;
          projectile3.FargoSouls().tikiTimer = 20 * projectile3.MaxUpdates;
        }
      }
      if (!this.tikiMinion)
        return;
      if (projectile.type != 433)
      {
        ((Entity) projectile).position.X += ((Entity) projectile).velocity.X;
        if (!projectile.tileCollide || (double) ((Entity) projectile).velocity.Y < 0.0 || projectile.shouldFallThrough)
          ((Entity) projectile).position.Y += ((Entity) projectile).velocity.Y;
      }
      if (this.tikiTimer > 0)
        --this.tikiTimer;
      else
        this.tikiMinion = false;
      if (!Utils.NextBool(Main.rand, 2))
        return;
      int index1 = Dust.NewDust(new Vector2(((Entity) projectile).position.X - 2f, ((Entity) projectile).position.Y - 2f), ((Entity) projectile).width + 4, ((Entity) projectile).height + 4, 44, ((Entity) projectile).velocity.X * 0.4f, ((Entity) projectile).velocity.Y * 0.4f, 100, Color.LimeGreen, 0.8f);
      Main.dust[index1].noGravity = true;
      Dust dust = Main.dust[index1];
      dust.velocity = Vector2.op_Multiply(dust.velocity, 1.8f);
      Main.dust[index1].velocity.Y -= 0.5f;
      if (!Utils.NextBool(Main.rand, 4))
        return;
      Main.dust[index1].noGravity = false;
      Main.dust[index1].scale *= 0.5f;
    }

    public virtual void PostAI(Projectile projectile)
    {
      Player player = Main.player[projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (((Entity) projectile).whoAmI == player.heldProj || projectile.aiStyle == 75 || projectile.type == 632)
      {
        this.DeletionImmuneRank = 2;
        this.TimeFreezeImmune = true;
        this.IsAHeldProj = true;
        if (player.HeldItem.IsWeapon())
        {
          fargoSoulsPlayer.WeaponUseTimer = Math.Max(fargoSoulsPlayer.WeaponUseTimer, 2);
          fargoSoulsPlayer.TryAdditionalAttacks(projectile.damage, projectile.DamageType);
          player.AccessoryEffects().TryAdditionalAttacks(projectile.damage, projectile.DamageType);
          if (projectile.type == 705 && player.HasEffect<MythrilEffect>() && fargoSoulsPlayer.Player.FargoSouls().MythrilTimer > -60 && this.counter > 60)
            projectile.Kill();
        }
        if (projectile.type == 630)
          player.reuseDelay = Math.Max(0, 20 - this.counter);
      }
      if (projectile.hostile && projectile.damage > 0 && projectile.aiStyle != 10 && --this.GrazeCD < 0)
      {
        this.GrazeCD = 6;
        if (((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead)
        {
          FargoSoulsPlayer fargoPlayer = Main.LocalPlayer.FargoSouls();
          if (fargoPlayer.Graze && !Main.LocalPlayer.immune && Main.LocalPlayer.hurtCooldowns[0] <= 0 && Main.LocalPlayer.hurtCooldowns[1] <= 0)
          {
            bool? nullable = ProjectileLoader.CanDamage(projectile);
            bool flag = false;
            if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue) && ProjectileLoader.CanHitPlayer(projectile, Main.LocalPlayer) && this.GrazeCheck(projectile))
            {
              this.GrazeCD = 30 * projectile.MaxUpdates;
              if (fargoPlayer.NekomiSet)
                NekomiHood.OnGraze(fargoPlayer, projectile.damage * 4);
              if (fargoPlayer.DeviGraze)
                SparklingAdoration.OnGraze(fargoPlayer, projectile.damage * 4);
              if (fargoPlayer.CirnoGraze)
                IceQueensCrown.OnGraze(fargoPlayer, projectile.damage * 4);
            }
          }
        }
      }
      if (this.HuntressProj == 1 && (double) Utils.Distance(((Entity) projectile).Center, ((Entity) Main.player[projectile.owner]).Center) > 1500.0)
      {
        fargoSoulsPlayer.HuntressStage = 0;
        this.HuntressProj = -1;
      }
      if ((double) this.CirnoBurst <= 0.0)
        return;
      this.CirnoBurst -= 1f / (float) projectile.MaxUpdates;
      if ((double) this.CirnoBurst <= 0.0 && Main.myPlayer == projectile.owner)
      {
        Vector2 vector2 = Vector2.op_Multiply(Utils.NextVector2Unit(Main.rand, 0.0f, 6.28318548f), Math.Max(((Vector2) ref ((Entity) projectile).velocity).Length(), 8f));
        Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, vector2, ModContent.ProjectileType<FrostShardFriendly>(), projectile.damage, 2f, projectile.owner, 0.0f, 0.0f, 0.0f);
      }
      projectile.Kill();
    }

    public virtual bool TileCollideStyle(
      Projectile projectile,
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      if (projectile.type == 196)
        fallThrough = false;
      if ((double) this.TungstenScale != 1.0)
      {
        width = (int) ((double) width / (double) this.TungstenScale);
        height = (int) ((double) height / (double) this.TungstenScale);
      }
      return base.TileCollideStyle(projectile, ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual bool? CanDamage(Projectile projectile)
    {
      if (!this.canHurt)
        return new bool?(false);
      return this.TimeFrozen > 0 && this.counter > 10 * projectile.MaxUpdates ? new bool?(false) : base.CanDamage(projectile);
    }

    public virtual bool CanHitPlayer(Projectile projectile, Player target) => true;

    public virtual void ModifyHitNPC(
      Projectile projectile,
      NPC target,
      ref NPC.HitModifiers modifiers)
    {
      FargoSoulsPlayer fargoSoulsPlayer = Main.player[projectile.owner].FargoSouls();
      if (this.stormTimer > 0)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, fargoSoulsPlayer.ForceEffect<ForbiddenEnchant>() ? 1.6f : 1.3f);
      }
      if ((double) this.TungstenScale != 1.0 && projectile.type == 927)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, 0.4f);
      }
      if (Main.player[projectile.owner].HasEffect<NinjaEffect>())
      {
        float num = fargoSoulsPlayer.ForceEffect<NinjaEnchant>() ? 0.3f : 0.2f;
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, (float) (1.0 + (double) num * (double) Math.Min((float) ((double) (projectile.extraUpdates + 1) * (double) ((Vector2) ref ((Entity) projectile).velocity).Length() / 40.0), 1f)));
      }
      if (this.noInteractionWithNPCImmunityFrames)
        this.tempIframe = target.immune[projectile.owner];
      if (projectile.type == 756 && !projectile.usesLocalNPCImmunity && projectile.usesIDStaticNPCImmunity && projectile.idStaticNPCHitCooldown == 60 && this.noInteractionWithNPCImmunityFrames)
        ((NPC.HitModifiers) ref modifiers).SetCrit();
      if (!this.tikiMinion || this.tikiTimer <= 20 * projectile.MaxUpdates / 4)
        return;
      ((NPC.HitModifiers) ref modifiers).SetCrit();
    }

    public virtual void OnHitNPC(
      Projectile projectile,
      NPC target,
      NPC.HitInfo hit,
      int damageDone)
    {
      if (this.noInteractionWithNPCImmunityFrames)
        target.immune[projectile.owner] = this.tempIframe;
      if (Main.player[projectile.owner].HasEffect<NinjaEffect>())
        hit.Knockback *= 2f * Math.Min((float) ((double) (projectile.extraUpdates + 1) * (double) ((Vector2) ref ((Entity) projectile).velocity).Length() / 40.0), 1f);
      if (projectile.type == 756 && !projectile.usesLocalNPCImmunity && projectile.usesIDStaticNPCImmunity && projectile.idStaticNPCHitCooldown == 60 && this.noInteractionWithNPCImmunityFrames)
      {
        target.AddBuff(ModContent.BuffType<AnticoagulationBuff>(), 360, false);
        if (FargoSoulsUtil.NPCExists(target.realLife, Array.Empty<int>()) != null)
        {
          foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && (n.realLife == target.realLife || ((Entity) n).whoAmI == target.realLife) && ((Entity) n).whoAmI != ((Entity) target).whoAmI)))
            Projectile.perIDStaticNPCImmunity[projectile.type][((Entity) npc).whoAmI] = Main.GameUpdateCount + (uint) projectile.idStaticNPCHitCooldown;
        }
      }
      if (this.FrostFreeze)
      {
        target.AddBuff(324, 360, false);
        target.FargoSouls();
        int num1 = ModContent.BuffType<FrozenBuff>();
        int num2 = target.HasBuff(num1) ? 5 : 15;
        NPC head = FargoSoulsUtil.NPCExists(target.realLife, Array.Empty<int>());
        if (head != null)
        {
          head.AddBuff(num1, num2, false);
          foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.realLife == ((Entity) head).whoAmI && ((Entity) n).whoAmI != ((Entity) head).whoAmI)))
            npc.AddBuff(num1, num2, false);
        }
        else
          target.AddBuff(num1, num2, false);
      }
      Main.player[projectile.owner].FargoSouls();
      if (this.AdamModifier != 0)
        this.ReduceIFrames(projectile, target, this.AdamModifier);
      if (projectile.type != 80 || Main.player[projectile.owner].FargoSouls().FrigidGemstoneItem == null)
        return;
      target.AddBuff(44, 360, false);
    }

    private void ReduceIFrames(Projectile projectile, NPC target, int iframeModifier)
    {
      if (projectile.maxPenetrate == 1 || projectile.usesLocalNPCImmunity)
        return;
      if (projectile.usesIDStaticNPCImmunity)
      {
        if (projectile.idStaticNPCHitCooldown <= 1)
          return;
        Projectile.perIDStaticNPCImmunity[projectile.type][((Entity) target).whoAmI] = Main.GameUpdateCount + (uint) RoundReduce((float) projectile.idStaticNPCHitCooldown);
      }
      else
      {
        if (this.noInteractionWithNPCImmunityFrames || target.immune[projectile.owner] <= 1)
          return;
        target.immune[projectile.owner] = (int) RoundReduce((float) target.immune[projectile.owner]);
      }

      double RoundReduce(float iframes)
      {
        double num = Math.Round((double) iframes / (double) iframeModifier, 0, Utils.NextBool(Main.rand, 3) ? (MidpointRounding) 1 : (MidpointRounding) 2);
        if (num < 1.0)
          num = 1.0;
        return num;
      }
    }

    public virtual void ModifyHitPlayer(
      Projectile projectile,
      Player target,
      ref Player.HurtModifiers modifiers)
    {
      NPC sourceNpc = projectile.GetSourceNPC();
      if (sourceNpc != null && sourceNpc.FargoSouls().BloodDrinker)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, 1.3f);
      }
      if (!this.squeakyToy)
        return;
      ((Player.HurtModifiers) ref modifiers).SetMaxDamage(1);
      FargoSoulsPlayer.Squeak(((Entity) target).Center);
    }

    public virtual void OnKill(Projectile projectile, int timeLeft)
    {
      FargoSoulsPlayer fargoSoulsPlayer = Main.player[projectile.owner].FargoSouls();
      if (this.HuntressProj != 1)
        return;
      fargoSoulsPlayer.HuntressStage = 0;
    }

    public virtual void GrapplePullSpeed(Projectile projectile, Player player, ref float speed)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!player.HasEffect<MahoganyEffect>())
        return;
      float num = 1.5f;
      if (fargoSoulsPlayer.ForceEffect<RichMahoganyEnchant>())
        num = 2.5f;
      speed *= num;
    }

    public virtual void GrappleRetreatSpeed(Projectile projectile, Player player, ref float speed)
    {
      if (!player.HasEffect<MahoganyEffect>())
        return;
      float num = 3f;
      speed *= num;
    }

    public virtual void PostDraw(Projectile projectile, Color lightColor)
    {
      if (projectile.type != 129)
        return;
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/RuneBlast", (AssetRequestMode) 1).Value;
      int num1 = texture2D.Height / Main.projFrames[projectile.type];
      int num2 = num1 * projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) projectile).Center, Main.screenPosition), new Vector2(0.0f, projectile.gfxOffY)), new Rectangle?(rectangle), new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), projectile.rotation, vector2, projectile.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) projectile).Center, Main.screenPosition), new Vector2(0.0f, projectile.gfxOffY)), new Rectangle?(rectangle), new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), projectile.rotation, vector2, projectile.scale, spriteEffects, 0.0f);
    }

    static FargoSoulsGlobalProjectile()
    {
      List<int> intList1 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList1, 1);
      Span<int> span1 = CollectionsMarshal.AsSpan<int>(intList1);
      int num1 = 0;
      span1[num1] = ModContent.ProjectileType<MeteorFlame>();
      int num2 = num1 + 1;
      FargoSoulsGlobalProjectile.ShroomiteBlacklist = intList1;
      List<int> intList2 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList2, 2);
      Span<int> span2 = CollectionsMarshal.AsSpan<int>(intList2);
      int num3 = 0;
      span2[num3] = ModContent.ProjectileType<MechEyeProjectile>();
      int num4 = num3 + 1;
      span2[num4] = ModContent.ProjectileType<MechFlail>();
      num2 = num4 + 1;
      FargoSoulsGlobalProjectile.ShroomiteNerfList = intList2;
    }
  }
}
