// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon.DarkCaster
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon
{
  public class DarkCaster : DungeonTeleporters
  {
    public int AttackTimer;
    public bool SpawnedByTim;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(32);

    public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      bitWriter.WriteBit(this.SpawnedByTim);
    }

    public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.SpawnedByTim = bitReader.ReadBit();
    }

    public virtual void OnSpawn(NPC npc, IEntitySource source)
    {
      base.OnSpawn(npc, source);
      if (!(source is EntitySource_Parent entitySourceParent) || !(entitySourceParent.Entity is NPC entity) || entity.type != 45)
        return;
      this.SpawnedByTim = true;
    }

    public override void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.AttackTimer <= 300)
        return;
      this.AttackTimer = 0;
      if (this.SpawnedByTim)
        return;
      for (int index1 = 0; index1 < 5; ++index1)
      {
        SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          int index2 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.NextVector2CircularEdge(Main.rand, -4.5f, 4.5f), 27, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          if (index2 != Main.maxProjectiles)
            Main.projectile[index2].timeLeft = Main.rand.Next(180, 360);
        }
      }
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (!this.SpawnedByTim && NPC.downedBoss3)
        return base.CheckDead(npc);
      npc.life = 0;
      npc.HitEffect(0, 10.0, new bool?());
      ((Entity) npc).active = false;
      if (npc.DeathSound.HasValue)
      {
        SoundStyle soundStyle = npc.DeathSound.Value;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      }
      return false;
    }
  }
}
