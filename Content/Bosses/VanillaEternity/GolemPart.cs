// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.GolemPart
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public abstract class GolemPart : EModeNPCBehaviour
  {
    public int HealPerSecond;
    public int HealCounter;

    protected GolemPart(int heal) => this.HealPerSecond = heal;

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.trapImmune = true;
      npc.damage = (int) Math.Round((double) npc.damage * 1.1);
      npc.lifeMax = (int) Math.Round((double) npc.lifeMax * 1.75);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[69] = true;
      npc.buffImmune[20] = true;
      npc.buffImmune[68] = true;
      npc.buffImmune[ModContent.BuffType<ClippedWingsBuff>()] = true;
    }

    public override bool SafePreAI(NPC npc)
    {
      if (!WorldSavingSystem.SwarmActive && !npc.dontTakeDamage && this.HealPerSecond != 0)
      {
        npc.life += this.HealPerSecond / 60;
        if (npc.life > npc.lifeMax)
          npc.life = npc.lifeMax;
        if (++this.HealCounter >= 75)
        {
          this.HealCounter = Main.rand.Next(30);
          CombatText.NewText(((Entity) npc).Hitbox, CombatText.HealLife, this.HealPerSecond, false, false);
        }
      }
      return base.SafePreAI(npc);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(36, 600, true, false);
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
      target.AddBuff(195, 600, true, false);
    }

    public static string DungeonVariant
    {
      get
      {
        if (GenVars.crackedType == (ushort) 481)
          return "B";
        return GenVars.crackedType != (ushort) 482 ? "P" : "G";
      }
    }

    public static void LoadGolemSpriteBuffered(
      bool recolor,
      int type,
      Asset<Texture2D>[] vanillaTexture,
      Dictionary<int, Asset<Texture2D>> fargoBuffer,
      string texturePrefix)
    {
      if (recolor)
      {
        if (fargoBuffer.ContainsKey(type))
          return;
        fargoBuffer[type] = vanillaTexture[type];
        Asset<Texture2D>[] assetArray = vanillaTexture;
        int index = type;
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 3);
        interpolatedStringHandler.AppendFormatted(texturePrefix);
        interpolatedStringHandler.AppendFormatted<int>(type);
        interpolatedStringHandler.AppendFormatted(GolemPart.DungeonVariant);
        Asset<Texture2D> asset = EModeNPCBehaviour.LoadSprite(interpolatedStringHandler.ToStringAndClear()) ?? vanillaTexture[type];
        assetArray[index] = asset;
      }
      else
      {
        if (!fargoBuffer.ContainsKey(type))
          return;
        vanillaTexture[type] = fargoBuffer[type];
        fargoBuffer.Remove(type);
      }
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      GolemPart.LoadGolemSpriteBuffered(recolor, npc.type, TextureAssets.Npc, FargowiltasSouls.FargowiltasSouls.TextureBuffer.NPC, "NPC_");
    }
  }
}
