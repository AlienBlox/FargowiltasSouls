// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Snow.IceGolem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Snow
{
  public class IceGolem : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(243);

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
      if ((double) ((Entity) npc).velocity.Y < 0.0)
        ((Entity) npc).position.Y += ((Entity) npc).velocity.Y;
      ++this.Counter;
      if (this.Counter == 540)
        FargoSoulsUtil.DustRing(((Entity) npc).Center, 64, 135, 16f, new Color(), 3f);
      if (this.Counter <= 600)
        return;
      this.Counter = 0;
      if (npc.HasPlayerTarget && FargoSoulsUtil.HostCheck)
      {
        for (int index = 0; index < 12; ++index)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(6f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), 0.52359879016876221 * (double) index, new Vector2())), ModContent.ProjectileType<FrostfireballHostile>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, (float) npc.target, (float) (180 + Main.rand.Next(-60, 60)), 0.0f);
      }
      EModeNPCBehaviour.NetSync(npc);
    }
  }
}
