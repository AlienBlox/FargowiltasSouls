// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Earth.EarthChampion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Placables.Relics;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ItemDropRules;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Earth
{
  [AutoloadBossHead]
  public class EarthChampion : ModNPC
  {
    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 2;
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 6;
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
      span[num3] = 24;
      int num4 = num3 + 1;
      span[num4] = 68;
      int num5 = num4 + 1;
      span[num5] = ModContent.BuffType<LethargicBuff>();
      int num6 = num5 + 1;
      span[num6] = ModContent.BuffType<ClippedWingsBuff>();
      int num7 = num6 + 1;
      npc.AddDebuffImmunities(debuffs);
      Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> bestiaryDrawOffset = NPCID.Sets.NPCBestiaryDrawOffset;
      int type = this.NPC.type;
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers1;
      // ISSUE: explicit constructor call
      ((NPCID.Sets.NPCBestiaryDrawModifiers) ref bestiaryDrawModifiers1).\u002Ector();
      bestiaryDrawModifiers1.CustomTexturePath = "FargowiltasSouls/Content/Bosses/Champions/Earth/" + ((ModType) this).Name + "_Still";
      bestiaryDrawModifiers1.Scale = 0.75f;
      bestiaryDrawModifiers1.Position = new Vector2(0.0f, 10f);
      bestiaryDrawModifiers1.PortraitScale = new float?(0.5f);
      bestiaryDrawModifiers1.PortraitPositionXOverride = new float?(0.0f);
      bestiaryDrawModifiers1.PortraitPositionYOverride = new float?(0.0f);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      bestiaryDrawOffset.Add(type, bestiaryDrawModifiers2);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual Color? GetAlpha(Color drawColor)
    {
      return this.NPC.IsABestiaryIconDummy ? new Color?(this.NPC.GetBestiaryEntryColor()) : base.GetAlpha(drawColor);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 120;
      ((Entity) this.NPC).height = 180;
      this.NPC.damage = 130;
      this.NPC.defense = 80;
      this.NPC.lifeMax = 380000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit41);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath44);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      this.NPC.value = (float) Item.buyPrice(1, 0, 0, 0);
      this.NPC.boss = true;
      this.NPC.trapImmune = true;
      Mod mod;
      this.Music = Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod) ? MusicLoader.GetMusicSlot(mod, "Assets/Music/Champions") : 81;
      this.SceneEffectPriority = (SceneEffectPriority) 6;
      this.NPC.dontTakeDamage = true;
      this.NPC.alpha = (int) byte.MaxValue;
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * (double) balance);
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot) => false;

    public virtual void AI()
    {
      EModeGlobalNPC.championBoss = ((Entity) this.NPC).whoAmI;
      if ((double) this.NPC.localAI[3] == 0.0)
      {
        if (!this.NPC.HasValidTarget)
          this.NPC.TargetClosest(false);
        if ((double) this.NPC.ai[1] == 0.0)
        {
          ((Entity) this.NPC).Center = Vector2.op_Addition(((Entity) Main.player[this.NPC.target]).Center, new Vector2((float) (500 * Math.Sign(((Entity) this.NPC).Center.X - ((Entity) Main.player[this.NPC.target]).Center.X)), -250f));
          if (FargoSoulsUtil.HostCheck)
          {
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitY, 1000f)), Vector2.Zero, ModContent.ProjectileType<EarthChainBlast2>(), 0, 0.0f, Main.myPlayer, -Utils.ToRotation(Vector2.UnitY), 10f, 0.0f);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Subtraction(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitY, 1000f)), Vector2.Zero, ModContent.ProjectileType<EarthChainBlast2>(), 0, 0.0f, Main.myPlayer, Utils.ToRotation(Vector2.UnitY), 10f, 0.0f);
          }
        }
        if ((double) ++this.NPC.ai[1] <= 54.0)
          return;
        this.NPC.localAI[3] = 1f;
        this.NPC.ai[1] = 0.0f;
        this.NPC.netUpdate = true;
        if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
          ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
        if (!FargoSoulsUtil.HostCheck)
          return;
        for (int index = 0; index < 8; ++index)
        {
          float num = (float) (0.78539818525314331 * ((double) index + (double) Utils.NextFloat(Main.rand, -0.5f, 0.5f)));
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<EarthChainBlast2>(), 0, 0.0f, Main.myPlayer, num, 3f, 0.0f);
        }
        FargoSoulsUtil.NewNPCEasy(((Entity) this.NPC).GetSource_FromAI((string) null), ((Entity) this.NPC).Center, ModContent.NPCType<EarthChampionHand>(), ((Entity) this.NPC).whoAmI, ai2: (float) ((Entity) this.NPC).whoAmI, ai3: 1f, velocity: new Vector2());
        FargoSoulsUtil.NewNPCEasy(((Entity) this.NPC).GetSource_FromAI((string) null), ((Entity) this.NPC).Center, ModContent.NPCType<EarthChampionHand>(), ((Entity) this.NPC).whoAmI, ai2: (float) ((Entity) this.NPC).whoAmI, ai3: -1f, velocity: new Vector2());
      }
      else
      {
        Player player = Main.player[this.NPC.target];
        if (this.NPC.HasValidTarget && (double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 2500.0 && player.ZoneUnderworldHeight)
          this.NPC.timeLeft = 600;
        this.NPC.dontTakeDamage = false;
        this.NPC.alpha = 0;
        switch (this.NPC.ai[0])
        {
          case -1f:
            this.NPC.localAI[2] = 1f;
            ++this.NPC.ai[1];
            NPC npc1 = this.NPC;
            ((Entity) npc1).velocity = Vector2.op_Multiply(((Entity) npc1).velocity, 0.95f);
            if ((double) this.NPC.ai[1] == 120.0)
            {
              SoundEngine.PlaySound(ref SoundID.NPCDeath10, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if (!FargoSoulsUtil.HostCheck)
                break;
              if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
                ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
              if (!FargoSoulsUtil.HostCheck)
                break;
              float num1 = 0.7853982f * Utils.NextFloat(Main.rand);
              for (int index = 0; index < 8; ++index)
              {
                float num2 = num1 + (float) (0.78539818525314331 * ((double) index + (double) Utils.NextFloat(Main.rand, -0.5f, 0.5f)));
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<EarthChainBlast2>(), 0, 0.0f, Main.myPlayer, num2, 3f, 0.0f);
              }
              break;
            }
            if ((double) this.NPC.ai[1] <= 120.0)
              break;
            NPC npc2 = this.NPC;
            ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, 0.9f);
            int num3 = (int) ((double) (this.NPC.lifeMax / 3 / 120) * (double) Utils.NextFloat(Main.rand, 1f, 1.5f));
            this.NPC.life += num3;
            int num4 = this.NPC.lifeMax / (WorldSavingSystem.MasochistModeReal ? 1 : 2);
            if (this.NPC.life > num4)
              this.NPC.life = num4;
            CombatText.NewText(((Entity) this.NPC).Hitbox, CombatText.HealLife, num3, false, false);
            for (int index1 = 0; index1 < 5; ++index1)
            {
              int index2 = Dust.NewDust(((Entity) this.NPC).Center, 0, 0, 174, 0.0f, 0.0f, 0, new Color(), 1.5f);
              Main.dust[index2].noGravity = true;
              Dust dust = Main.dust[index2];
              dust.velocity = Vector2.op_Multiply(dust.velocity, 8f);
            }
            if ((double) this.NPC.ai[1] <= 240.0)
              break;
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.netUpdate = true;
            break;
          case 0.0f:
            if (!((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 2500.0 || !player.ZoneUnderworldHeight)
            {
              this.NPC.TargetClosest(false);
              if (this.NPC.timeLeft > 30)
                this.NPC.timeLeft = 30;
              this.NPC.noTileCollide = true;
              this.NPC.noGravity = true;
              ++((Entity) this.NPC).velocity.Y;
              break;
            }
            Vector2 center1 = ((Entity) player).Center;
            center1.Y -= 325f;
            if ((double) ((Entity) this.NPC).Distance(center1) > 50.0)
              this.Movement(center1, 0.4f, 16f, true);
            if ((double) this.NPC.localAI[2] != 0.0 || this.NPC.life >= this.NPC.lifeMax / 3)
              break;
            this.NPC.ai[0] = -1f;
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            for (int index = 0; index < Main.maxNPCs; ++index)
            {
              if (((Entity) Main.npc[index]).active && Main.npc[index].type == ModContent.NPCType<EarthChampionHand>() && (double) Main.npc[index].ai[2] == (double) ((Entity) this.NPC).whoAmI)
              {
                Main.npc[index].ai[0] = -1f;
                Main.npc[index].ai[1] = 0.0f;
                Main.npc[index].localAI[0] = 0.0f;
                Main.npc[index].localAI[1] = 0.0f;
                Main.npc[index].netUpdate = true;
              }
            }
            break;
          case 1f:
            if (!((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 2500.0 || !player.ZoneUnderworldHeight)
            {
              this.NPC.TargetClosest(false);
              if (this.NPC.timeLeft > 30)
                this.NPC.timeLeft = 30;
              this.NPC.noTileCollide = true;
              this.NPC.noGravity = true;
              ++((Entity) this.NPC).velocity.Y;
              break;
            }
            Vector2 center2 = ((Entity) player).Center;
            for (int index = 0; index < 22; ++index)
            {
              center2.Y -= 16f;
              Tile tileSafely = Framing.GetTileSafely(center2);
              if (((Tile) ref tileSafely).HasTile && !((Tile) ref tileSafely).IsActuated && Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] && !Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType])
              {
                center2.Y += 66f;
                break;
              }
            }
            if ((double) ((Entity) this.NPC).Distance(center2) > 50.0)
            {
              this.Movement(center2, 0.2f, fastY: true);
              NPC npc3 = this.NPC;
              ((Entity) npc3).position = Vector2.op_Addition(((Entity) npc3).position, Vector2.op_Division(Vector2.op_Subtraction(center2, ((Entity) this.NPC).Center), 30f));
            }
            if ((double) --this.NPC.ai[2] < 0.0)
            {
              this.NPC.ai[2] = 75f;
              SoundEngine.PlaySound(ref SoundID.NPCDeath13, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if ((double) this.NPC.ai[1] > 10.0 && FargoSoulsUtil.HostCheck)
              {
                for (int index = -1; index <= 1; ++index)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitY, 60f)), Vector2.op_Multiply((double) this.NPC.localAI[2] == 1.0 ? 12f : 8f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), (double) MathHelper.ToRadians((float) (8 * index)), new Vector2())), 258, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
            if ((double) ++this.NPC.ai[1] > 480.0)
            {
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.netUpdate = true;
            }
            if ((double) this.NPC.localAI[2] != 0.0 || this.NPC.life >= this.NPC.lifeMax / 3)
              break;
            this.NPC.ai[0] = -1f;
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            for (int index = 0; index < Main.maxNPCs; ++index)
            {
              if (((Entity) Main.npc[index]).active && Main.npc[index].type == ModContent.NPCType<EarthChampionHand>() && (double) Main.npc[index].ai[2] == (double) ((Entity) this.NPC).whoAmI)
              {
                Main.npc[index].ai[0] = -1f;
                Main.npc[index].ai[1] = 0.0f;
                Main.npc[index].localAI[0] = 0.0f;
                Main.npc[index].localAI[1] = 0.0f;
                Main.npc[index].netUpdate = true;
              }
            }
            break;
          default:
            this.NPC.ai[0] = 0.0f;
            goto case 0.0f;
        }
      }
    }

    private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f, bool fastY = false)
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
        ((Entity) this.NPC).velocity.Y += fastY ? speedModifier * 2f : speedModifier;
        if ((double) ((Entity) this.NPC).velocity.Y < 0.0)
          ((Entity) this.NPC).velocity.Y += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.NPC).velocity.Y -= fastY ? speedModifier * 2f : speedModifier;
        if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
          ((Entity) this.NPC).velocity.Y -= speedModifier * 2f;
      }
      if ((double) Math.Abs(((Entity) this.NPC).velocity.X) > (double) cap)
        ((Entity) this.NPC).velocity.X = cap * (float) Math.Sign(((Entity) this.NPC).velocity.X);
      if ((double) Math.Abs(((Entity) this.NPC).velocity.Y) <= (double) cap)
        return;
      ((Entity) this.NPC).velocity.Y = cap * (float) Math.Sign(((Entity) this.NPC).velocity.Y);
    }

    public virtual void FindFrame(int frameHeight)
    {
      this.NPC.frame.Y = 0;
      switch ((int) this.NPC.ai[0])
      {
        case -1:
          if ((double) this.NPC.ai[1] <= 120.0)
            break;
          this.NPC.frame.Y = frameHeight;
          break;
        case 1:
          if ((double) this.NPC.ai[2] >= 20.0)
            break;
          this.NPC.frame.Y = frameHeight;
          break;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      target.AddBuff(24, 300, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(67, 300, true, false);
      target.AddBuff(ModContent.BuffType<LethargicBuff>(), 300, true, false);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      for (int index = 1; index <= 4; ++index)
      {
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).width), Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).height)));
        if (!Main.dedServ)
        {
          IEntitySource sourceFromThis = ((Entity) this.NPC).GetSource_FromThis((string) null);
          Vector2 vector2_2 = vector2_1;
          Vector2 velocity = ((Entity) this.NPC).velocity;
          string name = ((ModType) this).Mod.Name;
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
          interpolatedStringHandler.AppendLiteral("EarthGore");
          interpolatedStringHandler.AppendFormatted<int>(index);
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          int type = ModContent.Find<ModGore>(name, stringAndClear).Type;
          double scale = (double) this.NPC.scale;
          Gore.NewGore(sourceFromThis, vector2_2, velocity, type, (float) scale);
        }
      }
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 3544;

    public virtual void OnKill()
    {
      NPC.SetEventFlagCleared(ref WorldSavingSystem.DownedBoss[2], -1);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) new ChampionEnchDropRule(BaseForce.EnchantsIn<EarthForce>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<EarthChampionRelic>()));
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color alpha = this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects = this.NPC.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < NPCID.Sets.TrailCacheLength[this.NPC.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (NPCID.Sets.TrailCacheLength[this.NPC.type] - index) / (float) NPCID.Sets.TrailCacheLength[this.NPC.type]);
        Vector2 oldPo = this.NPC.oldPos[index];
        float rotation = this.NPC.rotation;
        Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), color, rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      }
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Earth/" + ((ModType) this).Name + "_Glow", (AssetRequestMode) 1).Value;
      if (this.NPC.dontTakeDamage)
      {
        Vector2 vector2_2 = Vector2.op_Multiply(Vector2.UnitX, Utils.NextFloat(Main.rand, -180f, 180f));
        Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.NPC).Center, vector2_2), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.op_Multiply(this.NPC.GetAlpha(drawColor), 0.5f), this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
        Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.NPC).Center, vector2_2), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.op_Multiply(this.NPC.GetAlpha(drawColor), 0.5f), this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.op_Multiply(Color.White, this.NPC.Opacity), this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
