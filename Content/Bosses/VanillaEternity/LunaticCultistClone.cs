// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.LunaticCultistClone
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class LunaticCultistClone : EModeNPCBehaviour
  {
    public int TotalCultistCount;
    public int MyRitualPosition;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(440);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.TotalCultistCount);
      binaryWriter.Write7BitEncodedInt(this.MyRitualPosition);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.TotalCultistCount = binaryReader.Read7BitEncodedInt();
      this.MyRitualPosition = binaryReader.Read7BitEncodedInt();
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[ModContent.BuffType<ClippedWingsBuff>()] = true;
      npc.buffImmune[ModContent.BuffType<LethargicBuff>()] = true;
      npc.buffImmune[68] = true;
    }

    public virtual bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
      return FargoSoulsUtil.IsSummonDamage(projectile) && !ProjectileID.Sets.IsAWhip[projectile.type] ? new bool?(false) : base.CanBeHitByProjectile(npc, projectile);
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.ai[3], 439);
      if (npc1 != null)
      {
        if ((double) npc1.ai[3] == -1.0 && (double) npc1.ai[0] == 5.0)
        {
          if (npc.alpha > 0)
          {
            this.TotalCultistCount = 1;
            for (int index = 0; index < Main.maxNPCs; ++index)
            {
              if (((Entity) Main.npc[index]).active && Main.npc[index].type == npc.type && (double) Main.npc[index].ai[3] == (double) npc.ai[3])
              {
                if (index == ((Entity) npc).whoAmI)
                  this.MyRitualPosition = this.TotalCultistCount;
                ++this.TotalCultistCount;
              }
            }
          }
          if ((double) npc1.ai[1] > 30.0 && (double) npc1.ai[1] < 330.0)
          {
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) npc1).Center, ((Entity) Main.player[npc1.target]).Center);
            ((Entity) npc).Center = Vector2.op_Addition(((Entity) Main.player[npc1.target]).Center, Utils.RotatedBy(vector2, 2.0 * Math.PI / (double) this.TotalCultistCount * (double) this.MyRitualPosition, new Vector2()));
            Lighting.AddLight(((Entity) npc).Center, 1f, 1f, 1f);
          }
        }
        else if ((double) npc1.ai[3] == 0.0)
          npc.alpha = 0;
        Lighting.AddLight(((Entity) npc).Center, 1f, 1f, 1f);
      }
      return flag;
    }

    public virtual void HitEffect(NPC npc, NPC.HitInfo hit)
    {
      base.HitEffect(npc, hit);
      if (WorldSavingSystem.SwarmActive)
        return;
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.ai[3], 439);
      if (npc1 == null || NPC.CountNPCS(npc.type) >= (WorldSavingSystem.MasochistModeReal ? Math.Min(this.TotalCultistCount + 1, 12) : this.TotalCultistCount) || !FargoSoulsUtil.HostCheck)
        return;
      FargoSoulsUtil.NewNPCEasy(((Entity) npc1).GetSource_FromAI((string) null), ((Entity) npc).Center, 440, ai0: npc.ai[0], ai1: npc.ai[1], ai2: npc.ai[2], ai3: npc.ai[3], target: npc.target, velocity: new Vector2());
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
    }
  }
}
