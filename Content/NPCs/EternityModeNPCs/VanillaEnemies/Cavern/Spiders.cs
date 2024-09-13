// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.Spiders
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class Spiders : EModeNPCBehaviour
  {
    public int Counter;
    public bool TargetIsWebbed;

    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(239, 240, 236, 237, 164, 165, 163, 238);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (npc.type == 165 || npc.type == 240 || npc.type == 237)
      {
        if (this.Counter == 300)
          FargoSoulsUtil.DustRing(((Entity) npc).Center, 32, 30, 9f, new Color(), 1.5f);
        if (this.Counter > 300)
        {
          NPC npc1 = npc;
          ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, ((Entity) npc).velocity);
        }
        if (++this.Counter > 360)
        {
          this.Counter = 0;
          if (npc.HasValidTarget && (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) < 450.0 && Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0) && FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(14f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)), 472, 9, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      if (npc.type != 163 && npc.type != 238)
        return;
      if (++this.Counter > 10)
      {
        this.Counter = 0;
        this.TargetIsWebbed = false;
        int index = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
        if (index != -1)
        {
          Player player = Main.player[index];
          int buffIndex = player.FindBuffIndex(149);
          this.TargetIsWebbed = buffIndex != -1;
          if (this.TargetIsWebbed)
            player.AddBuff(ModContent.BuffType<DefenselessBuff>(), player.buffTime[buffIndex], true, false);
        }
      }
      if (!this.TargetIsWebbed)
        return;
      NPC npc2 = npc;
      ((Entity) npc2).position = Vector2.op_Addition(((Entity) npc2).position, ((Entity) npc).velocity);
    }

    public virtual Color? GetAlpha(NPC npc, Color drawColor)
    {
      if (!this.TargetIsWebbed)
        return base.GetAlpha(npc, drawColor);
      ((Color) ref drawColor).R = byte.MaxValue;
      ref Color local1 = ref drawColor;
      ((Color) ref local1).G = (byte) ((uint) ((Color) ref local1).G / 3U);
      ref Color local2 = ref drawColor;
      ((Color) ref local2).B = (byte) ((uint) ((Color) ref local2).B / 3U);
      return new Color?(drawColor);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<InfestedBuff>(), 300, true, false);
    }
  }
}
