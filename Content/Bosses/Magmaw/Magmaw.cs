// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Magmaw.Magmaw
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
namespace FargowiltasSouls.Content.Bosses.Magmaw
{
  [AutoloadBossHead]
  public class Magmaw : ModNPC
  {
    public int Trail;
    public int Frame;
    public int Anim;
    public Vector2 JawOffset = Vector2.Zero;
    private const int Right = 1;
    private const int Left = -1;
    public bool HitPlayer = true;
    public int Phase = 1;
    public int ChainDepth;
    public int MaxChainDepth = 5;
    public bool IdleReposition = true;
    public Vector2 LockVector1 = Vector2.Zero;
    public Vector2 LockVector2 = Vector2.Zero;
    private float AnimationSpeed = 1f;

    public virtual bool IsLoadingEnabled(Mod mod) => false;

    public Vector2 JawCenter => Vector2.op_Addition(((Entity) this.NPC).Center, this.JawOffset);

    private Player player => Main.player[this.NPC.target];

    public ref float IdleTime => ref this.NPC.localAI[0];

    public ref float CurrentState => ref this.NPC.localAI[1];

    public ref float LastState => ref this.NPC.localAI[2];

    public ref float Timer => ref this.NPC.ai[0];

    public ref float AI1 => ref this.NPC.ai[1];

    public ref float AI2 => ref this.NPC.ai[2];

    public ref float AI3 => ref this.NPC.ai[3];

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 1;
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 10;
      NPCID.Sets.TrailingMode[this.NPC.type] = 3;
      NPCID.Sets.MPAllowedEnemies[this.Type] = true;
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 8);
      Span<int> span = CollectionsMarshal.AsSpan<int>(debuffs);
      int num1 = 0;
      span[num1] = 31;
      int num2 = num1 + 1;
      span[num2] = 46;
      int num3 = num2 + 1;
      span[num3] = 68;
      int num4 = num3 + 1;
      span[num4] = 24;
      int num5 = num4 + 1;
      span[num5] = 323;
      int num6 = num5 + 1;
      span[num6] = ModContent.BuffType<HellFireBuff>();
      int num7 = num6 + 1;
      span[num7] = ModContent.BuffType<LethargicBuff>();
      int num8 = num7 + 1;
      span[num8] = ModContent.BuffType<ClippedWingsBuff>();
      int num9 = num8 + 1;
      npc.AddDebuffImmunities(debuffs);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual void SetDefaults()
    {
      this.NPC.aiStyle = -1;
      this.NPC.lifeMax = 70000;
      this.NPC.defense = 65;
      this.NPC.damage = 72;
      this.NPC.knockBackResist = 0.0f;
      ((Entity) this.NPC).width = 125;
      ((Entity) this.NPC).height = 92;
      this.NPC.boss = true;
      this.NPC.lavaImmune = true;
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit2);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath44);
      this.Music = 12;
      this.SceneEffectPriority = (SceneEffectPriority) 6;
      this.NPC.value = (float) Item.buyPrice(0, 15, 0, 0);
    }

    public virtual Color? GetAlpha(Color drawColor)
    {
      return this.NPC.IsABestiaryIconDummy ? new Color?(this.NPC.GetBestiaryEntryColor()) : base.GetAlpha(drawColor);
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
      writer.Write7BitEncodedInt(this.Phase);
      writer.Write7BitEncodedInt(this.ChainDepth);
      writer.Write7BitEncodedInt(this.MaxChainDepth);
      writer.Write(this.IdleReposition);
      Utils.WriteVector2(writer, this.LockVector1);
      Utils.WriteVector2(writer, this.LockVector2);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.NPC.localAI[0] = reader.ReadSingle();
      this.NPC.localAI[1] = reader.ReadSingle();
      this.NPC.localAI[2] = reader.ReadSingle();
      this.NPC.localAI[3] = reader.ReadSingle();
      this.Phase = reader.Read7BitEncodedInt();
      this.ChainDepth = reader.Read7BitEncodedInt();
      this.MaxChainDepth = reader.Read7BitEncodedInt();
      this.IdleReposition = reader.ReadBoolean();
      this.LockVector1 = Utils.ReadVector2(reader);
      this.LockVector2 = Utils.ReadVector2(reader);
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot)
    {
      return this.HitPlayer && this.Collides(((Entity) target).position, ((Entity) target).Size);
    }

    public virtual bool CanHitNPC(NPC target)
    {
      return this.HitPlayer && this.Collides(((Entity) target).position, ((Entity) target).Size);
    }

    public bool Collides(Vector2 boxPos, Vector2 boxDim)
    {
      Vector2 size = ((Entity) this.NPC).Size;
      Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).position, Vector2.op_Multiply(0.5f, new Vector2((float) ((Entity) this.NPC).width, (float) ((Entity) this.NPC).height)));
      float num1 = 0.0f;
      float num2 = 0.0f;
      if ((double) boxPos.X > (double) vector2.X)
        num1 = boxPos.X - vector2.X;
      else if ((double) boxPos.X + (double) boxDim.X < (double) vector2.X)
        num1 = boxPos.X + boxDim.X - vector2.X;
      if ((double) boxPos.Y > (double) vector2.Y)
        num2 = boxPos.Y - vector2.Y;
      else if ((double) boxPos.Y + (double) boxDim.Y < (double) vector2.Y)
        num2 = boxPos.Y + boxDim.Y - vector2.Y;
      float num3 = size.X / 2f;
      float num4 = size.Y / 2f;
      return (double) num1 * (double) num1 / ((double) num3 * (double) num3) + (double) num2 * (double) num2 / ((double) num4 * (double) num4) < 1.0;
    }

    public virtual bool? CanFallThroughPlatforms() => new bool?(true);

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      target.AddBuff(323, 600, true, false);
      if (WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(204, 300, true, false);
    }

    public virtual void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
    {
      if (!ProjectileID.Sets.CultistIsResistantTo[projectile.type] || FargoSoulsUtil.IsSummonDamage(projectile))
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 0.8f);
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D = TextureAssets.Npc[this.NPC.type].Value;
      Vector2 vector2 = Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos);
      float rotation = this.NPC.rotation;
      SpriteEffects spriteEffects = ((Entity) this.NPC).direction == -1 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < Math.Min(this.Trail, NPCID.Sets.TrailCacheLength[this.NPC.type]); ++index)
      {
        float num = this.NPC.oldRot[index] + (((Entity) this.NPC).direction == 1 ? 0.0f : 3.14159274f);
        Vector2 oldPo = this.NPC.oldPos[index];
        int frame = this.Frame;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, frame * texture2D.Height / Main.npcFrameCount[this.NPC.type], texture2D.Width, texture2D.Height / Main.npcFrameCount[this.NPC.type]);
        DrawData drawData;
        // ISSUE: explicit constructor call
        ((DrawData) ref drawData).\u002Ector(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(this.NPC.GetAlpha(drawColor), 0.5f / (float) index), num, new Vector2((float) (texture2D.Width / 2), (float) (texture2D.Height / 2 / Main.npcFrameCount[this.NPC.type])), this.NPC.scale, spriteEffects, 0.0f);
        GameShaders.Misc["LCWingShader"].UseColor(Color.Blue).UseSecondaryColor(Color.Black);
        GameShaders.Misc["LCWingShader"].Apply(new DrawData?(drawData));
        ((DrawData) ref drawData).Draw(spriteBatch);
      }
      spriteBatch.Draw(texture2D, vector2, new Rectangle?(this.NPC.frame), this.NPC.GetAlpha(drawColor), rotation, new Vector2((float) (texture2D.Width / 2), (float) (texture2D.Height / 2 / Main.npcFrameCount[this.NPC.type])), this.NPC.scale, spriteEffects, 0.0f);
      return false;
    }

    public virtual void FindFrame(int frameHeight)
    {
    }

    public virtual void OnKill()
    {
      NPC.SetEventFlagCleared(ref WorldSavingSystem.downedBoss[13], -1);
      NPC.SetEventFlagCleared(ref NPC.downedGolemBoss, 6);
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 499;

    public virtual void HitEffect(NPC.HitInfo hit)
    {
    }

    public virtual bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
      position = Vector2.op_Addition(this.JawCenter, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, 35f), this.NPC.scale));
      return new bool?(true);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
    }

    private bool AliveCheck(Player p, bool forceDespawn = false)
    {
      if (forceDespawn || !((Entity) p).active || p.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) p).Center) > 5000.0)
      {
        this.NPC.TargetClosest(true);
        p = Main.player[this.NPC.target];
        if (forceDespawn || !((Entity) p).active || p.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) p).Center) > 5000.0)
        {
          this.NPC.noTileCollide = true;
          if (this.NPC.timeLeft > 30)
            this.NPC.timeLeft = 30;
          ++((Entity) this.NPC).velocity.Y;
          if (this.NPC.timeLeft == 1 && FargoSoulsUtil.HostCheck)
            FargoSoulsUtil.ClearHostileProjectiles(2, ((Entity) this.NPC).whoAmI);
          return false;
        }
      }
      if (this.NPC.timeLeft < 600)
        this.NPC.timeLeft = 600;
      return true;
    }

    private void RotateTowards(Vector2 target, float speed)
    {
      float num = FargoSoulsUtil.RotationDifference(Utils.ToRotationVector2(this.NPC.rotation), Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, target));
      this.NPC.rotation = Utils.ToRotation(Utils.RotatedBy(Utils.ToRotationVector2(this.NPC.rotation), (double) Math.Sign(num) * (double) Math.Min(Math.Abs(num), (float) ((double) speed * 3.1415927410125732 / 180.0)), new Vector2()));
    }

    public virtual void AI()
    {
      if (!this.AliveCheck(this.player))
        return;
      this.HitPlayer = true;
      if (!((IEnumerable<Projectile>) Main.projectile).Any<Projectile>((Func<Projectile, bool>) (p => p.TypeAlive<MagmawJaw>() && (double) Luminance.Common.Utilities.Utilities.As<MagmawJaw>(p).ParentID == (double) ((Entity) this.NPC).whoAmI)) && FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), this.JawCenter, Vector2.Zero, ModContent.ProjectileType<MagmawJaw>(), this.NPC.damage, 2f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
      for (int side = -1; side < 2; side += 2)
      {
        if (!((IEnumerable<Projectile>) Main.projectile).Any<Projectile>((Func<Projectile, bool>) (p => p.TypeAlive<MagmawHand>() && (double) Luminance.Common.Utilities.Utilities.As<MagmawHand>(p).ParentID == (double) ((Entity) this.NPC).whoAmI && (double) Luminance.Common.Utilities.Utilities.As<MagmawHand>(p).Side == (double) side)) && FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) side), 400f)), Vector2.Zero, ModContent.ProjectileType<MagmawHand>(), this.NPC.damage, 2f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, (float) side, 0.0f);
      }
      this.JawOffset = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, (float) ((Entity) this.NPC).height), 0.675f);
      if (this.Phase != 1)
        ;
      this.HandleState();
      ++this.Timer;
    }

    public void HandleState()
    {
      switch ((FargowiltasSouls.Content.Bosses.Magmaw.Magmaw.State) this.CurrentState)
      {
        case FargowiltasSouls.Content.Bosses.Magmaw.Magmaw.State.Idle:
          this.Idle();
          break;
        case FargowiltasSouls.Content.Bosses.Magmaw.Magmaw.State.SwordClap:
          this.SwordClap();
          break;
        default:
          this.FindAttackFromIdle();
          break;
      }
    }

    public void HandleHandState(MagmawHand hand)
    {
      switch ((FargowiltasSouls.Content.Bosses.Magmaw.Magmaw.State) this.CurrentState)
      {
        case FargowiltasSouls.Content.Bosses.Magmaw.Magmaw.State.Idle:
          this.Idle_Hand(hand);
          break;
        case FargowiltasSouls.Content.Bosses.Magmaw.Magmaw.State.SwordClap:
          this.SwordClap_Hand(hand);
          break;
      }
    }

    private void Reset()
    {
      this.LockVector1 = Vector2.Zero;
      this.LockVector2 = Vector2.Zero;
      this.Timer = 0.0f;
      this.AI2 = 0.0f;
      this.AI3 = 0.0f;
    }

    private void FindAttackFromIdle()
    {
      this.Reset();
      this.ChainDepth = 1;
    }

    private void Followup()
    {
      this.Reset();
      ++this.ChainDepth;
    }

    private void AttackThenIdle(FargowiltasSouls.Content.Bosses.Magmaw.Magmaw.State newState)
    {
      this.Reset();
      this.ChainDepth = this.MaxChainDepth;
    }

    private void GoIdle(int idleTime = 60, bool reposition = false)
    {
      this.Reset();
      this.ChainDepth = 0;
      this.IdleReposition = reposition;
      this.IdleTime = (float) idleTime;
      this.CurrentState = 0.0f;
    }

    public void Idle()
    {
      this.HitPlayer = false;
      float num = 400f;
      Vector2 vector2 = ((Entity) this.NPC).Center;
      if ((double) ((Entity) this.NPC).Distance(((Entity) this.player).Center) > (double) num)
        vector2 = Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.player, ((Entity) this.NPC).Center), num));
      if (this.IdleReposition)
        vector2 = Vector2.op_Subtraction(((Entity) this.player).Center, Vector2.op_Multiply(Vector2.UnitY, 400f));
      ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.op_Subtraction(vector2, ((Entity) this.NPC).Center), 0.05f);
    }

    public void Idle_Hand(MagmawHand hand)
    {
      Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Addition(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, hand.Side), 220f), Vector2.op_Multiply(Vector2.UnitY, 80f)).SetMagnitude(220f));
      ((Entity) hand.Projectile).velocity = Vector2.op_Multiply(Vector2.op_Subtraction(vector2, ((Entity) hand.Projectile).Center), 0.15f);
      this.RotateTowards(Vector2.op_Subtraction(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, hand.Side), 0.7f), Vector2.UnitY), 0.15f);
    }

    public void SwordClap()
    {
    }

    public void SwordClap_Hand(MagmawHand hand)
    {
    }

    public enum State
    {
      Idle,
      SwordClap,
      SwordCharge,
      SwordChargePredictive,
    }
  }
}
