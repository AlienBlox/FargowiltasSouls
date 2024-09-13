// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Core.Systems;
using Luminance.Common.StateMachines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.CursedCoffin
{
  [AutoloadBossHead]
  public class CursedCoffin : ModNPC
  {
    public const bool Enabled = false;
    private bool Attacking;
    private bool ExtraTrail;
    public bool PhaseTwo;
    public int MashTimer;
    private int Frame;
    private Vector2 LockVector1;
    public static readonly Color GlowColor = new Color(224, 196, 252, 0);
    public const int BaseHP = 2222;
    public const int WavyShotFlightPrepTime = 60;
    public const int WavyShotFlightCirclingTime = 280;
    public const int WavyShotFlightEndTime = 0;
    public const int RandomStuffOpenTime = 60;
    public static readonly SoundStyle PhaseTransitionSFX = new SoundStyle("FargowiltasSouls/Assets/Sounds/CoffinPhaseTransition", (SoundType) 0);
    public static readonly SoundStyle SlamSFX;
    public static readonly SoundStyle SpiritDroneSFX;
    public static readonly SoundStyle BigShotSFX;
    public static readonly SoundStyle ShotSFX;
    public static readonly SoundStyle SoulShotSFX;
    public static readonly SoundStyle HandChargeSFX;
    private readonly List<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates> P1Attacks;
    private readonly List<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates> P2Attacks;
    private PushdownAutomata<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates> stateMachine;

    public virtual bool IsLoadingEnabled(Mod mod) => false;

    private int LastAttackChoice { get; set; }

    public float Timer
    {
      get => (float) this.StateMachine.CurrentState.Time;
      set => this.StateMachine.CurrentState.Time = (int) value;
    }

    public ref float ForceGrabPunish => ref this.NPC.ai[1];

    public ref float AI2 => ref this.NPC.ai[2];

    public ref float AI3 => ref this.NPC.ai[3];

    public Vector2 MaskCenter()
    {
      return Vector2.op_Subtraction(((Entity) this.NPC).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, (float) ((Entity) this.NPC).height), this.NPC.scale), 4f));
    }

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 4;
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 18;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
      NPCID.Sets.MPAllowedEnemies[this.Type] = true;
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 6);
      Span<int> span = CollectionsMarshal.AsSpan<int>(debuffs);
      int num1 = 0;
      span[num1] = 31;
      int num2 = num1 + 1;
      span[num2] = 46;
      int num3 = num2 + 1;
      span[num3] = 68;
      int num4 = num3 + 1;
      span[num4] = ModContent.BuffType<LethargicBuff>();
      int num5 = num4 + 1;
      span[num5] = ModContent.BuffType<ClippedWingsBuff>();
      int num6 = num5 + 1;
      span[num6] = ModContent.BuffType<TimeFrozenBuff>();
      int num7 = num6 + 1;
      npc.AddDebuffImmunities(debuffs);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual void SetDefaults()
    {
      this.NPC.aiStyle = -1;
      this.NPC.lifeMax = 2222;
      this.NPC.defense = 10;
      this.NPC.damage = 35;
      this.NPC.knockBackResist = 0.0f;
      ((Entity) this.NPC).width = 90;
      ((Entity) this.NPC).height = 150;
      this.NPC.boss = true;
      this.NPC.lavaImmune = true;
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit4);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath6);
      this.Music = 81;
      this.SceneEffectPriority = (SceneEffectPriority) 6;
      this.NPC.value = (float) Item.buyPrice(0, 2, 0, 0);
    }

    public virtual bool ModifyCollisionData(
      Rectangle victimHitbox,
      ref int immunityCooldownSlot,
      ref MultipliableFloat damageMultiplier,
      ref Rectangle npcHitbox)
    {
      if ((double) this.NPC.rotation != 0.0)
      {
        int num1 = npcHitbox.X + npcHitbox.Width / 2;
        int num2 = npcHitbox.Y + npcHitbox.Height / 2;
        float num3 = MathF.Abs(MathF.Sin(this.NPC.rotation % 6.28318548f));
        npcHitbox.Height = (int) ((double) MathHelper.Lerp((float) ((Entity) this.NPC).height, (float) ((Entity) this.NPC).width, num3) * (double) this.NPC.scale);
        npcHitbox.Width = (int) ((double) MathHelper.Lerp((float) ((Entity) this.NPC).width, (float) ((Entity) this.NPC).height, num3) * (double) this.NPC.scale);
        npcHitbox.X = num1 - npcHitbox.Width / 2;
        npcHitbox.Y = num2 - npcHitbox.Height / 2;
      }
      return true;
    }

    public virtual void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
    {
      if (!this.PhaseTwo && projectile.Colliding(((Entity) projectile).Hitbox, this.TopHitbox()) && this.Frame <= 1)
      {
        this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit54);
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, 1.3f);
      }
      else
        this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit4);
      base.ModifyHitByProjectile(projectile, ref modifiers);
    }

    public virtual void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
    {
      if (!this.PhaseTwo)
      {
        Rectangle hitbox = ((Entity) item).Hitbox;
        if (((Rectangle) ref hitbox).Intersects(this.TopHitbox()) && this.Frame <= 1)
        {
          this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit54);
          ref StatModifier local = ref modifiers.FinalDamage;
          local = StatModifier.op_Multiply(local, 1.3f);
          goto label_4;
        }
      }
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit4);
label_4:
      base.ModifyHitByItem(player, item, ref modifiers);
    }

    public Rectangle TopHitbox()
    {
      return new Rectangle((int) ((Entity) this.NPC).position.X, (int) ((Entity) this.NPC).position.Y, ((Entity) this.NPC).width, ((Entity) this.NPC).height / 3);
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * (double) balance);
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.NPC.localAI[0]);
      writer.Write(this.NPC.localAI[1]);
      writer.Write(this.NPC.localAI[2]);
      writer.Write(this.NPC.localAI[3]);
      writer.Write(this.PhaseTwo);
      writer.Write7BitEncodedInt(this.LastAttackChoice);
      writer.Write(this.StateMachine.StateStack.Count);
      EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>[] array = this.StateMachine.StateStack.ToArray();
      for (int index = 0; index < this.StateMachine.StateStack.Count; ++index)
        writer.Write((int) array[index].Identifier);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.NPC.localAI[0] = reader.ReadSingle();
      this.NPC.localAI[1] = reader.ReadSingle();
      this.NPC.localAI[2] = reader.ReadSingle();
      this.NPC.localAI[3] = reader.ReadSingle();
      this.PhaseTwo = reader.ReadBoolean();
      this.LastAttackChoice = reader.Read7BitEncodedInt();
      this.Timer = reader.ReadSingle();
      int num = reader.ReadInt32();
      this.StateMachine.StateStack.Clear();
      for (int index = 0; index < num; ++index)
        this.StateMachine.StateStack.Push(this.StateMachine.StateRegistry[(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates) reader.ReadInt32()]);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      if (this.NPC.IsABestiaryIconDummy)
        return true;
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos);
      SpriteEffects spriteEffects1 = ((Entity) this.NPC).direction == 1 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < (this.ExtraTrail ? NPCID.Sets.TrailCacheLength[this.NPC.type] : NPCID.Sets.TrailCacheLength[this.NPC.type] / 4); ++index)
      {
        Vector2 oldPo = this.NPC.oldPos[index];
        int frame = this.Frame;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, frame * texture2D1.Height / Main.npcFrameCount[this.NPC.type], texture2D1.Width, texture2D1.Height / Main.npcFrameCount[this.NPC.type]);
        DrawData drawData;
        // ISSUE: explicit constructor call
        ((DrawData) ref drawData).\u002Ector(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(drawColor, 0.5f / (float) index), this.NPC.rotation, new Vector2((float) (texture2D1.Width / 2), (float) (texture2D1.Height / 2 / Main.npcFrameCount[this.NPC.type])), this.NPC.scale, spriteEffects1, 0.0f);
        GameShaders.Misc["LCWingShader"].UseColor(Color.Blue).UseSecondaryColor(Color.Black);
        GameShaders.Misc["LCWingShader"].Apply(new DrawData?(drawData));
        ((DrawData) ref drawData).Draw(spriteBatch);
      }
      SpriteBatch spriteBatch1 = spriteBatch;
      Vector2 vector2_2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_2).\u002Ector((float) (texture2D1.Width / 2), (float) (texture2D1.Height / 2 / Main.npcFrameCount[this.NPC.type]));
      Texture2D texture2D2 = texture2D1;
      Vector2 vector2_3 = vector2_1;
      Rectangle? nullable = new Rectangle?(this.NPC.frame);
      Color color = drawColor;
      double rotation = (double) this.NPC.rotation;
      Vector2 vector2_4 = vector2_2;
      double scale = (double) this.NPC.scale;
      SpriteEffects spriteEffects2 = spriteEffects1;
      spriteBatch1.Draw(texture2D2, vector2_3, nullable, color, (float) rotation, vector2_4, (float) scale, spriteEffects2, 0.0f);
      if (!this.PhaseTwo)
      {
        float num1 = 1f;
        if (this.StateMachine.CurrentState != null && this.StateMachine.CurrentState.Identifier == FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.PhaseTransition)
          num1 = (float) (3.0 + 5.0 * ((double) this.Timer / 60.0));
        Texture2D texture2D3 = ModContent.Request<Texture2D>(this.Texture + "_MaskGlow", (AssetRequestMode) 1).Value;
        Color glowColor = FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.GlowColor;
        int num2 = (int) ((double) Main.GlobalTimeWrappedHourly * 60.0) % 60;
        DrawData drawData;
        // ISSUE: explicit constructor call
        ((DrawData) ref drawData).\u002Ector(texture2D3, Vector2.op_Addition(vector2_1, Utils.NextVector2Circular(Main.rand, num1, num1)), new Rectangle?(this.NPC.frame), Color.op_Multiply(glowColor, (float) (0.75 + 0.25 * (double) MathF.Sin((float) (6.2831854820251465 * (double) num2 / 60.0)))), this.NPC.rotation, new Vector2((float) (texture2D1.Width / 2), (float) (texture2D1.Height / 2 / Main.npcFrameCount[this.NPC.type])), this.NPC.scale, spriteEffects1, 0.0f);
        GameShaders.Misc["LCWingShader"].UseColor(Color.Purple).UseSecondaryColor(Color.Black);
        GameShaders.Misc["LCWingShader"].Apply(new DrawData?(drawData));
        ((DrawData) ref drawData).Draw(spriteBatch);
      }
      return false;
    }

    public virtual void FindFrame(int frameHeight)
    {
      this.NPC.spriteDirection = ((Entity) this.NPC).direction;
      this.NPC.frame.Y = frameHeight * this.Frame;
    }

    public virtual void OnKill()
    {
      NPC.SetEventFlagCleared(ref WorldSavingSystem.downedBoss[11], -1);
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 188;

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
    }

    public Player Player => Main.player[this.NPC.target];

    public virtual void OnSpawn(IEntitySource source)
    {
      this.Targeting();
      Player player = Main.player[this.NPC.target];
      if (!player.Alive())
        return;
      ((Entity) this.NPC).position = Vector2.op_Subtraction(Vector2.op_Addition(((Entity) player).Center, new Vector2(0.0f, -700f)), Vector2.op_Division(((Entity) this.NPC).Size, 2f));
      this.LockVector1 = Vector2.op_Subtraction(((Entity) player).Top, Vector2.op_Multiply(Vector2.UnitY, 50f));
      ((Entity) this.NPC).velocity = new Vector2(0.0f, 0.25f);
    }

    public virtual bool? CanFallThroughPlatforms()
    {
      return !this.NPC.noTileCollide && (double) ((Entity) this.Player).Top.Y <= (double) ((Entity) this.NPC).Bottom.Y + 30.0 ? new bool?() : new bool?(true);
    }

    public virtual void AI()
    {
      this.NPC.defense = this.NPC.defDefense;
      if (this.PhaseTwo)
        this.NPC.defense += 15;
      this.NPC.rotation = 0.0f;
      this.NPC.noTileCollide = true;
      if (!this.Targeting())
        return;
      this.NPC.timeLeft = 60;
      if (this.StateMachine?.StateStack?.Count.GetValueOrDefault(1) <= 0)
        this.StateMachine.StateStack.Push(this.StateMachine.StateRegistry[FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.RefillAttacks]);
      this.StateMachine.PerformBehaviors();
      this.StateMachine.PerformStateTransitionCheck();
      if (this.StateMachine.StateStack.Count <= 0)
        return;
      ++this.Timer;
    }

    [AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.Opening)]
    public void Opening()
    {
      if ((double) this.Timer < 0.0)
        return;
      this.ExtraTrail = true;
      if ((double) Math.Abs(((Entity) this.NPC).velocity.Y) < 22.0)
        ((Entity) this.NPC).velocity.Y *= 1.04f;
      if ((double) ((Entity) this.NPC).Center.Y >= (double) this.LockVector1.Y || (double) this.Timer > 120.0)
      {
        this.NPC.noTileCollide = false;
        if ((double) ((Entity) this.NPC).velocity.Y <= 1.0)
        {
          SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.SlamSFX, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          this.ExtraTrail = false;
          this.Timer = -60f;
          if (FargoSoulsUtil.HostCheck)
          {
            for (int index = -1; index <= 1; index += 2)
            {
              Vector2 vector2 = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) index), 3f);
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Subtraction(((Entity) this.NPC).Bottom, Vector2.op_Multiply(Vector2.UnitY, 50f)), vector2, ModContent.ProjectileType<CoffinSlamShockwave>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.1f), 1f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
          if (WorldSavingSystem.EternityMode && !WorldSavingSystem.DownedBoss[11] && FargoSoulsUtil.HostCheck)
            Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) Main.player[this.NPC.target]).Hitbox, ModContent.ItemType<CoffinSummon>(), 1, false, 0, false, false);
        }
      }
      if ((double) ((Entity) this.NPC).Center.Y < (double) this.LockVector1.Y + 800.0)
        return;
      ((Entity) this.NPC).velocity = Vector2.Zero;
    }

    [AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.PhaseTransition)]
    public void PhaseTransition()
    {
      this.HoverSound();
      if ((double) this.Timer > 55.0)
        this.PhaseTwo = true;
      ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_UnaryNegation(Vector2.UnitY), 5f), (float) (1.0 - (double) this.Timer / 90.0));
      this.NPC.rotation = Utils.NextFloat(Main.rand, (float) (0.37699112296104431 * ((double) this.Timer / 60.0)));
      SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.SpiritDroneSFX, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
    }

    [AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.StunPunish)]
    public void StunPunish()
    {
      NPC npc = this.NPC;
      ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.95f);
      if ((double) this.Timer < 20.0)
      {
        if (++this.NPC.frameCounter % 4.0 != 3.0 || this.Frame >= Main.npcFrameCount[this.Type] - 1)
          return;
        ++this.Frame;
      }
      else if ((double) this.Timer == 20.0)
      {
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.ShotSFX, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (!FargoSoulsUtil.HostCheck)
          return;
        Vector2 rotationVector2 = Utils.ToRotationVector2(this.NPC.rotation);
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(rotationVector2, 4f), ModContent.ProjectileType<CoffinHand>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.1f), 1f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 22f, 0.0f);
      }
      else
      {
        if (++this.NPC.frameCounter % 60.0 != 59.0)
          return;
        --this.Frame;
      }
    }

    [AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.SpiritGrabPunish)]
    public void SpiritGrabPunish()
    {
      ref float local1 = ref this.AI2;
      ref float local2 = ref this.AI3;
      this.HoverSound();
      if (++this.NPC.frameCounter % 10.0 == 9.0 && this.Frame > 0)
        --this.Frame;
      if ((double) this.Timer <= 1.0)
      {
        local1 = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Player, ((Entity) this.NPC).Center));
        local2 = ((Entity) this.NPC).Distance(((Entity) this.Player).Center);
      }
      if ((double) this.Timer > 60.0)
        return;
      float num = this.Timer / 60f;
      ((Entity) this.NPC).velocity = Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Player).Center, Vector2.op_Multiply(MathHelper.Lerp(local2, 350f, num), Vector2.Lerp(Utils.ToRotationVector2(local1), Vector2.op_UnaryNegation(Vector2.UnitY), num))), ((Entity) this.NPC).Center);
    }

    [AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.HoveringForSlam)]
    public void HoveringForSlam()
    {
      ref float local1 = ref this.AI2;
      ref float local2 = ref this.AI3;
      this.HoverSound();
      if ((double) this.Timer == 1.0)
      {
        float num = Utils.Clamp<float>(((Entity) this.NPC).Center.X - ((Entity) this.Player).Center.X, -200f, 200f);
        local1 = MathF.Asin(num / 200f);
        local2 = (float) Main.rand.Next(160, 220);
        if (!this.PhaseTwo)
          local2 -= 55f;
      }
      if ((double) this.Timer >= (double) local2 || (double) this.Timer < 0.0)
        return;
      this.NPC.noTileCollide = true;
      float num1 = 200f * MathF.Sin(local1 + (float) (3.1415927410125732 * ((double) this.Timer / 90.0)));
      float num2 = (float) (30.0 * (double) MathF.Sin((float) (3.1415927410125732 * ((double) this.Timer / 45.0))) - 350.0);
      this.Movement(Vector2.op_Addition(Vector2.op_Addition(((Entity) this.Player).Center, Vector2.op_Multiply(num1, Vector2.UnitX)), Vector2.op_Multiply(num2, Vector2.UnitY)), 0.1f, 10f, decel: 0.08f, slowdown: 20f);
    }

    [AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.SlamWShockwave)]
    public void SlamWShockwave()
    {
      ref float local = ref this.AI2;
      this.NPC.noTileCollide = (double) ((Entity) this.NPC).Bottom.Y + (double) ((Entity) this.NPC).velocity.Y < (double) ((Entity) this.Player).Bottom.Y - 16.0;
      if ((double) this.Timer < 0.0)
        return;
      ((Entity) this.NPC).velocity.X *= 0.97f;
      float num = (double) local == 2.0 ? 0.35f : 0.2f;
      if (WorldSavingSystem.EternityMode)
        ((Entity) this.NPC).velocity.X += (float) Math.Sign(((Entity) this.Player).Center.X - ((Entity) this.NPC).Center.X) * num;
      if ((double) ((Entity) this.NPC).velocity.Y >= 0.0 && (double) local == 0.0)
        local = 1f;
      if ((double) ((Entity) this.NPC).velocity.Y == 0.0 && (double) local > 0.0 && !this.NPC.noTileCollide)
      {
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.SlamSFX, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.ExtraTrail = false;
        if (FargoSoulsUtil.HostCheck)
        {
          for (int index = -1; index <= 1; index += 2)
          {
            Vector2 vector2 = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) index), 3f);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Subtraction(((Entity) this.NPC).Bottom, Vector2.op_Multiply(Vector2.UnitY, 50f)), vector2, ModContent.ProjectileType<CoffinSlamShockwave>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.1f), 1f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
        if (WorldSavingSystem.EternityMode && (double) local < 2.0)
        {
          local = 2f;
          this.Timer = 0.0f;
          ((Entity) this.NPC).velocity.Y = -10f;
        }
        else
        {
          this.Timer = (float) -(WorldSavingSystem.MasochistModeReal ? 80 : (WorldSavingSystem.EternityMode ? 100 : 120));
          ((Entity) this.NPC).velocity.X = 0.0f;
        }
      }
      else
      {
        ((Entity) this.NPC).velocity.Y += 0.175f;
        if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
          ((Entity) this.NPC).velocity.Y += 0.32f;
        if ((double) ((Entity) this.NPC).velocity.Y > 15.0)
          ((Entity) this.NPC).velocity.Y = 15f;
        this.ExtraTrail = true;
        if ((double) ((Entity) this.NPC).Center.Y < (double) this.LockVector1.Y + 1000.0)
          return;
        ((Entity) this.NPC).velocity = Vector2.Zero;
      }
    }

    [AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.WavyShotCircle)]
    public void WavyShotCircle()
    {
      int num1 = WorldSavingSystem.MasochistModeReal ? 60 : 70;
      float num2 = (float) (1.0 - (double) this.Timer / (double) num1);
      Vector2 vector2_1 = this.MaskCenter();
      if ((double) this.Timer < (double) num1)
      {
        Vector2 vector2_2 = Utils.RotatedByRandom(Vector2.UnitX, 6.2831854820251465);
        float num3 = 120f * num2 * Utils.NextFloat(Main.rand, 0.6f, 1.3f);
        Vector2 worldPosition = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.op_Multiply(vector2_2, num3), 2f));
        float lifetime = 15f;
        Vector2 velocity = Vector2.op_Division(Vector2.op_Subtraction(vector2_1, worldPosition), lifetime);
        float scale = (float) (2.0 - (double) num2 * 1.2000000476837158);
        new SparkParticle(worldPosition, velocity, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.GlowColor, scale, (int) lifetime).Spawn();
      }
      else if ((double) this.Timer == (double) num1)
      {
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BigShotSFX, new Vector2?(vector2_1), (SoundUpdateCallback) null);
        int num4 = Main.expertMode ? (WorldSavingSystem.EternityMode ? (WorldSavingSystem.MasochistModeReal ? 12 : 10) : 8) : 6;
        if (!FargoSoulsUtil.HostCheck)
          return;
        float num5 = Utils.NextFloat(Main.rand, 6.28318548f);
        for (int index = 0; index < num4; ++index)
        {
          Vector2 vector2_3 = Vector2.op_Multiply(Utils.ToRotationVector2(num5 + (float) (6.2831854820251465 * ((double) index / (double) num4))), 4f);
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_1, vector2_3, ModContent.ProjectileType<CoffinWaveShot>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 1f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      else
      {
        if ((double) this.Timer <= (double) (num1 + (WorldSavingSystem.MasochistModeReal || (double) this.AI3 < 1.0 ? 20 : 50)) || !this.PhaseTwo || (double) this.AI3 >= 1.0 || !WorldSavingSystem.EternityMode)
          return;
        this.AI3 = 1f;
        this.Timer = 0.0f;
      }
    }

    [AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.WavyShotFlight)]
    public void WavyShotFlight()
    {
      ref float local1 = ref this.AI2;
      ref float local2 = ref this.AI3;
      this.NPC.noTileCollide = true;
      this.HoverSound();
      if ((double) this.Timer <= 60.0)
      {
        Vector2 from = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Player, ((Entity) this.NPC).Center);
        local2 = Utils.ToRotation(from);
        float num1 = FargoSoulsUtil.RotationDifference(from, Vector2.op_UnaryNegation(Vector2.UnitY));
        float num2 = (float) Math.Sign(num1);
        this.Movement(Vector2.op_Addition(((Entity) this.Player).Center, Vector2.op_Multiply(from, 350f)), 0.08f, 30f, decel: 0.06f, slowdown: 50f);
        local1 = (float) ((6.2831854820251465 - (double) Math.Abs(num1)) * -(double) num2);
      }
      else if ((double) this.Timer <= 340.0)
      {
        float x = (float) (((double) this.Timer - 60.0) / 280.0);
        float num3 = MomentumProgress(x);
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Player).Center, Vector2.op_Multiply(Utils.ToRotationVector2(local2 + (local1 + 6.28318548f * (float) Math.Sign(local1)) * num3), 350f));
        float num4 = Utils.Clamp<float>(x / 0.2f, 0.0f, 1f);
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Subtraction(vector2_1, ((Entity) this.NPC).Center), num4);
        if ((double) this.Timer % 15.0 != 0.0 || (double) x < 0.20000000298023224 || (double) x > 0.800000011920929)
          return;
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.ShotSFX, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (!FargoSoulsUtil.HostCheck)
          return;
        Vector2 vector2_2 = this.MaskCenter();
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_2, Vector2.op_Multiply(Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo(vector2_2, ((Entity) this.Player).Center), -0.31415927410125732, new Vector2()), 4f), ModContent.ProjectileType<CoffinWaveShot>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 1f, Main.myPlayer, 1f, 0.0f, 0.0f);
      }
      else
      {
        if ((double) this.Timer >= 340.0)
          return;
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.96f);
      }

      static float MomentumProgress(float x)
      {
        return (float) ((double) x * (double) x * 3.0 - (double) x * (double) x * (double) x * 2.0);
      }
    }

    [AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.GrabbyHands)]
    public void GrabbyHands()
    {
      this.NPC.noTileCollide = true;
      this.HoverSound();
      if ((double) this.Timer < 40.0)
      {
        this.Movement(Vector2.op_Addition(((Entity) this.Player).Center, Vector2.op_Addition(Vector2.op_Multiply(Vector2.op_UnaryNegation(Vector2.UnitY), 300f), Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.Player).Center.X)), 200f))), 0.1f, 10f, decel: 0.08f, slowdown: 20f);
      }
      else
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.97f);
      }
      if ((double) this.Timer == 2.0)
      {
        this.AI3 = (float) Main.rand.Next(90, 103);
        this.NPC.netUpdate = true;
      }
      if ((double) this.Timer > 2.0 && (double) this.Timer == (double) this.AI3)
      {
        foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => p.TypeAlive<CoffinHand>() && (double) p.ai[0] == (double) ((Entity) this.NPC).whoAmI && (double) p.ai[1] == 1.0)))
        {
          SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.HandChargeSFX, new Vector2?(((Entity) projectile).Center), (SoundUpdateCallback) null);
          projectile.ai[1] = 2f;
          projectile.netUpdate = true;
        }
      }
      if ((double) this.Timer < 40.0)
      {
        if (++this.NPC.frameCounter % 4.0 != 3.0 || this.Frame >= Main.npcFrameCount[this.Type] - 1)
          return;
        ++this.Frame;
      }
      else if ((double) this.Timer == 40.0)
      {
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.ShotSFX, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (!FargoSoulsUtil.HostCheck)
          return;
        Vector2 rotationVector2 = Utils.ToRotationVector2(this.NPC.rotation);
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(rotationVector2, 4f), ModContent.ProjectileType<CoffinHand>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.1f), 1f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 1f, 1f);
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(rotationVector2, 4f), ModContent.ProjectileType<CoffinHand>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.1f), 1f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 1f, -1f);
        if (!WorldSavingSystem.EternityMode)
          return;
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation + 1.57079637f), 4f), ModContent.ProjectileType<CoffinHand>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.5f), 1f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 1f, Utils.NextBool(Main.rand) ? 1.5f : -1.5f);
      }
      else
      {
        if (++this.NPC.frameCounter % 60.0 != 59.0 || this.Frame <= 0)
          return;
        --this.Frame;
      }
    }

    [AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.RandomStuff)]
    public void RandomStuff()
    {
      ref float local = ref this.AI3;
      this.NPC.noTileCollide = true;
      Vector2 angle = CalculateAngle();
      this.NPC.rotation = Utils.ToRotation(Vector2.Lerp(Utils.ToRotationVector2(this.NPC.rotation), angle, this.Timer / 35f));
      this.HoverSound();
      this.Movement(Vector2.op_Addition(((Entity) this.Player).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.Player).Center.X)), 500f)), 0.1f, 10f, decel: 0.08f, slowdown: 20f);
      int num1 = (int) MathF.Floor((float) (60 / Main.npcFrameCount[this.Type]));
      if ((double) this.Timer < 60.0)
      {
        if (++this.NPC.frameCounter % (double) num1 != (double) (num1 - 1) || this.Frame >= Main.npcFrameCount[this.Type] - 1)
          return;
        ++this.Frame;
      }
      else if ((double) this.Timer < 370.0 && (double) this.Timer >= 60.0)
      {
        ((Entity) this.NPC).velocity.X *= 0.7f;
        int num2 = WorldSavingSystem.MasochistModeReal ? 20 : 25;
        if ((double) this.Timer % (double) num2 == 0.0)
        {
          int num3;
          switch (Main.rand.Next(3))
          {
            case 1:
              num3 = 5;
              break;
            case 2:
              num3 = 6;
              break;
            default:
              num3 = Main.rand.Next(5);
              break;
          }
          local = (float) num3;
          this.NPC.netUpdate = true;
        }
        if ((double) this.Timer % (double) num2 != (double) (num2 - 1))
          return;
        float num4 = local;
        SoundStyle soundStyle = (double) num4 == 5.0 ? SoundID.Item106 : ((double) num4 == 6.0 ? SoundID.NPCHit2 : SoundID.Item101);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (!FargoSoulsUtil.HostCheck)
          return;
        Vector2 vector2_1 = Vector2.op_Multiply(angle, Utils.NextFloat(Main.rand, 0.9f, 1.3f));
        Vector2 vector2_2 = Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(angle), 1.5707963705062866, new Vector2()), Utils.NextFloat(Main.rand, (float) (-((Entity) this.NPC).height / 3), (float) (((Entity) this.NPC).height / 3)));
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, vector2_2), vector2_1, ModContent.ProjectileType<CoffinRandomStuff>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 1f, Main.myPlayer, local, 0.0f, 0.0f);
      }
      else
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.96f);
        if (++this.NPC.frameCounter % 30.0 == 29.0 && this.Frame > 0)
          --this.Frame;
        if (this.Frame <= 0)
          return;
        this.NPC.rotation *= 0.9f;
      }

      Vector2 CalculateAngle()
      {
        ref float local = ref this.AI3;
        float num1 = CoffinRandomStuff.Gravity(this.AI3);
        double num2 = (double) ((Entity) this.Player).Center.X - (double) ((Entity) this.NPC).Center.X;
        float num3 = ((Entity) this.Player).Center.Y - ((Entity) this.NPC).Center.Y;
        float num4 = -10f;
        if ((double) num3 < 0.0)
        {
          float num5 = (float) (-(double) MathF.Sqrt((float) -((double) num3 - 300.0) * num1) / 1.5);
          if ((double) num5 < (double) num4)
            num4 = num5;
        }
        double num6 = (double) (-num4 / num1 + MathF.Sqrt(MathF.Pow(num4 / num1, 2f) + 2f * num3 / num1));
        return Vector2.op_Addition(Vector2.op_Multiply((float) (num2 / num6), Vector2.UnitX), Vector2.op_Multiply(num4, Vector2.UnitY));
      }
    }

    public void HoverSound()
    {
      SoundStyle soundStyle = SoundID.Item24;
      ((SoundStyle) ref soundStyle).MaxInstances = 1;
      ((SoundStyle) ref soundStyle).SoundLimitBehavior = (SoundLimitBehavior) 0;
      ((SoundStyle) ref soundStyle).Pitch = -0.5f;
      ((SoundStyle) ref soundStyle).Volume = 10f;
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
    }

    public void Movement(
      Vector2 pos,
      float accel = 0.03f,
      float maxSpeed = 20f,
      float lowspeed = 5f,
      float decel = 0.03f,
      float slowdown = 30f)
    {
      if ((double) ((Entity) this.NPC).Distance(pos) > (double) slowdown)
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Utils.SafeNormalize(Vector2.op_Subtraction(pos, ((Entity) this.NPC).Center), Vector2.Zero), maxSpeed), accel);
      else
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Utils.SafeNormalize(Vector2.op_Subtraction(pos, ((Entity) this.NPC).Center), Vector2.Zero), lowspeed), decel);
    }

    public bool Targeting()
    {
      Player player1 = Main.player[this.NPC.target];
      if (!((Entity) player1).active || player1.dead || player1.ghost || (double) ((Entity) this.NPC).Distance(((Entity) player1).Center) > 2400.0)
      {
        this.NPC.TargetClosest(false);
        Player player2 = Main.player[this.NPC.target];
        if (!((Entity) player2).active || player2.dead || player2.ghost || (double) ((Entity) this.NPC).Distance(((Entity) player2).Center) > 2400.0)
        {
          if (this.NPC.timeLeft > 60)
            this.NPC.timeLeft = 60;
          ((Entity) this.NPC).velocity.Y -= 0.4f;
          return false;
        }
      }
      return true;
    }

    public PushdownAutomata<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates> StateMachine
    {
      get
      {
        if (this.stateMachine == null)
          this.LoadStateMachine();
        return this.stateMachine;
      }
      set => this.stateMachine = value;
    }

    private void LoadStateMachine()
    {
      this.StateMachine = new PushdownAutomata<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(new EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.Opening));
      for (int index = 0; index < 11; ++index)
        this.StateMachine.RegisterState(new EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>((FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates) index));
      // ISSUE: method pointer
      this.StateMachine.OnStateTransition += new PushdownAutomata<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>.OnStateTransitionDelegate((object) this, __methodptr(OnStateTransition));
      AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>.FillStateMachineBehaviors<ModNPC>(this.StateMachine, (ModNPC) this);
      this.LoadTransition_ResetCycle();
      this.LoadTransition_PhaseTwoTransition();
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.Opening, new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(), false, (Func<bool>) (() => (double) this.Timer == -1.0), (Action) null);
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.PhaseTransition, new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.SlamWShockwave), false, (Func<bool>) (() => (double) this.Timer >= 60.0), (Action) (() =>
      {
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.PhaseTransitionSFX, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.NPC.netUpdate = true;
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2 = this.MaskCenter();
          NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) vector2.X, (int) vector2.Y, ModContent.NPCType<CursedSpirit>(), 0, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
        }
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.UnitY, 0.1f);
        this.LockVector1 = Vector2.op_Subtraction(((Entity) this.Player).Top, Vector2.op_Multiply(Vector2.UnitY, 250f));
        this.AI2 = 3f;
      }));
      this.StateMachine.ApplyToAllStatesExcept((Action<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>) (state => this.StateMachine.RegisterTransition(state, new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.StunPunish), false, (Func<bool>) (() => this.Player.HasBuff<StunnedBuff>() && !((IEnumerable<Projectile>) Main.projectile).Any<Projectile>((Func<Projectile, bool>) (p => p.TypeAlive<CoffinHand>()))), (Action) null)), new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates[2]
      {
        FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.StunPunish,
        FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.PhaseTransition
      });
      this.StateMachine.ApplyToAllStatesExcept((Action<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>) (state => this.StateMachine.RegisterTransition(state, new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.SpiritGrabPunish), false, (Func<bool>) (() => (double) this.ForceGrabPunish != 0.0), (Action) (() => this.ForceGrabPunish = 0.0f))), new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates[2]
      {
        FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.SpiritGrabPunish,
        FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.PhaseTransition
      });
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.StunPunish, new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(), false, (Func<bool>) (() => (double) this.Timer > 20.0 && this.Frame <= 0), (Action) (() =>
      {
        this.NPC.frameCounter = 0.0;
        this.Frame = 0;
      }));
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.SpiritGrabPunish, new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.SlamWShockwave), false, (Func<bool>) (() => (double) this.Timer > 60.0), (Action) (() =>
      {
        this.NPC.noTileCollide = true;
        this.LockVector1 = Vector2.op_Subtraction(((Entity) this.Player).Top, Vector2.op_Multiply(Vector2.UnitY, 250f));
        ((Entity) this.NPC).velocity = Vector2.Zero;
        ((Entity) this.NPC).velocity.Y = -2f;
        this.AI2 = 3f;
      }));
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.HoveringForSlam, new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.SlamWShockwave), false, (Func<bool>) (() => (double) this.Timer > 1.0 && (double) this.Timer == (double) this.AI3), (Action) (() =>
      {
        ((Entity) this.NPC).velocity.Y = -5f;
        ((Entity) this.NPC).velocity.X /= 2f;
        this.LockVector1 = Vector2.op_Subtraction(((Entity) this.Player).Top, Vector2.op_Multiply(Vector2.UnitY, 250f));
        this.AI2 = 0.0f;
      }));
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.SlamWShockwave, new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(), false, (Func<bool>) (() => (double) this.Timer == -1.0), (Action) null);
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.WavyShotCircle, new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(), false, (Func<bool>) (() =>
      {
        int num = (double) this.Timer > (double) ((WorldSavingSystem.MasochistModeReal ? 60 : 70) + (WorldSavingSystem.MasochistModeReal || (double) this.AI3 < 1.0 ? 20 : 50)) ? 1 : 0;
        bool flag = this.PhaseTwo && (double) this.AI3 < 1.0 && WorldSavingSystem.EternityMode;
        return num != 0 && !flag;
      }), (Action) null);
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.WavyShotFlight, new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.SlamWShockwave), false, (Func<bool>) (() => (double) this.Timer >= 340.0), (Action) (() =>
      {
        this.Frame = 0;
        ((Entity) this.NPC).velocity.X /= 2f;
        ((Entity) this.NPC).velocity.Y = -4f;
        this.LockVector1 = Vector2.op_Subtraction(((Entity) this.Player).Top, Vector2.op_Multiply(Vector2.UnitY, 250f));
        this.AI2 = 3f;
      }));
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.GrabbyHands, new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.SlamWShockwave), false, (Func<bool>) (() => (double) this.Timer > 40.0 && this.Frame <= 0 && (double) this.Timer > (double) this.AI3 + 10.0), (Action) (() =>
      {
        this.NPC.noTileCollide = true;
        this.LockVector1 = Vector2.op_Subtraction(((Entity) this.Player).Top, Vector2.op_Multiply(Vector2.UnitY, 250f));
        ((Entity) this.NPC).velocity.Y = -4f;
        ((Entity) this.NPC).velocity.X /= 2f;
      }));
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.RandomStuff, new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.SlamWShockwave), false, (Func<bool>) (() => (double) this.Timer > 370.0 && this.Frame <= 0 && (double) ((Entity) this.NPC).Center.Y < (double) ((Entity) this.Player).Center.Y - 100.0), (Action) (() =>
      {
        this.NPC.noTileCollide = true;
        this.LockVector1 = Vector2.op_Subtraction(((Entity) this.Player).Top, Vector2.op_Multiply(Vector2.UnitY, 250f));
        ((Entity) this.NPC).velocity.Y = -4f;
        ((Entity) this.NPC).velocity.X /= 2f;
        ((Entity) this.NPC).velocity = Vector2.Zero;
        this.NPC.rotation = 0.0f;
        this.NPC.frameCounter = 0.0;
        this.Frame = 0;
      }));
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.RandomStuff, new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.WavyShotFlight), false, (Func<bool>) (() => (double) this.Timer > 370.0 && this.Frame <= 0 && (double) ((Entity) this.NPC).Center.Y >= (double) ((Entity) this.Player).Center.Y - 100.0), (Action) (() =>
      {
        ((Entity) this.NPC).velocity = Vector2.Zero;
        this.NPC.rotation = 0.0f;
        this.NPC.frameCounter = 0.0;
        this.Frame = 0;
      }));
    }

    public void OnStateTransition(
      bool stateWasPopped,
      EntityAIState<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates> oldState)
    {
      this.NPC.netUpdate = true;
      this.NPC.TargetClosest(false);
      this.AI2 = 0.0f;
      this.AI3 = 0.0f;
      if (oldState == null || !this.P1Attacks.Contains(oldState.Identifier) && !this.P2Attacks.Contains(oldState.Identifier))
        return;
      this.LastAttackChoice = (int) oldState.Identifier;
    }

    public void LoadTransition_ResetCycle()
    {
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.RefillAttacks, new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(), false, (Func<bool>) (() => true), (Action) (() =>
      {
        this.NPC.netUpdate = true;
        if (!FargoSoulsUtil.HostCheck)
          return;
        this.StateMachine.StateStack.Clear();
        List<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates> list = (this.PhaseTwo ? (IEnumerable<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>) this.P2Attacks : (IEnumerable<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>) this.P1Attacks).Where<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>((Func<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates, bool>) (attack => attack != (FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates) this.LastAttackChoice)).ToList<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>();
        List<int> intList = new List<int>();
        for (int index = 0; index < list.Count; ++index)
          intList.Add(index);
        for (int index1 = 0; index1 < list.Count; ++index1)
        {
          int index2 = intList[Main.rand.Next(0, intList.Count)];
          this.StateMachine.StateStack.Push(this.StateMachine.StateRegistry[list[index2]]);
          intList.Remove(index2);
        }
      }));
    }

    public void LoadTransition_PhaseTwoTransition()
    {
      this.StateMachine.AddTransitionStateHijack((Func<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?, FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?>) (originalState =>
      {
        if (this.PhaseTwo || (double) this.NPC.GetLifePercent() > 0.800000011920929)
          return originalState;
        this.StateMachine.StateStack.Clear();
        return new FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?(FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.PhaseTransition);
      }), (Action<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates?>) null);
    }

    public CursedCoffin()
    {
      List<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates> behaviorStatesList1 = new List<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>();
      CollectionsMarshal.SetCount<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(behaviorStatesList1, 4);
      Span<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates> span1 = CollectionsMarshal.AsSpan<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(behaviorStatesList1);
      int num1 = 0;
      span1[num1] = FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.HoveringForSlam;
      int num2 = num1 + 1;
      span1[num2] = FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.WavyShotCircle;
      int num3 = num2 + 1;
      span1[num3] = FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.WavyShotFlight;
      int num4 = num3 + 1;
      span1[num4] = FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.GrabbyHands;
      int num5 = num4 + 1;
      this.P1Attacks = behaviorStatesList1;
      List<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates> behaviorStatesList2 = new List<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>();
      CollectionsMarshal.SetCount<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(behaviorStatesList2, 5);
      Span<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates> span2 = CollectionsMarshal.AsSpan<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates>(behaviorStatesList2);
      int num6 = 0;
      span2[num6] = FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.HoveringForSlam;
      int num7 = num6 + 1;
      span2[num7] = FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.WavyShotCircle;
      int num8 = num7 + 1;
      span2[num8] = FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.WavyShotFlight;
      int num9 = num8 + 1;
      span2[num9] = FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.GrabbyHands;
      int num10 = num9 + 1;
      span2[num10] = FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.RandomStuff;
      num5 = num10 + 1;
      this.P2Attacks = behaviorStatesList2;
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }

    static CursedCoffin()
    {
      SoundStyle soundStyle;
      // ISSUE: explicit constructor call
      ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/CoffinSlam", (SoundType) 0);
      ((SoundStyle) ref soundStyle).PitchVariance = 0.3f;
      FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.SlamSFX = soundStyle;
      // ISSUE: explicit constructor call
      ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/CoffinSpiritDrone", (SoundType) 0);
      ((SoundStyle) ref soundStyle).MaxInstances = 1;
      ((SoundStyle) ref soundStyle).SoundLimitBehavior = (SoundLimitBehavior) 0;
      ((SoundStyle) ref soundStyle).Volume = 0.2f;
      FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.SpiritDroneSFX = soundStyle;
      // ISSUE: explicit constructor call
      ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/CoffinBigShot", (SoundType) 0);
      ((SoundStyle) ref soundStyle).Volume = 0.6f;
      ((SoundStyle) ref soundStyle).PitchVariance = 0.3f;
      FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BigShotSFX = soundStyle;
      // ISSUE: explicit constructor call
      ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/CoffinShot", (SoundType) 0);
      ((SoundStyle) ref soundStyle).Volume = 0.3f;
      ((SoundStyle) ref soundStyle).PitchVariance = 0.3f;
      FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.ShotSFX = soundStyle;
      // ISSUE: explicit constructor call
      ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/CoffinSoulShot", (SoundType) 0);
      ((SoundStyle) ref soundStyle).Volume = 0.3f;
      ((SoundStyle) ref soundStyle).PitchVariance = 0.3f;
      FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.SoulShotSFX = soundStyle;
      FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.HandChargeSFX = new SoundStyle("FargowiltasSouls/Assets/Sounds/CoffinHandCharge", (SoundType) 0);
    }

    public enum BehaviorStates
    {
      Opening,
      PhaseTransition,
      StunPunish,
      SpiritGrabPunish,
      HoveringForSlam,
      SlamWShockwave,
      WavyShotCircle,
      WavyShotFlight,
      GrabbyHands,
      RandomStuff,
      RefillAttacks,
      Count,
    }
  }
}
