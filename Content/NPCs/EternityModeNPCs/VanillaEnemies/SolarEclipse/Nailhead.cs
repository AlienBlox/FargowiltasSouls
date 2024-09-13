// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.SolarEclipse.Nailhead
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.SolarEclipse
{
  public class Nailhead : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(463);

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

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.Counter == 420)
      {
        FargoSoulsUtil.DustRing(((Entity) npc).Center, 48, 64, 9f, new Color(), 3f);
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if (this.Counter > 420)
      {
        NPC npc1 = npc;
        ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, ((Entity) npc).velocity);
        if (this.Counter > 465 && this.Counter % 4 == 0)
          Spray();
      }
      if (this.Counter <= 555)
        return;
      this.Counter = 0;
      npc.netUpdate = true;
      EModeNPCBehaviour.NetSync(npc);

      void Spray()
      {
        if (!FargoSoulsUtil.HostCheck)
          return;
        int length = Main.rand.Next(3, 6);
        int[] numArray = new int[length];
        int index1 = 0;
        for (int index2 = 0; index2 < (int) byte.MaxValue; ++index2)
        {
          if (((Entity) Main.player[index2]).active && !Main.player[index2].dead && Collision.CanHitLine(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, ((Entity) Main.player[index2]).position, ((Entity) Main.player[index2]).width, ((Entity) Main.player[index2]).height))
          {
            numArray[index1] = index2;
            ++index1;
            if (index1 == length)
              break;
          }
        }
        if (index1 > 1)
        {
          for (int index3 = 0; index3 < 100; ++index3)
          {
            int index4 = Main.rand.Next(index1);
            int index5 = index4;
            while (index5 == index4)
              index5 = Main.rand.Next(index1);
            int num = numArray[index4];
            numArray[index4] = numArray[index5];
            numArray[index5] = num;
          }
        }
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector(-1f, -1f);
        for (int index6 = 0; index6 < index1; ++index6)
        {
          Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) Main.npc[numArray[index6]]).Center, ((Entity) npc).Center);
          ((Vector2) ref vector2_2).Normalize();
          vector2_1 = Vector2.op_Addition(vector2_1, vector2_2);
        }
        ((Vector2) ref vector2_1).Normalize();
        for (int index7 = 0; index7 < length; ++index7)
        {
          float num = (float) Main.rand.Next(8, 13);
          Vector2 vector2_3;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_3).\u002Ector((float) Main.rand.Next(-100, 101), (float) Main.rand.Next(-100, 101));
          ((Vector2) ref vector2_3).Normalize();
          if (index1 > 0)
          {
            vector2_3 = Vector2.op_Addition(vector2_3, vector2_1);
            ((Vector2) ref vector2_3).Normalize();
          }
          vector2_3 = Vector2.op_Multiply(vector2_3, num);
          if (index1 > 0)
          {
            --index1;
            vector2_3 = Vector2.op_Subtraction(((Entity) Main.player[numArray[index1]]).Center, ((Entity) npc).Center);
            ((Vector2) ref vector2_3).Normalize();
            vector2_3 = Vector2.op_Multiply(vector2_3, num);
          }
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center.X, ((Entity) npc).position.Y + (float) ((Entity) npc).width / 4f, vector2_3.X, vector2_3.Y, 498, (int) ((double) npc.damage * 0.15), 1f, -1, 0.0f, 0.0f, 0.0f);
        }
      }
    }
  }
}
