// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Life.LesserFairy
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Life
{
  public class LesserFairy : ModNPC
  {
    public int counter;

    public virtual string Texture => "Terraria/Images/NPC_75";

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = Main.npcFrameCount[75];
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.UIInfoProvider = (IBestiaryUICollectionInfoProvider) new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<LifeChampion>()], true);
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 20;
      ((Entity) this.NPC).height = 20;
      this.NPC.damage = 180;
      this.NPC.defense = 0;
      this.NPC.lifeMax = 1;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit5);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath7);
      this.NPC.value = 0.0f;
      this.NPC.knockBackResist = 0.0f;
      this.AnimationType = 75;
      this.NPC.aiStyle = -1;
      this.NPC.dontTakeDamage = true;
      this.NPC.noTileCollide = true;
      this.NPC.noGravity = true;
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot)
    {
      CooldownSlot = 1;
      return true;
    }

    public virtual void AI()
    {
      if (Utils.NextBool(Main.rand, 6))
      {
        int index = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 87, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index].noGravity = true;
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
      }
      if (Utils.NextBool(Main.rand, 40))
        SoundEngine.PlaySound(ref SoundID.Pixie, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).velocity.X < 0.0 ? -1 : 1;
      this.NPC.rotation = ((Entity) this.NPC).velocity.X * 0.1f;
      if (++this.counter > 60 && this.counter < 240)
      {
        if (!this.NPC.HasValidTarget)
          this.NPC.TargetClosest(true);
        if ((double) ((Entity) this.NPC).Distance(((Entity) Main.player[this.NPC.target]).Center) >= 300.0)
          return;
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) Main.player[this.NPC.target]).Center), ((Vector2) ref ((Entity) this.NPC).velocity).Length());
      }
      else
      {
        if (this.counter <= 300)
          return;
        this.NPC.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<PurifiedBuff>(), 300, true, false);
    }

    public virtual Color? GetAlpha(Color drawColor) => new Color?(Color.White);

    public virtual void FindFrame(int frameHeight)
    {
      if (++this.NPC.frameCounter >= 4.0)
      {
        this.NPC.frame.Y += frameHeight;
        this.NPC.frameCounter = 0.0;
      }
      if (this.NPC.frame.Y < frameHeight * Main.npcFrameCount[this.NPC.type])
        return;
      this.NPC.frame.Y = 0;
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 87, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      }
    }
  }
}
