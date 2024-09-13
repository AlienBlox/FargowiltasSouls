// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Shadow.ShadowOrbNPC
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Shadow
{
  public class ShadowOrbNPC : ModNPC
  {
    public virtual void SetStaticDefaults()
    {
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      NPCID.Sets.ImmuneToAllBuffs[this.Type] = true;
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.UIInfoProvider = (IBestiaryUICollectionInfoProvider) new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<ShadowChampion>()], true);
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[3]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 32;
      ((Entity) this.NPC).height = 32;
      this.NPC.defense = 9999;
      this.NPC.lifeMax = 9999;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit1);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      this.NPC.chaseable = false;
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.lifeMax = 9999;
    }

    public virtual void SendExtraAI(BinaryWriter writer) => writer.Write(this.NPC.localAI[3]);

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      float num = reader.ReadSingle();
      if ((double) this.NPC.localAI[3] == 1.0)
        return;
      this.NPC.localAI[3] = num;
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot) => false;

    public virtual void AI()
    {
      if (this.NPC.buffType[0] != 0)
        this.NPC.DelBuff(0);
      NPC npc = FargoSoulsUtil.NPCExists(this.NPC.ai[0], ModContent.NPCType<ShadowChampion>());
      if (npc == null)
      {
        ((Entity) this.NPC).active = false;
        this.NPC.netUpdate = true;
      }
      else
      {
        this.NPC.netAlways = true;
        this.NPC.scale = (float) (((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.20000000298023224 + 0.949999988079071);
        this.NPC.life = this.NPC.lifeMax;
        this.NPC.damage = 0;
        this.NPC.defDamage = 0;
        ((Entity) this.NPC).position = Vector2.op_Addition(((Entity) npc).Center, Utils.RotatedBy(new Vector2(this.NPC.ai[1], 0.0f), (double) this.NPC.ai[3], new Vector2()));
        ((Entity) this.NPC).position.X -= (float) (((Entity) this.NPC).width / 2);
        ((Entity) this.NPC).position.Y -= (float) (((Entity) this.NPC).height / 2);
        float num = 0.07f;
        if ((double) this.NPC.ai[1] != 110.0)
          num = 0.03f;
        this.NPC.ai[3] += num;
        if ((double) this.NPC.ai[3] > 3.1415927410125732)
        {
          this.NPC.ai[3] -= 6.28318548f;
          this.NPC.netUpdate = true;
        }
        this.NPC.rotation = this.NPC.ai[3] + 1.57079637f;
        if ((double) this.NPC.ai[1] != 110.0 && (double) this.NPC.ai[1] != 700.0)
        {
          this.NPC.ai[2] += 0.09106066f;
          if ((double) this.NPC.ai[2] > 3.1415927410125732)
            this.NPC.ai[2] -= 6.28318548f;
          this.NPC.ai[1] += (float) Math.Sin((double) this.NPC.ai[2]) * 30f;
        }
        this.NPC.alpha = (double) this.NPC.localAI[3] == 1.0 ? 150 : 0;
        if ((double) this.NPC.ai[1] == 110.0 && (double) npc.life < (double) npc.lifeMax * 0.66 || (double) this.NPC.ai[1] == 700.0 && (double) npc.life < (double) npc.lifeMax * 0.33)
          ((Entity) this.NPC).active = false;
        this.NPC.dontTakeDamage = (double) npc.ai[0] == -1.0;
        if ((double) this.NPC.localAI[3] != 1.0)
          return;
        this.NPC.dontTakeDamage = true;
        this.NPC.netUpdate = true;
      }
    }

    public virtual void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
    {
      this.NPC.dontTakeDamage = true;
      this.NPC.localAI[3] = 1f;
      this.NPC.netUpdate = true;
      modifiers.Null();
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 36; ++index1)
      {
        Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitX, 10f), (double) (index1 - 17) * 6.2831854820251465 / 36.0, new Vector2()), ((Entity) this.NPC).Center);
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.NPC).Center);
        int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 27, 0.0f, 0.0f, 0, new Color(), 3f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity = vector2_2;
      }
    }

    public virtual void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
    {
      modifiers.Null();
      ++this.NPC.life;
      this.NPC.netUpdate = true;
    }

    public virtual void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
    {
      if (FargoSoulsUtil.CanDeleteProjectile(projectile))
      {
        projectile.penetrate = 0;
        projectile.timeLeft = 0;
      }
      modifiers.Null();
      ++this.NPC.life;
      this.NPC.netUpdate = true;
    }

    public virtual bool CheckActive() => false;

    public virtual bool PreKill() => false;

    public virtual bool? DrawHealthBar(byte hbPos, ref float scale, ref Vector2 Pos)
    {
      return new bool?(false);
    }

    public virtual Color? GetAlpha(Color drawColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.NPC.Opacity));
    }
  }
}
