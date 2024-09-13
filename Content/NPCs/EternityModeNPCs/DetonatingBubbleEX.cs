﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.DetonatingBubbleEX
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs
{
  public class DetonatingBubbleEX : ModNPC
  {
    public virtual string Texture => "Terraria/Images/NPC_371";

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 2;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      NPCID.Sets.ImmuneToRegularBuffs[this.Type] = true;
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 36;
      ((Entity) this.NPC).height = 36;
      this.NPC.damage = 100;
      this.NPC.lifeMax = 5000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit3);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath3);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.alpha = (int) byte.MaxValue;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      this.NPC.chaseable = false;
    }

    public virtual void AI()
    {
      if (this.NPC.buffTime[0] != 0)
      {
        this.NPC.buffImmune[this.NPC.buffType[0]] = true;
        this.NPC.DelBuff(0);
      }
      if (this.NPC.alpha > 50)
        this.NPC.alpha -= 30;
      else
        this.NPC.alpha = 50;
      NPC npc = this.NPC;
      ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 1.04f);
      ++this.NPC.ai[0];
      if ((double) this.NPC.ai[0] < 120.0)
        return;
      this.NPC.life = 0;
      this.NPC.checkDead();
      ((Entity) this.NPC).active = false;
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot)
    {
      CooldownSlot = 1;
      return true;
    }

    public virtual void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
    {
    }

    public virtual bool CheckDead()
    {
      this.NPC.FargoSouls().Needled = false;
      return true;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (target.hurtCooldowns[1] != 0)
        return;
      target.AddBuff(103, 420, true, false);
      if (WorldSavingSystem.MasochistModeReal)
        target.AddBuff(ModContent.BuffType<SqueakyToyBuff>(), 120, true, false);
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
      target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 1200, true, false);
      target.FargoSouls().MaxLifeReduction += FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.fishBossEX, 370) ? 100 : 25;
    }

    public virtual void FindFrame(int frameHeight)
    {
      if (!TextureAssets.Npc[this.NPC.type].IsLoaded)
        return;
      this.NPC.frame.Y = TextureAssets.Npc[this.NPC.type].Value.Height / 2;
    }

    public virtual bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
      return new bool?(false);
    }
  }
}
