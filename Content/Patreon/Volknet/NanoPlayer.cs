// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Volknet.NanoPlayer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System.IO;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Volknet
{
  public class NanoPlayer : ModPlayer
  {
    public int NanoCoreMode;
    private int oldNanoCoreMode;

    public virtual void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
      if (this.NanoCoreMode == this.oldNanoCoreMode)
        return;
      this.oldNanoCoreMode = this.NanoCoreMode;
      ModPacket packet = ((ModType) this).Mod.GetPacket(256);
      ((BinaryWriter) packet).Write((byte) 11);
      ((BinaryWriter) packet).Write((byte) ((Entity) this.Player).whoAmI);
      ((BinaryWriter) packet).Write7BitEncodedInt(this.NanoCoreMode);
      packet.Send(toWho, fromWho);
    }

    public virtual void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
    {
    }

    public virtual void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
    {
    }
  }
}
