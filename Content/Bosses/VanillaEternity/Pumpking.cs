// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.Pumpking
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
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class Pumpking : EModeNPCBehaviour
  {
    public int AttackTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(327);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.AttackTimer);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.AttackTimer = binaryReader.Read7BitEncodedInt();
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.AttackTimer <= 300)
        return;
      this.AttackTimer = 0;
      if (((Entity) npc).whoAmI != NPC.FindFirstNPC(npc.type) || !FargoSoulsUtil.HostCheck)
        return;
      for (int index1 = -1; index1 <= 1; ++index1)
      {
        if (index1 != 0)
        {
          for (int index2 = 0; index2 < 20; ++index2)
          {
            Vector2 vector2 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), 0.52359879016876221 * (Main.rand.NextDouble() - 0.5) + 1.5707963705062866 * (double) index1, new Vector2());
            float num1 = Utils.NextFloat(Main.rand, 1.04f, 1.05f);
            float num2 = Utils.NextFloat(Main.rand, 0.03f);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<PumpkingFlamingScythe>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 2f), 0.0f, Main.myPlayer, num1, num2, 0.0f);
          }
        }
      }
    }
  }
}
