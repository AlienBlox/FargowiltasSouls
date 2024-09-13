// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.EbonwoodEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class EbonwoodEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<TimberHeader>();

    public override int ToggleItemType => ModContent.ItemType<EbonwoodEnchant>();

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      bool force = fargoSoulsPlayer.ForceEffect<EbonwoodEnchant>();
      int AoE = force ? 400 : 200;
      foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && !n.friendly && n.lifeMax > 5 && !n.dontTakeDamage)))
      {
        Vector2 vector2 = FargoSoulsUtil.ClosestPointInHitbox((Entity) npc, ((Entity) player).Center);
        if ((double) ((Entity) player).Distance(vector2) < (double) AoE && (force || Collision.CanHitLine(((Entity) player).Center, 0, 0, vector2, 0, 0)) && !npc.HasBuff<CorruptedBuffForce>() && !npc.HasBuff<CorruptedBuff>())
          npc.AddBuff(ModContent.BuffType<CorruptingBuff>(), 2, false);
        if (npc.FargoSouls().EbonCorruptionTimer > 180 && !npc.HasBuff<CorruptedBuffForce>() && !npc.HasBuff<CorruptedBuff>())
          EbonwoodEffect.EbonwoodProc(player, npc, AoE, force, 5);
      }
      for (int index = 0; index < 20; ++index)
      {
        Vector2 vector2_1 = new Vector2();
        double num = Main.rand.NextDouble() * 2.0 * Math.PI;
        vector2_1.X += (float) Math.Sin(num) * (float) AoE;
        vector2_1.Y += (float) Math.Cos(num) * (float) AoE;
        Vector2 vector2_2 = Vector2.op_Subtraction(Vector2.op_Addition(((Entity) player).Center, vector2_1), new Vector2(4f, 4f));
        if (force || Collision.CanHitLine(((Entity) player).Left, 0, 0, vector2_2, 0, 0) || Collision.CanHitLine(((Entity) player).Right, 0, 0, vector2_2, 0, 0))
        {
          Dust dust1 = Main.dust[Dust.NewDust(vector2_2, 0, 0, 27, 0.0f, 0.0f, 100, Color.White, 1f)];
          dust1.velocity = ((Entity) player).velocity;
          if (Utils.NextBool(Main.rand, 3))
          {
            Dust dust2 = dust1;
            dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2_1), -5f));
          }
          dust1.noGravity = true;
        }
      }
    }

    public static void EbonwoodProc(Player player, NPC npc, int AoE, bool force, int limit)
    {
      foreach (Entity entity in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && !n.friendly && n.lifeMax > 5 && !n.dontTakeDamage)))
      {
        Vector2 vector2 = FargoSoulsUtil.ClosestPointInHitbox(entity, ((Entity) npc).Center);
        if ((double) ((Entity) npc).Distance(vector2) < (double) AoE && !npc.HasBuff<CorruptedBuffForce>() && !npc.HasBuff<CorruptedBuff>() && limit > 0)
          EbonwoodEffect.EbonwoodProc(player, npc, AoE, force, limit - 1);
      }
      EbonwoodEffect.Corrupt(npc, force);
      SoundEngine.PlaySound(ref SoundID.NPCDeath55, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      npc.FargoSouls().EbonCorruptionTimer = 0;
      for (int index = 0; index < 60; ++index)
      {
        Vector2 vector2_1 = new Vector2();
        double num = Main.rand.NextDouble() * 2.0 * Math.PI;
        vector2_1.X += (float) Math.Sin(num) * (float) AoE;
        vector2_1.Y += (float) Math.Cos(num) * (float) AoE;
        Vector2 vector2_2 = Vector2.op_Subtraction(Vector2.op_Addition(((Entity) npc).Center, vector2_1), new Vector2(4f, 4f));
        Dust dust1 = Main.dust[Dust.NewDust(vector2_2, 0, 0, 27, 0.0f, 0.0f, 100, Color.White, 1f)];
        dust1.velocity = ((Entity) npc).velocity;
        if (Utils.NextBool(Main.rand, 3))
        {
          Dust dust2 = dust1;
          dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2_1), -5f));
        }
        dust1.noGravity = true;
      }
    }

    private static void Corrupt(NPC npc, bool force)
    {
      if (npc.HasBuff<CorruptedBuffForce>() || npc.HasBuff<CorruptedBuff>())
        return;
      if (force)
        npc.AddBuff(ModContent.BuffType<CorruptedBuffForce>(), 240, false);
      else
        npc.AddBuff(ModContent.BuffType<CorruptedBuff>(), 240, false);
    }
  }
}
