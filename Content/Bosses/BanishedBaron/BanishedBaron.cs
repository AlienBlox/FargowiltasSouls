// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Items.Placables.Relics;
using FargowiltasSouls.Content.Items.Placables.Trophies;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Content.Items.Weapons.Challengers;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.BanishedBaron
{
  [AutoloadBossHead]
  public class BanishedBaron : ModNPC
  {
    public static readonly SoundStyle BaronRoar = new SoundStyle("FargowiltasSouls/Assets/Sounds/BaronRoar", (SoundType) 0);
    public static readonly SoundStyle BaronYell = new SoundStyle("FargowiltasSouls/Assets/Sounds/BaronYell", (SoundType) 0);
    public static List<int> P1Attacks;
    public static List<int> P2Attacks;
    public bool Attacking = true;
    public bool HitPlayer = true;
    public int Trail;
    private bool DidWhirlpool;
    public int Frame;
    public int Phase = 1;
    public int Anim;
    public int ArenaProjID = -1;
    public const int MaxWhirlpools = 40;
    public List<int> availablestates = new List<int>();
    public Vector2 LockVector1 = Vector2.Zero;
    public int LastState;
    public Vector2 LockVector2 = Vector2.Zero;
    private float AnimationSpeed = 1f;

    private Player player => Main.player[this.NPC.target];

    public ref float Timer => ref this.NPC.ai[0];

    public ref float State => ref this.NPC.ai[1];

    public ref float AI2 => ref this.NPC.ai[2];

    public ref float AI3 => ref this.NPC.ai[3];

    public ref float AI4 => ref this.NPC.localAI[0];

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 19;
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 10;
      NPCID.Sets.TrailingMode[this.NPC.type] = 3;
      NPCID.Sets.MPAllowedEnemies[this.Type] = true;
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 5);
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
      npc.AddDebuffImmunities(debuffs);
      Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> bestiaryDrawOffset = NPCID.Sets.NPCBestiaryDrawOffset;
      int type = this.NPC.type;
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers1;
      // ISSUE: explicit constructor call
      ((NPCID.Sets.NPCBestiaryDrawModifiers) ref bestiaryDrawModifiers1).\u002Ector();
      bestiaryDrawModifiers1.Rotation = 3.14159274f;
      bestiaryDrawModifiers1.Position = Vector2.op_Multiply(Vector2.UnitX, 60f);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      bestiaryDrawOffset.Add(type, bestiaryDrawModifiers2);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual void SetDefaults()
    {
      this.NPC.aiStyle = -1;
      this.NPC.lifeMax = 32000;
      this.NPC.defense = 15;
      this.NPC.damage = 69;
      this.NPC.knockBackResist = 0.0f;
      ((Entity) this.NPC).width = 132;
      ((Entity) this.NPC).height = 132;
      this.NPC.boss = true;
      this.NPC.lavaImmune = true;
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      NPC npc = this.NPC;
      SoundStyle soundStyle;
      // ISSUE: explicit constructor call
      ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/BaronHit", (SoundType) 0);
      // ISSUE: field reference
      ((SoundStyle) ref soundStyle).Variants = RuntimeHelpers.CreateSpan<int>(__fieldref (\u003CPrivateImplementationDetails\u003E.\u0034636993D3E1DA4E9D6B8F87B79E8F7C6D018580D52661950EABC3845C5897A4D4));
      ((SoundStyle) ref soundStyle).PitchRange = (-0.7f, -0.5f);
      ((SoundStyle) ref soundStyle).Volume = 0.5f;
      SoundStyle? nullable = new SoundStyle?(soundStyle);
      npc.HitSound = nullable;
      this.NPC.DeathSound = new SoundStyle?(new SoundStyle("FargowiltasSouls/Assets/Sounds/BaronDeath", (SoundType) 0));
      this.NPC.alpha = (int) byte.MaxValue;
      Mod mod;
      this.Music = Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod) ? MusicLoader.GetMusicSlot(mod, "Assets/Music/Baron") : 58;
      this.SceneEffectPriority = (SceneEffectPriority) 6;
      this.NPC.value = (float) Item.buyPrice(0, 2, 0, 0);
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
      writer.Write7BitEncodedInt(this.ArenaProjID);
      writer.Write7BitEncodedInt(this.Phase);
      Utils.WriteVector2(writer, this.LockVector1);
      Utils.WriteVector2(writer, this.LockVector2);
      writer.Write7BitEncodedInt(this.LastState);
      writer.Write(this.DidWhirlpool);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.NPC.localAI[0] = reader.ReadSingle();
      this.ArenaProjID = reader.Read7BitEncodedInt();
      this.Phase = reader.Read7BitEncodedInt();
      this.LockVector1 = Utils.ReadVector2(reader);
      this.LockVector2 = Utils.ReadVector2(reader);
      this.LastState = reader.Read7BitEncodedInt();
      this.DidWhirlpool = reader.ReadBoolean();
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

    public virtual bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
      return new bool?((double) this.Timer >= 140.0 || (double) this.State != 5.0);
    }

    public virtual void BossHeadSlot(ref int index)
    {
      if ((double) this.Timer < 140.0 && (double) this.State == 5.0)
        index = -1;
      else
        index = NPCID.Sets.BossHeadTextures[this.Type];
    }

    public virtual bool? CanFallThroughPlatforms() => new bool?(true);

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      target.AddBuff(30, 600, true, false);
      if (!WorldSavingSystem.EternityMode)
      {
        target.AddBuff(148, 300, true, false);
      }
      else
      {
        target.AddBuff(148, 600, true, false);
        target.FargoSouls().MaxLifeReduction += 50;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 1800, true, false);
      }
    }

    public virtual void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
    {
      if ((double) this.State != 1.0)
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Division(local, 4f);
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos);
      float num1 = this.NPC.rotation + (((Entity) this.NPC).direction == 1 ? 0.0f : 3.14159274f);
      if (this.NPC.aiStyle != -1 && (double) this.NPC.ai[0] != 7.0)
      {
        Vector2 vector2_2 = ((Entity) this.NPC).DirectionTo(((Entity) this.player).Center);
        ((Entity) this.NPC).direction = Math.Sign(vector2_2.X);
        num1 = Utils.ToRotation(vector2_2) + (((Entity) this.NPC).direction == 1 ? 0.0f : 3.14159274f);
      }
      int num2 = this.NPC.frame.Y / (texture2D1.Height / Main.npcFrameCount[this.NPC.type]);
      SpriteEffects spriteEffects1 = ((Entity) this.NPC).direction == -1 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < Math.Min(this.Trail, NPCID.Sets.TrailCacheLength[this.NPC.type]); ++index)
      {
        float num3 = this.NPC.oldRot[index] + (((Entity) this.NPC).direction == 1 ? 0.0f : 3.14159274f);
        Vector2 oldPo = this.NPC.oldPos[index];
        int frame = this.Frame;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, frame * texture2D1.Height / Main.npcFrameCount[this.NPC.type], texture2D1.Width, texture2D1.Height / Main.npcFrameCount[this.NPC.type]);
        DrawData drawData;
        // ISSUE: explicit constructor call
        ((DrawData) ref drawData).\u002Ector(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(this.NPC.GetAlpha(drawColor), 0.5f / (float) index), num3, new Vector2((float) (texture2D1.Width / 2), (float) (texture2D1.Height / 2 / Main.npcFrameCount[this.NPC.type])), this.NPC.scale, spriteEffects1, 0.0f);
        GameShaders.Misc["LCWingShader"].UseColor(Color.Blue).UseSecondaryColor(Color.Black);
        GameShaders.Misc["LCWingShader"].Apply(new DrawData?(drawData));
        ((DrawData) ref drawData).Draw(spriteBatch);
      }
      SpriteBatch spriteBatch1 = spriteBatch;
      Vector2 vector2_3;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_3).\u002Ector((float) (texture2D1.Width / 2), (float) (texture2D1.Height / 2 / Main.npcFrameCount[this.NPC.type]));
      Texture2D texture2D2 = texture2D1;
      Vector2 vector2_4 = vector2_1;
      Rectangle? nullable = new Rectangle?(this.NPC.frame);
      Color alpha = this.NPC.GetAlpha(drawColor);
      double num4 = (double) num1;
      Vector2 vector2_5 = vector2_3;
      double scale = (double) this.NPC.scale;
      SpriteEffects spriteEffects2 = spriteEffects1;
      spriteBatch1.Draw(texture2D2, vector2_4, nullable, alpha, (float) num4, vector2_5, (float) scale, spriteEffects2, 0.0f);
      return false;
    }

    public virtual void FindFrame(int frameHeight)
    {
      double num1 = 60.0 / (10.0 * (double) this.AnimationSpeed);
      int num2 = 0;
      int num3 = 5;
      switch (this.Phase)
      {
        case 1:
          switch (this.Anim)
          {
            case 1:
              num2 = 6;
              num3 = 7;
              break;
          }
          break;
        case 2:
          switch (this.Anim)
          {
            case 0:
              num2 = 11;
              num3 = 16;
              break;
            case 1:
              num2 = 17;
              num3 = 18;
              break;
          }
          break;
      }
      this.NPC.spriteDirection = ((Entity) this.NPC).direction;
      if (++this.NPC.frameCounter >= num1)
      {
        if (++this.Frame > num3 || this.Frame < num2)
          this.Frame = num2;
        this.NPC.frameCounter = 0.0;
      }
      this.NPC.frame.Y = frameHeight * this.Frame;
      if (this.Anim != 3)
        return;
      this.NPC.frame.Y = frameHeight * 9;
    }

    public virtual void OnKill()
    {
      NPC.SetEventFlagCleared(ref WorldSavingSystem.downedBoss[12], -1);
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 499;

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life - ((NPC.HitInfo) ref hit).Damage < 0 && !this.NPC.dontTakeDamage)
      {
        hit.Null();
        this.NPC.life = 10;
        this.NPC.dontTakeDamage = true;
        this.State = 16f;
        this.Timer = 0.0f;
        ((Entity) this.NPC).velocity = Vector2.Zero;
        SoundStyle soundStyle = SoundID.Item62;
        ((SoundStyle) ref soundStyle).Pitch = 0.0f;
        ((SoundStyle) ref soundStyle).Volume = 2f;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        Vector2 vector2 = Vector2.op_Subtraction(((Entity) this.NPC).position, Vector2.op_Division(((Entity) this.NPC).Size, 2f));
        int num1 = ((Entity) this.NPC).width * 2;
        int num2 = ((Entity) this.NPC).height * 2;
        for (int index1 = 0; index1 < 30; ++index1)
        {
          int index2 = Dust.NewDust(vector2, num1, num2, 31, 0.0f, 0.0f, 100, new Color(), 3f);
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
        }
        for (int index3 = 0; index3 < 20; ++index3)
        {
          int index4 = Dust.NewDust(vector2, num1, num2, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
          Main.dust[index4].noGravity = true;
          Dust dust1 = Main.dust[index4];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
          int index5 = Dust.NewDust(vector2, num1, num2, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust2 = Main.dust[index5];
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
        }
        float num3 = 0.5f;
        if (!Main.dedServ)
        {
          for (int index6 = 0; index6 < 4; ++index6)
          {
            int index7 = Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
            Gore gore1 = Main.gore[index7];
            gore1.velocity = Vector2.op_Multiply(gore1.velocity, num3);
            Gore gore2 = Main.gore[index7];
            gore2.velocity = Vector2.op_Addition(gore2.velocity, Utils.RotatedBy(new Vector2(1f, 1f), 1.5707963705062866 * (double) index6, new Vector2()));
          }
        }
      }
      if (this.NPC.life > 0)
        return;
      ScreenShakeSystem.StartShake(15f, 6.28318548f, new Vector2?(), 0.25f);
      DefaultInterpolatedStringHandler interpolatedStringHandler;
      for (int index = 1; index <= 4; ++index)
      {
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).width), Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).height)));
        float num4 = ((Vector2) ref ((Entity) this.NPC).velocity).Length();
        float num5 = Utils.NextFloat(Main.rand, num4 * 1.25f, num4 * 1.6f);
        if (!Main.dedServ)
        {
          IEntitySource sourceFromThis = ((Entity) this.NPC).GetSource_FromThis((string) null);
          Vector2 vector2_2 = vector2_1;
          Vector2 vector2_3 = Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedByRandom(Vector2.UnitY, 1.5707963705062866)), num5);
          string name = ((ModType) this).Mod.Name;
          interpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
          interpolatedStringHandler.AppendLiteral("BaronGore");
          interpolatedStringHandler.AppendFormatted<int>(index);
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          int type = ModContent.Find<ModGore>(name, stringAndClear).Type;
          double scale = (double) this.NPC.scale;
          Gore.NewGore(sourceFromThis, vector2_2, vector2_3, type, (float) scale);
        }
      }
      for (int index = 0; index < 20; ++index)
      {
        Vector2 vector2_4 = Utils.NextVector2FromRectangle(Main.rand, ((Entity) this.NPC).Hitbox);
        Vector2 vector2_5 = Vector2.op_Multiply(Utils.NextVector2CircularEdge(Main.rand, 1f, 1f), Utils.NextFloat(Main.rand, 16f, 24f));
        int num = Main.rand.Next(6) + 1;
        if (!Main.dedServ)
        {
          IEntitySource sourceFromThis = ((Entity) this.NPC).GetSource_FromThis((string) null);
          Vector2 vector2_6 = vector2_4;
          Vector2 vector2_7 = vector2_5;
          string name = ((ModType) this).Mod.Name;
          interpolatedStringHandler = new DefaultInterpolatedStringHandler(14, 1);
          interpolatedStringHandler.AppendLiteral("BaronScrapGore");
          interpolatedStringHandler.AppendFormatted<int>(num);
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          int type = ModContent.Find<ModGore>(name, stringAndClear).Type;
          double scale = (double) this.NPC.scale;
          Gore.NewGore(sourceFromThis, vector2_6, vector2_7, type, (float) scale);
        }
      }
      if (!FargoSoulsUtil.HostCheck)
        return;
      NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, 55, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.BossBag(ModContent.ItemType<BanishedBaronBag>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(ModContent.ItemType<BaronTrophy>(), 10, 1, 1));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<BaronRelic>()));
      LeadingConditionRule leadingConditionRule = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.NotExpert());
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.OneFromOptions(1, new int[4]
      {
        ModContent.ItemType<TheBaronsTusk>(),
        ModContent.ItemType<RoseTintedVisor>(),
        ModContent.ItemType<NavalRustrifle>(),
        ModContent.ItemType<DecrepitAirstrikeRemote>()
      }), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(5003, 1, 1, 5), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.OneFromOptions(3, new int[3]
      {
        3096,
        3037,
        3120
      }), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(5139, 4, 1, 1), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(2354, 3, 2, 5), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(2355, 2, 2, 5), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(2356, 5, 2, 5), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(3183, 50, 1, 1), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(2360, 50, 1, 1), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(2294, 150, 1, 1), false);
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) leadingConditionRule);
    }

    public virtual bool PreAI() => base.PreAI();

    public virtual void AI()
    {
      if ((double) this.State == 16.0)
        this.NPC.aiStyle = -1;
      else if (Main.getGoodWorld)
      {
        if (this.NPC.aiStyle != -1)
          return;
        if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.15)
        {
          int num = this.NPC.lifeMax / 2 - this.NPC.life;
          this.NPC.HealEffect(num, true);
          this.NPC.life += num;
          this.NPC.aiStyle = 69;
          ((Entity) this.NPC).velocity = Vector2.Zero;
          this.NPC.ai[0] = 0.0f;
          this.NPC.ai[1] = 0.0f;
          this.NPC.ai[2] = 0.0f;
          this.NPC.ai[3] = 0.0f;
          this.NPC.localAI[0] = 0.0f;
          this.NPC.localAI[1] = 0.0f;
          this.NPC.localAI[2] = 0.0f;
          this.NPC.localAI[3] = 0.0f;
          this.NPC.netUpdate = true;
          Projectile projectile = Main.projectile[this.ArenaProjID];
          if ((projectile == null || !((Entity) projectile).active ? 0 : (projectile.type == ModContent.ProjectileType<BaronArenaWhirlpool>() ? 1 : 0)) != 0)
          {
            projectile.ai[2] = 0.0f;
            projectile.netUpdate = true;
          }
        }
      }
      this.NPC.noTileCollide = this.Phase == 2 || WorldSavingSystem.MasochistModeReal || Collision.SolidCollision(Vector2.op_Addition(((Entity) this.NPC).position, Vector2.op_Division(((Entity) this.NPC).Size, 10f)), (int) ((double) ((Entity) this.NPC).width * 0.89999997615814209), (int) ((double) ((Entity) this.NPC).height * 0.89999997615814209)) || !Collision.CanHitLine(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, ((Entity) this.player).position, ((Entity) this.player).width, ((Entity) this.player).height);
      if (this.Phase == 1 && (double) ((Entity) this.NPC).Center.Y < (double) ((Entity) this.player).Center.Y && !Collision.WetCollision(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height) && !Collision.SolidCollision(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height))
        ((Entity) this.NPC).position.Y += 4f;
      this.NPC.defense = this.NPC.defDefense;
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) Utils.ToRotationVector2(this.NPC.rotation).X > 0.0 ? 1 : -1;
      if ((this.HitPlayer || (double) this.State == 1.0) && ((double) this.Timer >= 90.0 || (double) this.State != 5.0))
        Lighting.AddLight(((Entity) this.NPC).Center, 15);
      if (!this.AliveCheck(this.player))
        return;
      if ((double) this.State == 0.0 && (double) this.Timer == 0.0)
      {
        ((Entity) this.NPC).position = Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.player).Center, new Vector2((float) (Math.Sign(((Entity) this.player).Center.X - (float) Main.spawnTileX) * 1400), -100f)), Vector2.op_Division(((Entity) this.NPC).Size, 2f));
        this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) this.player).Center));
        ((Entity) this.NPC).velocity = Vector2.Zero;
      }
      switch (this.Phase)
      {
        case 1:
          if (((double) this.State != 0.0 || (double) this.Timer >= 60.0) && (double) ((Entity) this.NPC).Distance(((Entity) Main.LocalPlayer).Center) < 2000.0)
            Main.LocalPlayer.AddBuff(ModContent.BuffType<BaronsBurdenBuff>(), 1, true, false);
          if (this.Wet() && (double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() > 3.0 && Utils.NextBool(Main.rand, 2) && Main.netMode != 2)
          {
            Vector2 worldPosition = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), 120f), 2f)), Utils.NextVector2Circular(Main.rand, 10f, 10f));
            new Bubble(worldPosition, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedByRandom(((Entity) this.NPC).velocity, 0.37699112296104431)), Utils.NextFloat(Main.rand, 0.6f, 1f)), 2f), 1f, 30, rotation: Utils.NextFloat(Main.rand, 6.28318548f)).Spawn();
            Dust.NewDust(worldPosition, 2, 2, 33, -((Entity) this.NPC).velocity.X, -((Entity) this.NPC).velocity.Y, 0, new Color(), 1f);
            break;
          }
          break;
        case 2:
          int num1 = ModContent.ProjectileType<BaronArenaWhirlpool>();
          if (this.ArenaProjID == -1 && FargoSoulsUtil.HostCheck)
            this.ArenaProjID = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.player).Center, Vector2.Zero, num1, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
          this.NPC.netUpdate = true;
          break;
      }
      if (this.Attacking)
      {
        float num2 = this.State;
        if ((double) num2 != 0.0)
        {
          if ((double) num2 != 1.0)
          {
            if ((double) num2 != 15.0)
            {
              if ((double) num2 != 2.0)
              {
                if ((double) num2 != 3.0)
                {
                  if ((double) num2 != 4.0)
                  {
                    if ((double) num2 != 5.0)
                    {
                      if ((double) num2 != 6.0)
                      {
                        if ((double) num2 != 7.0)
                        {
                          if ((double) num2 != 8.0)
                          {
                            if ((double) num2 != 9.0)
                            {
                              if ((double) num2 != 10.0)
                              {
                                if ((double) num2 != 11.0)
                                {
                                  if ((double) num2 != 12.0)
                                  {
                                    if ((double) num2 != 13.0)
                                    {
                                      if ((double) num2 != 14.0)
                                      {
                                        if ((double) num2 == 16.0)
                                        {
                                          this.DeathAnimation();
                                          goto label_65;
                                        }
                                      }
                                      else
                                      {
                                        this.P2LaserSweep();
                                        goto label_65;
                                      }
                                    }
                                    else if (WorldSavingSystem.EternityMode)
                                    {
                                      this.P2Whirlpool();
                                      goto label_65;
                                    }
                                  }
                                  else
                                  {
                                    this.P2MineFlurry();
                                    goto label_65;
                                  }
                                }
                                else if (Main.expertMode)
                                {
                                  this.P2SpinDash();
                                  goto label_65;
                                }
                              }
                              else if (Main.expertMode)
                              {
                                this.P2RocketStorm();
                                goto label_65;
                              }
                            }
                            else
                            {
                              this.P2CarpetBomb();
                              goto label_65;
                            }
                          }
                          else
                          {
                            this.P2PredictiveDash();
                            goto label_65;
                          }
                        }
                        else if (WorldSavingSystem.EternityMode)
                        {
                          this.P1Whirlpool();
                          goto label_65;
                        }
                      }
                      else
                      {
                        this.P1SineSwim();
                        goto label_65;
                      }
                    }
                    else
                    {
                      this.P1FadeDash();
                      goto label_65;
                    }
                  }
                  else
                  {
                    this.P1SurfaceMines();
                    goto label_65;
                  }
                }
                else if (Main.expertMode)
                {
                  this.P1RocketStorm();
                  goto label_65;
                }
                this.StateReset();
              }
              else
                this.P1BigNuke();
            }
            else
              this.Swim();
          }
          else
            this.Phase2Transition();
        }
        else
          this.Opening();
      }
label_65:
      ++this.Timer;
    }

    private bool Wet(Vector2? pos = null)
    {
      if (!pos.HasValue)
        pos = new Vector2?(((Entity) this.NPC).position);
      return Collision.WetCollision(pos.Value, ((Entity) this.NPC).width, ((Entity) this.NPC).height);
    }

    private void Opening()
    {
      this.HitPlayer = false;
      this.Anim = 1;
      if (Vector2.op_Equality(this.LockVector1, Vector2.Zero))
      {
        for (int index = 0; index < Main.maxProjectiles; ++index)
        {
          Projectile projectile = Main.projectile[index];
          if (projectile.TypeAlive<MechLureProjectile>())
          {
            this.LockVector1 = ((Entity) projectile).Center;
            break;
          }
        }
        if (Vector2.op_Equality(this.LockVector1, Vector2.Zero))
          this.LockVector1 = ((Entity) this.player).Center;
      }
      if (this.NPC.alpha > 0)
        this.NPC.alpha -= 2;
      else
        this.NPC.alpha = 0;
      if (Vector2.op_Inequality(this.LockVector1, Vector2.Zero))
      {
        this.RotateTowards(this.LockVector1, 1.5f);
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Subtraction(this.LockVector1, ((Entity) this.NPC).Center), this.Timer / 90f), 0.4f), 0.3f);
      }
      if ((double) this.Timer == 60.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -24f, 0.0f);
        if (WorldSavingSystem.EternityMode && !WorldSavingSystem.DownedBoss[12] && FargoSoulsUtil.HostCheck)
          Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) Main.player[this.NPC.target]).Hitbox, ModContent.ItemType<MechLure>(), 1, false, 0, false, false);
      }
      if ((double) this.Timer <= 90.0)
        return;
      this.HitPlayer = true;
      this.Anim = 0;
      this.StateReset();
    }

    private void Phase2Transition()
    {
      if ((double) this.Timer < 60.0)
        this.RotateTowards(Vector2.op_Addition(((Entity) this.NPC).Center, this.LockVector1), 15f);
      else if ((double) this.Timer < 135.0)
        this.RotateTowards(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Addition(Vector2.op_Multiply(Vector2.Normalize(Vector2.op_UnaryNegation(this.LockVector1)), 2f), Vector2.op_Multiply(Vector2.UnitY, 1f))), 15f);
      else if ((double) this.Timer < 150.0)
        this.RotateTowards(Vector2.op_Addition(((Entity) this.NPC).Center, new Vector2((float) (3 * Math.Sign(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) this.player).Center).X)), -10f)), 15f);
      if ((double) this.Timer == 1.0)
      {
        SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/BaronHit", (SoundType) 0);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.HitPlayer = false;
        this.LockVector1 = Vector2.op_Multiply(Vector2.UnitX, (float) Math.Sign(((Entity) this.player).Center.X - ((Entity) this.NPC).Center.X));
      }
      if ((double) this.Timer == 45.0)
      {
        SoundEngine.PlaySound(ref SoundID.DD2_KoboldExplosion, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        Vector2 worldPosition = Vector2.op_Subtraction(((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), (float) ((Entity) this.NPC).width * 0.25f));
        if (!Main.dedServ)
          Gore.NewGorePerfect(((Entity) this.NPC).GetSource_FromThis((string) null), worldPosition, Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.ToRotationVector2(this.NPC.rotation)), 4f), ModContent.Find<ModGore>(((ModType) this).Mod.Name, "BaronGorePropeller").Type, 1f).light = 1f;
        for (int index = 0; index < 5; ++index)
          new SparkParticle(worldPosition, Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedByRandom(Utils.ToRotationVector2(this.NPC.rotation), 0.39269909262657166)), Utils.NextFloat(Main.rand, 8f, 12f)), Color.Orange, 1f, 40).Spawn();
      }
      this.Anim = (double) this.Timer <= 45.0 || (double) this.Timer >= 150.0 ? 0 : 3;
      if ((double) this.Timer < 150.0)
      {
        if (!Main.dedServ)
          FargoSoulsUtil.ScreenshakeRumble(6f);
        if (((Entity) Main.LocalPlayer).wet)
          ((Entity) Main.LocalPlayer).velocity.Y -= 0.05f;
      }
      if ((double) this.Timer == 150.0)
      {
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), 20f);
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron.BaronYell, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.Phase = 2;
        FargoSoulsUtil.ClearHostileProjectiles(2, ((Entity) this.NPC).whoAmI);
        Mod mod;
        this.Music = Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod) ? MusicLoader.GetMusicSlot(mod, "Assets/Music/Baron2") : 12;
      }
      if ((double) this.Timer <= 150.0)
        return;
      if (!this.Wet() || (double) this.Timer > 210.0)
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.95f);
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() < 0.10000000149011612)
        {
          this.DidWhirlpool = false;
          this.HitPlayer = true;
          this.availablestates.Clear();
          this.StateReset();
        }
      }
      if (!((Entity) Main.LocalPlayer).wet || (double) ((Entity) Main.LocalPlayer).velocity.Y <= -30.0)
        return;
      ((Entity) Main.LocalPlayer).velocity.Y -= 1.25f;
      ((Entity) Main.LocalPlayer).position.Y -= 6f;
    }

    private void DeathAnimation()
    {
      Main.musicFade[Main.curMusic] = (float) Utils.Lerp(1.0, 0.30000001192092896, (double) this.Timer / 200.0);
      NPC npc1 = this.NPC;
      ((Entity) npc1).velocity = Vector2.op_Multiply(((Entity) npc1).velocity, 0.96f);
      this.Trail = 8;
      this.Anim = 1;
      this.NPC.noTileCollide = false;
      this.NPC.waterMovementSpeed = 0.75f;
      if ((double) ((Entity) this.NPC).velocity.Y < 20.0)
        ((Entity) this.NPC).velocity.Y += 0.6f;
      if ((double) this.Timer % 20.0 == 19.0)
      {
        SoundStyle beep = BaronNuke.Beep;
        ((SoundStyle) ref beep).Pitch = Utils.Clamp<float>((float) (1.25 * ((double) this.Timer / 200.0) - 1.0), -1f, 1f);
        SoundEngine.PlaySound(ref beep, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      }
      if ((double) this.Timer % (double) (int) Math.Round(30.0 - 28.0 * ((double) this.Timer / 200.0)) == 0.0)
      {
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).position, Vector2.op_Division(((Entity) this.NPC).Size, 3f));
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector((int) vector2_1.X, (int) vector2_1.Y, (int) ((double) ((Entity) this.NPC).width / 3.0), (int) ((double) ((Entity) this.NPC).height / 3.0));
        Vector2 vector2_2 = Utils.NextVector2FromRectangle(Main.rand, rectangle);
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(vector2_2), (SoundUpdateCallback) null);
        Vector2 position = vector2_2;
        Vector2 velocity = ((Entity) this.NPC).velocity;
        new ExpandingBloomParticle(position, velocity, Color.Lerp(Color.Yellow, Color.Red, Utils.NextFloat(Main.rand)), Vector2.op_Multiply(Vector2.One, 3f), Vector2.op_Multiply(Vector2.One, 6f), 15).Spawn();
        Vector2 vector2_3 = Utils.NextVector2CircularEdge(Main.rand, 1f, 1f);
        float num1 = Utils.NextFloat(Main.rand, 6f, 15f);
        int num2 = Main.rand.Next(6) + 1;
        if (!Main.dedServ)
        {
          IEntitySource sourceFromThis = ((Entity) this.NPC).GetSource_FromThis((string) null);
          Vector2 vector2_4 = position;
          Vector2 vector2_5 = Vector2.op_Multiply(vector2_3, num1);
          string name = ((ModType) this).Mod.Name;
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(14, 1);
          interpolatedStringHandler.AppendLiteral("BaronScrapGore");
          interpolatedStringHandler.AppendFormatted<int>(num2);
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          int type = ModContent.Find<ModGore>(name, stringAndClear).Type;
          double scale = (double) this.NPC.scale;
          Gore.NewGore(sourceFromThis, vector2_4, vector2_5, type, (float) scale);
        }
      }
      if (this.Wet())
      {
        NPC npc2 = this.NPC;
        ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, 0.85f);
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), ((Vector2) ref ((Entity) this.NPC).velocity).Length()), 0.2f);
        this.RotateTowards(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitX, (float) Math.Sign(Utils.ToRotationVector2(this.NPC.rotation).X))), 0.25f);
      }
      else
        this.RotateTowards(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.UnitY), 1.2f);
      if (!Collision.SolidCollision(Vector2.op_Addition(((Entity) this.NPC).position, ((Entity) this.NPC).velocity), ((Entity) this.NPC).width, ((Entity) this.NPC).height) && (double) this.Timer <= 200.0)
        return;
      SoundEngine.PlaySound(ref SoundID.Item62, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      this.NPC.StrikeInstantKill();
    }

    private void Swim()
    {
      int num1 = (int) Math.Min(((Entity) this.NPC).Distance(((Entity) this.player).Center) + 50f, 450f);
      if ((double) this.Timer == 1.0)
      {
        if (Collision.SolidCollision(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height))
        {
          this.AI3 = -2f;
          for (int index = 0; index < 30; ++index)
          {
            this.LockVector1 = Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 6.2831854820251465), (float) num1));
            if (!Collision.SolidCollision(Vector2.op_Subtraction(this.LockVector1, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), ((Entity) this.NPC).width, ((Entity) this.NPC).height) && this.Wet(new Vector2?(Vector2.op_Subtraction(this.LockVector1, Vector2.op_Division(((Entity) this.NPC).Size, 2f)))))
              break;
          }
        }
        else
          this.LockVector1 = Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.player, ((Entity) this.NPC).Center), (float) num1));
        if (this.Wet() && WorldSavingSystem.MasochistModeReal)
          Thirsty();
      }
      if ((double) Vector2.Distance(((Entity) this.NPC).Center, this.LockVector1) < 25.0 || (double) this.Timer > 100.0 || (double) this.AI3 != -2.0 && (double) this.AI3 != 0.0)
      {
        this.AI3 = (float) (((double) this.AI3 + 1.0) * 3.0);
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.85f);
        this.RotateTowards(((Entity) this.player).Center, 3f);
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() >= 0.10000000149011612 || (double) this.Timer <= 50.0)
          return;
        if (this.Wet() && WorldSavingSystem.MasochistModeReal)
          Thirsty();
        ((Entity) this.NPC).velocity = Vector2.Zero;
        this.StateReset();
      }
      else
      {
        if (Collision.SolidCollision(Vector2.op_Addition(((Entity) this.NPC).Center, ((Entity) this.NPC).velocity), ((Entity) this.NPC).width, ((Entity) this.NPC).height) && (double) this.Timer < 40.0 && (double) this.AI3 != -2.0)
          this.Timer = 40f;
        Vector2 vector2 = Vector2.op_Subtraction(this.LockVector1, ((Entity) this.NPC).Center);
        float num2 = (double) this.AI3 >= 0.0 ? 28f : 14f;
        if (WorldSavingSystem.EternityMode && !WorldSavingSystem.MasochistModeReal)
          num2 *= 1.2f;
        float num3 = (double) this.AI3 >= 0.0 ? 20f : 10f;
        ((Vector2) ref vector2).Normalize();
        vector2 = Vector2.op_Multiply(vector2, num2);
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.NPC).velocity, num3 - 1f), vector2), num3), 0.3f);
        if (Vector2.op_Equality(((Entity) this.NPC).velocity, Vector2.Zero))
        {
          ((Entity) this.NPC).velocity.X = -0.15f;
          ((Entity) this.NPC).velocity.Y = -0.05f;
        }
        this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity);
      }

      void Thirsty()
      {
        int num1 = (int) ((Entity) this.NPC).Center.X / 16;
        int num2 = (int) ((Entity) this.NPC).Center.Y / 16;
        Tile tile = ((Tilemap) ref Main.tile)[num1, num2];
        if (((Tile) ref tile).LiquidAmount <= (byte) 0 || ((Tile) ref tile).LiquidType != 0)
          return;
        ((Tile) ref tile).LiquidAmount = (byte) 0;
        CombatText.NewText(((Entity) this.NPC).Hitbox, Color.Blue, Language.GetTextValue("Mods.FargowiltasSouls.NPCs.BanishedBaron.Slurp"), false, false);
        if (Main.netMode == 2)
        {
          NetMessage.sendWater(num1, num2);
          NetMessage.SendTileSquare(-1, num1, num2, 1, (TileChangeType) 0);
        }
        else
          WorldGen.SquareTileFrame(num1, num2, true);
      }
    }

    private void P1BigNuke()
    {
      this.RotateTowards(((Entity) this.player).Center, 2f);
      this.Anim = (double) this.Timer >= 30.0 ? 0 : 1;
      if ((double) this.Timer == 30.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item61, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), 10f);
          this.AI2 = Utils.NextFloat(Main.rand, 190f, 220f);
          this.NPC.netUpdate = true;
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<BaronNuke>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, this.AI2, (float) ((Entity) this.player).whoAmI, 0.0f);
        }
      }
      if ((double) this.Timer <= (double) this.AI2 + 10.0 || (double) this.AI2 <= 0.0)
        return;
      this.Anim = 0;
      this.StateReset();
    }

    private void P1RocketStorm()
    {
      this.RotateTowards(((Entity) this.player).Center, 3f);
      if ((double) this.Timer < 80.0 && (double) this.Timer % 6.0 == 0.0 && (double) this.Timer > 30.0)
      {
        this.Anim = 1;
        SoundEngine.PlaySound(ref SoundID.Item64, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          float num1 = (float) (((double) this.Timer - 10.0) / 60.0);
          Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation + (1.57079637f + num1 * 0.5235988f) * (float) -((Entity) this.NPC).direction), (float) (10.0 * (double) num1 + 10.0));
          float num2 = WorldSavingSystem.MasochistModeReal ? 1.2f : 1f;
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<BaronRocket>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 3f, (float) ((Entity) this.player).whoAmI, num2);
        }
      }
      if ((double) this.Timer <= 80.0)
        return;
      this.Anim = 0;
      this.StateReset();
    }

    private void P1SurfaceMines()
    {
      this.NPC.noTileCollide = true;
      if ((double) this.Timer == 1.0)
      {
        Vector2 vector2 = Vector2.op_Subtraction(((Entity) this.player).Center, ((Entity) this.NPC).Center);
        if ((double) Math.Abs(vector2.X) < 200.0 || (double) Math.Abs(vector2.Y) > 350.0)
        {
          this.State = 15f;
          this.StateReset();
        }
        this.LockVector1 = new Vector2(((Entity) this.player).Center.X, ((Entity) this.player).Center.Y + 800f);
      }
      if ((double) this.Timer >= 1.0)
        this.RotateTowards(this.LockVector1, 2f);
      if ((double) this.Timer < 33.0)
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitX, ((Entity) this.player).Center.X + (float) (Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X) * 400)), Vector2.op_Multiply(Vector2.UnitY, ((Entity) this.player).Center.Y)), ((Entity) this.NPC).Center), this.Timer / 33f), 3f), 0.3f);
      if ((double) this.Timer >= 33.0 && (double) this.Timer <= 40.0)
        ((Entity) this.NPC).velocity = Vector2.Lerp(Vector2.op_Division(((Entity) this.NPC).velocity, 3f), Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), 12f), (float) (((double) this.Timer - 33.0) / 7.0));
      if ((double) this.Timer == 40.0)
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron.BaronYell, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      if ((double) this.Timer > 60.0)
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.97f);
      }
      else if ((double) ((Entity) this.player).velocity.Y > 0.0)
        ((Entity) this.NPC).velocity.Y += ((Entity) this.player).velocity.Y;
      if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() > 12.0)
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), 12f);
      if ((double) this.Timer >= 60.0 && (double) this.Timer % 5.0 == 0.0 && (double) this.AI3 < 4.0)
      {
        ++this.AI3;
        SoundEngine.PlaySound(ref SoundID.Item61, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 rotationVector2 = Utils.ToRotationVector2(this.NPC.rotation);
          float num = Math.Max(((Entity) this.player).velocity.Y, 0.0f);
          Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) Math.Sign(rotationVector2.X)), this.AI3), 8f), Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, rotationVector2.Y), 8f + num));
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_1, ModContent.ProjectileType<BaronMine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, (float) ((Entity) this.player).whoAmI, 0.0f);
          if (WorldSavingSystem.MasochistModeReal)
          {
            Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) Math.Sign(-rotationVector2.X)), this.AI3 - 1f), 8f), Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, rotationVector2.Y), 8f + num));
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<BaronMine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, (float) ((Entity) this.player).whoAmI, 0.0f);
          }
        }
      }
      if ((double) this.Timer <= 160.0)
        return;
      this.StateReset();
    }

    private void P1FadeDash()
    {
      int num1 = WorldSavingSystem.EternityMode ? 30 : 50;
      if ((double) this.Timer == 1.0)
      {
        this.LockVector1 = !Utils.NextBool(Main.rand) ? Utils.RotatedByRandom(Vector2.op_Multiply(Vector2.op_UnaryNegation(Vector2.UnitY), (float) Main.rand.Next(500, 600)), 1.5707963705062866) : Utils.RotatedByRandom(Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_UnaryNegation(Vector2.UnitY), 620f), 1.5707963705062866 * (Utils.NextBool(Main.rand) ? 1.0 : -1.0), new Vector2()), 0.78539818525314331);
        this.NPC.netUpdate = true;
      }
      if ((double) this.Timer < 90.0 && (double) this.Timer % 3.0 == 0.0)
      {
        bool flag = false;
        Vector2 vector2 = Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.player).Center, this.LockVector1), Vector2.op_Division(((Entity) this.NPC).Size, 2f));
        if (!Collision.SolidCollision(vector2, ((Entity) this.NPC).width, ((Entity) this.NPC).height) && !Collision.WetCollision(vector2, ((Entity) this.NPC).width, ((Entity) this.NPC).height))
          flag = true;
        for (float num2 = 0.0f; (double) num2 < 0.60000002384185791; num2 += 0.1f)
        {
          if (Collision.SolidCollision(Vector2.op_Subtraction(vector2, Vector2.op_Multiply(this.LockVector1, num2)), ((Entity) this.NPC).width, ((Entity) this.NPC).height))
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          this.LockVector1 = Utils.RotatedByRandom(Vector2.op_Multiply(Vector2.UnitY, (float) Main.rand.Next(500, 600)), 3.1415927410125732);
          this.NPC.netUpdate = true;
        }
      }
      if ((double) this.Timer < 60.0)
      {
        if (this.NPC.buffType[0] != 0)
          this.NPC.DelBuff(0);
        this.RotateTowards(((Entity) this.player).Center, 2f);
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() < 1.0)
        {
          NPC npc1 = this.NPC;
          ((Entity) npc1).velocity = Vector2.op_Addition(((Entity) npc1).velocity, Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), 0.02f));
          NPC npc2 = this.NPC;
          ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, 1.03f);
        }
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), ((Vector2) ref ((Entity) this.NPC).velocity).Length()), 0.3f);
        this.NPC.Opacity -= 0.02f;
        if ((double) this.NPC.Opacity < 0.0)
          this.NPC.Opacity = 0.0f;
        this.HitPlayer = false;
      }
      if ((double) this.Timer == 60.0 && FargoSoulsUtil.HostCheck)
      {
        this.NPC.netUpdate = true;
        this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, Vector2.op_Addition(((Entity) this.player).Center, this.LockVector1)));
      }
      if ((double) this.Timer > 60.0 && (double) this.Timer < 90.0)
      {
        if (this.NPC.buffType[0] != 0)
          this.NPC.DelBuff(0);
        this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, Vector2.op_Addition(((Entity) this.player).Center, this.LockVector1)));
        this.NPC.noTileCollide = true;
        ((Entity) this.NPC).Center = Vector2.op_Addition(((Entity) this.player).Center, this.LockVector1);
        this.NPC.netUpdate = true;
        this.NPC.dontTakeDamage = true;
      }
      if ((double) this.Timer == 90.0)
      {
        if (!Collision.SolidCollision(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height))
          this.NPC.noTileCollide = false;
        ((Entity) this.NPC).velocity = Vector2.Zero;
        this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) this.player).Center));
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), (float) ((Entity) this.NPC).width), 0.35f)), Vector2.Zero, ModContent.ProjectileType<BaronEyeFlash>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
      }
      if ((double) this.Timer > 90.0 && (double) this.Timer < (double) (90 + num1))
      {
        this.RotateTowards(((Entity) this.player).Center, 2f);
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() < 3.0)
        {
          NPC npc3 = this.NPC;
          ((Entity) npc3).velocity = Vector2.op_Addition(((Entity) npc3).velocity, Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), 0.03f));
          NPC npc4 = this.NPC;
          ((Entity) npc4).velocity = Vector2.op_Multiply(((Entity) npc4).velocity, 1.03f);
        }
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), ((Vector2) ref ((Entity) this.NPC).velocity).Length()), 0.3f);
        this.NPC.Opacity += 1f / (float) num1;
        if ((double) this.NPC.Opacity > 1.0)
          this.NPC.Opacity = 1f;
        this.HitPlayer = true;
      }
      int num3 = 90 + num1;
      int num4 = 7;
      float num5 = 45f;
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.player).Center, ((Entity) this.NPC).Center);
      float num6 = (float) Math.Sqrt((double) ((Vector2) ref vector2_1).Length()) / 1.5f;
      if ((double) this.Timer > (double) (num3 - num4) && (double) this.Timer < (double) num3)
      {
        float num7 = Math.Max((this.Timer - (float) (num3 - num4)) / (float) num4, 0.0f);
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), num5 + num6), num7);
      }
      if ((double) this.Timer == (double) num3)
      {
        this.NPC.Opacity = 1f;
        this.NPC.dontTakeDamage = false;
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron.BaronRoar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), num5 + num6);
      }
      if ((double) this.Timer > (double) (90 + num1))
      {
        if (this.NPC.noTileCollide)
        {
          NPC npc = this.NPC;
          ((Entity) npc).position = Vector2.op_Subtraction(((Entity) npc).position, Vector2.op_Division(((Entity) this.NPC).velocity, 2f));
        }
        else if (Collision.SolidCollision(Vector2.op_Addition(((Entity) this.NPC).position, ((Entity) this.NPC).velocity), ((Entity) this.NPC).width, ((Entity) this.NPC).height))
        {
          SoundEngine.PlaySound(ref SoundID.Dig, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          ((Entity) this.NPC).velocity = Vector2.op_Division(Vector2.op_UnaryNegation(((Entity) this.NPC).velocity), 4f);
          ((Entity) this.NPC).velocity = Utils.RotatedByRandom(((Entity) this.NPC).velocity, 0.47123894095420837);
          this.NPC.SimpleStrikeNPC(500, 1, false, 0.0f, (DamageClass) null, false, 0.0f, false);
          this.AI3 = 1f;
          if (FargoSoulsUtil.HostCheck)
          {
            for (int index = 0; index < 6; ++index)
            {
              Vector2 vector2_2 = Vector2.op_Multiply(Vector2.Normalize(Utils.RotatedByRandom(((Entity) this.NPC).velocity, 0.62831854820251465)), Utils.NextFloat(Main.rand, 7f, 10f));
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, vector2_2), vector2_2, ModContent.ProjectileType<BaronScrap>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
        }
        this.NPC.dontTakeDamage = false;
        NPC npc5 = this.NPC;
        ((Entity) npc5).velocity = Vector2.op_Multiply(((Entity) npc5).velocity, 0.975f);
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() > 8.0 && WorldSavingSystem.EternityMode && (double) this.AI3 != 1.0 && (double) this.Timer % 11.0 == 0.0)
        {
          SoundEngine.PlaySound(ref SoundID.Item64, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          for (int index = -1; index < 2; index += 2)
          {
            if (FargoSoulsUtil.HostCheck)
            {
              Vector2 rotationVector2 = Utils.ToRotationVector2(this.NPC.rotation);
              double num8 = 1.5707963705062866 * (double) index;
              vector2_1 = new Vector2();
              Vector2 vector2_3 = vector2_1;
              Vector2 vector2_4 = Vector2.op_Multiply(Utils.RotatedBy(rotationVector2, num8, vector2_3), 1f);
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(vector2_4, 20f)), Vector2.op_Multiply(vector2_4, 0.3f), ModContent.ProjectileType<BaronRocket>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 4f, (float) ((Entity) this.player).whoAmI, 0.0f);
            }
          }
        }
      }
      if (((double) this.Timer == (double) (90 + num1 + 15) || (double) this.Timer == (double) (90 + num1 + 30)) && WorldSavingSystem.EternityMode && (double) this.AI3 != 1.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item64, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2_5 = Vector2.op_Multiply(((Entity) this.NPC).velocity, 1.2f);
          float num9 = WorldSavingSystem.MasochistModeReal ? 1.5f : 1f;
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_5, ModContent.ProjectileType<BaronRocket>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 3f, (float) ((Entity) this.player).whoAmI, num9);
        }
      }
      if ((double) this.Timer <= (double) (90 + num1 + 30) || (double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() >= 1.5)
        return;
      this.NPC.noTileCollide = false;
      this.StateReset();
    }

    private void P1SineSwim()
    {
      int num1 = WorldSavingSystem.MasochistModeReal ? 100 : (WorldSavingSystem.EternityMode ? 150 : 180);
      float num2 = (float) Math.Pow((double) this.Timer / (double) num1, 2.0);
      this.NPC.noTileCollide = true;
      if ((double) this.Timer == 1.0)
      {
        this.AI2 = (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X);
        this.LockVector1 = Vector2.op_Addition(((Entity) this.player).Center, new Vector2(1000f * this.AI2, 0.0f));
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron.BaronYell, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.NPC.netUpdate = true;
      }
      if ((double) this.AI3 == 0.0)
      {
        Vector2 vector2_1 = Vector2.op_Addition(this.LockVector1, new Vector2((float) (-(double) this.AI2 * (double) num2 * 1750.0), 600f * (float) Math.Sin(6.2831854820251465 * (double) num2 * 2.0)));
        this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2_1));
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(1f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2_1)), ((Entity) this.NPC).Distance(vector2_1)), 1.2f), 0.3f);
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() > 25.0)
        {
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 25f / ((Vector2) ref ((Entity) this.NPC).velocity).Length());
        }
        if ((double) this.Timer == Math.Round((double) num1 * 0.33000001311302185) || (double) this.Timer == Math.Round((double) num1 * 0.6600000262260437) || (double) this.Timer == Math.Round((double) num1 * 0.99000000953674316))
        {
          SoundEngine.PlaySound(ref SoundID.Item63, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2_2 = Vector2.op_Multiply(Utils.ToRotationVector2(-this.NPC.rotation), 5f);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<BaronMine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 2f, (float) ((Entity) this.player).whoAmI, 0.0f);
          }
        }
        if ((double) num2 > 1.0)
          this.AI3 = 1f;
      }
      if ((double) this.AI3 == 1.0)
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.96f);
      }
      if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() >= 1.0)
        return;
      this.NPC.noTileCollide = false;
      ((Entity) this.NPC).velocity = Vector2.Zero;
      this.StateReset();
    }

    private void P1Whirlpool()
    {
      this.NPC.noTileCollide = true;
      if ((double) this.Timer == 1.0)
      {
        if (this.DidWhirlpool)
        {
          this.DidWhirlpool = false;
          this.LastState = (int) this.State;
          this.State = 15f;
          this.StateReset();
          return;
        }
        this.DidWhirlpool = true;
        for (int index = 0; index < 20; ++index)
        {
          this.LockVector1 = Vector2.op_Subtraction(((Entity) this.player).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, 2000f), (float) (20 - index)));
          if (this.Wet(new Vector2?(Vector2.op_Addition(this.LockVector1, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, 2f), 2000f), 20f)))))
          {
            this.NPC.netUpdate = true;
            break;
          }
        }
      }
      if ((double) this.Timer < 60.0)
      {
        this.HitPlayer = false;
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Subtraction(this.LockVector1, ((Entity) this.NPC).Center), this.Timer / 60f), 0.04f), 0.3f);
        this.RotateTowards(this.LockVector1, 2f);
        if (!this.Wet(new Vector2?(Vector2.op_Addition(((Entity) this.NPC).position, Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitY, (float) ((Entity) this.NPC).height), 4f)))) && (double) ((Entity) this.NPC).Center.Y < (double) ((Entity) this.player).Center.Y)
        {
          if (Collision.WetCollision(((Entity) this.player).position, ((Entity) this.player).width, ((Entity) this.player).height))
            ((Entity) this.NPC).velocity.Y *= 0.2f;
          else if ((double) ((Entity) this.NPC).Center.Y < (double) ((Entity) this.player).Center.Y - 500.0)
            ((Entity) this.NPC).velocity.Y *= 0.2f;
        }
        else
          ((Entity) this.NPC).velocity.Y += 0.25f;
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).LengthSquared() >= 900.0)
          ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), 30f);
      }
      else
      {
        this.HitPlayer = true;
        ((Entity) this.NPC).velocity = Vector2.Zero;
      }
      if ((double) this.Timer > 50.0)
        this.RotateTowards(Vector2.op_Subtraction(((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.UnitY, (double) MathHelper.ToRadians(1f), new Vector2())), 16f);
      if ((double) this.Timer == 60.0 && FargoSoulsUtil.HostCheck)
      {
        this.AnimationSpeed = 2f;
        int num = Main.rand.Next(2);
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitY, (float) (((Entity) this.NPC).height / 2 + 24))), Vector2.Zero, ModContent.ProjectileType<BaronWhirlpool>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 40f, (float) num);
      }
      if ((double) this.Timer < 480.0)
        return;
      this.AnimationSpeed = 1f;
      this.NPC.noTileCollide = false;
      ((Entity) this.NPC).velocity = Vector2.Zero;
      this.StateReset();
    }

    private void P2PredictiveDash()
    {
      float shootSpeed = 35f;
      if ((double) this.Timer == 1.0 && FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<BloomLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 2f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
      if ((double) this.Timer == 67.0)
        SoundEngine.PlaySound(ref SoundID.MaxMana, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      if ((double) this.Timer < 67.0)
      {
        Vector2 vector2 = Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) this.NPC).Center, ((Entity) this.player).Center)), 500f));
        if ((double) ((Entity) this.NPC).Distance(vector2) < 25.0 && (double) this.AI3 == 0.0 || (double) this.Timer % 60.0 == 0.0 && (double) this.AI3 != 0.0)
        {
          this.AI3 = -1f;
          this.NPC.netUpdate = true;
        }
        if ((double) this.AI3 == 0.0)
        {
          if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() < 20.0 + (double) ((Vector2) ref ((Entity) this.player).velocity).Length() / 2.0)
          {
            NPC npc = this.NPC;
            ((Entity) npc).velocity = Vector2.op_Addition(((Entity) npc).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2), 0.02f));
            ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2), ((Vector2) ref ((Entity) this.NPC).velocity).Length()), 1.05f), 0.5f);
          }
          else
            ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2), (float) (20.0 + (double) ((Vector2) ref ((Entity) this.player).velocity).Length() / 2.0)), 0.5f);
        }
        else
          ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Division(Vector2.op_Subtraction(vector2, ((Entity) this.NPC).Center), 10f), 0.3f);
        if ((double) ((Entity) this.NPC).Distance(((Entity) this.player).Center) < 200.0)
        {
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Subtraction(((Entity) npc).velocity, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) this.player).Center));
        }
        this.LockVector1 = FargoSoulsUtil.PredictiveAim(((Entity) this.NPC).Center, ((Entity) this.player).Center, ((Entity) this.player).velocity, shootSpeed);
        this.NPC.rotation = Utils.ToRotation(Vector2.Lerp(Utils.ToRotationVector2(this.NPC.rotation), Vector2.Normalize(this.LockVector1), 0.2f));
      }
      int num1 = 7;
      if ((double) this.Timer > (double) (72 - num1) && (double) this.Timer < 72.0)
      {
        float num2 = Math.Max((this.Timer - (float) (72 - num1)) / (float) num1, 0.0f);
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), shootSpeed), num2);
      }
      if ((double) this.Timer == 72.0)
      {
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron.BaronRoar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.Trail = 8;
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), shootSpeed);
        Vector2 vector2_1 = Vector2.Normalize(((Entity) this.NPC).velocity);
        Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.player).Center, ((Entity) this.NPC).Center);
        float num3 = Vector2.Dot(vector2_1, vector2_2);
        this.LockVector2 = Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(vector2_1, num3));
        if ((double) Math.Abs(FargoSoulsUtil.RotationDifference(this.LockVector1, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) this.player).Center))) > 1.5707963705062866)
          this.AI3 = 1f;
      }
      if ((double) this.Timer >= 72.0 && (double) this.Timer < 137.0 && (double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() > 5.0 && (double) this.Timer % (double) (int) (160.0 / (double) ((Vector2) ref ((Entity) this.NPC).velocity).Length()) == 0.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item64, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        for (int index = -1; index < 2; index += 2)
        {
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedBy(Utils.ToRotationVector2(this.NPC.rotation), 1.5707963705062866 * (double) index, new Vector2()), 1f);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(vector2, 20f)), Vector2.op_Multiply(vector2, 0.3f), ModContent.ProjectileType<BaronRocket>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 2f, (float) ((Entity) this.player).whoAmI, 0.0f);
          }
        }
      }
      if ((double) ((Entity) this.NPC).Distance(this.LockVector2) <= (double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() || (double) this.Timer > 117.0 && (double) this.Timer < 162.0 || (double) this.Timer > 207.0 && WorldSavingSystem.EternityMode)
        this.AI3 = 1f;
      if ((double) this.AI3 == 1.0)
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.97f);
        if ((double) this.Timer >= 137.0)
          this.RotateTowards(((Entity) this.player).Center, 8f);
      }
      if ((double) this.Timer == 162.0 && WorldSavingSystem.EternityMode)
      {
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron.BaronRoar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), shootSpeed), 1.2f);
      }
      if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() >= 5.0 || (double) this.AI3 != 1.0 || WorldSavingSystem.EternityMode && (double) this.Timer <= 162.0)
        return;
      this.Trail = 0;
      NPC npc1 = this.NPC;
      ((Entity) npc1).velocity = Vector2.op_Multiply(((Entity) npc1).velocity, 0.9f);
      if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() >= 0.699999988079071)
        return;
      this.StateReset();
    }

    private void P2CarpetBomb()
    {
      if ((double) this.Timer == 1.0)
        this.AI2 = (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X);
      if ((double) this.AI3 == 0.0)
      {
        this.LockVector1 = Vector2.op_Addition(((Entity) this.player).Center, new Vector2(800f * this.AI2, -300f));
        this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, this.LockVector1));
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() < 30.0 + (double) ((Vector2) ref ((Entity) this.player).velocity).Length())
        {
          NPC npc1 = this.NPC;
          ((Entity) npc1).velocity = Vector2.op_Addition(((Entity) npc1).velocity, Vector2.op_Multiply(0.5f, Utils.ToRotationVector2(this.NPC.rotation)));
          NPC npc2 = this.NPC;
          ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, 1.03f);
        }
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(((Vector2) ref ((Entity) this.NPC).velocity).Length(), Utils.ToRotationVector2(this.NPC.rotation)), 0.3f);
        if ((double) ((Entity) this.NPC).Distance(this.LockVector1) < 25.0)
          this.AI3 = 1f;
      }
      if ((double) this.AI3 > 0.0)
      {
        float p = this.AI3 / 90f;
        Vector2 vector2_1 = Curve(p);
        this.NPC.rotation = Utils.ToRotation(Vector2.op_Subtraction(Curve(p + 1E-05f), vector2_1));
        this.LockVector1 = Vector2.op_Addition(((Entity) this.player).Center, vector2_1);
        ((Entity) this.NPC).velocity = Vector2.op_Subtraction(this.LockVector1, ((Entity) this.NPC).Center);
        ++this.AI3;
        if ((double) this.AI3 % Math.Round(90.0 / (WorldSavingSystem.MasochistModeReal ? 8.0 : (WorldSavingSystem.EternityMode ? 7.0 : 6.0))) == 0.0)
        {
          SoundEngine.PlaySound(ref SoundID.Item63, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2_2;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2_2).\u002Ector(0.0f, (float) Main.rand.Next(15, 20));
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<BaronMine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 2f, (float) ((Entity) this.player).whoAmI, 0.0f);
          }
        }
        if ((double) p >= 1.0)
        {
          this.AI3 = -1f;
          SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron.BaronYell, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        }
      }
      if ((double) this.AI3 < 0.0)
      {
        if ((double) ((Entity) this.NPC).Distance(((Entity) this.player).Center) < 300.0 && (double) this.AI3 < -30.0)
          this.AI2 = -2f;
        if ((double) this.AI2 == -2.0)
        {
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.975f);
        }
        else
        {
          float num = (float) (15.0 + (double) ((Vector2) ref ((Entity) this.player).velocity).Length() / 2.0);
          if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() < (double) num)
          {
            NPC npc = this.NPC;
            ((Entity) npc).velocity = Vector2.op_Addition(((Entity) npc).velocity, Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), 0.02f));
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), ((Vector2) ref ((Entity) this.NPC).velocity).Length()), 1.03f);
          }
          else
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), num);
          this.RotateTowards(((Entity) this.player).Center, 2.5f);
        }
        --this.AI3;
      }
      if (((double) this.AI3 >= -30.0 || (double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() >= 1.25) && (double) this.AI3 >= -220.0)
        return;
      ((Entity) this.NPC).velocity = Vector2.Zero;
      this.StateReset();

      Vector2 Curve(float p)
      {
        return new Vector2((float) ((double) this.AI2 * 800.0 * (1.0 - 2.0 * (double) p)), (float) (-300.0 - 200.0 * Math.Sin(3.1415927410125732 * (double) p)));
      }
    }

    private void P2RocketStorm()
    {
      float num1 = 1f;
      float num2 = 0.5f;
      if ((double) this.Timer == 1.0)
      {
        this.AI2 = (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X);
        this.LockVector1 = new Vector2(500f * this.AI2, 0.0f);
      }
      this.RotateTowards(((Entity) this.player).Center, 4f);
      this.LockVector1 = Utils.RotatedBy(this.LockVector1, (double) MathHelper.ToRadians(this.AI3 + num2 * (float) Math.Sign(this.AI3)) * (double) num1, new Vector2());
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.player).Center, this.LockVector1);
      if ((double) ((Entity) this.NPC).Distance(vector2_1) < 200.0 && (double) this.AI3 == 0.0 || (double) this.Timer % 60.0 == 0.0 && (double) this.AI3 != 0.0)
      {
        this.AI3 = Utils.NextFloat(Main.rand, -1f, 1f);
        this.NPC.netUpdate = true;
      }
      if ((double) this.AI3 == 0.0)
      {
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() <= 20.0 + (double) ((Vector2) ref ((Entity) this.player).velocity).Length() / 2.0)
        {
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Addition(((Entity) npc).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2_1), 0.02f));
          ((Entity) this.NPC).velocity = ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2_1), ((Vector2) ref ((Entity) this.NPC).velocity).Length()), 1.05f), 0.5f);
        }
        else
          ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2_1), (float) (20.0 + (double) ((Vector2) ref ((Entity) this.player).velocity).Length() / 2.0));
        if ((double) this.Timer > 2.0)
          this.Timer = 2f;
      }
      else
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Division(Vector2.op_Subtraction(vector2_1, ((Entity) this.NPC).Center), 10f), 0.3f);
      if ((double) this.AI3 != 0.0 && (double) this.Timer < 300.0)
      {
        if (((double) this.Timer > 40.0 && (double) this.Timer < 100.0 || (double) this.Timer > 140.0 && (double) this.Timer < 200.0 ? 1 : ((double) this.Timer <= 240.0 ? 0 : ((double) this.Timer < 300.0 ? 1 : 0))) != 0)
        {
          this.Anim = 1;
          if ((double) this.Timer % 8.0 == 0.0)
          {
            SoundEngine.PlaySound(ref SoundID.Item64, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              int num3 = (double) this.Timer <= 140.0 || (double) this.Timer >= 200.0 ? 1 : -1;
              float num4 = Utils.NextFloat(Main.rand, 10f, 25f);
              Vector2 vector2_2 = Vector2.op_Multiply(Utils.RotatedByRandom(Utils.RotatedBy(Utils.ToRotationVector2(this.NPC.rotation), 1.5707963705062866 * (double) num3, new Vector2()), 0.39269909262657166), num4);
              float num5 = WorldSavingSystem.MasochistModeReal ? 1f : 1f;
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.Normalize(vector2_2), 20f)), vector2_2, ModContent.ProjectileType<BaronRocket>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, (float) ((Entity) this.player).whoAmI, num5);
            }
          }
        }
        else
          this.Anim = 0;
      }
      if ((double) this.Timer > 300.0)
        this.Anim = 0;
      if ((double) this.Timer <= 310.0)
        return;
      ((Entity) this.NPC).velocity = Vector2.Zero;
      this.StateReset();
    }

    private void P2SpinDash()
    {
      float num1 = 3.5f;
      if ((double) this.Timer == 1.0)
        this.AI2 = (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X);
      if ((double) this.AI3 == 0.0)
      {
        this.LockVector1 = Utils.RotatedBy(new Vector2(0.0f, 500f), 1.0995573997497559 * -(double) this.AI2, new Vector2());
        Vector2 vector2 = Vector2.op_Addition(((Entity) this.player).Center, this.LockVector1);
        this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2));
        this.NPC.netUpdate = true;
        float num2 = 500f * MathHelper.ToRadians(3f);
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() < (double) num2 + (double) ((Vector2) ref ((Entity) this.player).velocity).Length())
        {
          NPC npc1 = this.NPC;
          ((Entity) npc1).velocity = Vector2.op_Addition(((Entity) npc1).velocity, Vector2.op_Multiply(0.5f, Utils.ToRotationVector2(this.NPC.rotation)));
          NPC npc2 = this.NPC;
          ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, 1.03f);
        }
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(((Vector2) ref ((Entity) this.NPC).velocity).Length(), Utils.ToRotationVector2(this.NPC.rotation)), 0.3f);
        if ((double) ((Entity) this.NPC).Distance(vector2) < 25.0)
        {
          this.AI3 = Utils.NextFloat(Main.rand, 0.2f, 0.4f);
          this.NPC.netUpdate = true;
        }
      }
      if ((double) this.AI3 > 0.0)
      {
        this.AI2 += (float) Math.Sign(this.AI2);
        Vector2 vector2 = Vector2.op_Addition(((Entity) this.player).Center, this.LockVector1);
        if ((double) Math.Abs(this.AI2) <= 38.0)
        {
          ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Division(Vector2.op_Subtraction(vector2, ((Entity) this.NPC).Center), 10f), 0.3f);
          this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity);
          if ((double) Math.Abs(this.AI2) == 38.0)
            SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron.BaronYell, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        }
        else
        {
          num1 = 0.025f;
          this.RotateTowards(((Entity) this.player).Center, 6f);
          if ((double) Math.Abs(this.AI2) > 65.0)
          {
            SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron.BaronRoar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), 27f);
            this.AI3 = -1f;
            this.AI2 = (float) Math.Sign(this.AI2);
          }
        }
        this.LockVector1 = Utils.RotatedBy(this.LockVector1, (double) MathHelper.ToRadians(this.AI3 * (float) Math.Sign(this.AI2) * num1), new Vector2());
      }
      if ((double) this.AI3 < 0.0)
      {
        if ((double) this.AI3 == -1.0)
          this.AI2 = (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X);
        --this.AI3;
        if ((double) this.AI3 >= -110.0)
        {
          if ((double) Math.Abs(this.AI2) < 60.0)
            this.AI2 += (float) (Math.Sign(this.AI2) * 2);
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.995f);
          ((Entity) this.NPC).velocity.Y += 0.2f;
          this.NPC.rotation += (float) (-(double) this.AI2 * 3.1415927410125732 / 16.0 / 60.0);
        }
        else
        {
          if ((double) Math.Abs(this.AI2) > 4.0)
            this.AI2 -= (float) (Math.Sign(this.AI2) * 2);
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.955f);
          this.NPC.rotation += (float) (-(double) this.AI2 * 3.1415927410125732 / 16.0 / 60.0);
        }
        if ((double) this.AI3 < -42.0)
        {
          this.Anim = 1;
          if (FargoSoulsUtil.HostCheck)
          {
            float num3 = Utils.NextFloat(Main.rand, 8f, 13f);
            Vector2 rotationVector2 = Utils.ToRotationVector2(Utils.NextFloat(Main.rand, 6.28318548f));
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, rotationVector2), Vector2.op_Multiply(rotationVector2, num3), ModContent.ProjectileType<BaronScrap>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      if ((double) this.AI3 >= -130.0)
        return;
      this.Anim = 0;
      ((Entity) this.NPC).velocity = Vector2.Zero;
      this.StateReset();
    }

    private void P2MineFlurry()
    {
      if ((double) this.Timer == 1.0)
      {
        this.AI2 = (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X);
        this.LockVector1 = new Vector2(500f * this.AI2, 0.0f);
      }
      this.LockVector1 = new Vector2(500f * this.AI2, 0.0f);
      Vector2 vector2 = Vector2.op_Addition(((Entity) this.player).Center, this.LockVector1);
      if ((double) ((Entity) this.NPC).Distance(vector2) < 25.0 && (double) this.AI3 == 0.0 || (double) this.Timer % 60.0 == 0.0 && (double) this.AI3 != 0.0)
        this.AI3 = 1f;
      if ((double) this.AI3 == 0.0)
      {
        --this.Timer;
        this.RotateTowards(((Entity) this.player).Center, 4f);
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() < 200.0 + (double) ((Vector2) ref ((Entity) this.player).velocity).Length() / 2.0)
        {
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Addition(((Entity) npc).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2), 0.02f));
          ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2), ((Vector2) ref ((Entity) this.NPC).velocity).Length()), 1.05f), 0.7f);
        }
        else
          ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2), (float) (20.0 + (double) ((Vector2) ref ((Entity) this.player).velocity).Length() / 2.0)), 0.7f);
      }
      else
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Division(Vector2.op_Subtraction(vector2, ((Entity) this.NPC).Center), 10f), 0.3f);
      int num1 = (double) this.Timer < 20.0 || (double) this.Timer > 50.0 ? ((double) this.Timer < 130.0 ? 0 : ((double) this.Timer <= 160.0 ? 1 : 0)) : 1;
      bool flag = (double) this.Timer >= 20.0 && (double) this.Timer <= 35.0 || (double) this.Timer >= 130.0 && (double) this.Timer <= 145.0;
      if ((double) this.Timer == 5.0)
      {
        this.AI4 = Utils.NextBool(Main.rand) ? 1f : -1f;
        this.NPC.netUpdate = true;
      }
      if ((double) this.Timer == 115.0)
      {
        this.AI4 = -this.AI4;
        this.NPC.netUpdate = true;
      }
      if ((double) this.Timer == 5.0 || (double) this.Timer == 115.0)
      {
        int num2 = 0;
        if ((double) this.AI4 == (double) Math.Sign(((Entity) this.player).velocity.X))
          num2 = (int) ((double) ((Entity) this.player).velocity.X * 10.0);
        this.LockVector2 = new Vector2(0.0f, Utils.NextFloat(Main.rand, (float) (250 + num2), (float) (350 + num2)) * this.AI4);
        this.NPC.netUpdate = true;
      }
      if ((double) this.Timer > 5.0)
      {
        int speed = WorldSavingSystem.MasochistModeReal ? 15 : 10;
        this.RotateTowards(Vector2.op_Addition(((Entity) this.player).Center, this.LockVector2), (float) speed);
      }
      if (num1 != 0)
      {
        this.Anim = 1;
        if ((double) this.Timer % 5.0 == 3.0 && (!flag || WorldSavingSystem.MasochistModeReal))
        {
          SoundEngine.PlaySound(ref SoundID.Item63, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Subtraction(((Entity) npc).velocity, Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), 6f));
          if (FargoSoulsUtil.HostCheck)
          {
            float num3 = (float) Main.rand.Next(10, 38);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(num3, Utils.ToRotationVector2(this.NPC.rotation)), ModContent.ProjectileType<BaronMine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 2f, (float) ((Entity) this.player).whoAmI, 0.0f);
          }
        }
      }
      else
        this.Anim = 0;
      if ((double) this.Timer <= 320.0)
        return;
      ((Entity) this.NPC).velocity = Vector2.Zero;
      this.StateReset();
    }

    private void P2Whirlpool()
    {
      Projectile projectile = Main.projectile[this.ArenaProjID];
      if ((projectile == null || !((Entity) projectile).active ? 0 : (projectile.type == ModContent.ProjectileType<BaronArenaWhirlpool>() ? 1 : 0)) == 0)
      {
        ((Entity) this.NPC).velocity = Vector2.Zero;
        this.StateReset();
      }
      else
      {
        if ((double) this.Timer == 1.0)
        {
          if (this.DidWhirlpool)
          {
            this.DidWhirlpool = false;
            this.StateReset();
            return;
          }
          this.DidWhirlpool = true;
          this.Anim = 1;
          SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -24f, 0.0f);
        }
        if ((double) this.Timer == 61.0)
        {
          this.Anim = 0;
          projectile.ai[2] = 1f;
          projectile.netUpdate = true;
        }
        if ((double) this.Timer >= 61.0)
        {
          this.LockVector1 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) this.NPC).Center, ((Entity) this.player).Center)), 500f);
          Vector2 vector2 = Vector2.op_Addition(((Entity) this.player).Center, this.LockVector1);
          if ((double) ((Entity) this.NPC).Distance(vector2) < 25.0 && (double) this.AI3 == 0.0 || (double) this.Timer % 60.0 == 0.0 && (double) this.AI3 != 0.0)
          {
            this.AI3 = Utils.NextFloat(Main.rand, -1f, 1f);
            this.NPC.netUpdate = true;
          }
          if ((double) this.AI3 == 0.0)
          {
            if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() < 20.0 + (double) ((Vector2) ref ((Entity) this.player).velocity).Length() / 2.0)
            {
              NPC npc = this.NPC;
              ((Entity) npc).velocity = Vector2.op_Addition(((Entity) npc).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2), 0.02f));
              ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2), ((Vector2) ref ((Entity) this.NPC).velocity).Length()), 1.05f), 0.3f);
            }
            else
              ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2), (float) (20.0 + (double) ((Vector2) ref ((Entity) this.player).velocity).Length() / 2.0)), 0.3f);
          }
          else
            ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Division(Vector2.op_Subtraction(vector2, ((Entity) this.NPC).Center), 10f), 0.3f);
        }
        this.RotateTowards(((Entity) this.player).Center, 2f);
        if ((double) this.Timer > 420.0)
        {
          projectile.ai[2] = 0.0f;
          projectile.netUpdate = true;
        }
        if ((double) this.Timer <= (WorldSavingSystem.MasochistModeReal ? 560.0 : 500.0))
          return;
        ((Entity) this.NPC).velocity = Vector2.Zero;
        this.StateReset();
      }
    }

    private void P2LaserSweep()
    {
      int num1 = 120;
      if ((double) this.Timer == 1.0)
      {
        Projectile projectile = Main.projectile[this.ArenaProjID];
        if ((projectile == null || !((Entity) projectile).active ? 0 : (projectile.type == ModContent.ProjectileType<BaronArenaWhirlpool>() ? 1 : 0)) == 0)
        {
          this.LockVector1 = new Vector2((float) (500 * Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X)), -350f);
        }
        else
        {
          this.LockVector1 = new Vector2((float) ((int) Math.Min(Math.Abs(((Entity) projectile).Center.X - ((Entity) this.player).Center.X), 500f) * Math.Sign(((Entity) projectile).Center.X - ((Entity) this.player).Center.X)), -350f);
          this.HitPlayer = false;
        }
      }
      if ((double) this.Timer < 30.0 && (double) ((Entity) this.NPC).Distance(Vector2.op_Addition(((Entity) this.player).Center, this.LockVector1)) > 15.0)
        --this.Timer;
      else if ((double) this.Timer < 30.0)
      {
        this.Timer = 30f;
        this.HitPlayer = true;
        if (WorldSavingSystem.EternityMode)
        {
          SoundEngine.PlaySound(ref SoundID.Item61, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation), 10f);
            int num2 = Main.rand.Next(160, 160);
            this.NPC.netUpdate = true;
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(vector2), ModContent.ProjectileType<BaronNuke>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) num2, (float) ((Entity) this.player).whoAmI, 1f);
          }
        }
      }
      if ((double) this.Timer < 30.0)
      {
        Vector2 vector2 = Vector2.op_Addition(((Entity) this.player).Center, this.LockVector1);
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() < 20.0 + (double) ((Vector2) ref ((Entity) this.player).velocity).Length() / 2.0)
        {
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Addition(((Entity) npc).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2), 0.02f));
          ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2), ((Vector2) ref ((Entity) this.NPC).velocity).Length()), 1.05f), 0.75f);
        }
        else
          ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2), (float) (20.0 + (double) ((Vector2) ref ((Entity) this.player).velocity).Length() / 2.0)), 0.75f);
        this.RotateTowards(((Entity) this.player).Center, 2f);
      }
      else if ((double) this.Timer < 120.0)
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.8f);
        if ((double) this.Timer == 30.0)
        {
          SoundStyle soundStyle;
          // ISSUE: explicit constructor call
          ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/LaserTelegraph", (SoundType) 0);
          ((SoundStyle) ref soundStyle).Volume = 1.25f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          this.AI3 = Utils.NextBool(Main.rand) ? 1f : -1f;
          this.LockVector1 = Utils.RotatedBy(Vector2.op_Subtraction(((Entity) this.player).Center, ((Entity) this.NPC).Center), (double) this.AI3 * 0.62831854820251465, new Vector2());
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<BloomLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 7f, (float) ((Entity) this.NPC).whoAmI, 110f);
          this.NPC.netUpdate = true;
        }
        this.RotateTowards(Vector2.op_Addition(((Entity) this.NPC).Center, this.LockVector1), 1.75f);
      }
      else if ((double) this.Timer < (double) (120 + num1))
      {
        float num3 = WorldSavingSystem.EternityMode ? 1.1f : 1f;
        if ((double) this.Timer == 120.0)
        {
          this.AI4 = FargoSoulsUtil.RotationDifference(Utils.ToRotationVector2(this.NPC.rotation), Vector2.op_Subtraction(((Entity) this.player).Center, ((Entity) this.NPC).Center));
          SoundStyle soundStyle;
          // ISSUE: explicit constructor call
          ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/LaserSound_Slow", (SoundType) 0);
          ((SoundStyle) ref soundStyle).Pitch = -0.2f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.ToRotationVector2(this.NPC.rotation), ModContent.ProjectileType<BaronDeathray>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, (float) num1);
          this.NPC.netUpdate = true;
        }
        this.NPC.rotation += (float) ((double) Math.Sign(this.AI4) * (double) num3 * 3.1415927410125732 / 180.0);
      }
      else if (WorldSavingSystem.EternityMode && (double) this.Timer > (double) (120 + num1))
      {
        this.StateReset();
      }
      else
      {
        if ((double) this.Timer <= (double) (120 + num1 + 10))
          return;
        ((Entity) this.NPC).velocity = Vector2.Zero;
        this.StateReset();
      }
    }

    private void StateReset(bool randomizeState = true)
    {
      this.NPC.TargetClosest(false);
      if (WorldSavingSystem.EternityMode && (double) this.State == 14.0)
      {
        this.State = 8f;
        this.availablestates.Remove(14);
        this.availablestates.Remove(8);
      }
      else if ((double) this.State == 15.0 || this.Phase == 2)
      {
        if (randomizeState)
          this.RandomizeState();
      }
      else
        this.State = 15f;
      if ((((double) this.NPC.GetLifePercent() >= 0.66666668653488159 || this.Phase != 1 ? 0 : (Main.expertMode ? 1 : 0)) | ((double) this.NPC.GetLifePercent() >= 0.5 || this.Phase != 1 ? (false ? 1 : 0) : (!Main.expertMode ? 1 : 0))) != 0)
        this.State = 1f;
      this.Timer = 0.0f;
      this.AI2 = 0.0f;
      this.AI3 = 0.0f;
    }

    private void RandomizeState()
    {
      if (this.availablestates.Count < 1)
      {
        this.availablestates.Clear();
        if (this.Phase == 1)
          this.availablestates.AddRange((IEnumerable<int>) FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron.P1Attacks);
        else if (this.Phase == 2)
          this.availablestates.AddRange((IEnumerable<int>) FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron.P2Attacks);
        this.availablestates.Remove(this.LastState);
      }
      if (FargoSoulsUtil.HostCheck)
      {
        int index = Main.rand.Next(this.availablestates.Count);
        this.State = (float) this.availablestates[index];
        this.availablestates.RemoveAt(index);
      }
      this.LastState = (int) this.State;
      this.NPC.netUpdate = true;
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
      if ((double) num <= 0.0 && (double) num >= 0.0)
        return;
      this.NPC.rotation = Utils.ToRotation(Utils.RotatedBy(Utils.ToRotationVector2(this.NPC.rotation), (double) Math.Sign(num) * (double) Math.Min(Math.Abs(num), (float) ((double) speed * 3.1415927410125732 / 180.0)), new Vector2()));
    }

    static BanishedBaron()
    {
      List<int> intList1 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList1, 6);
      Span<int> span1 = CollectionsMarshal.AsSpan<int>(intList1);
      int num1 = 0;
      span1[num1] = 2;
      int num2 = num1 + 1;
      span1[num2] = 3;
      int num3 = num2 + 1;
      span1[num3] = 4;
      int num4 = num3 + 1;
      span1[num4] = 5;
      int num5 = num4 + 1;
      span1[num5] = 6;
      int num6 = num5 + 1;
      span1[num6] = 7;
      int num7 = num6 + 1;
      FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron.P1Attacks = intList1;
      List<int> intList2 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList2, 7);
      Span<int> span2 = CollectionsMarshal.AsSpan<int>(intList2);
      int num8 = 0;
      span2[num8] = 8;
      int num9 = num8 + 1;
      span2[num9] = 9;
      int num10 = num9 + 1;
      span2[num10] = 10;
      int num11 = num10 + 1;
      span2[num11] = 11;
      int num12 = num11 + 1;
      span2[num12] = 12;
      int num13 = num12 + 1;
      span2[num13] = 13;
      int num14 = num13 + 1;
      span2[num14] = 14;
      num7 = num14 + 1;
      FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron.P2Attacks = intList2;
    }

    public enum StateEnum
    {
      Opening,
      Phase2Transition,
      P1BigNuke,
      P1RocketStorm,
      P1SurfaceMines,
      P1FadeDash,
      P1SineSwim,
      P1Whirlpool,
      P2PredictiveDash,
      P2CarpetBomb,
      P2RocketStorm,
      P2SpinDash,
      P2MineFlurry,
      P2Whirlpool,
      P2LaserSweep,
      Swim,
      DeathAnimation,
    }
  }
}
