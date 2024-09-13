// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.StunnedBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class StunnedBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = true;
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
      if (player.mount.Active)
        player.mount.Dismount(player);
      player.FargoSouls().Stunned = true;
      player.FargoSouls().NoUsingItems = 2;
      if (((Entity) player).whoAmI != Main.myPlayer || player.buffTime[buffIndex] % 60 != 55)
        return;
      SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/DizzyBird", (SoundType) 0);
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(), (SoundUpdateCallback) null);
    }

    public virtual void Update(NPC npc, ref int buffIndex)
    {
      if (npc.boss)
        return;
      ((Entity) npc).velocity.X *= 0.0f;
      ((Entity) npc).velocity.Y *= 0.0f;
      npc.frameCounter = 0.0;
    }

    public virtual bool ReApply(Player player, int time, int buffIndex) => time > 3;
  }
}
