// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.UndeadMiner
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class UndeadMiner : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(44);

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
      if (this.Counter == 180)
      {
        if (npc.DeathSound.HasValue)
        {
          SoundStyle soundStyle = npc.DeathSound.Value;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        }
        FargoSoulsUtil.DustRing(((Entity) npc).Center, 32, 159, 5f, new Color(), 2f);
      }
      if (++this.Counter <= 240)
        return;
      this.Counter = 0;
      EModeNPCBehaviour.NetSync(npc);
      if (!FargoSoulsUtil.HostCheck || !npc.HasValidTarget || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) >= 800.0)
        return;
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
      vector2_1.Y -= Math.Abs(vector2_1.X) * 0.25f;
      vector2_1.X += (float) Main.rand.Next(-20, 21);
      vector2_1.Y += (float) Main.rand.Next(-20, 21);
      ((Vector2) ref vector2_1).Normalize();
      Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 12f);
      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2_2, 102, (int) ((double) npc.damage * 0.7), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<LethargicBuff>(), 600, true, false);
      target.AddBuff(80, 300, true, false);
      target.AddBuff(199, 300, true, false);
    }
  }
}
