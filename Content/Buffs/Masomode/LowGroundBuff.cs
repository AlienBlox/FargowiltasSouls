// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.LowGroundBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class LowGroundBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.buffNoSave[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.FargoSouls().LowGround = true;
      if (player.grapCount > 0)
        player.RemoveAllGrapplingHooks();
      if (player.mount.Active)
        player.mount.Dismount(player);
      Tile tileSafely1 = Framing.GetTileSafely(((Entity) player).Bottom);
      Tile tileSafely2 = Framing.GetTileSafely(Vector2.op_Addition(((Entity) player).Bottom, Vector2.op_Multiply(Vector2.UnitY, 8f)));
      if (Collision.SolidCollision(((Entity) player).BottomLeft, ((Entity) player).width, 16))
        return;
      if ((double) ((Entity) player).velocity.Y >= 0.0 && (IsPlatform((int) ((Tile) ref tileSafely1).TileType) || IsPlatform((int) ((Tile) ref tileSafely2).TileType)))
        ((Entity) player).position.Y += 2f;
      if ((double) ((Entity) player).velocity.Y != 0.0)
        return;
      ((Entity) player).position.Y += 16f;

      static bool IsPlatform(int tileType) => tileType == 19 || tileType == 380;
    }
  }
}
