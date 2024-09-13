// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.MartianSaucer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
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
  public class MartianSaucer : EModeNPCBehaviour
  {
    public int AttackTimer;
    public int RayCounter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(395);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.AttackTimer);
      binaryWriter.Write7BitEncodedInt(this.RayCounter);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.AttackTimer = binaryReader.Read7BitEncodedInt();
      this.RayCounter = binaryReader.Read7BitEncodedInt();
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[ModContent.BuffType<ClippedWingsBuff>()] = true;
    }

    public override bool SafePreAI(NPC npc)
    {
      if (this.RayCounter <= 3)
        return base.SafePreAI(npc);
      ((Entity) npc).velocity = Vector2.Zero;
      if (++this.AttackTimer % 4 == 0 && npc.HasValidTarget && FargoSoulsUtil.HostCheck)
      {
        Vector2 vector2 = Vector2.op_Multiply(10f, ((Entity) npc).DirectionFrom(((Entity) Main.player[npc.target]).Center));
        int num = this.AttackTimer / 30 + 2;
        for (int index = 0; index < num; ++index)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(vector2, 6.2831854820251465 / (double) num * (double) index, new Vector2()), 449, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      if (this.AttackTimer >= 295)
      {
        this.AttackTimer = 0;
        this.RayCounter = 0;
        npc.ai[0] = 0.0f;
        npc.ai[1] = 0.0f;
        npc.ai[2] = 0.0f;
        npc.ai[3] = 0.0f;
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      return false;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      EModeGlobalNPC.Aura(npc, 200f, 164, dustid: 229, color: new Color());
      if (npc.dontTakeDamage || !npc.HasValidTarget)
        return;
      if (((double) npc.ai[3] - 60.0) % 120.0 == 65.0)
        ++this.RayCounter;
      if (((double) npc.ai[3] - 60.0) % 120.0 == 0.0)
        this.AttackTimer = 20;
      if (this.AttackTimer <= 0 || --this.AttackTimer % 2 != 0)
        return;
      Vector2 vector2 = Vector2.op_Multiply(14f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), (Main.rand.NextDouble() - 0.5) * 0.785398185253143 / 5.0, new Vector2()));
      if (!FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, 449, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.6666667f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public virtual bool PreKill(NPC npc)
    {
      if (NPC.downedGolemBoss)
        return base.PreKill(npc);
      for (int index = 0; index < 10; ++index)
      {
        if (FargoSoulsUtil.HostCheck)
          Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, 58, 1, false, 0, false, false);
      }
      Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, ModContent.ItemType<SaucerControlConsole>(), 1, false, 0, false, false);
      return false;
    }
  }
}
