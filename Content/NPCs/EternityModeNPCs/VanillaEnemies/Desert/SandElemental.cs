// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Desert.SandElemental
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Spirit;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Desert
{
  public class SandElemental : EModeNPCBehaviour
  {
    public int WormTimer;
    public int AttackTimer;
    public Vector2 AttackTarget;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(541);

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
      if (++this.AttackTimer == 270)
      {
        if (!npc.HasValidTarget)
        {
          this.AttackTimer = 0;
        }
        else
        {
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          this.AttackTarget = ((Entity) Main.player[npc.target]).Center;
          this.AttackTarget.Y -= 650f;
          int num = (int) ((Entity) npc).Distance(this.AttackTarget) / 10;
          Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, this.AttackTarget), 10f);
          for (int index1 = 0; index1 < num; ++index1)
          {
            int index2 = Dust.NewDust(Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(vector2, (float) index1)), 0, 0, 269, 0.0f, 0.0f, 0, new Color(), 1f);
            Main.dust[index2].noLight = true;
            Main.dust[index2].scale = 1.5f;
          }
        }
        EModeNPCBehaviour.NetSync(npc);
      }
      if (this.AttackTimer > 300 && this.AttackTimer % 3 == 0 && FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(this.AttackTarget, Utils.NextVector2Circular(Main.rand, 80f, 80f)), new Vector2(Utils.NextFloat(Main.rand, -0.5f, 0.5f), Utils.NextFloat(Main.rand, 3f)), ModContent.ProjectileType<SpiritCrossBone>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      if (this.AttackTimer <= 390)
        return;
      this.AttackTimer = 0;
      EModeNPCBehaviour.NetSync(npc);
    }
  }
}
