// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.Critters.TophatSquirrelCritter
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Misc;
using FargowiltasSouls.Content.Items.Placables;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.Critters
{
  public class TophatSquirrelCritter : ModNPC
  {
    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 6;
      NPCID.Sets.TownCritter[this.NPC.type] = true;
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 50;
      ((Entity) this.NPC).height = 32;
      this.NPC.damage = 0;
      this.NPC.chaseable = false;
      this.NPC.defense = 0;
      this.NPC.lifeMax = 100;
      Main.npcCatchable[this.NPC.type] = true;
      this.NPC.catchItem = (int) (short) ModContent.ItemType<TopHatSquirrelCaught>();
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit1);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
      this.NPC.value = 0.0f;
      this.NPC.knockBackResist = 0.25f;
      this.Banner = this.NPC.type;
      this.BannerItem = ModContent.ItemType<TophatSquirrelBanner>();
      this.AnimationType = 299;
      this.NPC.aiStyle = 7;
      this.AIType = 299;
      this.NPC.friendly = true;
      this.NPC.rarity = 1;
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      for (int index = 0; index < 20; ++index)
        Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 5, (float) hit.HitDirection, -1f, 0, new Color(), 1f);
    }
  }
}
