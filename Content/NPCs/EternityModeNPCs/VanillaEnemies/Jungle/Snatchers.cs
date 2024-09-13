// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Jungle.Snatchers
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Jungle
{
  public class Snatchers : EModeNPCBehaviour
  {
    public int DashTimer;
    public int BiteTimer;
    public int BittenPlayer = -1;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(56, 43, 175);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.DashTimer);
      binaryWriter.Write7BitEncodedInt(this.BiteTimer);
      binaryWriter.Write7BitEncodedInt(this.BittenPlayer);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.DashTimer = binaryReader.Read7BitEncodedInt();
      this.BiteTimer = binaryReader.Read7BitEncodedInt();
      this.BittenPlayer = binaryReader.Read7BitEncodedInt();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.damage = (int) (2.0 / 3.0 * (double) npc.damage);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[20] = true;
      npc.buffImmune[70] = true;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      int num = npc.type == 175 ? 120 : 360;
      if (this.BittenPlayer != -1)
      {
        this.DashTimer = 0;
        Player player = Main.player[this.BittenPlayer];
        if (this.BiteTimer > 0 && ((Entity) player).active && !player.ghost && !player.dead && ((double) ((Entity) npc).Distance(((Entity) player).Center) < 160.0 || ((Entity) player).whoAmI != Main.myPlayer) && player.FargoSouls().MashCounter < 20)
        {
          player.AddBuff(ModContent.BuffType<GrabbedBuff>(), 2, true, false);
          ((Entity) player).velocity = Vector2.Zero;
          ((Entity) npc).Center = ((Entity) player).Center;
        }
        else
        {
          this.BittenPlayer = -1;
          this.BiteTimer = -90;
          ((Entity) npc).velocity = Vector2.op_Multiply(15f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, new Vector2(npc.ai[0] * 16f, npc.ai[1] * 16f)));
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
        }
      }
      else if (++this.DashTimer > num && (double) ((Entity) npc).Distance(new Vector2((float) ((int) npc.ai[0] * 16), (float) ((int) npc.ai[1] * 16))) < 1000.0 && npc.HasValidTarget)
      {
        this.DashTimer = 0;
        ((Entity) npc).velocity = Vector2.op_Multiply(15f, Vector2.Normalize(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center)));
      }
      if (this.DashTimer == num - 30)
        EModeNPCBehaviour.NetSync(npc);
      if (this.BiteTimer < 0)
        ++this.BiteTimer;
      if (this.BiteTimer <= 0)
        return;
      --this.BiteTimer;
    }

    public virtual void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
    {
      base.ModifyHitPlayer(npc, target, ref modifiers);
      target.longInvince = true;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(30, 300, true, false);
      if (this.BittenPlayer == -1 && this.BiteTimer == 0)
      {
        this.BittenPlayer = ((Entity) target).whoAmI;
        this.BiteTimer = 360;
        EModeNPCBehaviour.NetSync(npc, false);
      }
      if (!WorldSavingSystem.MasochistModeReal || npc.type != 43 || !target.Male)
        return;
      target.KillMe(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.FargowiltasSouls.DeathMessage.Snatchers", (object) target.name)), 999999.0, 0, false);
    }

    public virtual void OnKill(NPC npc)
    {
      Player player = FargoSoulsUtil.PlayerExists(npc.lastInteraction);
      if (!Utils.NextBool(Main.rand, player == null || !player.FargoSouls().HasJungleRose ? 200 : 20))
        return;
      Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, ModContent.Find<ModItem>("Fargowiltas", "PlanterasFruit").Type, 1, false, 0, false, false);
    }
  }
}
