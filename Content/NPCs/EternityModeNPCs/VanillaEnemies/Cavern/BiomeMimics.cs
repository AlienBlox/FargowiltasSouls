// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.BiomeMimics
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public abstract class BiomeMimics : EModeNPCBehaviour
  {
    public int AttackCycleTimer;
    public int IndividualAttackTimer;
    public bool DoStompAttack;
    public bool CanDoAttack;

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.AttackCycleTimer);
      binaryWriter.Write7BitEncodedInt(this.IndividualAttackTimer);
      bitWriter.WriteBit(this.DoStompAttack);
      bitWriter.WriteBit(this.CanDoAttack);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.AttackCycleTimer = binaryReader.Read7BitEncodedInt();
      this.IndividualAttackTimer = binaryReader.Read7BitEncodedInt();
      this.DoStompAttack = bitReader.ReadBit();
      this.CanDoAttack = bitReader.ReadBit();
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[ModContent.BuffType<ClippedWingsBuff>()] = true;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (this.DoStompAttack)
      {
        if ((double) ((Entity) npc).velocity.Y == 0.0)
        {
          this.DoStompAttack = false;
          SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            for (int index = -1; index <= 1; ++index)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Bottom, new Vector2((float) (index * 16 * 4), -48f)), Vector2.Zero, ModContent.ProjectileType<BigMimicExplosion>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      else if ((double) ((Entity) npc).velocity.Y > 0.0 && npc.noTileCollide)
        this.DoStompAttack = true;
      if (!npc.dontTakeDamage && (double) npc.ai[0] != 0.0)
      {
        ++this.AttackCycleTimer;
        ++this.IndividualAttackTimer;
        if (this.AttackCycleTimer == 240 && !this.CanDoAttack && FargoSoulsUtil.HostCheck)
        {
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<IronParry>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          EModeNPCBehaviour.NetSync(npc);
        }
      }
      if (this.AttackCycleTimer <= 300)
        return;
      this.AttackCycleTimer = 0;
      this.CanDoAttack = !this.CanDoAttack;
    }
  }
}
