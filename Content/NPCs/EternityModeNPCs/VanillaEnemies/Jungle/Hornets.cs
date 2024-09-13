// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Jungle.Hornets
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Jungle
{
  public class Hornets : EModeNPCBehaviour
  {
    public int Timer;

    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(42, 231, 232, 233, 234, 235, -57, -59, -61, -63, -65, -20, -21, -56, -58, -60, -62, -64, -19, 176, -18);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[20] = true;
      npc.buffImmune[70] = true;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.Timer > (WorldSavingSystem.MasochistModeReal ? 240 : 420))
        this.Timer = 0;
      if (!npc.HasPlayerTarget)
        return;
      if ((!npc.HasValidTarget || !Main.player[npc.target].FargoSouls().Swarming ? 0 : (!Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0) ? 1 : 0)) != 0)
        npc.noTileCollide = true;
      else if (npc.noTileCollide && !Collision.SolidCollision(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height))
        npc.noTileCollide = false;
      if (!npc.noTileCollide && (!npc.HasValidTarget || !Main.player[npc.target].FargoSouls().Swarming))
        return;
      int index = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 44, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 0, new Color(), 1f);
      Main.dust[index].noGravity = true;
      if (this.Timer != 0)
        return;
      if (!Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0))
        ((Entity) npc).velocity = Vector2.op_Multiply(Math.Min(6f, ((Vector2) ref ((Entity) npc).velocity).Length()), Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center));
      if ((double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > 1200.0)
        this.Timer += 90;
      npc.netUpdate = true;
      EModeNPCBehaviour.NetSync(npc);
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.beeBoss, 222))
        return base.CheckDead(npc);
      ((Entity) npc).active = false;
      if (npc.DeathSound.HasValue)
      {
        SoundStyle soundStyle = npc.DeathSound.Value;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      }
      return false;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<InfestedBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<SwarmingBuff>(), 600, true, false);
    }
  }
}
