// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Boss.CoffinTossBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Boss
{
  public class CoffinTossBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.controlLeft = false;
      player.controlRight = false;
      player.controlJump = false;
      player.controlDown = false;
      player.controlUseItem = false;
      player.controlUseTile = false;
      player.controlHook = false;
      player.releaseHook = true;
      player.RemoveAllGrapplingHooks();
      if (player.mount.Active)
        player.mount.Dismount(player);
      player.FargoSouls().Stunned = true;
      player.FargoSouls().NoUsingItems = 2;
      ((Entity) player).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) player).velocity), 30f);
      player.fullRotation = Utils.ToRotation(((Entity) player).velocity) + 1.57079637f;
      player.fullRotationOrigin = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) player).position);
      if (player.buffTime[buffIndex] < 2)
      {
        player.fullRotation = 0.0f;
        player.DelBuff(buffIndex);
      }
      if (!Collision.SolidCollision(Vector2.op_Addition(((Entity) player).position, ((Entity) player).velocity), ((Entity) player).width, ((Entity) player).height))
        return;
      int num = 35;
      player.Hurt(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.FargowiltasSouls.DeathMessage.CoffinToss", (object) player.name)), num, 0, false, false, 0, false, 0.0f, 0.0f, 4.5f);
      player.DelBuff(buffIndex);
      SoundEngine.PlaySound(ref SoundID.NPCHit18, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
      Player player1 = player;
      ((Entity) player1).velocity = Vector2.op_Multiply(((Entity) player1).velocity, -1f);
      player.fullRotation = 0.0f;
    }
  }
}
