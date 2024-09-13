// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.RoyalSubject
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs
{
  public class RoyalSubject : ModNPC
  {
    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 7;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      NPCID.Sets.SpecificDebuffImmunity[this.Type] = NPCID.Sets.SpecificDebuffImmunity[222];
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.UIInfoProvider = (IBestiaryUICollectionInfoProvider) new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[222], true);
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary.RoyalSubject")
      }));
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 40;
      ((Entity) this.NPC).height = 40;
      this.NPC.aiStyle = 43;
      this.AIType = 222;
      this.NPC.damage = 25;
      this.NPC.defense = 8;
      this.NPC.lifeMax = 600;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit1);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
      this.NPC.knockBackResist = 0.0f;
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.timeLeft = NPC.activeTime * 30;
      this.NPC.npcSlots = 7f;
      this.NPC.scale = 1f;
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * 0.7 * Math.Max(1.0, (double) balance / 2.0));
      this.NPC.damage = (int) ((double) this.NPC.damage * 0.9);
    }

    public virtual void AI()
    {
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.beeBoss, 222) && !NPC.AnyNPCs(222))
      {
        this.NPC.life = 0;
        this.NPC.HitEffect(0, 10.0, new bool?());
        this.NPC.checkDead();
      }
      else
      {
        if ((double) this.NPC.ai[0] != 0.0)
        {
          this.NPC.ai[0] = 0.0f;
          this.NPC.netUpdate = true;
        }
        if ((double) this.NPC.ai[1] != 2.0 && (double) this.NPC.ai[1] != 3.0)
        {
          this.NPC.ai[1] = 2f;
          this.NPC.netUpdate = true;
        }
        NPC npc = this.NPC;
        ((Entity) npc).position = Vector2.op_Subtraction(((Entity) npc).position, Vector2.op_Division(((Entity) this.NPC).velocity, 3f));
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      target.AddBuff(20, Main.rand.Next(60, 180), true, false);
      target.AddBuff(ModContent.BuffType<InfestedBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<SwarmingBuff>(), 600, true, false);
    }

    public virtual bool CheckDead()
    {
      NPC npc = FargoSoulsUtil.NPCExists(EModeGlobalNPC.beeBoss, new int[1]
      {
        222
      });
      if (npc != null && FargoSoulsUtil.HostCheck && npc.GetGlobalNPC<QueenBee>().BeeSwarmTimer < 600)
      {
        npc.ai[0] = 0.0f;
        npc.ai[1] = 4f;
        npc.ai[2] = -44f;
        npc.ai[3] = 0.0f;
        npc.netUpdate = true;
      }
      if (this.NPC.DeathSound.HasValue)
      {
        SoundStyle soundStyle = this.NPC.DeathSound.Value;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      }
      ((Entity) this.NPC).active = false;
      return false;
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 5, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
        Main.dust[index2].scale += 0.75f;
      }
      for (int index = 303; index <= 308; ++index)
      {
        if (!Main.dedServ && !Main.dedServ)
          Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width), (float) Main.rand.Next(((Entity) this.NPC).height))), Vector2.op_Division(((Entity) this.NPC).velocity, 2f), index, this.NPC.scale);
      }
    }

    public virtual void FindFrame(int frameHeight)
    {
      ++this.NPC.frameCounter;
      if ((double) this.NPC.localAI[0] == 1.0)
      {
        if (this.NPC.frameCounter > 4.0)
        {
          this.NPC.frame.Y += frameHeight;
          this.NPC.frameCounter = 0.0;
        }
        if (this.NPC.frame.Y < 3 * frameHeight)
          return;
        this.NPC.frame.Y = 0;
      }
      else
      {
        if (this.NPC.frameCounter > 4.0)
        {
          this.NPC.frame.Y += frameHeight;
          this.NPC.frameCounter = 0.0;
        }
        if (this.NPC.frame.Y < 3 * frameHeight)
          this.NPC.frame.Y = 3 * frameHeight;
        if (this.NPC.frame.Y < Main.npcFrameCount[this.Type] * frameHeight)
          return;
        this.NPC.frame.Y = 3 * frameHeight;
      }
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D = (!SoulConfig.Instance.BossRecolors ? 0 : (WorldSavingSystem.EternityMode ? 1 : 0)) != 0 ? ModContent.Request<Texture2D>(this.Texture + "22", (AssetRequestMode) 2).Value : TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
      SpriteEffects spriteEffects1 = this.NPC.spriteDirection < 1 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Color alpha = this.NPC.GetAlpha(drawColor);
      Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY));
      Rectangle? nullable = new Rectangle?(frame);
      Color color = alpha;
      double rotation = (double) this.NPC.rotation;
      Vector2 vector2_3 = vector2_1;
      double scale = (double) this.NPC.scale;
      SpriteEffects spriteEffects2 = spriteEffects1;
      Main.EntitySpriteDraw(texture2D, vector2_2, nullable, color, (float) rotation, vector2_3, (float) scale, spriteEffects2, 0.0f);
      return false;
    }
  }
}
