// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.Creeper
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class Creeper : EModeNPCBehaviour
  {
    public int IchorAttackTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(267);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.IchorAttackTimer);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.IchorAttackTimer = binaryReader.Read7BitEncodedInt();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax = (int) Math.Round((double) npc.lifeMax * 1.25);
      this.IchorAttackTimer = Main.rand.Next(60 * NPC.CountNPCS(267)) + Main.rand.Next(61) + 60;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[69] = true;
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      if (WorldSavingSystem.SwarmActive)
        return flag;
      if (--this.IchorAttackTimer < 0)
      {
        this.IchorAttackTimer = 60 * NPC.CountNPCS(267) - 30;
        if (this.IchorAttackTimer >= 60)
          this.IchorAttackTimer += Main.rand.Next(-30, 31);
        if (npc.HasPlayerTarget && FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(10f, Utils.RotatedByRandom(((Entity) npc).DirectionFrom(((Entity) Main.player[npc.target]).Center), Math.PI)), ModContent.ProjectileType<GoldenShowerHoming>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, (float) npc.target, -60f, 0.0f);
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if (this.IchorAttackTimer % 60 == 0)
        this.IchorAttackTimer = Math.Min(this.IchorAttackTimer, 60 * NPC.CountNPCS(npc.type));
      return flag;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      target.AddBuff(20, 120, true, false);
      target.AddBuff(22, 120, true, false);
      target.AddBuff(30, 120, true, false);
      target.AddBuff(32, 120, true, false);
      target.AddBuff(33, 120, true, false);
      target.AddBuff(36, 120, true, false);
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (npc.DeathSound.HasValue)
      {
        SoundStyle soundStyle = npc.DeathSound.Value;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      }
      ((Entity) npc).active = false;
      return false;
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
    }
  }
}
