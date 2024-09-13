// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Magmaw.MagmawJaw
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Magmaw
{
  public class MagmawJaw : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Type] = 1;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 96;
      ((Entity) this.Projectile).height = 58;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.light = 1f;
      this.Projectile.FargoSouls().DeletionImmuneRank = 10;
      this.Projectile.hide = true;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.Projectile.localAI[1]);
      writer.Write(this.Projectile.localAI[2]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.Projectile.localAI[1] = reader.ReadSingle();
      this.Projectile.localAI[2] = reader.ReadSingle();
    }

    public ref float ParentID => ref this.Projectile.ai[0];

    public virtual void AI()
    {
      int index = (int) this.ParentID;
      if (!index.IsWithinBounds(Main.maxNPCs))
      {
        this.Projectile.Kill();
      }
      else
      {
        NPC npc = Main.npc[index];
        if (!npc.TypeAlive<FargowiltasSouls.Content.Bosses.Magmaw.Magmaw>())
        {
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.timeLeft = 60;
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.op_Subtraction(Luminance.Common.Utilities.Utilities.As<FargowiltasSouls.Content.Bosses.Magmaw.Magmaw>(npc).JawCenter, ((Entity) this.Projectile).Center), 0.95f);
          this.Projectile.damage = npc.damage;
        }
      }
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      if (!this.Projectile.hide)
        return;
      behindNPCs.Add(index);
    }

    public NPC GetParent()
    {
      int index = (int) this.ParentID;
      if (!index.IsWithinBounds(Main.maxNPCs))
      {
        this.Projectile.Kill();
        return (NPC) null;
      }
      NPC npc = Main.npc[index];
      if (npc.TypeAlive<FargowiltasSouls.Content.Bosses.Magmaw.Magmaw>())
        return npc;
      this.Projectile.Kill();
      return (NPC) null;
    }
  }
}
