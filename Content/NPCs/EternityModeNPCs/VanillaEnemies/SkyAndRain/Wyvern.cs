// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.SkyAndRain.Wyvern
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
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.SkyAndRain
{
  public class Wyvern : EModeNPCBehaviour
  {
    public int AttackTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(87);

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

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      this.AttackTimer = Main.rand.Next(180);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (!Main.hardMode || !Utils.NextBool(Main.rand, 10) || !npc.FargoSouls().CanHordeSplit)
        return;
      EModeGlobalNPC.Horde(npc, 2);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.AttackTimer <= 240)
        return;
      this.AttackTimer = 0;
      if (!FargoSoulsUtil.HostCheck || !Vector2.op_Inequality(((Entity) npc).velocity, Vector2.Zero))
        return;
      Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(((Entity) npc).velocity), 1.5f);
      for (int index = 0; index < 12; ++index)
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(vector2, 0.52359879016876221 * (double) index, new Vector2()), ModContent.ProjectileType<LightBall>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.01f * (float) ((Entity) npc).direction, 0.0f);
    }
  }
}
