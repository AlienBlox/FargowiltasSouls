// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Shooters
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies
{
  public abstract class Shooters : EModeNPCBehaviour
  {
    protected readonly int AttackThreshold;
    protected readonly int ProjectileType;
    protected readonly int DustType;
    protected readonly float Distance;
    protected readonly float Speed;
    protected readonly float DamageMultiplier;
    protected readonly int Telegraph;
    protected readonly bool NeedLineOfSight;
    public int AttackTimer;

    protected Shooters(
      int attackThreshold,
      int projectileType,
      float speed,
      float damageMultiplier = 1f,
      int dustType = 159,
      float distance = 1000f,
      int telegraph = 30,
      bool needLineOfSight = false)
    {
      this.AttackThreshold = attackThreshold;
      this.ProjectileType = projectileType;
      this.DustType = dustType;
      this.Distance = distance;
      this.Speed = speed;
      this.DamageMultiplier = damageMultiplier;
      this.Telegraph = telegraph;
      this.NeedLineOfSight = needLineOfSight;
    }

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
      this.AttackTimer = -Main.rand.Next(60);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      ++this.AttackTimer;
      if (this.AttackTimer == this.AttackThreshold - this.Telegraph)
      {
        if (!npc.HasPlayerTarget || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > (double) this.Distance || this.NeedLineOfSight && !Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0))
          this.AttackTimer = 0;
        else if (this.DustType != -1)
          FargoSoulsUtil.DustRing(((Entity) npc).Center, 32, this.DustType, 5f, new Color(), 2f);
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if (this.AttackTimer > this.AttackThreshold - this.Telegraph)
      {
        NPC npc1 = npc;
        ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, ((Entity) npc).velocity);
      }
      if (this.AttackTimer <= this.AttackThreshold)
        return;
      this.AttackTimer = 0;
      npc.netUpdate = true;
      EModeNPCBehaviour.NetSync(npc);
      if (!npc.HasPlayerTarget || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) >= (double) this.Distance || this.NeedLineOfSight && (!this.NeedLineOfSight || !Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0)) || !FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(this.Speed, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)), this.ProjectileType, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, this.DamageMultiplier), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }
  }
}
