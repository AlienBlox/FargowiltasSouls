// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Globals.EModeNPCBehaviour
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.Globals
{
  public abstract class EModeNPCBehaviour : GlobalNPC
  {
    public NPCMatcher Matcher;
    public bool RunEmodeAI = true;
    public bool FirstTick = true;

    public virtual bool InstancePerEntity => true;

    public virtual bool AppliesToEntity(NPC entity, bool lateInstantiation)
    {
      return lateInstantiation && this.Matcher.Satisfies(entity.type);
    }

    public virtual void Load()
    {
      this.Matcher = this.CreateMatcher();
      ((ModType) this).Load();
    }

    public abstract NPCMatcher CreateMatcher();

    public virtual GlobalNPC NewInstance(NPC target)
    {
      this.TryLoadSprites(target);
      if (!WorldSavingSystem.EternityVanillaBehaviour && target.ModNPC == null)
        return (GlobalNPC) null;
      return !WorldSavingSystem.EternityMode || !this.Matcher.Satisfies(target.type) ? (GlobalNPC) null : ((GlobalType<NPC, GlobalNPC>) this).NewInstance(target);
    }

    public virtual void OnFirstTick(NPC npc)
    {
    }

    public virtual bool SafePreAI(NPC npc) => base.PreAI(npc);

    public virtual bool PreAI(NPC npc)
    {
      if (this.FirstTick)
      {
        this.FirstTick = false;
        this.OnFirstTick(npc);
      }
      return this.RunEmodeAI && this.SafePreAI(npc);
    }

    public virtual void SafePostAI(NPC npc) => base.PostAI(npc);

    public virtual void PostAI(NPC npc)
    {
      if (!this.RunEmodeAI)
        return;
      this.SafePostAI(npc);
    }

    public virtual void ModifyHitByAnything(NPC npc, Player player, ref NPC.HitModifiers modifiers)
    {
    }

    public virtual void SafeModifyHitByItem(
      NPC npc,
      Player player,
      Item item,
      ref NPC.HitModifiers modifiers)
    {
    }

    public virtual void ModifyHitByItem(
      NPC npc,
      Player player,
      Item item,
      ref NPC.HitModifiers modifiers)
    {
      base.ModifyHitByItem(npc, player, item, ref modifiers);
      if (!WorldSavingSystem.EternityMode)
        return;
      this.SafeModifyHitByItem(npc, player, item, ref modifiers);
      this.ModifyHitByAnything(npc, player, ref modifiers);
    }

    public virtual void SafeModifyHitByProjectile(
      NPC npc,
      Projectile projectile,
      ref NPC.HitModifiers modifiers)
    {
    }

    public virtual void ModifyHitByProjectile(
      NPC npc,
      Projectile projectile,
      ref NPC.HitModifiers modifiers)
    {
      base.ModifyHitByProjectile(npc, projectile, ref modifiers);
      if (!WorldSavingSystem.EternityMode)
        return;
      this.SafeModifyHitByProjectile(npc, projectile, ref modifiers);
      this.ModifyHitByAnything(npc, Main.player[projectile.owner], ref modifiers);
    }

    public virtual void OnHitByAnything(NPC npc, Player player, NPC.HitInfo hit, int damageDone)
    {
    }

    public virtual void SafeOnHitByItem(
      NPC npc,
      Player player,
      Item item,
      NPC.HitInfo hit,
      int damageDone)
    {
    }

    public virtual void OnHitByItem(
      NPC npc,
      Player player,
      Item item,
      NPC.HitInfo hit,
      int damageDone)
    {
      base.OnHitByItem(npc, player, item, hit, damageDone);
      if (!WorldSavingSystem.EternityMode)
        return;
      this.SafeOnHitByItem(npc, player, item, hit, damageDone);
    }

    public virtual void SafeOnHitByProjectile(
      NPC npc,
      Projectile projectile,
      NPC.HitInfo hit,
      int damageDone)
    {
    }

    public virtual void OnHitByProjectile(
      NPC npc,
      Projectile projectile,
      NPC.HitInfo hit,
      int damageDone)
    {
      base.OnHitByProjectile(npc, projectile, hit, damageDone);
      if (!WorldSavingSystem.EternityMode)
        return;
      this.SafeOnHitByProjectile(npc, projectile, hit, damageDone);
    }

    protected static void NetSync(NPC npc, bool onlySendFromServer = true)
    {
      if (onlySendFromServer && Main.netMode != 2 || Main.netMode == 0)
        return;
      NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }

    public void TryLoadSprites(NPC npc)
    {
      if (Main.dedServ)
        return;
      bool recolor = SoulConfig.Instance.BossRecolors && WorldSavingSystem.EternityMode;
      if (!recolor && !FargowiltasSouls.FargowiltasSouls.Instance.LoadedNewSprites)
        return;
      FargowiltasSouls.FargowiltasSouls.Instance.LoadedNewSprites = true;
      this.LoadSprites(npc, recolor);
    }

    public virtual void LoadSprites(NPC npc, bool recolor)
    {
    }

    protected static Asset<Texture2D> LoadSprite(string texture)
    {
      Asset<Texture2D> asset;
      return ModContent.RequestIfExists<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/Resprites/" + texture, ref asset, (AssetRequestMode) 1) ? asset : (Asset<Texture2D>) null;
    }

    protected static void LoadSpriteBuffered(
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
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 2);
        interpolatedStringHandler.AppendFormatted(texturePrefix);
        interpolatedStringHandler.AppendFormatted<int>(type);
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

    protected static void LoadSpecial(
      bool recolor,
      ref Asset<Texture2D> vanillaResource,
      ref Asset<Texture2D> fargoSoulsBuffer,
      string name)
    {
      if (recolor)
      {
        if (fargoSoulsBuffer != null)
          return;
        fargoSoulsBuffer = vanillaResource;
        vanillaResource = EModeNPCBehaviour.LoadSprite(name) ?? vanillaResource;
      }
      else
      {
        if (fargoSoulsBuffer == null)
          return;
        vanillaResource = fargoSoulsBuffer;
        fargoSoulsBuffer = (Asset<Texture2D>) null;
      }
    }

    protected static void LoadNPCSprite(bool recolor, int type)
    {
      EModeNPCBehaviour.LoadSpriteBuffered(recolor, type, TextureAssets.Npc, FargowiltasSouls.FargowiltasSouls.TextureBuffer.NPC, "NPC_");
    }

    protected static void LoadBossHeadSprite(bool recolor, int type)
    {
      EModeNPCBehaviour.LoadSpriteBuffered(recolor, type, TextureAssets.NpcHeadBoss, FargowiltasSouls.FargowiltasSouls.TextureBuffer.NPCHeadBoss, "NPC_Head_Boss_");
    }

    protected static void LoadGore(bool recolor, int type)
    {
      EModeNPCBehaviour.LoadSpriteBuffered(recolor, type, TextureAssets.Gore, FargowiltasSouls.FargowiltasSouls.TextureBuffer.Gore, "Gores/Gore_");
    }

    protected static void LoadGoreRange(bool recolor, int type, int lastType)
    {
      for (int type1 = type; type1 <= lastType; ++type1)
        EModeNPCBehaviour.LoadGore(recolor, type1);
    }

    protected static void LoadExtra(bool recolor, int type)
    {
      EModeNPCBehaviour.LoadSpriteBuffered(recolor, type, TextureAssets.Extra, FargowiltasSouls.FargowiltasSouls.TextureBuffer.Extra, "Extra_");
    }

    protected static void LoadGolem(bool recolor, int type)
    {
      EModeNPCBehaviour.LoadSpriteBuffered(recolor, type, TextureAssets.Golem, FargowiltasSouls.FargowiltasSouls.TextureBuffer.Golem, "GolemLights");
    }

    protected static void LoadDest(bool recolor, int type)
    {
      EModeNPCBehaviour.LoadSpriteBuffered(recolor, type, TextureAssets.Dest, FargowiltasSouls.FargowiltasSouls.TextureBuffer.Dest, "Dest");
    }

    protected static void LoadGlowMask(bool recolor, int type)
    {
      EModeNPCBehaviour.LoadSpriteBuffered(recolor, type, TextureAssets.GlowMask, FargowiltasSouls.FargowiltasSouls.TextureBuffer.GlowMask, "Glow_");
    }

    protected static void LoadProjectile(bool recolor, int type)
    {
      EModeNPCBehaviour.LoadSpriteBuffered(recolor, type, TextureAssets.Projectile, FargowiltasSouls.FargowiltasSouls.TextureBuffer.Projectile, "Projectile_");
    }
  }
}
