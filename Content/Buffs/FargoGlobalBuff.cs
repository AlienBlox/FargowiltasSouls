// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.FargoGlobalBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs
{
  internal class FargoGlobalBuff : GlobalBuff
  {
    public virtual void ModifyBuffText(
      int type,
      ref string buffName,
      ref string tip,
      ref int rare)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      if (type == 59)
      {
        tip = tip + "\n" + Language.GetTextValue("Mods.FargowiltasSouls.EModeBalance.ShadowDodge");
      }
      else
      {
        if (type != 62)
          return;
        tip = tip + "\n" + Language.GetTextValue("Mods.FargowiltasSouls.EModeBalance.IceBarrier");
      }
    }

    public static int[] DebuffsToLetDecreaseNormally
    {
      get
      {
        return new int[6]
        {
          47,
          156,
          23,
          ModContent.BuffType<FusedBuff>(),
          ModContent.BuffType<TimeFrozenBuff>(),
          ModContent.BuffType<StunnedBuff>()
        };
      }
    }

    public virtual void Update(int type, Player player, ref int buffIndex)
    {
      switch (type)
      {
        case 24:
          if (WorldSavingSystem.EternityMode && Main.raining && (double) ((Entity) player).position.Y < Main.worldSurface * 16.0)
          {
            Tile tileSafely = Framing.GetTileSafely(((Entity) player).Center);
            if (((Tile) ref tileSafely).WallType == (ushort) 0 && player.buffTime[buffIndex] > 2)
            {
              --player.buffTime[buffIndex];
              break;
            }
            break;
          }
          break;
        case 46:
          if (WorldSavingSystem.EternityMode && player.buffTime[buffIndex] > 900)
          {
            player.buffTime[buffIndex] = 900;
            break;
          }
          break;
        case 137:
          Main.buffNoTimeDisplay[type] = false;
          if (WorldSavingSystem.EternityMode)
          {
            player.FargoSouls().Slimed = true;
            break;
          }
          break;
        case 160:
          if (((Entity) player).whoAmI == Main.myPlayer && player.buffTime[buffIndex] % 60 == 55)
          {
            SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/DizzyBird", (SoundType) 0);
            SoundEngine.PlaySound(ref soundStyle, new Vector2?(), (SoundUpdateCallback) null);
            break;
          }
          break;
        case 308:
        case 311:
        case 312:
        case 314:
          if (WorldSavingSystem.EternityMode)
          {
            if (player.Eternity().HasWhipBuff)
              player.buffTime[buffIndex] = Math.Min(player.buffTime[buffIndex], 1);
            player.Eternity().HasWhipBuff = true;
            break;
          }
          break;
      }
      if (WorldSavingSystem.EternityMode && player.buffTime[buffIndex] > 5 && Main.debuff[type] && player.Eternity().ShorterDebuffsTimer <= 0 && !Main.buffNoTimeDisplay[type] && type != 25 && (!BuffID.Sets.NurseCannotRemoveDebuff[type] || type == 94 || type == 21) && !((IEnumerable<int>) FargoGlobalBuff.DebuffsToLetDecreaseNormally).Contains<int>(type) && (type != 31 || !FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.brainBoss, 266)))
        --player.buffTime[buffIndex];
      base.Update(type, player, ref buffIndex);
    }

    public virtual void Update(int type, NPC npc, ref int buffIndex)
    {
      switch (type)
      {
        case 22:
          npc.color = Color.Gray;
          if (npc.buffTime[buffIndex] % 30 != 0 || !FargoSoulsUtil.HostCheck)
            break;
          Player player = ((IEnumerable<Player>) Main.player).FirstOrDefault<Player>((Func<Player, bool>) (p => ((Entity) p).active && !p.dead && p.HasEffect<AncientShadowDarkness>()));
          if (player == null || !((Entity) player).active || player.dead)
            break;
          FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
          if (fargoSoulsPlayer.AncientShadowFlameCooldown > 0)
            break;
          fargoSoulsPlayer.AncientShadowFlameCooldown = 30;
          for (int index1 = 0; index1 < Main.maxNPCs; ++index1)
          {
            NPC npc1 = Main.npc[index1];
            if (((Entity) npc1).active && !npc1.friendly && (double) Vector2.Distance(((Entity) npc).Center, ((Entity) npc1).Center) < 250.0)
            {
              Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) npc1).Center, ((Entity) npc).Center)), 5f);
              int index2 = Projectile.NewProjectile(((Entity) player).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, 496, 40 + FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              if (index2.IsWithinBounds(Main.maxProjectiles))
              {
                Main.projectile[index2].friendly = true;
                Main.projectile[index2].hostile = false;
              }
              if (Utils.NextBool(Main.rand, 3))
                break;
            }
          }
          break;
        case 24:
          if (!WorldSavingSystem.EternityMode || !Main.raining || (double) ((Entity) npc).position.Y >= Main.worldSurface * 16.0)
            break;
          Tile tileSafely = Framing.GetTileSafely(((Entity) npc).Center);
          if (((Tile) ref tileSafely).WallType != (ushort) 0 || npc.buffTime[buffIndex] <= 2)
            break;
          --npc.buffTime[buffIndex];
          break;
        case 36:
          npc.FargoSouls().BrokenArmor = true;
          break;
        case 68:
          npc.FargoSouls().Suffocation = true;
          break;
        case 144:
          npc.FargoSouls().Electrified = true;
          break;
        case 309:
        case 315:
        case 316:
        case 319:
        case 326:
          if (WorldSavingSystem.EternityMode && npc.Eternity().HasWhipDebuff)
            npc.buffTime[buffIndex] = Math.Min(npc.buffTime[buffIndex], 1);
          npc.Eternity().HasWhipDebuff = true;
          break;
      }
    }
  }
}
