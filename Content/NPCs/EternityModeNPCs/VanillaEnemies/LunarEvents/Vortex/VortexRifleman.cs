// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Vortex.VortexRifleman
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Vortex
{
  public class VortexRifleman : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(425);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.Counter);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.Counter = binaryReader.Read7BitEncodedInt();
    }

    public override bool SafePreAI(NPC npc)
    {
      if (this.Counter <= 0)
        return base.SafePreAI(npc);
      ((Entity) npc).velocity = Vector2.Zero;
      if (this.Counter >= 20 && this.Counter % 10 == 0)
      {
        Vector2 center = ((Entity) npc).Center;
        center.X += (float) ((Entity) npc).direction * 30f;
        center.Y += 2f;
        Vector2 vector2_1 = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) ((Entity) npc).direction), 8f);
        if (FargoSoulsUtil.HostCheck)
        {
          int num = Main.expertMode ? 50 : 75;
          for (int index = 0; index < 4; ++index)
          {
            Vector2 vector2_2 = Vector2.op_Addition(vector2_1, Utils.RandomVector2(Main.rand, -0.8f, 0.8f));
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), center.X, center.Y, vector2_2.X, vector2_2.Y, 638, num, 1f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
        SoundEngine.PlaySound(ref SoundID.Item36, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      }
      if (++this.Counter >= 80)
      {
        this.Counter = 0;
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      return false;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if ((double) npc.localAI[2] < 180.0 + (double) Main.rand.Next(180) || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) >= 400.0 || (double) Math.Abs(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center).Y) >= 0.5 || !Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0))
        return;
      npc.localAI[2] = 0.0f;
      this.Counter = 1;
      EModeNPCBehaviour.NetSync(npc);
    }
  }
}
