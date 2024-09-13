// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.A_SourceNPCGlobalProjectile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class A_SourceNPCGlobalProjectile : GlobalProjectile
  {
    internal static Dictionary<int, bool> SourceNPCSync = new Dictionary<int, bool>();
    internal static Dictionary<int, bool> DamagingSync = new Dictionary<int, bool>();
    public NPC sourceNPC;

    public virtual void Unload()
    {
      ((ModType) this).Unload();
      A_SourceNPCGlobalProjectile.SourceNPCSync.Clear();
      A_SourceNPCGlobalProjectile.DamagingSync.Clear();
    }

    public virtual bool InstancePerEntity => true;

    public virtual void OnSpawn(Projectile projectile, IEntitySource source)
    {
      if (!(source is EntitySource_Parent entitySourceParent))
        return;
      if (entitySourceParent.Entity is NPC)
      {
        projectile.SetSourceNPC(entitySourceParent.Entity as NPC);
      }
      else
      {
        if (!(entitySourceParent.Entity is Projectile))
          return;
        Projectile entity = entitySourceParent.Entity as Projectile;
        projectile.SetSourceNPC(entity.GetSourceNPC());
      }
    }

    public static bool NeedsSync(Dictionary<int, bool> dict, int projectileType)
    {
      bool flag;
      return dict.TryGetValue(projectileType, out flag) & flag;
    }

    public virtual void SendExtraAI(Projectile projectile, BitWriter bits, BinaryWriter writer)
    {
      if (A_SourceNPCGlobalProjectile.NeedsSync(A_SourceNPCGlobalProjectile.SourceNPCSync, projectile.type))
        writer.Write7BitEncodedInt(this.sourceNPC != null ? ((Entity) this.sourceNPC).whoAmI : Main.maxNPCs);
      if (!A_SourceNPCGlobalProjectile.NeedsSync(A_SourceNPCGlobalProjectile.DamagingSync, projectile.type))
        return;
      bits.WriteBit(projectile.friendly);
      bits.WriteBit(projectile.hostile);
      bits.WriteBit(projectile.CountsAsClass(DamageClass.Default));
    }

    public virtual void ReceiveExtraAI(Projectile projectile, BitReader bits, BinaryReader reader)
    {
      if (A_SourceNPCGlobalProjectile.NeedsSync(A_SourceNPCGlobalProjectile.SourceNPCSync, projectile.type))
        this.sourceNPC = FargoSoulsUtil.NPCExists(reader.Read7BitEncodedInt(), Array.Empty<int>());
      if (!A_SourceNPCGlobalProjectile.NeedsSync(A_SourceNPCGlobalProjectile.DamagingSync, projectile.type))
        return;
      projectile.friendly = bits.ReadBit();
      projectile.hostile = bits.ReadBit();
      if (!bits.ReadBit())
        return;
      projectile.DamageType = DamageClass.Default;
    }

    public virtual bool PreAI(Projectile projectile)
    {
      if (this.sourceNPC != null && !((Entity) this.sourceNPC).active)
        this.sourceNPC = (NPC) null;
      return base.PreAI(projectile);
    }
  }
}
