// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrelPart
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.TrojanSquirrel
{
  public abstract class TrojanSquirrelPart : ModNPC
  {
    protected int baseWidth;
    protected int baseHeight;

    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      Main.npcFrameCount[this.NPC.type] = 8;
      NPCID.Sets.MPAllowedEnemies[this.Type] = true;
      NPCID.Sets.TrailCacheLength[this.Type] = 8;
      NPCID.Sets.TrailingMode[this.Type] = 3;
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 3);
      Span<int> span = CollectionsMarshal.AsSpan<int>(debuffs);
      int num1 = 0;
      span[num1] = 31;
      int num2 = num1 + 1;
      span[num2] = ModContent.BuffType<LethargicBuff>();
      int num3 = num2 + 1;
      span[num3] = ModContent.BuffType<ClippedWingsBuff>();
      int num4 = num3 + 1;
      npc.AddDebuffImmunities(debuffs);
    }

    public virtual void SetDefaults()
    {
      base.SetDefaults();
      this.NPC.damage = 24;
      this.NPC.defense = 2;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit7);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      if (!Main.getGoodWorld)
        return;
      ++this.NPC.scale;
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * (double) balance);
    }

    public virtual void ModifyHoverBoundingBox(ref Rectangle boundingBox)
    {
      boundingBox = ((Entity) this.NPC).Hitbox;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      base.SendExtraAI(writer);
      writer.Write(this.NPC.scale);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      base.ReceiveExtraAI(reader);
      this.NPC.scale = reader.ReadSingle();
    }

    public virtual void PostAI()
    {
      base.PostAI();
      if (this is FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel)
        ((Entity) this.NPC).position = ((Entity) this.NPC).Bottom;
      ((Entity) this.NPC).width = (int) ((double) this.baseWidth * (double) this.NPC.scale);
      ((Entity) this.NPC).height = (int) ((double) this.baseHeight * (double) this.NPC.scale);
      if (!(this is FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel))
        return;
      ((Entity) this.NPC).Bottom = ((Entity) this.NPC).position;
    }
  }
}
