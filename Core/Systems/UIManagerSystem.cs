// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Systems.UIManagerSystem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls.Core.Systems
{
  public class UIManagerSystem : ModSystem
  {
    public virtual void UpdateUI(GameTime gameTime) => FargoUIManager.UpdateUI(gameTime);

    public virtual void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
      FargoUIManager.ModifyInterfaceLayers(layers);
    }
  }
}
